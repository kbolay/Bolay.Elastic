﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public class AsciiFolding : TokenFilterBase
    {
        public AsciiFolding() : base(TokenFilterEnum.AsciiFolding) { }
    }
}
