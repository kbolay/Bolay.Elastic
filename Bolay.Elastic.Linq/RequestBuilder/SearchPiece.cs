using Bolay.Elastic.Mapping.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public class SearchPiece : ISearchPiece
    {
        // TODO: get later...
        public PropertyInfo ObjectProperty
        {
            get { throw new NotImplementedException(); }
        }

        public DocumentPropertyBase MappingProperty { get; private set; }
        public SearchType SearchType { get; private set; }
        public object Value { get; private set; }        

        public SearchPiece(DocumentPropertyBase mappingProperty, SearchType searchType, object value)
        {
            MappingProperty = mappingProperty;
            SearchType = searchType;
            Value = value;
        }

        public string BuildPiece()
        {
            throw new NotImplementedException();
        }
    }
}
