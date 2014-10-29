using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Linq.RequestBuilder
{
    public class SearchTreeNode
    {
        public LinkType Link { get; set; }
        public SearchTreeNode LeftNode { get; set; }
        public SearchTreeNode RightNode { get; set; }
        public ISearchPiece LeftPiece { get; set; }
        public ISearchPiece RightPiece { get; set; }
    }
}
