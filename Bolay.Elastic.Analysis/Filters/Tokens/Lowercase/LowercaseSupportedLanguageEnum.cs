using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens.Lowercase
{
    public sealed class LowercaseSupportedLanguageEnum : TypeSafeEnumBase<LowercaseSupportedLanguageEnum>
    {
        public static readonly LowercaseSupportedLanguageEnum Greek = new LowercaseSupportedLanguageEnum("Greek");
        public static readonly LowercaseSupportedLanguageEnum Turkish = new LowercaseSupportedLanguageEnum("Turkish");

        private LowercaseSupportedLanguageEnum(string value) 
            : base(value)
        {
            _AllItems.Add(this); 
        }
    }
}