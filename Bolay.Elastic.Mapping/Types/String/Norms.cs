using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.String
{
    public class Norms
    {
        private const string _NORMS = "norms";
        private const string _IS_ENABLED = "enabled";
        private const string _LOADING = "loading";

        // TODO: figure out a good way to force the specific defaults in the case of a change on index value
        // i think this has to be done in the constructor...
        internal const bool _IS_ENABLED_ANALYZED_DEFAULT = true;
        internal const bool _IS_ENABLED_NOT_ANALYZED_DEFAULT = false;
        internal static NormLoadingEnum _LOADING_DEFAULT = NormLoadingEnum.Lazy;

        /// <summary>
        /// Gets or sets whether norms are enabled.
        /// Defaults to true for analyzed properties, and false for not_analyzed properties.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the loading method.
        /// Defaults to lazy. (This can be changed.)
        /// </summary>
        public NormLoadingEnum Loading { get; set; }

        public Norms()
        {
            IsEnabled = _IS_ENABLED_ANALYZED_DEFAULT;
            Loading = _LOADING_DEFAULT;
        }

        internal static void Serialize(Norms norms, Dictionary<string, object> fieldDict, bool isAnalyzed = true)
        {
            if (norms == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            Dictionary<string, object> normsDict = new Dictionary<string, object>();

            if (isAnalyzed)
                normsDict.AddObject(_IS_ENABLED, norms.IsEnabled, _IS_ENABLED_ANALYZED_DEFAULT);
            else
                normsDict.AddObject(_IS_ENABLED, norms.IsEnabled, _IS_ENABLED_NOT_ANALYZED_DEFAULT);

            normsDict.AddObject(_LOADING, norms.Loading, _LOADING_DEFAULT);

            if (normsDict.Any())
                fieldDict.Add(_NORMS, normsDict);
        }
        internal static Norms Deserialize(Dictionary<string, object> fieldDict, bool isAnalyzed = true)
        {
            if (fieldDict == null || !fieldDict.ContainsKey(_NORMS))
                return null;

            Norms norms = new Norms();
            Dictionary<string, object> normsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_NORMS));
            if (isAnalyzed)
                norms.IsEnabled = normsDict.GetBool(_IS_ENABLED, _IS_ENABLED_ANALYZED_DEFAULT);
            else
                norms.IsEnabled = normsDict.GetBool(_IS_ENABLED, _IS_ENABLED_NOT_ANALYZED_DEFAULT);

            norms.Loading = NormLoadingEnum.Find(normsDict.GetString(_LOADING, _LOADING_DEFAULT.ToString()));

            return norms;
        }
    }
}
