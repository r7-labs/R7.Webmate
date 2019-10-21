using System;
using System.Text.RegularExpressions;
using NGettext;
using R7.Webmate.Xwt.Icons;
using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt
{
    public class TextViewLabel: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Label lblPreview = new Label ();

        protected Button btnCopy = new Button ();

        protected Button btnOpenFullView = new Button ();

        public bool AllowQuickCopy { get; protected set; }

        string _text;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                lblPreview.Text = FormatLabel (_text);
                lblPreview.TooltipText = _text;
            }
         }

        // TODO: Allow to set AllowQuickCopy later
        public TextViewLabel (bool allowQuickCopy)
        {
            AllowQuickCopy = allowQuickCopy;

            lblPreview.Font = Font.SystemMonospaceFont;
            lblPreview.TextColor = Color.FromName ("black");

            lblPreview.Ellipsize = EllipsizeMode.End;
            lblPreview.Selectable = true;

            btnOpenFullView.Label = "...";
            btnOpenFullView.Clicked += BtnOpenFullView_Clicked;

            var hbox = new HBox ();
            hbox.PackStart (lblPreview, true, true);
            hbox.PackStart (btnOpenFullView, false, true);

            if (AllowQuickCopy) {
                btnCopy.Label = T.GetString ("Copy");
                btnCopy.Image = FAIconHelper.GetIcon ("copy").WithSize (IconSize.Small);
                btnCopy.Clicked += BtnCopy_Clicked;
                hbox.PackStart (btnCopy, false, true);
            }

            Content = hbox;
            Content.Show ();
        }

        void BtnCopy_Clicked (object sender, EventArgs e)
        {
            Clipboard.SetText (Text);
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
