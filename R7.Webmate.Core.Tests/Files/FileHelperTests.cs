using R7.Webmate.Core.Files;
using Xunit;

namespace R7.Webmate.Core.Tests.Files
{
    public class FileHelperTests
    {
        [Fact]
        public void GetSuffixedFilePathTest ()
        {
            Assert.Equal ("somefile.suffix.txt", FileHelper.GetSuffixedFilePath ("somefile.txt", ".suffix"));
            Assert.Equal ("/usr/share/somefile.suffix.txt", FileHelper.GetSuffixedFilePath ("/usr/share/somefile.txt", ".suffix"));
            Assert.Equal ("C:\\Windows\\somefile.suffix.txt", FileHelper.GetSuffixedFilePath ("C:\\Windows\\somefile.txt", ".suffix"));
        }

        [Fact]
        public void GetUserFilePathTest ()
        {
            Assert.Equal ("somefile.user.txt", FileHelper.GetUserFilePath ("somefile.txt"));
            Assert.Equal ("/usr/share/somefile.user.txt", FileHelper.GetUserFilePath ("/usr/share/somefile.txt"));
            Assert.Equal ("C:\\Windows\\somefile.user.txt", FileHelper.GetUserFilePath ("C:\\Windows\\somefile.txt"));
        }
    }
}
