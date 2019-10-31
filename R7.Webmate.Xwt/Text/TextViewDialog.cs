using NGettext;
using Xwt;
using Xwt.Formats;
using R7.Webmate.Xwt.Icons;

namespace R7.Webmate.Xwt.Text
{
    public class TextViewDialog: Dialog
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        #region Widgets

        protected RichTextView TextView = new RichTextView ();

        protected HBox Toolbar = new HBox ();

        protected Button btnCopy;

        #endregion

        public string Text {
            get { return TextView.PlainText; }
            set { TextView.LoadText (value ?? string.Empty, TextFormat.Plain); }
        }

        bool _allowEdit;
        public bool AllowEdit {
            get { return _allowEdit; }
            set {
                _allowEdit = value;
                UpdateView ();
            }
        }

        public TextViewDialog ()
        {
            Width = 400;
            Height = 300;
            Title = T.GetString ("View Text");

            TextView.Font = Config.Instance.MonospaceFont;

            btnCopy = new Button (IconHelper.GetIcon ("copy").WithSize (IconSize.Small), T.GetString ("Copy All"));
            btnCopy.Clicked += (sender, e) => {
                Clipboard.SetText (TextView.PlainText);
            };

            Toolbar.PackStart (btnCopy, false, true);

            var vbox = new VBox ();
            vbox.PackStart (new ScrollView (TextView), true, true);
            vbox.PackStart (Toolbar, false, true);

            UpdateView ();

            Content = vbox;
            Content.Show ();
        }

        void UpdateView ()
        {
            TextView.ReadOnly = !AllowEdit;

            Buttons.Clear ();
            if (AllowEdit) {
                Buttons.Add (new DialogButton (T.GetString ("Save"), Command.Save));
                Buttons.Add (new DialogButton (T.GetString ("Cancel"), Command.Cancel));
                DefaultCommand = Command.Save;
            }
            else {
                Buttons.Add (new DialogButton (T.GetString ("Close"), Command.Close));
                DefaultCommand = Command.Close;
            }
        }
    }
}
