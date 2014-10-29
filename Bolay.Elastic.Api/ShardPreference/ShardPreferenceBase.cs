using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ShardPreference
{
    public abstract class ShardPreferenceBase
    {
        protected const string _PREFERENCE_VALUE_DELIMITER = ":";
        protected const string _MULTI_VALUE_DELIMITER = ",";

        protected ShardPreferenceTypeEnum PreferenceType { get; private set; }

        public ShardPreferenceBase(ShardPreferenceTypeEnum preferenceType)
        {
            if (preferenceType == null)
                throw new ArgumentNullException("preferenceType", "ShardPrefernceBase requires a shard preference type.");

            PreferenceType = preferenceType;
        }

        public virtual string ToString()
        {
            return PreferenceType.ToString();
        }
    }
}
