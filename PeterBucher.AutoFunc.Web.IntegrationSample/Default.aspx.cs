using System;
using System.Linq;
using System.Web.UI;

using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Web.IntegrationSample
{
    public partial class _Default : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Controls.Add(
                new LiteralControl(
                    this.WelcomeRepository.GetWelcomeText().Aggregate((current, next) => current + " " + next)));
        }
    }
}