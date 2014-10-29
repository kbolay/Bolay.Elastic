using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2ElasticSearch
{
    public abstract class DocumentType<T> where T : class
    {
        private readonly IEnumerable<string> Indices;
        private readonly string Name;

        public QueryableCollection<T> Collection { get; private set; }

        protected DocumentType(IEnumerable<string> indices, string name, QueryableCollection<T> collection)
        {
            Indices = indices;
            Name = name;
            Collection = collection;
        }


    }
}
