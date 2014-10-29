using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields._Source
{
    [JsonConverter(typeof(CompressionThresholdSerializer))]
    public class CompressionThreshold
    {
        public Int64 Size { get; private set; }
        public SizeUnitEnum Unit { get; private set; }

        public CompressionThreshold(string value)
        {
            if (value.Contains(SizeUnitEnum.Byte.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(SizeUnitEnum.Byte.ToString(), ""));
                Unit = SizeUnitEnum.Byte;
            }
            else if (value.Contains(SizeUnitEnum.Kilobyte.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(SizeUnitEnum.Kilobyte.ToString(), ""));
                Unit = SizeUnitEnum.Kilobyte;
            }
            else
            {
                throw new ArgumentException("String cannot be converted into compression threshold.");
            }
        }
        public CompressionThreshold(Int64 size, SizeUnitEnum unit)
        {
            this.Size = size;
            this.Unit = unit;
        }

        public override string ToString()
        {
            if (Size <= 0 || Unit == null)
                return null;

            return string.Format("{0}{1}", Size, Unit);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CompressionThreshold))
                return false;

            if (obj == null)
                return false;

            CompressionThreshold compression = obj as CompressionThreshold;
            if (this.Size == compression.Size && this.Unit == compression.Unit)
            {
                return true;
            }

            return false;
        }
    }
}
