using Bolay.Elastic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _ConnectionString { get; set; }
        public string ConnectionString { get { return _ConnectionString; } }

        public ConnectionStringProvider(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            _ConnectionString = connectionString;
        }
    }
}
