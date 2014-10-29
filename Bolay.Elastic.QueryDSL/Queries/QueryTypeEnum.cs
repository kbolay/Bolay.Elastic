using Bolay.Elastic.Models;
using Bolay.Elastic.QueryDSL.Queries.Bool;
using Bolay.Elastic.QueryDSL.Queries.Boosting;
using Bolay.Elastic.QueryDSL.Queries.Common;
using Bolay.Elastic.QueryDSL.Queries.ConstantScore;
using Bolay.Elastic.QueryDSL.Queries.Fuzzy;
using Bolay.Elastic.QueryDSL.Queries.FuzzyLikeThis;
using Bolay.Elastic.QueryDSL.Queries.HasChild;
using Bolay.Elastic.QueryDSL.Queries.Ids;
using Bolay.Elastic.QueryDSL.Queries.Indices;
using Bolay.Elastic.QueryDSL.Queries.Match;
using Bolay.Elastic.QueryDSL.Queries.MatchAll;
using Bolay.Elastic.QueryDSL.Queries.MoreLikeThis;
using Bolay.Elastic.QueryDSL.Queries.Nested;
using Bolay.Elastic.QueryDSL.Queries.Prefix;
using Bolay.Elastic.QueryDSL.Queries.QueryString;
using Bolay.Elastic.QueryDSL.Queries.Range;
using Bolay.Elastic.QueryDSL.Queries.Regex;
using Bolay.Elastic.QueryDSL.Queries.Scoring;
using Bolay.Elastic.QueryDSL.Queries.SimpleQueryString;
using Bolay.Elastic.QueryDSL.Queries.Span;
using Bolay.Elastic.QueryDSL.Queries.Term;
using Bolay.Elastic.QueryDSL.Queries.Terms;
using Bolay.Elastic.QueryDSL.Queries.TopChildren;
using Bolay.Elastic.QueryDSL.Queries.Wildcard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.QueryDSL.Queries
{
    public sealed class QueryTypeEnum : TypeSafeEnumBase<QueryTypeEnum>
    {
        public Type ImplementationType { get; private set; }

        public static readonly QueryTypeEnum Bool = new QueryTypeEnum("bool", typeof(BoolQuery));
        public static readonly QueryTypeEnum Term = new QueryTypeEnum("term", typeof(TermQuery));
        public static readonly QueryTypeEnum Boosting = new QueryTypeEnum("boosting", typeof(BoostingQuery));
        public static readonly QueryTypeEnum Match = new QueryTypeEnum("match", typeof(MatchQuery));
        public static readonly QueryTypeEnum MatchPhrase = new QueryTypeEnum("match_phrase", typeof(MatchPhraseQuery));
        public static readonly QueryTypeEnum MatchPhrasePrefix = new QueryTypeEnum("match_phrase_prefix", typeof(MatchPhrasePrefixQuery));
        public static readonly QueryTypeEnum MultiMatch = new QueryTypeEnum("multi_match", typeof(MultiMatchQuery));
        public static readonly QueryTypeEnum Common = new QueryTypeEnum("common", typeof(CommonQuery));
        public static readonly QueryTypeEnum ConstantScoreQuery = new QueryTypeEnum("constant_score", typeof(ConstantScoreQueryBase));
        public static readonly QueryTypeEnum FuzzyLikeThis = new QueryTypeEnum("fuzzy_like_this", typeof(FuzzyLikeThisQuery));
        public static readonly QueryTypeEnum Flt = new QueryTypeEnum("flt", typeof(FuzzyLikeThisQuery));
        public static readonly QueryTypeEnum FuzzLikeThisField = new QueryTypeEnum("fuzzy_like_this_field", typeof(FuzzyLikeThisFieldQuery));
        public static readonly QueryTypeEnum FltField = new QueryTypeEnum("flt_field", typeof(FuzzyLikeThisFieldQuery));
        public static readonly QueryTypeEnum FunctionScore = new QueryTypeEnum("function_score", typeof(FunctionScoreQueryBase));
        public static readonly QueryTypeEnum Fuzzy = new QueryTypeEnum("fuzzy", typeof(FuzzyQueryBase));
        public static readonly QueryTypeEnum HasChild = new QueryTypeEnum("has_child", typeof(HasChildQuery));
        public static readonly QueryTypeEnum HasParent = new QueryTypeEnum("has_parent", typeof(HasChildQuery));
        public static readonly QueryTypeEnum Ids = new QueryTypeEnum("ids", typeof(IdsQuery));
        public static readonly QueryTypeEnum Indices = new QueryTypeEnum("indices", typeof(IndicesQuery));
        public static readonly QueryTypeEnum MatchAll = new QueryTypeEnum("match_all", typeof(MatchAllQuery));
        public static readonly QueryTypeEnum MoreLikeThis = new QueryTypeEnum("more_like_this", typeof(MoreLikeThisQuery));
        public static readonly QueryTypeEnum Mlt = new QueryTypeEnum("mlt", typeof(MoreLikeThisQuery));
        public static readonly QueryTypeEnum MoreLikeThisField = new QueryTypeEnum("more_like_this_field", typeof(MoreLikeThisQuery));
        public static readonly QueryTypeEnum MltField = new QueryTypeEnum("mlt_field", typeof(MoreLikeThisQuery));
        public static readonly QueryTypeEnum Nested = new QueryTypeEnum("nested", typeof(NestedQuery));
        public static readonly QueryTypeEnum Prefix = new QueryTypeEnum("prefix", typeof(PrefixQuery));
        public static readonly QueryTypeEnum QueryString = new QueryTypeEnum("query_string", typeof(QueryStringQuery));
        public static readonly QueryTypeEnum SimpleQueryString = new QueryTypeEnum("simple_query_string", typeof(SimpleQueryStringQuery));
        public static readonly QueryTypeEnum Range = new QueryTypeEnum("range", typeof(RangeQueryBase));
        public static readonly QueryTypeEnum Regex = new QueryTypeEnum("regexp", typeof(RegexQuery));
        public static readonly QueryTypeEnum SpanFirst = new QueryTypeEnum(SpanQueryTypeEnum.First.ToString(), typeof(SpanFirstQuery));
        public static readonly QueryTypeEnum SpanTerm = new QueryTypeEnum(SpanQueryTypeEnum.Term.ToString(), typeof(SpanTermQuery));
        public static readonly QueryTypeEnum SpanMultiTerm = new QueryTypeEnum(SpanQueryTypeEnum.MultiTerm.ToString(), typeof(SpanMultiTermQuery));
        public static readonly QueryTypeEnum SpanNot = new QueryTypeEnum(SpanQueryTypeEnum.Not.ToString(), typeof(SpanNotQuery));
        public static readonly QueryTypeEnum SpanOr = new QueryTypeEnum(SpanQueryTypeEnum.Or.ToString(), typeof(SpanOrQuery));
        public static readonly QueryTypeEnum Terms = new QueryTypeEnum("terms", typeof(TermsQuery));
        public static readonly QueryTypeEnum TopChildren = new QueryTypeEnum("top_children", typeof(TopChildrenQuery));
        public static readonly QueryTypeEnum Wildcard = new QueryTypeEnum("wildcard", typeof(WildcardQuery));

        private QueryTypeEnum(string value, Type implementationType)
            : base(value)
        {
            ImplementationType = implementationType;

            _AllItems.Add(this);
        }
    }
}
