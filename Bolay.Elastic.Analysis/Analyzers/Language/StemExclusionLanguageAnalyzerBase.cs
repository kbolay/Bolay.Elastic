using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Analyzers.Language
{
    public abstract class StemExclusionLanguageAnalyzerBase : LanguageAnalyzerBase
    {
        /// <summary>
        /// Gets or sets the stem exclusions.
        /// </summary>
        public IEnumerable<string> StemExclusions { get; set; }

        /// <summary>
        /// Create a language analyzer that allows stem exclusions.
        /// </summary>
        /// <param name="name">Sets the name of the language analyzer.</param>
        /// <param name="type">Sets the language analyzer type.</param>
        public StemExclusionLanguageAnalyzerBase(string name, AnalyzerTypeEnum type)
            : base(name, type)
        { }
    }
}
