using System.Collections.Generic;

namespace R7.Webmate.Text.Models
{
    public abstract class TextCleanerModelBase
    {
        public string Source { get; set; }

        public IList<TextResult> Results = new List<TextResult> ();

        public abstract void Process ();
    }
}
