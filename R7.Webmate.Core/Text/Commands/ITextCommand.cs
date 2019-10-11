namespace R7.Webmate.Core.Text.Commands
{
    public interface ITextCommand
    {
        bool IsDisabled { get; set; }

        string Execute (string value);
    }
}
