using System.Text.RegularExpressions;
using System.Web;

namespace R7.Webmate.Core.Text.Commands
{
    public class ReplaceEntitiesWithCharsCommand: TextCommandBase
    {
        public override string Run (string text)
        {
            text = HtmlHelper.DecodeSpecialEntities (text);
            text = Regex.Replace (text, @"&\w+;", m => HttpUtility.HtmlEncode (HttpUtility.HtmlDecode (m.Value)));
            text = Regex.Replace (text, @"&#\d+;", m => HttpUtility.HtmlDecode (HttpUtility.HtmlDecode (m.Value)));
            return text;
        }
    }
}
