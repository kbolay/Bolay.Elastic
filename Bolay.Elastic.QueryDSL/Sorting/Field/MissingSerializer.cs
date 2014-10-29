using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Sorting.Field
{
    public class MissingSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            MissingValue missingValue = null;
            MissingTypeEnum missingType = MissingTypeEnum.First;
            missingType = MissingTypeEnum.Find(reader.Value.ToString());
            if (missingType != null)
            {
                missingValue = new MissingValue(missingType);
            }
            else
            {
                missingValue = new MissingValue(reader.Value.ToString());
            }

            return missingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is MissingValue))
                throw new SerializeTypeException<MissingValue>();

            MissingValue missing = value as MissingValue;
            if (missing == null)
                return;

            if (missing.MissingType != null)
                serializer.Serialize(writer, missing.MissingType.ToString());
            else if(string.IsNullOrWhiteSpace(missing.CustomValue))
                serializer.Serialize(writer, missing.CustomValue);

            return;
        }
    }
}
