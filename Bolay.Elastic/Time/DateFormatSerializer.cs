using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Time
{
    public class DateFormatSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType != typeof(string))
                return null;

            return new DateFormat(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateFormat))
                throw new SerializeTypeException<DateFormat>();

            DateFormat format = value as DateFormat;

            if (!string.IsNullOrWhiteSpace(format.Format))
                serializer.Serialize(writer, format.Format);
        }
    }
}
