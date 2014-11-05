using Bolay.Elastic;
using Bolay.Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Properties
{
    public sealed class PostingFormatEnum : TypeSafeEnumBase<PostingFormatEnum>
    {
        public Uri Documentation { get; private set; }

        /// <summary>
        /// A postings format that uses disk-based storage but loads its terms and postings directly into memory. 
        /// Note this postings format is very memory intensive and has certain limitation that don’t allow 
        /// segments to grow beyond 2.1GB see {@link DirectPostingsFormat} for details.
        /// </summary>
        public static readonly PostingFormatEnum Direct = new PostingFormatEnum("direct", "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        /// <summary>
        /// A postings format that stores its entire terms, postings, positions and payloads in a finite
        /// state transducer. This format should only be used for primary keys or with fields where each
        /// term is contained in a very low number of documents.
        /// </summary>
        public static readonly PostingFormatEnum Memory = new PostingFormatEnum("memory", "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        /// <summary>
        /// A postings format in-lines the posting lists for very low frequent terms in the term dictionary. 
        /// This is useful to improve lookup performance for low-frequent terms.
        /// </summary>
        public static readonly PostingFormatEnum Pulsing = new PostingFormatEnum("pulsing", "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        /// <summary>
        /// A postings format that uses a bloom filter to improve term lookup performance. 
        /// This is useful for primarily keys or fields that are used as a delete key.
        /// </summary>
        public static readonly PostingFormatEnum BloomDefault = new PostingFormatEnum("bloom_default", "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        /// <summary>
        /// A postings format that combines the advantages of bloom and pulsing to 
        /// further improve lookup performance.
        /// </summary>
        public static readonly PostingFormatEnum BloomPulsing = new PostingFormatEnum("bloom_pulsing", "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        /// <summary>
        /// The default Elasticsearch postings format offering best general purpose performance. 
        /// This format is used if no postings format is specified in the field mapping.
        /// </summary>
        public static readonly PostingFormatEnum Default = new PostingFormatEnum("default", "http://www.elasticsearch.org/guide/reference/mapping/core-types/");

        private PostingFormatEnum(string value, string uri) : this(value, new Uri(uri)) { }
        private PostingFormatEnum(string value, Uri documentation)
            : base(value)
        {            
            this.Documentation = documentation;
            _AllItems.Add(this);
        }
    }
}
