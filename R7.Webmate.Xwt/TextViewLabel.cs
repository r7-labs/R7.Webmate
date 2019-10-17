using System;
using System.Text.RegularExpressions;
using NGettext;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class TextViewLabel: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Label lblPreview = new Label ();

        protected Button btnOpenFullView = new Button ();

        string _text;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                lblPreview.Text = FormatLabel (_text);
            }
         }

        public TextViewLabel ()
        {
            lblPreview.Ellipsize = EllipsizeMode.End;
            lblPreview.Selectable = true;

            btnOpenFullView.Label = "...";
            btnOpenFullView.Clicked += BtnOpenFullView_Clicked;

            var hbox = new HBox ();
            hbox.PackStart (lblPreview, true, true);
            hbox.PackStart (btnOpenFullView, false, true);

            Content = hbox;
            Content.Show ();
        }

        void BtnOpenFullView_Clicked (object sender, EventArgs e)
        {
            var dlgTextView = new TextViewDialog ();
            dlgTextView.Text = Text;
            dlgTextView.Run (ParentWindow);
        }

        string FormatLabel (string text)
        {
            return Regex.Replace (text.Replace ("\r\n", " ").Replace ("\n", " "), @"\s+", " ").TrimStart ();
        }
    }
}
