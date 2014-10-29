using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Analyze.IndexAnalysis.Tokenizers
{
    public class Keyword : TokenizerBase
    {
        private const Int64 _BUFFER_SIZE_DEFAULT = 256;

        private Int64? _BufferSize { get; set; }

        public Int64 BufferSize 
        {
            get
            {
                if (_BufferSize.HasValue)
                    return _BufferSize.Value;
                return _BUFFER_SIZE_DEFAULT;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("BufferSize");

                _BufferSize = value;
            }
        }

        public Keyword() : base(TokenizerEnum.Keyword) { }
    }
}
