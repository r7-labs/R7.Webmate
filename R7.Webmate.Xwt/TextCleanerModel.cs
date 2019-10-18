using System.Collections.Generic;

namespace R7.Webmate.Xwt
{
    public class TextCleanerModel
    {
        public string Source { get; set; }

        public IList<TextCleanerResult> Results = new List<TextCleanerResult> ();
    }
       
    public class TextCleanerResult
    {
        public string Text { get; set; }

        public TextCleanerResultType ResultType { get; set; }
    }
}
