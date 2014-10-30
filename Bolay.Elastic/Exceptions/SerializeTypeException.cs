using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Exceptions
{
    public class SerializeTypeException : Exception
    {
        private Type _targetType;

        public Type TargetType { get { return _targetType; } }

        public SerializeTypeException(Type type)
            : base("Serializer expects " + type.Name + ".")
        {
            _targetType = type;
        }
    }
    public class SerializeTypeException<T> : Exception
    {
        public Type TargetType { get { return typeof(T); } }

        public SerializeTypeException()
            : base("Serializer expects " + typeof(T).Name + ".")
        { }
    }
}
