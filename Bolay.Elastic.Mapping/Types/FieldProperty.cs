using Bolay.Elastic.Api.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Mapping.Types
{
    /// <summary>
    /// For the following types: string, float, double, byte, short, long, integer, ip, date, boolean
    /// </summary>
    public abstract class FieldProperty : MappingBase, IDocumentProperty
    {
        private const string _TYPE = "type";
        private const string _INDEX_NAME = "index_name";
        private const string _BOOST = "boost";
        private const string _INCLUDE_IN_ALL = "include_in_all";
        private const string _POSTINGS_FORMAT = "postings_format";
        private const string _FIELD_DATA = "fielddata";
        private const string _SIMILARITY = "similarity";
        private const string _DOC_VALUES_FORMAT = "doc_values_format";
        private const string _COPY_TO = "copy_to";
        private const string _NULL_VALUE = "null_value";
        private const string _FIELDS = "fields";


        internal static IndexSettingEnum _INDEX_DEFAULT = IndexSettingEnum.Analyzed;
        internal static bool _STORE_DEFAULT = false;
        internal static PropertyTypeEnum _PROPERTY_TYPE_DEFAULT = PropertyTypeEnum.Object;
        internal const Double _BOOST_DEFAULT = 1.0;
        internal const bool _INCLUDE_IN_ALL_DEFAULT = true;
        internal static DocValuesFormatEnum _DOC_VALUES_FORMAT_DEFAULT = DocValuesFormatEnum.Default;
        internal static SimilarityAlgorithmEnum _SIMILARITY_DEFAULT = SimilarityAlgorithmEnum.Default;
        internal static PostingFormatEnum _POSTINGS_FORMAT_DEFAULT = PostingFormatEnum.Default;

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public PropertyTypeEnum PropertyType { get; private set; }

        /// <summary>
        /// Gets or sets the name of the field that will be stored in the index. 
        /// Defaults to the property/field name.
        /// </summary>
        public string IndexName { get; set; }   

        /// <summary>
        /// Gets or sets the boost value. 
        /// Defaults to 1.0.
        /// </summary>
        public Double Boost { get; set; }

        /// <summary>
        /// Gets or sets whether the field will be included in the _all field (if enabled). If index is set to 
        /// no this defaults to false, otherwise.
        /// Defaults to true or to the parent object type setting.
        /// </summary>
        public bool IncludeInAll { get; set; }

        /// <summary>
        /// Gets or sets posting_format. Posting formats define how fields are written into the index and how fields are represented into memory. 
        /// Defaults to default.
        /// </summary>
        public PostingFormatEnum PostingsFormat { get; set; }

        /// <summary>
        /// Gets or sets the value to control which field values are loaded into memory, which is particularly useful for string fields. 
        /// When specifying the mapping for a field, you can also specify a fielddata filter.
        /// </summary>
        public FieldDataFilter FieldData { get; set; }

        /// <summary>
        /// Gets or sets value used to configure a scoring algorithm per field.
        /// Defaults to default.
        /// </summary>
        public SimilarityAlgorithmEnum Similarity { get; set; }

        /// <summary>
        /// Gets or sets the doc_values_format.
        /// Defaults to default.
        /// </summary>
        public DocValuesFormatEnum DocValuesFormat { get; set; }

        /// <summary>
        /// Gets or sets the field(s) to copy this field to.
        /// </summary>
        public IEnumerable<string> CopyTo { get; set; }

        /// <summary>
        /// Gets the null value for the field property.
        /// </summary>
        public abstract object NullValue { get; set; }

        /// <summary>
        /// Gets or sets the fields. 
        /// </summary>
        public IEnumerable<IDocumentProperty> Fields { get; set; }

        public FieldProperty(string name, PropertyTypeEnum propertyType)
            : base(_INDEX_DEFAULT, _STORE_DEFAULT)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "All document properties require a name.");
            if (propertyType == null)
                throw new ArgumentNullException("propertyType", "All document properties require a property type.");

            Name = name;
            PropertyType = propertyType;
            IndexName = name;
            Boost = _BOOST_DEFAULT;
            IncludeInAll = _INCLUDE_IN_ALL_DEFAULT;
            DocValuesFormat = _DOC_VALUES_FORMAT_DEFAULT;
            Similarity = _SIMILARITY_DEFAULT;
            PostingsFormat = _POSTINGS_FORMAT_DEFAULT;
        }

        internal static void Serialize(FieldProperty field, Dictionary<string, object> fieldDict)
        {
            if (field == null)
                return;

            if (fieldDict == null)
                fieldDict = new Dictionary<string, object>();

            fieldDict.AddObject(_TYPE, field.PropertyType.ToString(), _PROPERTY_TYPE_DEFAULT.ToString());
            MappingBase.Serialize(field, fieldDict);
            fieldDict.AddObject(_INCLUDE_IN_ALL, field.IncludeInAll, _INCLUDE_IN_ALL_DEFAULT);
            fieldDict.AddObject(_BOOST, field.Boost, _BOOST_DEFAULT);
            fieldDict.AddObject(_INDEX_NAME, field.IndexName, field.Name);
            fieldDict.AddObject(_POSTINGS_FORMAT, field.PostingsFormat.ToString(), _POSTINGS_FORMAT_DEFAULT.ToString());
            if (field.FieldData != null)
                fieldDict.AddObject(_FIELD_DATA, field.FieldData.Serialize());
            fieldDict.AddObject(_SIMILARITY, field.Similarity.ToString(), _SIMILARITY_DEFAULT.ToString());
            fieldDict.AddObject(_DOC_VALUES_FORMAT, field.DocValuesFormat.ToString(), _DOC_VALUES_FORMAT_DEFAULT.ToString());

            if (field.CopyTo != null && field.CopyTo.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                int count = field.CopyTo.Count(x => !string.IsNullOrWhiteSpace(x));

                if (count > 1)
                    fieldDict.AddObject(_COPY_TO, field.CopyTo.Where(x => !string.IsNullOrWhiteSpace(x)));
                else
                    fieldDict.AddObject(_COPY_TO, field.CopyTo.First(x => !string.IsNullOrWhiteSpace(x)));
            }

            fieldDict.AddObject(_NULL_VALUE, field.NullValue);
            if (field.Fields != null && field.Fields.Any(x => x != null))
                fieldDict.AddObject(_FIELDS, new DocumentPropertyCollection(field.Fields.Where(x => x != null)));
        }

        internal static void Deserialize(FieldProperty field, Dictionary<string, object> fieldDict)
        {
            MappingBase.Deserialize(field, fieldDict);
            field.IncludeInAll = fieldDict.GetBool(_INCLUDE_IN_ALL, _INCLUDE_IN_ALL_DEFAULT);
            field.Boost = fieldDict.GetDouble(_BOOST, _BOOST_DEFAULT);

            if (fieldDict.ContainsKey(_COPY_TO))
            {
                try 
                {
                    field.CopyTo = JsonConvert.DeserializeObject<IEnumerable<string>>(fieldDict.GetString(_COPY_TO));
                }
                catch
                {
                    field.CopyTo = new List<string>(){ fieldDict.GetString(_COPY_TO) };
                }
            }

            field.DocValuesFormat = DocValuesFormatEnum.Find(fieldDict.GetString(_DOC_VALUES_FORMAT, _DOC_VALUES_FORMAT_DEFAULT.ToString()));

            if(fieldDict.ContainsKey(_FIELD_DATA))
            {
                field.FieldData = FieldDataFilter.Deserialize(JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldDict.GetString(_FIELD_DATA)));
            }
            
            if(fieldDict.ContainsKey(_FIELDS))
            {
                field.Fields = JsonConvert.DeserializeObject<DocumentPropertyCollection>(fieldDict.GetString(_FIELDS));
            }

            field.IndexName = fieldDict.GetString(_INDEX_NAME, field.Name);
            if(fieldDict.ContainsKey(_NULL_VALUE))
                field.NullValue = fieldDict[_NULL_VALUE];

            field.PostingsFormat = PostingFormatEnum.Find(fieldDict.GetString(_POSTINGS_FORMAT, _POSTINGS_FORMAT_DEFAULT.ToString()));
            field.Similarity = SimilarityAlgorithmEnum.Find(fieldDict.GetString(_SIMILARITY, _SIMILARITY_DEFAULT.ToString()));
        }
    }
}
