﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Catalan : StemExclusionLanguageBase
    {
        public Catalan() : base(AnalyzerEnum.Catalan) { }
    }
}
