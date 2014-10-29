using Bolay.Elastic.Api.Analyze.IndexAnalysis.Analyzers;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.CharacterFilters;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.TokenFilters;
using Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.Models
{
    public class AnalyzeRequest
    {
        public string Index { get; set; }
        public string Field { get; set; }
        public AnalyzerEnum Analyzer { get; set; }
        public TokenizerEnum Tokenizer { get; set; }
        public List<TokenFilterEnum> TokenFilters { get; set; }
        public List<CharacterFilterEnum> CharacterFilters { get; set; }
        public string Text { get; set; }

        public AnalyzeRequest(string text) 
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("text");

            Text = text;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("?");

            StringBuilder textBuilder = new StringBuilder();
            textBuilder.Append("text=");
            textBuilder.Append(Text);

            if (string.IsNullOrWhiteSpace(Index))
            {
                if ((Analyzer == null || Analyzer == AnalyzerEnum.Custom) && Tokenizer != null)
                {
                    builder.Append("tokenizer=");
                    builder.Append(Tokenizer.ToString());

                    if (TokenFilters != null && TokenFilters.Any())
                    {

                        StringBuilder filterBuilder = new StringBuilder();
                        foreach (TokenFilterEnum filter in TokenFilters)
                        {
                            if (filterBuilder.Length > 0)
                                filterBuilder.Append(",");

                            filterBuilder.Append(filter.ToString());
                        }
                        builder.Append("&filters=");
                        builder.Append(filterBuilder);
                    }

                    if (CharacterFilters != null && CharacterFilters.Any())
                    {

                        StringBuilder filterBuilder = new StringBuilder();
                        foreach (CharacterFilterEnum filter in CharacterFilters)
                        {
                            if (filterBuilder.Length > 0)
                                filterBuilder.Append(",");

                            filterBuilder.Append(filter.ToString());
                        }
                        builder.Append("&char_filters=");
                        builder.Append(filterBuilder);
                    }
                }
                else
                {
                    builder.Append("analyzer=");
                    builder.Append(Analyzer.ToString());
                }
            }
            else
            {
                if (builder.Length > 1)
                    builder.Append("&");

                builder.Append(textBuilder);
            }
            return builder.ToString();
        }
    }
}
