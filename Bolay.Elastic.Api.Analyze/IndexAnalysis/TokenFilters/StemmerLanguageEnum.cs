using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters
{
    public sealed class StemmerLanguageEnum : TypeSafeEnumBase<StemmerLanguageEnum>
    {
        public static readonly StemmerLanguageEnum Arabic = new StemmerLanguageEnum("arabic");
        public static readonly StemmerLanguageEnum Armenian = new StemmerLanguageEnum("armenian");
        public static readonly StemmerLanguageEnum Basque = new StemmerLanguageEnum("basque");
        public static readonly StemmerLanguageEnum Brazilian = new StemmerLanguageEnum("brazilian");
        public static readonly StemmerLanguageEnum Bulgarian = new StemmerLanguageEnum("bulgarian");
        public static readonly StemmerLanguageEnum Catalan = new StemmerLanguageEnum("catalan");
        public static readonly StemmerLanguageEnum Czech = new StemmerLanguageEnum("czech");
        public static readonly StemmerLanguageEnum Danish = new StemmerLanguageEnum("danish");
        public static readonly StemmerLanguageEnum Dutch = new StemmerLanguageEnum("dutch");
        public static readonly StemmerLanguageEnum Finnish = new StemmerLanguageEnum("finnish");
        public static readonly StemmerLanguageEnum French = new StemmerLanguageEnum("french");
        public static readonly StemmerLanguageEnum German = new StemmerLanguageEnum("german");
        public static readonly StemmerLanguageEnum German2 = new StemmerLanguageEnum("german2");
        public static readonly StemmerLanguageEnum Greek = new StemmerLanguageEnum("greek");
        public static readonly StemmerLanguageEnum Hungarian = new StemmerLanguageEnum("hungarian");
        public static readonly StemmerLanguageEnum Italian = new StemmerLanguageEnum("italian");
        public static readonly StemmerLanguageEnum Kp = new StemmerLanguageEnum("kp");
        public static readonly StemmerLanguageEnum KStem = new StemmerLanguageEnum("kstem");
        public static readonly StemmerLanguageEnum Lovins = new StemmerLanguageEnum("lovins");
        public static readonly StemmerLanguageEnum Latvian = new StemmerLanguageEnum("latvian");
        public static readonly StemmerLanguageEnum Norwegian = new StemmerLanguageEnum("norwegian");
        public static readonly StemmerLanguageEnum MinimalNorwegian = new StemmerLanguageEnum("minimal_norwegian");
        public static readonly StemmerLanguageEnum Porter = new StemmerLanguageEnum("porter");
        public static readonly StemmerLanguageEnum Portuguese = new StemmerLanguageEnum("portuguese");
        public static readonly StemmerLanguageEnum Romanian = new StemmerLanguageEnum("romanian");
        public static readonly StemmerLanguageEnum Russian = new StemmerLanguageEnum("russian");
        public static readonly StemmerLanguageEnum Spanish = new StemmerLanguageEnum("spanish");
        public static readonly StemmerLanguageEnum Swedish = new StemmerLanguageEnum("swedish");
        public static readonly StemmerLanguageEnum Turkish = new StemmerLanguageEnum("turkish");
        public static readonly StemmerLanguageEnum MinimalEnglish = new StemmerLanguageEnum("minimal_english");
        public static readonly StemmerLanguageEnum PossessiveEnglish = new StemmerLanguageEnum("possessive_english");
        public static readonly StemmerLanguageEnum LightFinnish = new StemmerLanguageEnum("light_finnish");
        public static readonly StemmerLanguageEnum LightFrench = new StemmerLanguageEnum("light_french");
        public static readonly StemmerLanguageEnum MinimalFrench = new StemmerLanguageEnum("minimal_french");
        public static readonly StemmerLanguageEnum LightGerman = new StemmerLanguageEnum("light_german");
        public static readonly StemmerLanguageEnum MinimalGerman = new StemmerLanguageEnum("minimal_german");
        public static readonly StemmerLanguageEnum Hindi = new StemmerLanguageEnum("hindi");
        public static readonly StemmerLanguageEnum LightHungarian = new StemmerLanguageEnum("light_hungarian");
        public static readonly StemmerLanguageEnum Indonesian = new StemmerLanguageEnum("indonesian");
        public static readonly StemmerLanguageEnum LightItalian = new StemmerLanguageEnum("light_italian");
        public static readonly StemmerLanguageEnum LightPortuguese = new StemmerLanguageEnum("light_portuguese");
        public static readonly StemmerLanguageEnum MinimalPortuguese = new StemmerLanguageEnum("minimal_portuguese");
        public static readonly StemmerLanguageEnum LightRussian = new StemmerLanguageEnum("light_russian");
        public static readonly StemmerLanguageEnum LightSpanish = new StemmerLanguageEnum("light_spanish");
        public static readonly StemmerLanguageEnum LightSwedish = new StemmerLanguageEnum("light_swedish");

        private StemmerLanguageEnum(string value)
            : base(value)
        {
            _AllItems.Add(this);
        }
    }
}
