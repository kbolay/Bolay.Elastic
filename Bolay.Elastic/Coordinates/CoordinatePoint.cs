using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Coordinates
{
    [JsonConverter(typeof(CoordinatePointSerializer))]
    public class CoordinatePoint
    {
        // TODO: Convert Lat/Lon to GeoHash value
        // TODO: Convert GeoHash value to Lat/Lon
        // http://en.wikipedia.org/wiki/Geohash

        public Double Latitude { get; private set; }
        public Double Longitude { get; private set; }

        public string GeoHash { get; private set; }

        public CoordinatePoint(Double latitude, Double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public CoordinatePoint(string geoHash)
        {
            GeoHash = geoHash;
        }
    }
}
