using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters
{
    public interface ICharacterFilter : IAnalysisVersion
    {
        string Name { get; }
        CharacterFilterTypeEnum Type { get; }
    }
}
