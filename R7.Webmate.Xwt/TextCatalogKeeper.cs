using NGettext;

namespace R7.Webmate.Xwt
{
    public static class TextCatalogKeeper
    {
        static ICatalog _catalog;

        public static void SetDefault (ICatalog catalog)
        {
            _catalog = catalog;
        }

        public static ICatalog GetDefault ()
        {
            return _catalog;
        }
    }
}
