using System.Web;
using Xunit;

namespace R7.Webmate.Tests.Text.Processings
{
    public class TextProcessingTestBase
    {
        protected readonly string TestProcessingsPath = "../../../resources/processings";

        protected string X (string text)
        {
            return HttpUtility.HtmlDecode (text);
        }

        protected string XX (string text)
        {
            return ToNamedEntities (HttpUtility.HtmlEncode (text));
        }

        protected string ToNamedEntities (string text)
        {
            return text.Replace ("&#160;", "&nbsp;");
        }
    }

    public class TextProcessingTestBaseTests: TextProcessingTestBase
    {
        [Fact]
        public void X_Test ()
        {
            Assert.Equal ("\u00A0", X ("&nbsp;"));
        }

        [Fact]
        public void XX_Test ()
        {
            Assert.Equal ("&nbsp;", XX ("\u00A0"));
        }
    }
}
