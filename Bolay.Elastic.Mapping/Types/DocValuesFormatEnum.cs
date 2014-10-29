using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    public sealed class DocValuesFormatEnum : TypeSafeEnumBase<DocValuesFormatEnum>
    {
        public static readonly DocValuesFormatEnum Memory = new DocValuesFormatEnum("memory");
        public static readonly DocValuesFormatEnum Disk = new DocValuesFormatEnum("disk");
        public static readonly DocValuesFormatEnum Default = new DocValuesFormatEnum("default");

        private DocValuesFormatEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
