using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        private Type _targetType;
        private string _propertyName;

        public Type TargetType { get { return _targetType; } }
        public string PropertyName { get { return _propertyName; } }

        public PropertyNotFoundException(Type type, string propertyName)
            : base("Unable to find property " + propertyName + " on type " + type.Name + ".")
        {
            _targetType = type;
        }
    }
}
