using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Exceptions
{
    public class ParsingException<T> : Exception
    {
        public Type Type { get { return typeof(T); } }
        public string PropertyName { get; private set; }

        public ParsingException(string propertyName)
            : base(propertyName + " is required to be a " + typeof(T).Name)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName", "ParsingException requires a property name.");

            PropertyName = propertyName;
        }
    }
}
