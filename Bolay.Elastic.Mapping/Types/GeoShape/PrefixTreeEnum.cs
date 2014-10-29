using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types.GeoShape
{
    public sealed class PrefixTreeEnum : TypeSafeEnumBase<PrefixTreeEnum>
    {
        /// <summary>
        /// Uses geohashes for grid squares. Geohashes are base32 encoded strings of the bits of 
        /// the latitude and longitude interleaved. So the longer the hash, the more precise it is. 
        /// Each character added to the geohash represents another tree level and adds 5 bits of 
        /// precision to the geohash. A geohash represents a rectangular area and has 32 sub rectangles. 
        /// The maximum amount of levels in Elasticsearch is 24.
        /// http://en.wikipedia.org/wiki/Geohash
        /// </summary>
        public static readonly PrefixTreeEnum GeoHash = new PrefixTreeEnum("geohash");

        /// <summary>
        /// Uses a quadtree for grid squares. Similar to geohash, quad trees interleave the bits
        /// of the latitude and longitude the resulting hash is a bit set. A tree level in a quad
        /// tree represents 2 bits in this bit set, one for each coordinate. The maximum amount
        /// of levels for the quad trees in elastic search is 50.
        /// http://en.wikipedia.org/wiki/Quadtree
        /// </summary>
        public static readonly PrefixTreeEnum QuadTree = new PrefixTreeEnum("quadtree");

        private PrefixTreeEnum(string value) : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
