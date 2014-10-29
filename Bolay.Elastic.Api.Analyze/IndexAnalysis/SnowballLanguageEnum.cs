using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Models
{
    public sealed class SnowballLanguageEnum : TypeSafeEnumBase<SnowballLanguageEnum>
    {
        public static readonly SnowballLanguageEnum Armenian = new SnowballLanguageEnum("Armenian");
        public static readonly SnowballLanguageEnum Basque = new SnowballLanguageEnum("Basque");
        public static readonly SnowballLanguageEnum Catalan = new SnowballLanguageEnum("Catalan");
        public static readonly SnowballLanguageEnum Danish = new SnowballLanguageEnum("Danish");
        public static readonly SnowballLanguageEnum Dutch = new SnowballLanguageEnum("Dutch");
        public static readonly SnowballLanguageEnum English = new SnowballLanguageEnum("English");
        public static readonly SnowballLanguageEnum Finnish = new SnowballLanguageEnum("Finnish");
        public static readonly SnowballLanguageEnum French = new SnowballLanguageEnum("French");
        public static readonly SnowballLanguageEnum German = new SnowballLanguageEnum("Gernam");
        public static readonly SnowballLanguageEnum German2 = new SnowballLanguageEnum("German2");
        public static readonly SnowballLanguageEnum Hugarian = new SnowballLanguageEnum("Hungarian");
        public static readonly SnowballLanguageEnum Italian = new SnowballLanguageEnum("Italian");
        public static readonly SnowballLanguageEnum Kp = new SnowballLanguageEnum("Kp");
        public static readonly SnowballLanguageEnum Lovins = new SnowballLanguageEnum("Lovins");
        public static readonly SnowballLanguageEnum Norwegian = new SnowballLanguageEnum("Norwegian");
        public static readonly SnowballLanguageEnum Porter = new SnowballLanguageEnum("Porter");
        public static readonly SnowballLanguageEnum Portuguese = new SnowballLanguageEnum("Portuguese");
        public static readonly SnowballLanguageEnum Romanian = new SnowballLanguageEnum("Romanian");
        public static readonly SnowballLanguageEnum Russian = new SnowballLanguageEnum("Russian");
        public static readonly SnowballLanguageEnum Spanish = new SnowballLanguageEnum("Spanish");
        public static readonly SnowballLanguageEnum Swedish = new SnowballLanguageEnum("Swedish");
        public static readonly SnowballLanguageEnum Turkish = new SnowballLanguageEnum("Turkish");

        private SnowballLanguageEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
