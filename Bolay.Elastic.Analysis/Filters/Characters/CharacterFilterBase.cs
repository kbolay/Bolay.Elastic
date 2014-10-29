using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Characters
{
    public abstract class CharacterFilterBase : ICharacterFilter
    {
        private const string _TYPE = "type";
        private const string _VERSION = "version";

        private Double? _Version { get; set; }

        /// <summary>
        /// Gets the name of character filter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the character filter.
        /// </summary>
        public CharacterFilterTypeEnum Type { get; private set; }

        /// <summary>
        /// Gets or sets the fuctionality to use based on lucene version.
        /// </summary>
        public Double? Version
        {
            get { return _Version; }
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Version", "Version must be greater than zero.");

                _Version = value;
            }
        }

        /// <summary>
        /// Creates a character filter.
        /// </summary>
        /// <param name="name">Sets the name of the character filter.</param>
        /// <param name="type">Sets the type of the character filter.</param>
        public CharacterFilterBase(string name, CharacterFilterTypeEnum type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Character filters require a name.");
            if (type == null)
                throw new ArgumentNullException("type", "Character filters require a type.");

            Name = name;
            Type = type;
        }

        internal static void Serialize(CharacterFilterBase filter, Dictionary<string, object> fieldDict)
        {
            if (filter == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_TYPE, filter.Type.ToString());
            fieldDict.AddObject(_VERSION, filter.Version);
        }

        internal static void Deserialize(CharacterFilterBase filter, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return;

            filter.Version = fieldDict.GetDoubleOrNull(_VERSION);
        }
    }
}
