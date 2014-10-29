using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    public class MultiFieldNamingConvention
    {
        private const string _SEPARATOR_DEFAULT = "_";
        private const bool _PREPEND_PROPERTY_NAME_DEFAULT = true;

        private bool? _PrependPropertyName { get; set; }
        private string _Separator { get; set; }

        public bool PrependPropertyName
        {
            get
            {
                if (_PrependPropertyName.HasValue)
                    return _PrependPropertyName.Value;
                return _PREPEND_PROPERTY_NAME_DEFAULT;
            }
            set
            {
                _PrependPropertyName = value;
            }
        }
        public string Separator
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_Separator))
                    return _Separator;
                return _SEPARATOR_DEFAULT;
            }
            set
            {
                _Separator = value;
            }
        }
    }
}
