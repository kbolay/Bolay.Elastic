using Bolay.Elastic;
using Bolay.Elastic.Mapping.Properties.Attachment;
using Bolay.Elastic.Mapping.Properties.Binary;
using Bolay.Elastic.Mapping.Properties.Boolean;
using Bolay.Elastic.Mapping.Properties.Date;
using Bolay.Elastic.Mapping.Properties.GeoPoint;
using Bolay.Elastic.Mapping.Properties.GeoShape;
using Bolay.Elastic.Mapping.Properties.Ip;
using Bolay.Elastic.Mapping.Properties.Nested;
using Bolay.Elastic.Mapping.Properties.Numbers.Bytes;
using Bolay.Elastic.Mapping.Properties.Numbers.Doubles;
using Bolay.Elastic.Mapping.Properties.Numbers.Floats;
using Bolay.Elastic.Mapping.Properties.Numbers.Integers;
using Bolay.Elastic.Mapping.Properties.Numbers.Longs;
using Bolay.Elastic.Mapping.Properties.Numbers.Shorts;
using Bolay.Elastic.Mapping.Properties.Numbers.TokenCount;
using Bolay.Elastic.Mapping.Properties.Object;
using Bolay.Elastic.Mapping.Properties.String;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties
{
    public sealed class PropertyTypeEnum : TypeSafeEnumBase<PropertyTypeEnum>
    {
        public string Description { get; private set; }
        public Uri Documentation { get; private set; }
        public Type ImplementationType { get; private set; }

        public static readonly PropertyTypeEnum Object = new PropertyTypeEnum("object",
            typeof(ObjectProperty),
            "An object with its own properties.", 
            "http://www.elasticsearch.org/guide/reference/mapping/object-type/");

        public static readonly PropertyTypeEnum String = new PropertyTypeEnum("string",
            typeof(StringProperty),
            "A string.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Integer = new PropertyTypeEnum("integer",
            typeof(IntegerProperty),
            "An 32 bit integer.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Long = new PropertyTypeEnum("long", 
            typeof(LongProperty),
            "A 64 bit integer.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Float = new PropertyTypeEnum("float", 
            typeof(FloatProperty),
            "A float.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Double = new PropertyTypeEnum("double", 
            typeof(DoubleProperty),
            "A double.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Byte = new PropertyTypeEnum("byte", 
            typeof(ByteProperty),
            "A byte.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Short = new PropertyTypeEnum("short", 
            typeof(ShortProperty),
            "A 16 bit integer.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Boolean = new PropertyTypeEnum("boolean", 
            typeof(BooleanProperty),
            "A boolean.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Date = new PropertyTypeEnum("date", 
            typeof(DateProperty),
            "A datetime.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        public static readonly PropertyTypeEnum Binary = new PropertyTypeEnum("binary", 
            typeof(BinaryProperty),
            "A binary.", 
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        //public static readonly PropertyTypeEnum MultiField = new PropertyTypeEnum("multi_field", 
        //    typeof(MultiFieldProperty),
        //    "Meant for use on fields with multiple analyzers.", 
        //    "http://www.elasticsearch.org/guide/reference/mapping/multi-field-type/");

        public static readonly PropertyTypeEnum Nested = new PropertyTypeEnum("nested", 
            typeof(NestedObjectProperty),
            "A nested document.", 
            "http://www.elasticsearch.org/guide/reference/mapping/nested-type/");

        public static readonly PropertyTypeEnum IpAddress = new PropertyTypeEnum("ip", 
            typeof(IpAddressProperty),
            "An ipv4 address.", 
            "http://www.elasticsearch.org/guide/reference/mapping/ip-type/");

        public static readonly PropertyTypeEnum GeoPoint = new PropertyTypeEnum("geo_point", 
            typeof(GeoPointProperty),
            "A single point.", 
            "http://www.elasticsearch.org/guide/reference/mapping/geo-point-type/");

        public static readonly PropertyTypeEnum GeoShape = new PropertyTypeEnum("geo_shape", 
            typeof(GeoShapeProperty),
            "A shape drawn from multiple points.", 
            "http://www.elasticsearch.org/guide/reference/mapping/geo-shape-type/");

        public static readonly PropertyTypeEnum Attachment = new PropertyTypeEnum("attachment", 
            typeof(AttachmentProperty),
            "An attachment.", 
            "http://www.elasticsearch.org/guide/reference/mapping/attachment-type/");

        public static readonly PropertyTypeEnum TokenCount = new PropertyTypeEnum("token_count",
            typeof(TokenCountProperty),
            "A way to find the number of tokens in a field.",
            "http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/mapping-core-types.html#token_count");

        public static readonly PropertyTypeEnum MultiField = new PropertyTypeEnum("multi_field",
            typeof(StringProperty),
            "Not even supported in v1.0.0. I'll pretend its a string.",
            "http://www.elasticsearch.org/guide/reference/mapping/core-types/");
        
        private PropertyTypeEnum(string value, Type implementationType, string description, string documentationLink) : this(value, implementationType, description, new Uri(documentationLink)) { }
        private PropertyTypeEnum(string value, Type implementationType, string description, Uri documentationLink)
            : base(value)
        {
            ImplementationType = implementationType;
            Description = description;
            Documentation = documentationLink;

            _AllItems.Add(this);
        }
    }
}
