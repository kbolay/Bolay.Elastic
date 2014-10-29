using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Exceptions
{
    public class RequiredPropertyMissingException : Exception
    {
        public string PropertyName { get; private set; }

        public RequiredPropertyMissingException(string propertyName)
            : base(propertyName + " is a required property for.")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName", "RequiredPropertyMissingException requires a property name.");

            PropertyName = propertyName;
        }
    }
}
