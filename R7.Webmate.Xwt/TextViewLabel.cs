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

        bool _allowQuickCopy = true;
        public bool AllowQuickCopy {
            get { return _allowQuickCopy; }
            set {
                _allowQuickCopy = value;
                UpdateView ();
            }
        }

        string _text = string.Empty;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                UpdateView ();
            }
         }

        protected void UpdateView ()
        {
            if (!string.IsNullOrEmpty (Text)) {
                lblPreview.Text = FormatLabel (Text);
                // TODO: Shorten tooltip text
                lblPreview.TooltipText = Text;
                lblPreview.TextColor = Color.FromName ("black");
                btnOpenFullView.Visible = true;
                btnCopy.Visible = AllowQuickCopy;
            }
            else {
                lblPreview.Text = T.GetString ("<empty>");
                lblPreview.TooltipText = string.Empty;
                lblPreview.TextColor = Color.FromName ("red");
                btnOpenFullView.Visible = false;
                btnCopy.Visible = false;
            }
        }

        public TextViewLabel ()
        {
            lblPreview.Font = Font.SystemMonospaceFont;
            lblPreview.Ellipsize = EllipsizeMode.End;
            lblPreview.Selectable = true;

            btnOpenFullView.Label = "...";
            btnOpenFullView.Clicked += BtnOpenFullView_Clicked;

            var hbox = new HBox ();
            hbox.PackStart (lblPreview, true, true);
            hbox.PackStart (btnOpenFullView, false, true);

            btnCopy.Label = T.GetString ("Copy");
            btnCopy.Image = IconHelper.GetIcon ("copy").WithSize (IconSize.Small);
            btnCopy.Clicked += BtnCopy_Clicked;
            hbox.PackStart (btnCopy, false, true);

            Content = hbox;
            Content.Show ();

            UpdateView ();
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
