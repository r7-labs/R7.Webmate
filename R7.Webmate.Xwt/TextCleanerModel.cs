using System.Collections.Generic;

namespace R7.Webmate.Xwt
{
    // TODO: Store result types along with results
    public class TextCleanerModel
    {
        public string Source { get; set; }

        public IList<string> Results = new List<string> ();
    }
}
