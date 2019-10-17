using System.Collections.Generic;

namespace R7.Webmate.Xwt
{
    public class TextCleanerModel
    {
        public string Source { get; set; }

        public IList<string> Results = new List<string> ();
    }
}
