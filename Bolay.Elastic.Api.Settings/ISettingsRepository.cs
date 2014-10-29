using Bolay.Elastic.Analysis;
using Bolay.Elastic.Api.Settings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Settings
{
    public interface ISettingsRepository
    {
        IEnumerable<Models.Settings> Get();
        IEnumerable<Models.Settings> GetByAlias(string alias);
        Models.Settings GetByIndex(string indexName);
    }
}
