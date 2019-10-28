using NGettext;
using Xwt;
using Xwt.Formats;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Text
{
    public class TextViewDialog: Dialog
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected RichTextView TextView = new RichTextView ();

        string _text;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                if (!string.IsNullOrEmpty (_text)) {
                    TextView.LoadText (_text, TextFormat.Plain);
                }
            }
        }

        public TextViewDialog ()
        {
            Width = 400;
            Height = 300;
            Title = T.GetString ("View Text");

            TextView.Font = Config.Instance.MonospaceFont;

            var vbox = new VBox ();
            vbox.PackStart (new ScrollView (TextView), true, true);

            Content = vbox;
            Content.Show ();
        }
    }
}
