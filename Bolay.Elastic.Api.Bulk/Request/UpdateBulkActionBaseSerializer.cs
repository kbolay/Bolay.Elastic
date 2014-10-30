using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    internal class UpdateBulkActionBaseSerializer : JsonConverter
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

            Dictionary<string, object> actionFieldsDict = new Dictionary<string, object>();

            actionFieldsDict.AddObject(BulkActionBase.INDEX, action.Index);
            actionFieldsDict.AddObject(BulkActionBase.TYPE, action.Type);
            actionFieldsDict.AddObject(BulkActionBase.DOCUMENT_ID, action.DocumentId);
            actionFieldsDict.AddObject(BulkActionBase.VERSION, action.Version);
            actionFieldsDict.AddObject(BulkActionBase.ROUTING, action.Routing);
            actionFieldsDict.AddObject(BulkActionBase.PARENT, action.Parent);
            actionFieldsDict.AddObject(BulkActionBase.TIMESTAMP, action.TimeStamp);
            actionFieldsDict.AddObject(BulkActionBase.TIME_TO_LIVE, action.TimeToLive);
            actionFieldsDict.AddObject(UpdateBulkAction<object>.RETRY_ON_CONFLICT, action.RetriesOnConflict);

            Dictionary<string, object> actionDict = new Dictionary<string, object>();
            actionDict.Add(action.Action, actionFieldsDict);

            serializer.Serialize(writer, actionDict);
        }
    }
}
