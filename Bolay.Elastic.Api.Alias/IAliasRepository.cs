using Bolay.Elastic.Api.Alias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Alias
{
    public interface IAliasRepository
    {
        AliasResponse Get(string index = "*", string alias = "*");
        bool Add(string index, string alias, string filter = null, string search_routing = null, string index_routing = null);
        bool Add(List<string> indexes, string alias, string filter = null, string search_routing = null, string index_routing = null);
        bool Update(string index, string alias, string filter = null, string search_routing = null, string index_routing = null);
        bool Delete(string index, string alias);
    }
}
