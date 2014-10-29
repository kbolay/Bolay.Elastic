using Bolay.Elastic.Api.Basic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Basic
{
    public interface IBasicRepository
    {
        BasicInformation Get();
    }
}
