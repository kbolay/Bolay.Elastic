using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Filters;
using Bolay.Elastic.QueryDSL.Highlighting;
using Bolay.Elastic.QueryDSL.IndexBoosts;
using Bolay.Elastic.QueryDSL.Queries;
using Bolay.Elastic.QueryDSL.Rescoring;
using Bolay.Elastic.QueryDSL.ScriptFields;
using Bolay.Elastic.QueryDSL.Sorting;
using Bolay.Elastic.QueryDSL.SourceFiltering;
using Bolay.Elastic.QueryDSL.Suggesters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL
{
    public sealed class SearchPieceTypeEnum : TypeSafeEnumBase<SearchPieceTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly SearchPieceTypeEnum Query = new SearchPieceTypeEnum("query", typeof(IQuery));
        public static readonly SearchPieceTypeEnum Filter = new SearchPieceTypeEnum("filter", typeof(IFilter));
        public static readonly SearchPieceTypeEnum Sort = new SearchPieceTypeEnum("sort", typeof(ISortClause));
        public static readonly SearchPieceTypeEnum IndicesBoost = new SearchPieceTypeEnum("indices_boost", typeof(IndicesBoost));
        public static readonly SearchPieceTypeEnum Highlighter = new SearchPieceTypeEnum("highlighter", typeof(Highlighter));
        public static readonly SearchPieceTypeEnum Rescoring = new SearchPieceTypeEnum("rescore", typeof(Rescore));
        public static readonly SearchPieceTypeEnum ScriptField = new SearchPieceTypeEnum("script_field", typeof(ScriptFields.ScriptFieldRequest));
        public static readonly SearchPieceTypeEnum SourceFilter = new SearchPieceTypeEnum("_source", typeof(SourceFilter));
        public static readonly SearchPieceTypeEnum Suggestor = new SearchPieceTypeEnum("suggest", typeof(Suggest));

        private SearchPieceTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;

            _AllItems.Add(this);
        }
    }
}
