﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers
{
    public class Arabic : StemExclusionLanguageBase
    {
        public Arabic() : base(AnalyzerEnum.Arabic) { }
    }
}
