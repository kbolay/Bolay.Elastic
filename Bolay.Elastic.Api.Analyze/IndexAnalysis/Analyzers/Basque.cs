﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Basque : StemExclusionLanguageBase
    {
        public Basque() : base(AnalyzerEnum.Basque) { }
    }
}
