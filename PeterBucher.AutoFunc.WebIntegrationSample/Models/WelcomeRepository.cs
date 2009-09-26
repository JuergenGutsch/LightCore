﻿using System.Collections.Generic;

namespace PeterBucher.AutoFunc.WebIntegrationSample.Models
{
    public class WelcomeRepository : IWelcomeRepository
    {
        public IEnumerable<string> GetWelcomeText()
        {
            yield return "Hello ";
            yield return "Wold, it works!";
        }
    }
}