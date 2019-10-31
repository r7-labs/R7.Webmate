using System;
using System.Text.RegularExpressions;
using NGettext;
using R7.Webmate.Xwt.Icons;
using Xwt;
using Xwt.Drawing;

namespace R7.Webmate.Xwt.Text
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

        bool _allowEdit;
        public bool AllowEdit {
            get { return _allowEdit; }
            set {
                _allowEdit = value;
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
                btnOpenFullView.Visible = AllowEdit;
                btnCopy.Visible = false;
            }
        }

        public TextViewLabel ()
        {
            lblPreview.Font = Config.Instance.MonospaceFont;
            lblPreview.Ellipsize = EllipsizeMode.End;
            lblPreview.Selectable = true;

            btnOpenFullView.TooltipText = T.GetString ("Click to open full text.");
            btnOpenFullView.Image = IconHelper.GetIcon ("ellipsis-h").WithBoxSize (IconSize.Small);
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
            dlgTextView.AllowEdit = AllowEdit;
            dlgTextView.Text = Text;

            var result = dlgTextView.Run (ParentWindow);

            if (AllowEdit && result == Command.Save) {
                Text = dlgTextView.Text;
            }
        }

        string FormatLabel (string text)
        {
            return Regex.Replace (text.Replace ("\r\n", " ").Replace ("\n", " "), @"\s+", " ").TrimStart ();
        }
    }
}
