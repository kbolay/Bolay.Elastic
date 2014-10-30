using Bolay.Elastic.Exceptions;
using Bolay.Elastic.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    public class UpdateBulkRequestSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type updateActionType = typeof(UpdateBulkAction<>);
            Type valueType = value.GetType();

            if (valueType != updateActionType)
            {
                throw new SerializeTypeException(updateActionType);
            }

            UpdateBulkAction<object> action = value as UpdateBulkAction<object>;

            Dictionary<string, object> fieldDict = new Dictionary<string, object>();
            fieldDict.AddObject(UpdateBulkAction<object>.PARTIAL_DOCUMENT, action.Document);
            action.UpdateScript.Serialize(fieldDict);
            fieldDict.AddObject(UpdateBulkAction<object>.UPSERT, action.UpsertDocument);
            fieldDict.AddObject(UpdateBulkAction<object>.DOC_AS_UPSERT, action.IsUpsert, false);

            serializer.Serialize(writer, fieldDict);
        }
    }
}
