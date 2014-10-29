using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Distance
{
    [JsonConverter(typeof(DistanceSerializer))]
    public class DistanceValue
    {
        public Double Size { get; private set; }
        public DistanceUnitEnum Unit { get; private set; }

        /// <summary>
        /// Create a distance value from a string.
        /// Unit defaults to meter.
        /// </summary>
        /// <param name="value"></param>
        public DistanceValue(string value)
        {
            if (value.Contains(DistanceUnitEnum.Centimeter.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Centimeter.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Centimeter;
            }
            else if (value.Contains(DistanceUnitEnum.Inch.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Inch.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Inch;
            }
            else if (value.Contains(DistanceUnitEnum.Kilometer.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Kilometer.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Kilometer;
            }
            else if (value.Contains(DistanceUnitEnum.Mile.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Mile.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Mile;
            }
            else if (value.Contains(DistanceUnitEnum.Millimeter.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Millimeter.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Millimeter;
            }
            else if (value.Contains(DistanceUnitEnum.Meter.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Meter.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Meter;
            }            
            else if (value.Contains(DistanceUnitEnum.Yard.ToString()))
            {
                Size = Convert.ToInt64(value.Replace(DistanceUnitEnum.Yard.ToString(), string.Empty));
                Unit = DistanceUnitEnum.Yard;
            }
            else
            {
                Int64 sizeValue;
                if (Int64.TryParse(value, out sizeValue))
                {
                    Size = sizeValue;
                    Unit = DistanceUnitEnum.Meter;
                }
                else
                    throw new ArgumentException("String cannot be converted into distance precision.");
            }
        }

        /// <summary>
        /// Create a distance unit by size and unit.
        /// </summary>
        /// <param name="size">The numeric value for the distance.</param>
        /// <param name="unit">The unit of measure for the distance.</param>
        public DistanceValue(Double size, DistanceUnitEnum unit)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException("size", "Distance size must be greater than zero.");
            if (unit == null)
                throw new ArgumentNullException("unit", "Distance unit is required.");

            Size = size;
            Unit = unit;
        }

        public override string ToString()
        {
            if (Size <= 0 || Unit == null)
                return null;

            return string.Format("{0}{1}", Size, Unit.ToString());
        }
    }
}
