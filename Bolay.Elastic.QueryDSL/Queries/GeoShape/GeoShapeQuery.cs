using Bolay.Elastic.GeoShapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries.GeoShape
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/query-dsl-geo-shape-query.html
    /// </summary>
    [JsonConverter(typeof(GeoShapeSerializer))]
    public class GeoShapeQuery : QueryBase
    {
        public string Field { get; private set; }
        public GeoShapeBase GeoShape { get; private set; }

        public GeoShapeQuery(string field, GeoShapeBase geoShape)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException("field", "GeoShapeQuery expects a field.");
            if (geoShape == null)
                throw new ArgumentNullException("geoShape", "The geo shape query expects a geo shape from Bolay.Elastic.GeoShape");

            Field = field;
            GeoShape = geoShape;
        }
    }
}
