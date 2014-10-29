using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters
{
    [JsonConverter(typeof(CharacterFilterCollectionSerializer))]
    internal class CharacterFilterCollection : List<ICharacterFilter>
    {

    }
}
