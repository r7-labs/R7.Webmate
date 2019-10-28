using System.Collections.Generic;

namespace R7.Webmate.Xwt.Text
{
    public abstract class TextCleanerModelBase
    {
        public string Source { get; set; }

        public IList<TextCleanerResult> Results = new List<TextCleanerResult> ();

        public abstract void Process ();
    }
}
