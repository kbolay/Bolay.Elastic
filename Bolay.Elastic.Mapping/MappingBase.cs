using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping
{
    public abstract class MappingBase
    {
        internal const string _INDEX_KEY = "index";
        internal const string _STORE_KEY = "store";

        private IndexSettingEnum _INDEX_DEFAULT { get; set; }
        private bool _STORE_DEFAULT { get; set; }

        internal bool IsAnalyzed 
        { 
            get 
            {
                if (_Index != null && _Index == IndexSettingEnum.Analyzed)
                    return true;

                return false;
            } 
        }

        private IndexSettingEnum _Index { get; set; }

        /// <summary>
        /// Gets or sets the index value for the field.
        /// </summary>
        public IndexSettingEnum Index 
        {
            get { return _Index; } 
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("Index requires a value.");

                _Index = value; 
            } 
        }
                
        /// <summary>
        /// Gets or sets the store value for the field.
        /// </summary>
        public bool Store { get; set; }

        /// <summary>
        /// Creates the mapping value.
        /// </summary>
        public MappingBase(IndexSettingEnum indexDefault, bool storeDefault) 
        {
            if (indexDefault == null)
                throw new ArgumentNullException("indexDefault", "MappingBase requires an index default value.");

            _INDEX_DEFAULT = indexDefault;
            _STORE_DEFAULT = storeDefault;

            Index = _INDEX_DEFAULT;
            Store = _STORE_DEFAULT;
        }

        internal static void Serialize(MappingBase mappingBase, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_INDEX_KEY, mappingBase.Index.ToString(), mappingBase._INDEX_DEFAULT.ToString());
            fieldDict.AddObject(_STORE_KEY, mappingBase.Store, mappingBase._STORE_DEFAULT);
        }

        internal static void Deserialize(MappingBase mappingBase, Dictionary<string, object> fieldDict)
        {
            if (fieldDict == null || !fieldDict.Any())
                return;

            IndexSettingEnum indexSetting = IndexSettingEnum.No;
            mappingBase.Index = IndexSettingEnum.Find(fieldDict.GetString(_INDEX_KEY, mappingBase._INDEX_DEFAULT.ToString()));

            StoreSettingEnum storeSetting = StoreSettingEnum.No;
            mappingBase.Store = fieldDict.GetBool(_STORE_KEY, mappingBase._STORE_DEFAULT);
        }
    }
}