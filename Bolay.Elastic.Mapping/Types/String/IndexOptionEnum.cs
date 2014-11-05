using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties.String
{
    public sealed class IndexOptionEnum : TypeSafeEnumBase<IndexOptionEnum>
    {
        /// <summary>
        /// Document numbers are indexed.
        /// </summary>
        public static readonly IndexOptionEnum DocumentId = new IndexOptionEnum("docs");

        /// <summary>
        /// Document numbers and term frequencies are indexed.
        /// </summary>
        public static readonly IndexOptionEnum TermFrequency = new IndexOptionEnum("freqs");

        /// <summary>
        /// Document numbers, term frequencies, and positions are indexed.
        /// </summary>
        public static readonly IndexOptionEnum Position = new IndexOptionEnum("positions");

        /// <summary>
        /// Only available after 0.90
        /// Document numbers, term frequencies, positions, and offets are indexed.
        /// </summary>
        public static readonly IndexOptionEnum Offset = new IndexOptionEnum("offsets");

        private IndexOptionEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
