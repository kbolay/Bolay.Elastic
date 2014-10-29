using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Exceptions
{
    public class ConflictingPropertiesException : Exception
    {
        private const string _DELIMITER = ",";
        public IEnumerable<string> ConflictingFields { get; set; }

        public ConflictingPropertiesException(IEnumerable<string> fields)
            : base(string.Join(_DELIMITER, fields.ToArray()) + " conflict with one another.")
        { }
        
    }
}
