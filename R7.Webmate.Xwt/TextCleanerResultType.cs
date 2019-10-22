using NGettext;

namespace R7.Webmate.Xwt
{
    public enum TextCleanerResultType
    {
        Text,
        AsciiText,
        HTML
    }

    static class TextCleanerResultTypeHelper
    {
        static ICatalog T = TextCatalogKeeper.GetDefault ();

        public static string GetString (TextCleanerResultType resultType)
        {
            switch (resultType) {
                case TextCleanerResultType.Text: return T.GetString ("Тext");
                case TextCleanerResultType.AsciiText: return T.GetString ("ASCII Text");
                case TextCleanerResultType.HTML: return T.GetString ("HTML");
            }

            return string.Empty;
        }
    }
}
