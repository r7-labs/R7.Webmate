using System;
using NGettext;

namespace R7.Webmate.Xwt
{
    [Obsolete ("It's a fake class to translate additional strings", true)]
    internal class TextCatalogAdditionalStrings
    {
        protected ICatalog T = null;

        TextCatalogAdditionalStrings ()
        {
            T.GetString ("HTML");
            T.GetString ("Text");
            T.GetString ("Simplified text");
            T.GetString ("HTML table");
            T.GetString ("Boostrap table");
            T.GetString ("UPPER CASE");
            T.GetString ("lower case");
            T.GetString ("Sentence case");
            T.GetString ("Camel Case");
            T.GetString ("iNVERTED cASE");
        }
    }
}
