using Bolay.Elastic.GeoShapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bolay.Elastic.QueryDSL.Filters.GeoShape
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geo-shape-filter.html
    /// </summary>
    [JsonConverter(typeof(GeoShapeSerializer))]
    public class GeoShapeFilter : FilterBase
    {
        public string Field { get; private set; }
        public GeoShapeBase GeoShape { get; private set; }

        public GeoShapeFilter(string field, GeoShapeBase geoShape)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoShapeFilter expects a field.");
            if (geoShape == null)
                throw new ArgumentNullException("geoShape", "GeoShapeFilter expects a geo shape from Bolay.Elastic.GeoShape");

            Field = field;
            GeoShape = geoShape;
            Cache = GeoShapeSerializer._CACHE_DEFAULT;
        }
    }
}
