using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Document.Models
{
    public class VersionTypeEnum : TypeSafeEnumBase<VersionTypeEnum>
    {
        public static readonly VersionTypeEnum Internal = new VersionTypeEnum("internal");
        public static readonly VersionTypeEnum External = new VersionTypeEnum("external");
        public static readonly VersionTypeEnum ExternalGte = new VersionTypeEnum("external_gte");
        public static readonly VersionTypeEnum Force = new VersionTypeEnum("force");

        private VersionTypeEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
