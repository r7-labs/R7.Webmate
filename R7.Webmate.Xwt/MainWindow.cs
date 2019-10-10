//
//  MainWindow.cs
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

using NGettext;
using Xwt;
using R7.Webmate.Xwt.Icons;

namespace R7.Webmate.Xwt
{
    public class MainWindow: Window
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        public MainWindow ()
        {
            Title = T.GetString ("R7.Webmate.Xwt - ") + OSHelper.GetXwtToolkit ().ToString ();
            Width = 500;
            Height = 400;

            var notebook = new Notebook ();
            notebook.TabOrientation = NotebookTabOrientation.Bottom;
            notebook.Add (new TextCleanerWidget (), T.GetString ("Text Cleaner"));

            var vbox = new VBox ();
            vbox.PackStart (notebook, true, true);

            Content = vbox;
            Content.Show ();
        }
    }
}
