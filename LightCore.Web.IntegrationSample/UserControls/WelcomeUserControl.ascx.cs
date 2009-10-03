using System;
using System.Linq;
using LightCore.TestTypes;

namespace LightCore.Web.IntegrationSample.UserControls
{
    public partial class WelcomeUserControl : System.Web.UI.UserControl
    {
        public IWelcomeRepository WelcomeRepository
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblTest.Text =
                this.WelcomeRepository.GetWelcomeText()
                .Aggregate((current, next) => current + " " + next);
        }
    }
}