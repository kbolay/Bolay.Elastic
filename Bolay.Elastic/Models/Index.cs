using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Models
{
    public class Index
    {
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public List<string> Aliases { get; set; }
    }
}
