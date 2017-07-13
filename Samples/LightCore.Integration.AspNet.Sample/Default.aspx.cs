using System;
using System.Web.UI;

using LightCore.TestTypes;

namespace LightCore.Web.IntegrationSample
{
    public partial class _Default : Page
    {
        public string Test
        {
            get;
            set;
        }

        public IFoo Foo
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Form.Controls.Add(this.LoadControl("~/UserControls/WelcomeUserControl.ascx"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Controls.Add(
                new LiteralControl(((Foo) this.Foo).Arg1));
        }
    }
}