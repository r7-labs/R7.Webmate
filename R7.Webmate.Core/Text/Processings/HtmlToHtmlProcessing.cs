using System.Text.RegularExpressions;

namespace R7.Webmate.Core.Text.Processings
{
    /// <summary>
    /// Simulate MatchGroup from .NET
    /// </summary>
    class MatchGroup
    {
        public int Index;

        public int Length;

        public string Value;

        public string NewValue;
    }

    public class HtmlToHtmlProcessing: TextProcessingBase
    {
        public ITextProcessing TextToTextProcessing { get; set; }

        public override string Execute (string text)
        {
            return HtmlToHtml (text);
        }

        protected virtual string HtmlToHtml (string text)
        {
            // TODO: Refactor regions into methods

            #region Attributes

            // TODO: More precise and universal regex for attr match
            // get match collections, attrs @title, @alt, @summary going first
            var attrs = Regex.Matches (text, @"(title|alt|summary)=[""'](.*?)[""']", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // calculate real matches count, without empty and non-successful 
            var attrsCount = CountValidMatches (attrs, 2);

            // make an array of MatchGroups to store new attribute values
            var attrGroups = new MatchGroup [attrsCount];
            CopyValidMatches (attrs, attrGroups, 2);

            // pass all matched values to cleanup
            foreach (var group in attrGroups) {
                group.NewValue = TextToTextProcessing.Execute (group.Value);
            }

            // now, we need to apply changes back to original text, before proceed with tags and values
            text = ApplyMatchGroups (text, attrGroups);

            #endregion

            #region Tag values

            // get values
            var values = Regex.Matches (text, ">(.*?)<", RegexOptions.Singleline);
            var valuesCount = CountValidMatches (values, 1);

            var valueGroups = new MatchGroup [valuesCount];
            CopyValidMatches (values, valueGroups, 1);

            foreach (var group in valueGroups) {
                group.NewValue = TextToTextProcessing.Execute (group.Value);
            }

            text = ApplyMatchGroups (text, valueGroups);

            #endregion

            return text;
        }

        private string ApplyMatchGroups (string text, MatchGroup [] matchGroups)
        {
            var offset = 0;
            var newText = text;

            foreach (var _group in matchGroups) {
                newText = newText.Remove (_group.Index + offset, _group.Length);
                newText = newText.Insert (_group.Index + offset, _group.NewValue);
                offset = newText.Length - text.Length;
            }

            return newText;
        }

        private int CountValidMatches (MatchCollection matches, int nGroup)
        {
            var matchCount = 0;
            foreach (Match match in matches) {
                if (match.Success && !string.IsNullOrWhiteSpace (match.Groups [nGroup].Value)) {
                    matchCount++;
                }
            }

            return matchCount;
        }

        private void CopyValidMatches (MatchCollection matches, MatchGroup [] matchArray, int groupNum)
        {
            var matchIndex = 0;
            foreach (Match match in matches) {
                if (match.Success && !string.IsNullOrWhiteSpace (match.Groups [groupNum].Value)) {
                    matchArray [matchIndex] = new MatchGroup ();
                    matchArray [matchIndex].Index = match.Groups [groupNum].Index;
                    matchArray [matchIndex].Length = match.Groups [groupNum].Length;
                    matchArray [matchIndex].Value = match.Groups [groupNum].Value;
                    matchIndex++;
                }
            }
        }
    }
}
