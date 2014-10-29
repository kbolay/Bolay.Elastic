using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Highlighting
{
    /// <summary>
    /// Pre-loaded schemas for tags of highlighted values.
    /// This enum can be extended, because the class is not sealed.
    /// </summary>
    public class TagsSchemaEnum : TypeSafeEnumBase<TagsSchemaEnum>
    {
        public static readonly TagsSchemaEnum None = new TagsSchemaEnum("none");
        public static readonly TagsSchemaEnum Styled = new TagsSchemaEnum("styled");

        private TagsSchemaEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
