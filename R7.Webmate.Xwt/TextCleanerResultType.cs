using NGettext;

namespace R7.Webmate.Xwt
{
    public enum TextCleanerResultType
    {
        Text,
        HTML
    }

    class TextCleanerResultTypeStrings
    {
        ICatalog T = null;

        void Strings ()
        {
            T.GetString ("Text");
            T.GetString ("HTML");
        }
    }
}
