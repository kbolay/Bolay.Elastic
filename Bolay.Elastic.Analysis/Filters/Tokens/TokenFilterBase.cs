using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Filters.Tokens
{
    public abstract class TokenFilterBase : ITokenFilter
    {
        private const string _TYPE = "type";
        private const string _VERSION = "version";

        /// <summary>
        /// Gets the name of the token filter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the token filter.
        /// </summary>
        public TokenFilterTypeEnum Type { get; private set; }

        /// <summary>
        /// Gets or sets the version of the token filter.
        /// </summary>
        public Double? Version { get; set; }

        /// <summary>
        /// Creates a token filter.
        /// </summary>
        /// <param name="name">Sets the name of the token filter.</param>
        /// <param name="type">Sets the type of the token filter.</param>
        public TokenFilterBase(string name, TokenFilterTypeEnum type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Token Filters require a name.");
            if (type == null)
                throw new ArgumentNullException("type", "Token Filters require a type.");

            Name = name;
            Type = type;
        }

        internal static void Serialize(TokenFilterBase filter, Dictionary<string, object> fieldDict)
        {
            if (filter == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.Add(_TYPE, filter.Type.ToString());
            fieldDict.AddObject(_VERSION, filter.Version);
        }

        internal static void Deserialize(TokenFilterBase filter, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return;

            filter.Version = fieldDict.GetDoubleOrNull(_VERSION);
        }
    }
}
