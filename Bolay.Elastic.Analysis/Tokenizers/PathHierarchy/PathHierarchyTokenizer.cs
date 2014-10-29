using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Analysis.Tokenizers.PathHierarchy
{
    /// <summary>
    /// http://www.elasticsearch.org/guide/en/elasticsearch/reference/1.x/analysis-pathhierarchy-tokenizer.html
    /// </summary>
    [JsonConverter(typeof(PathHierarchyTokenizerSerializer))]
    public class PathHierarchyTokenizer : TokenizerBase
    {
        internal const string _DELIMITER_DEFAULT = "/";
        internal const Int64 _BUFFER_SIZE_DEFAULT = 1024;
        internal const Int64 _SKIP_DEFAULT = 0;
        internal const bool _REVERSE_DEFAULT = false;

        private Int64 _BufferSize { get; set; }
        private Int64 _Skip { get; set; }

        /// <summary>
        /// Gets or sets the delimiter of the path.
        /// Defaults to /.
        /// </summary>
        public string Delimeter { get; set; }

        /// <summary>
        /// Gets or sets the replacement character.
        /// Defaults to the delimiter.
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// Gets or sets the buffer size.
        /// Defaults to 1024.
        /// </summary>
        public Int64 BufferSize 
        {
            get { return _BufferSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("BufferSize", "Must be greater than zero.");
                _BufferSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the reverse value.
        /// Defaults to false.
        /// </summary>
        public bool Reverse { get; set; }

        /// <summary>
        /// Gets or sets the skip value.
        /// Defaults to 0.
        /// </summary>
        public Int64 Skip
        {
            get { return _Skip; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("Skip", "Must be greater than or equal to zero.");
                _Skip = value;
            }
        }

        /// <summary>
        /// Creates a path hierarchy tokenizer.
        /// </summary>
        /// <param name="name">Sets the name of the tokenizer.</param>
        public PathHierarchyTokenizer(string name) 
            : base(name, TokenizerTypeEnum.PathHierarchy) 
        {
            Delimeter = _DELIMITER_DEFAULT;
            Replacement = _DELIMITER_DEFAULT;
            BufferSize = _BUFFER_SIZE_DEFAULT;
            Reverse = _REVERSE_DEFAULT;
            Skip = _SKIP_DEFAULT;
        }
    }
}
