using System.Collections.Generic;
using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
    public interface ITextProcessing
    {
        IDictionary<string, bool> Options { get; set; }

        IList<ITextCommand> Commands { get; set; }

        string Process (string text);
    }
}
