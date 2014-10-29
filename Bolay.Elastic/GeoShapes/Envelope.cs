using Bolay.Elastic.Coordinates;
using Bolay.Elastic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.GeoShapes
{
    [JsonConverter(typeof(EnvelopeSerializer))]
    public class Envelope : GeoShapeBase
    {
        public CoordinatePoint TopLeft { get; private set; }
        public CoordinatePoint BottomRight { get; private set; }

        public Envelope(CoordinatePoint topLeft, CoordinatePoint bottomRight)
            : base(GeoShapeTypeEnum.Envelope)
        {
            if (topLeft == null)
                throw new ArgumentNullException("topLeft", "Envelope requires a top left coordinate point.");
            if (bottomRight == null)
                throw new ArgumentNullException("bottomRight", "Envelope requires a bottom right coordinate point.");

            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}
