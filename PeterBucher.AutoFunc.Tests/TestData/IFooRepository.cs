﻿using System.Collections.Generic;

namespace PeterBucher.AutoFunc.Tests.TestData
{
    public interface IFooRepository
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}