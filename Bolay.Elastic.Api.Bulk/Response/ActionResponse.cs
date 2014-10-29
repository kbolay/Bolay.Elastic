using Bolay.Elastic.Api.Bulk.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Response
{
    [JsonConverter(typeof(ActionResponseSerializer))]
    public class ActionResponse
    {
        public string Action { get; set; }
        public string Index { get; set; }
        public string Type { get; set; }
        public string DocumentId { get; set; }
        public int Version { get; set; }
        public int Status { get; set; }
        public bool Found { get; set; }
        public bool Ok { get; set; }

        public bool Succesful 
        {
            get 
            {
                if (Status >= 200 && Status < 300 || Ok)
                {
                    return true;
                }

                return false;
            }
        }

        public string Error { get; set; }
    }
}
