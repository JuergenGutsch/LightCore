using System;
using System.Linq;
using System.Web.UI;

using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Web.IntegrationSample
{
    public partial class _Default : Page
    {
        public string Test
        {
            get;
            set;
        }

        public IWelcomeRepository WelcomeRepository
        {
            get;
            set;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Controls.Add(this.LoadControl("~/UserControls/WelcomeUserControl.ascx"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Controls.Add(
                new LiteralControl(
                    this.WelcomeRepository.GetWelcomeText().Aggregate((current, next) => current + " " + next)));
        }
    }
}