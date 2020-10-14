using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace R7.Webmate.Text.Models
{
    // TODO: Test me!
    public class CaseChangerModel
    {
       public string Source { get; set; }

        public IList<TextCleanerResult> Results = new List<TextCleanerResult> ();

        public string LowerCase (string s)
        {
            return s.ToLower ();
        }

        public string UpperCase (string s)
        {
            return s.ToUpper ();
        }

        public string InvertedCase (string s)
		{
			var upper = s.ToUpper ();
			var lower = s.ToLower ();
			var chars = s.ToCharArray();

			for (var i = 0; i < s.Length; i++) {
				if (upper[i] == s[i])
					chars[i] = lower[i];
				else if (lower[i] == s[i])
					chars[i] = upper[i];
				else
					chars[i] = s[i];
			}

			return new string(chars);
		}

		public string SentenceCase (string s)
		{
			var guid = "_" + Guid.NewGuid ().ToString () + "_";
			var upper = s.ToUpper ();

			var sentence = Regex.Replace (". " + s.ToLower(), @"([\.\!\?\u2026]\s*?)(\b\w)(\w*?)",
				string.Format ("$1{0}$3", guid)).Substring (2);

			var index = -1;
			do
			{
				index = sentence.IndexOf (guid);
				if (index >= 0) {
					sentence = sentence.Remove (index, guid.Length);
					sentence = sentence.Insert (index, upper[index].ToString ());
				}

			} while (index >= 0);

			return sentence;
		}

		public string CamelCase (string s)
		{
			var guid = "_" + Guid.NewGuid ().ToString () + "_";
			var upper = s.ToUpper();

			var sentence = Regex.Replace (s.ToLower (), @"(\b\w)(\w*?)",
				string.Format ("$2{0}", guid));

			var index = -1;
			do {
				index = sentence.IndexOf (guid);
				if (index >= 0) {
					sentence = sentence.Remove (index, guid.Length);
					sentence = sentence.Insert (index, upper[index].ToString());
				}

			} while (index >= 0);

			return sentence;
		}

        public void Process ()
        {
            Results.Clear ();

            Results.Add (new TextCleanerResult {
                Text = UpperCase (Source),
                Label = "Upper Case",
                Format = TextCleanerResultFormat.Text
            });

            Results.Add (new TextCleanerResult {
                Text = LowerCase (Source),
                Label = "Lower Case",
                Format = TextCleanerResultFormat.Text
            });

            Results.Add (new TextCleanerResult {
                Text = SentenceCase (Source),
                Label = "Sentence Case",
                Format = TextCleanerResultFormat.Text
            });

            Results.Add (new TextCleanerResult {
                Text = CamelCase (Source),
                Label = "Camel Case",
                Format = TextCleanerResultFormat.Text
            });

            Results.Add (new TextCleanerResult {
                Text = InvertedCase (Source),
                Label = "Inverted Case",
                Format = TextCleanerResultFormat.Text
            });
        }
    }
}
