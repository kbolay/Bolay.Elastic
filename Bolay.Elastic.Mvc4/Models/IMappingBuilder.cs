using Bolay.Elastic.Api.Mapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Mapping.Builder
{
    public interface IMappingBuilder
    {
        IndexMapping BuildMapping(string sampleJson);
        IndexMapping BuildMapping(Type type);
    }
}
