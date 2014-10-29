using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters.Mapping
{
    [JsonConverter(typeof(CharacterMappingSerializer))]
    public class CharacterMapping
    {
        internal const string _DELIMITER = "=>";

        /// <summary>
        /// Gets the characters to find.
        /// </summary>
        public string Find { get; private set; }

        /// <summary>
        /// Gets the characters to replace the found characters with.
        /// </summary>
        public string Replace { get; private set; }

        /// <summary>
        /// Creates a character mapping.
        /// </summary>
        /// <param name="find">The characters to find.</param>
        /// <param name="replace">The characters to replace the characters found.</param>
        public CharacterMapping(string find, string replace)
        {
            if (string.IsNullOrWhiteSpace(find))
                throw new ArgumentNullException("find", "CharacterMapping requires a find value.");
            if (string.IsNullOrWhiteSpace(replace))
                throw new ArgumentNullException("replace", "CharacterMapping requires a replace value.");

            Find = find;
            Replace = replace;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Find);
            builder.Append(_DELIMITER);
            builder.Append(Replace);

            return builder.ToString();
        }
    }
}
