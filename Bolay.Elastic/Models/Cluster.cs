using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Models
{
    public class Cluster
    {
        public string Name { get; set; }
        public Uri Uri { get; set; }
        public List<Index> Indexes { get; set; }

        public Cluster(string name, string uri) : this(name, new Uri(uri)) { }
        public Cluster(string name, Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri", "Elastic cluster requires a uri.");

            if (!string.IsNullOrWhiteSpace(name))
                this.Name = name;
            else
                this.Name = uri.ToString();

            this.Uri = uri;
        }


    }
}
