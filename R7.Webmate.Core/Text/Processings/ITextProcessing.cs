using System.Collections.Generic;
using R7.Webmate.Core.Text.Commands;

namespace R7.Webmate.Core.Text.Processings
{
    public interface ITextProcessing
    {
        ITextProcessingParams Params { get; set; }

        IList<ITextCommand> Commands { get; set; }

        string Execute (string text);
    }
}
