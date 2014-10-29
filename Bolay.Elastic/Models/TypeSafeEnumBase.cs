using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Models
{
    /// <summary>
    /// Inherit this class to create type safe enumerations.
    /// Using a class instead of an enum allows you to attach other data to your each item in the enumeration.
    /// </summary>
    /// <typeparam name="T">The class you are making an enumeration of.</typeparam>
    public abstract class TypeSafeEnumBase<T> where T : class
    {
        /// <summary>
        /// The string name of the item in the enumeration.
        /// The value ToString will return.
        /// </summary>
        protected string Value { get; set; }

        /// <summary>
        /// Fill this in the constructor with each newly created instance of T.
        /// </summary>
        protected static List<T> _AllItems { get; set; }

        /// <summary>
        /// Returns all items of the enumeration.
        /// </summary>
        public static IEnumerable<T> All { get { return _AllItems; } }

        /// <summary>
        /// Call using : base(value) from the constructor of the class.
        /// </summary>
        /// <param name="value">The string name to associate with this enumeration value. It will be returned by ToString().</param>
        protected TypeSafeEnumBase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "Cannot create TypeSafeEnum without a value.");

            if(_AllItems == null)
                _AllItems = new List<T>();
            
            this.Value = value;            
        }

        /// <summary>
        /// Find a specific item in the enumeration by name.
        /// </summary>
        /// <param name="value">The string value to match on.</param>
        /// <returns></returns>
        public static T Find(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "Finding a " + typeof(T).ToString() + " requires a value.");

            if (_AllItems == null)
                _AllItems = new List<T>();

            if (!_AllItems.Any())
                return null;

            return _AllItems.FirstOrDefault(x => x.ToString().Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get the string value of the enumerated item.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Value;
        }
    }
}
