using System;
using System.Text;
using NGettext;
using R7.Webmate.Text.Models;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt.Text
{
    public class DecodeHexStringWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected DecodeHexStringModel Model = new DecodeHexStringModel ();

        protected Button btnPaste;

        protected Button btnDecode;

        protected VBox vboxResults = new VBox ();

        protected HBox hboxGenerate = new HBox ();

        protected HBox hboxPaste = new HBox();

        protected ScrollView scrResults;

        protected TextViewLabel lblSrc = new TextViewLabel ();

        protected CheckBox chkAutoProcess = new CheckBox ();

        public DecodeHexStringWidget ()
        {
            var vbox = new VBox ();

            lblSrc.AllowQuickCopy = false;
            lblSrc.AllowEdit = true;

            btnPaste = new Button (IconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            chkAutoProcess.Label = T.GetString ("Process on paste?");
            chkAutoProcess.Active = true;

            hboxPaste.PackStart(btnPaste, true, true);

            btnDecode = new Button (IconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString("Decode"));
            btnDecode.Clicked += btnDecode_Clicked;

            hboxGenerate.PackStart (btnDecode, true, true);

            scrResults = new ScrollView (vboxResults);

            vbox.PackStart(hboxPaste, true, true);
            vbox.PackStart(lblSrc, false, true);
            vbox.PackStart (hboxGenerate, false, true);
            vbox.PackStart (chkAutoProcess, false, true);
            vbox.PackStart (scrResults, true, true);

            vbox.Margin = Const.VBOX_MARGIN;
            Content = vbox;
            Content.Show ();
        }

        void BtnPaste_Clicked (object sender, EventArgs e)
        {
            Model.HexString = Clipboard.GetText () ?? string.Empty;
            lblSrc.Text = Model.HexString;

            if (chkAutoProcess.Active) {
                btnDecode_Clicked(sender, e);
            }
        }

        void btnDecode_Clicked (object sender, EventArgs e)
        {
            Model.HexString = lblSrc.Text;
            var bytes = Model.Process();

            var text1 = Encoding.GetEncoding("Windows-1251").GetString(bytes);
            var result1 = new TextResult {
                Text = text1,
                Label = "Windows-1251"
            };

            var text2 = Encoding.UTF8.GetString(bytes);
            var result2 = new TextResult {
                Text = text2,
                Label = "UTF-8"
            };

            vboxResults.Clear();
            AddResult(1, result1);
            AddResult(2, result2);
        }

        protected void AddResult (int index, TextResult result)
        {
            var lblResult = new TextViewLabel();
            lblResult.Text = result.Text;

            var vboxResult = new VBox();
            vboxResult.MarginLeft = Const.VBOX_MARGIN;
            vboxResult.MarginRight = Const.VBOX_MARGIN;
            vboxResult.MarginBottom = 3;
            vboxResult.PackStart(lblResult, false, true);

            var frmResult = new Frame();

            frmResult.Label = string.Format(T.GetString("Result #{0} - {1}"), index, T.GetString(result.Label));

            frmResult.Content = vboxResult;
            vboxResults.PackStart(frmResult);
        }
    }
}
