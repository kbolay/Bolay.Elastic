using Bolay.Elastic.Mapping.Fields._All;
using Bolay.Elastic.Mapping.Fields._Analyzer;
using Bolay.Elastic.Mapping.Fields._Id;
using Bolay.Elastic.Mapping.Fields._Index;
using Bolay.Elastic.Mapping.Fields._Parent;
using Bolay.Elastic.Mapping.Fields._Routing;
using Bolay.Elastic.Mapping.Fields._Size;
using Bolay.Elastic.Mapping.Fields._Source;
using Bolay.Elastic.Mapping.Fields._Timestamp;
using Bolay.Elastic.Mapping.Fields._Ttl;
using Bolay.Elastic.Mapping.Fields._Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Fields
{
    public class DocumentMapping
    {
        private const string _ID = "_id";
        private const string _TYPE = "_type";
        private const string _SOURCE = "_source";
        private const string _ALL = "_all";
        private const string _ANALYZER = "_analyzer";
        private const string _PARENT = "_parent";
        private const string _ROUTING = "_routing";
        private const string _INDEX = "_index";
        private const string _SIZE = "_size";
        private const string _TIMESTAMP = "_timestamp";
        private const string _TIME_TO_LIVE = "_ttl";

        private static DocumentIdentifier _ID_DEFAULT = new DocumentIdentifier();
        private static DocumentType _TYPE_DEFAULT = new DocumentType();
        private static DocumentSource _SOURCE_DEFAULT = new DocumentSource();
        private static All _ALL_DEFAULT = new All();
        //private static DocumentAnalyzer _ANALYZER_DEFAULT = new DocumentAnalyzer();
        //private static ParentType _PARENT_DEFAULT = new ParentType();
        private static DocumentRouting _ROUTING_DEFAULT = new DocumentRouting();
        private static DocumentIndex _INDEX_DEFAULT = new DocumentIndex();
        private static DocumentSize _SIZE_DEFAULT = new DocumentSize();
        private static DocumentTimestamp _TIMESTAMP_DEFAULT = new DocumentTimestamp();
        //private static DocumentTimeToLive _TIME_TO_LIVE_DEFAULT = new DocumentTimeToLive();

        /// <summary>
        /// Gets or sets the _id mapping settings.
        /// </summary>
        public DocumentIdentifier Id { get; set; }

        /// <summary>
        /// Gets or sets the _type mapping settings.
        /// </summary>
        public DocumentType Type { get; set; }

        /// <summary>
        /// Gets or sets the _source mapping settings.
        /// </summary>
        public DocumentSource Source { get; set; }

        /// <summary>
        /// Gets or sets the _all mapping settings.
        /// </summary>
        public All All { get; set; }

        /// <summary>
        /// Gets or sets the _analyzer mapping settings.
        /// </summary>
        public DocumentAnalyzer Analyzer { get; set; }

        /// <summary>
        /// Gets or sets the _parent mapping settings.
        /// </summary>
        public ParentType Parent { get; set; }

        /// <summary>
        /// Gets or sets the _routing mapping settings.
        /// </summary>
        public DocumentRouting Routing { get; set; }

        /// <summary>
        /// Gets or sets the _index mapping settings.
        /// </summary>
        public DocumentIndex Index { get; set; }

        /// <summary>
        /// Gets or sets the _size mapping settings.
        /// </summary>
        public DocumentSize Size { get; set; }

        /// <summary>
        /// Gets or sets the _timestamp mapping settings.
        /// </summary>
        public DocumentTimestamp Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the _ttl mapping settings.
        /// </summary>
        public DocumentTimeToLive TimeToLive { get; set; }

        /// <summary>
        /// Create the document mapping object. Creats all fields with default values. _analyzer, _parent, _ttl have no default values.
        /// </summary>
        public DocumentMapping() 
        {
            Id = _ID_DEFAULT;
            Source = _SOURCE_DEFAULT;
            Type = _TYPE_DEFAULT;
            All = _ALL_DEFAULT;
            Routing = _ROUTING_DEFAULT;
            Index = _INDEX_DEFAULT;
            Size = _SIZE_DEFAULT;
            Timestamp = _TIMESTAMP_DEFAULT;
        }

        internal static void Serialize(DocumentMapping fields, Dictionary<string, object> fieldDict)
        {
            if (fields == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_ID, fields.Id, _ID_DEFAULT);
            fieldDict.AddObject(_TYPE, fields.Type, _TYPE_DEFAULT);
            fieldDict.AddObject(_SOURCE, fields.Source, _SOURCE_DEFAULT);
            fieldDict.AddObject(_ALL, fields.All, _ALL_DEFAULT);
            fieldDict.AddObject(_ANALYZER, fields.Analyzer);
            fieldDict.AddObject(_PARENT, fields.Parent);
            fieldDict.AddObject(_ROUTING, fields.Routing, _ROUTING_DEFAULT);
            fieldDict.AddObject(_INDEX, fields.Index, _INDEX_DEFAULT);
            fieldDict.AddObject(_SIZE, fields.Size, _SIZE_DEFAULT);
            fieldDict.AddObject(_TIMESTAMP, fields.Timestamp, _TIMESTAMP_DEFAULT);
            fieldDict.AddObject(_TIME_TO_LIVE, fields.TimeToLive);
        }

        internal static DocumentMapping Deserialize(Dictionary<string, object> fieldDict)
        {
            DocumentMapping mapping = new DocumentMapping();
            if (fieldDict.ContainsKey(_ALL))
                mapping.All = JsonConvert.DeserializeObject<All>(fieldDict.GetString(_ALL));
            if (fieldDict.ContainsKey(_ANALYZER))
                mapping.Analyzer = JsonConvert.DeserializeObject<DocumentAnalyzer>(fieldDict.GetString(_ANALYZER));
            if (fieldDict.ContainsKey(_ID))
                mapping.Id = JsonConvert.DeserializeObject<DocumentIdentifier>(fieldDict.GetString(_ID));
            if (fieldDict.ContainsKey(_INDEX))
                mapping.Index = JsonConvert.DeserializeObject<DocumentIndex>(fieldDict.GetString(_INDEX));
            if (fieldDict.ContainsKey(_PARENT))
                mapping.Parent = JsonConvert.DeserializeObject<ParentType>(fieldDict.GetString(_PARENT));
            if (fieldDict.ContainsKey(_ROUTING))
                mapping.Routing = JsonConvert.DeserializeObject<DocumentRouting>(fieldDict.GetString(_ROUTING));
            if (fieldDict.ContainsKey(_SIZE))
                mapping.Size = JsonConvert.DeserializeObject<DocumentSize>(fieldDict.GetString(_SIZE));
            if (fieldDict.ContainsKey(_SOURCE))
                mapping.Source = JsonConvert.DeserializeObject<DocumentSource>(fieldDict.GetString(_SOURCE));
            if (fieldDict.ContainsKey(_TIME_TO_LIVE))
                mapping.TimeToLive = JsonConvert.DeserializeObject<DocumentTimeToLive>(fieldDict.GetString(_TIME_TO_LIVE));
            if (fieldDict.ContainsKey(_TIMESTAMP))
                mapping.Timestamp = JsonConvert.DeserializeObject<DocumentTimestamp>(fieldDict.GetString(_TIMESTAMP));
            if (fieldDict.ContainsKey(_TYPE))
                mapping.Type = JsonConvert.DeserializeObject<DocumentType>(fieldDict.GetString(_TYPE));

            return mapping;
        }
    }
}
