using System.Collections.Generic;
using R7.Webmate.Text.Commands;

namespace R7.Webmate.Text.Processings
{
    public interface ITextProcessing
    {
        IDictionary<string, bool> Options { get; set; }

        IList<ITextCommand> Commands { get; set; }

        string Process (string text);
    }
}
