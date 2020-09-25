using System;
using System.Text;
using NGettext;
using R7.Webmate.Xwt.Icons;
using R7.Webmate.Xwt.Text;
using Xwt;

namespace R7.Webmate.Xwt
{
    public class HelloWorldWidget: Widget
    {
        protected ICatalog T = TextCatalogKeeper.GetDefault ();

        protected TextEntry txtName;

        protected TextEntry txtHello;

        public HelloWorldWidget ()
        {
            var vbox = new VBox ();
            var lblName = new Label (T.GetString ("Name:"));
            var btnHello = new Button (T.GetString ("Click me!"));

            txtName = new TextEntry  ();
            txtHello = new TextEntry ();

            btnHello.Clicked += btnHello_Clicked;

            vbox.PackStart (lblName, false, true);
            vbox.PackStart (txtName, false, true);
            vbox.PackStart (btnHello, false, true);
            vbox.PackStart (txtHello, false, true);

            vbox.Margin = Const.VBOX_MARGIN;
            Content = vbox;
            Content.Show ();
        }

        void btnHello_Clicked (object sender, EventArgs e)
        {
            txtHello.Text = string.Format (T.GetString ("Hello, {0}!"), txtName.Text);
        }
    }
}
