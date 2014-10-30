using Bolay.Elastic.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Request
{
    internal class UpdateBulkActionSerializer : JsonConverter
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
            Type valueGenericType = valueType.GetGenericTypeDefinition();            

            if (valueGenericType != updateActionType)
            {
                throw new SerializeTypeException(updateActionType);
            }

            BulkActionBase actionBase = value as BulkActionBase;

            Dictionary<string, object> actionFieldsDict = new Dictionary<string, object>();

            actionFieldsDict.AddObject(BulkActionBase.INDEX, actionBase.Index);
            actionFieldsDict.AddObject(BulkActionBase.TYPE, actionBase.Type);
            actionFieldsDict.AddObject(BulkActionBase.DOCUMENT_ID, actionBase.DocumentId);
            actionFieldsDict.AddObject(BulkActionBase.VERSION, actionBase.Version);
            actionFieldsDict.AddObject(BulkActionBase.ROUTING, actionBase.Routing);
            actionFieldsDict.AddObject(BulkActionBase.PARENT, actionBase.Parent);
            actionFieldsDict.AddObject(BulkActionBase.TIMESTAMP, actionBase.TimeStampStr);
            actionFieldsDict.AddObject(BulkActionBase.TIME_TO_LIVE, actionBase.TimeToLive);

            Type genericType = valueType.GetGenericArguments().First();
            Type updateGenericType = updateActionType.MakeGenericType(new Type[] { genericType });

            var updateAction = Convert.ChangeType(value, updateGenericType);
            int retries = (int)updateGenericType.GetProperty("RetriesOnConflict").GetValue(value);
            
            actionFieldsDict.AddObject(UpdateBulkAction<object>.RETRY_ON_CONFLICT, retries);

            Dictionary<string, object> actionDict = new Dictionary<string, object>();
            actionDict.Add(actionBase.Action, actionFieldsDict);

            serializer.Serialize(writer, actionDict);
        }
    }
}
