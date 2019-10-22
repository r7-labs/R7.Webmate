using System;
using System.Text;
using Xwt;

namespace R7.Webmate.Xwt
{
    public static class ClipboardHelper
    {
        public static string TryGetHtml ()
        {
            try {
                if (Clipboard.ContainsData (TransferDataType.Html)) {
                    var clipboardData = Clipboard.GetData (TransferDataType.Html);
                    // on Windows, this should be just string
                    if (clipboardData.GetType ().Name == typeof (string).Name) {
                        return (string) clipboardData;
                    }
                    // on Unix, this should be byte array
                    if (clipboardData.GetType ().Name == typeof (byte []).Name) {
                        return Encoding.Default.GetString ((byte []) clipboardData);
                    }

                    // TODO: Cannot get clipboard data, fallback to Clipboard.GetText?
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log error
                Console.WriteLine (ex.Message);
            }
            return string.Empty;
        }
    }
}
