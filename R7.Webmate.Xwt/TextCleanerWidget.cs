//
//  TextCleanerWidget.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2019 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text.RegularExpressions;
using NGettext;
using R7.Webmate.Xwt.Icons;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class TextCleanerWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected Button btnPaste;

        protected Label lblSrc = new Label ();

        protected Button btnProcess;

        protected VBox vboxResults = new VBox ();

        // part of the model
        protected string Src;

        public TextCleanerWidget ()
        {
            btnPaste = new Button (FAIconHelper.GetIcon ("paste").WithSize (IconSize.Medium), T.GetString ("Paste"));
            btnPaste.Clicked += BtnPaste_Clicked;

            var btnPasteMenu = new MenuButton ();

            btnProcess = new Button (FAIconHelper.GetIcon ("play-circle").WithSize (IconSize.Medium), T.GetString ("Process"));
            btnProcess.Clicked += BtnProcess_Clicked;

            var hboxPaste = new HBox ();
            hboxPaste.PackStart (btnPaste, true, true);
            hboxPaste.PackStart (btnPasteMenu, false, true);

            lblSrc.Ellipsize = EllipsizeMode.End;
            lblSrc.Selectable = true;

            var vbox = new VBox ();
            vbox.PackStart (hboxPaste, true, true);
            vbox.PackStart (lblSrc, false, true);
            vbox.PackStart (btnProcess, true, true);
            vbox.PackStart (vboxResults, true, true);

            vbox.Margin = 5;
            Content = vbox;
            Content.Show ();
        }

        void BtnPaste_Clicked (object sender, EventArgs e)
        {
            Src = Clipboard.GetText ();
            lblSrc.Text = FormatLabel (Src);
        }

        void BtnProcess_Clicked (object sender, EventArgs e)
        {
            // TODO: Process text here
            var result = Src;

            // clear previos results
            vboxResults.Clear ();

            // add new results
            var index = 0;
            AddResult (++index, T.GetString ("Text"), result);
            AddResult (++index, T.GetString ("HTML"), "<p>" + result + "</p>");
        }

        void AddResult (int index, string format, string result)
        {
            var lblResult = new Label ();
            lblResult.Ellipsize = EllipsizeMode.End;
            lblResult.Selectable = true;
            lblResult.Text = FormatLabel (result);

            var btnCopy = new Button (FAIconHelper.GetIcon ("copy").WithSize (IconSize.Small), T.GetString ("Copy"));
            btnCopy.Clicked += (sender1, e1) => {
                Clipboard.SetText (result);
            };

            var vboxResult = new VBox ();
            vboxResult.Margin = 5;
            vboxResult.PackStart (lblResult, false, true);
            vboxResult.PackStart (btnCopy, true, true);

            var frmResult = new Frame ();
            frmResult.Label = "#" + index + " " + format;
            frmResult.Content = vboxResult;
            vboxResults.PackStart (frmResult);
        }

        string FormatLabel (string text)
        {
            return Regex.Replace (text.Replace ("\r\n", " ").Replace ("\n", " "), @"\s+", " ").TrimStart ();
        }
    }
}
