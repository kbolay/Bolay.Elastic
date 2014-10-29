using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Settings.Models
{
    [JsonConverter(typeof(SettingsCollectionSerializer))]
    public class SettingsCollection : List<Settings>
    {
    }
}
