﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public class Lowercase : TokenizerBase
    {
        public Lowercase() : base(TokenizerEnum.Lowercase) { }
    }
}
