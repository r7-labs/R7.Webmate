using System;
using System.Text.RegularExpressions;

namespace R7.Webmate.Core.Text
{
    public static class HtmlHelper
    {
        public static bool IsHtml (string text)
        {
            if (!string.IsNullOrEmpty (text)) {
                var stripped = text.Replace ("</", "").Replace ("/>", "");
                return (text.Length - stripped.Length) > 0;
            }

            return false;
        }

        public static string GetBodyContents (string html)
        {
            var bodyStart = Regex.Match (html, @"<body.*?>", RegexOptions.IgnoreCase);
            var bodyEnd = Regex.Match (html, @"</body>", RegexOptions.IgnoreCase);

            if (bodyStart.Success && bodyEnd.Success) {
                html = html.Substring (bodyStart.Index + bodyStart.Length,
                    bodyEnd.Index - (bodyStart.Index + bodyStart.Length)).Trim ();
            }

            return html;
        }

        public static string GetFirstTable (string html)
        {
            var tableIndex = html.IndexOf ("<table", StringComparison.InvariantCultureIgnoreCase);
            if (tableIndex >= 0) {
                html = html.Substring (tableIndex);
                html = html.Substring (0, html.IndexOf ("</table>", StringComparison.InvariantCultureIgnoreCase) + "</table>".Length);
                return html;
            }

            return string.Empty;
        }
    }
}
