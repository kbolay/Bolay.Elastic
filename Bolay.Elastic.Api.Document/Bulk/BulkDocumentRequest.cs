using Bolay.Elastic.Api.Document.Models;
using Bolay.Elastic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Bulk
{
    public class BulkDocumentRequest : DocumentRequestBase
    {
        private const string _BULK_VALUE = "_bulk";

        public IEnumerable<BulkActionBase> Actions { get; private set; }

        public override Uri BuildUri(IElasticUriProvider clusterUriProvider)
        {
            return new Uri(clusterUriProvider.ClusterUri, _BULK_VALUE);
        }

        public override string BuildQueryString()
        {
            return null;
        }

        public string BuildContent()
        {
            if (Actions == null || !Actions.Any(x => x != null))
                return null;

            StringBuilder content = new StringBuilder();
           
            foreach (BulkActionBase action in Actions.Where(x => x != null))
            {
                content.AppendLine(BuildActionMetaData(action));

                if(action.Document != null)
                    content.AppendLine(JsonConvert.SerializeObject(action.Document));
            }

            if(content.Length == 0)
                return null;

            return content.ToString();
        }

        private string BuildActionMetaData(BulkActionBase action)
        {
            if (action == null)
                return null;

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add(action.OperationType.ToString(), action);

            return JsonConvert.SerializeObject(dict);
        }
    }
}
