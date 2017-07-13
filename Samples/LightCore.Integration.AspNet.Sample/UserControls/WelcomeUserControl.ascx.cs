using System;

using LightCore.TestTypes;

namespace LightCore.Web.IntegrationSample.UserControls
{
    public partial class WelcomeUserControl : System.Web.UI.UserControl
    {
        public IFoo Foo
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblTest.Text =
                ((Foo) this.Foo).Arg1;
        }
    }
}