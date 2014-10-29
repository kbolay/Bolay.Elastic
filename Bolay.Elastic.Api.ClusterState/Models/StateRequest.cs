using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class StateRequest
    {
        /// <summary>
        /// Retrieve the cluster state local to a particular node.
        /// </summary>
        public bool Local { get; set; }

        /// <summary>
        /// Set to true to filter out the nodes part of the response.
        /// </summary>
        public bool FilterNodes { get; set; }
        
        /// <summary>
        /// Set to true to filter out the Routing Table Indices and Routing Nodes part of the response.
        /// </summary>
        public bool FilterRouting { get; set; }

        /// <summary>
        /// Set to true to filter out the metadata part of the response.
        /// </summary>
        public bool FilterMetaData { get; set; }

        /// <summary>
        /// Set to true to filter out the blocks part of the response.
        /// </summary>
        public bool FilterBlocks { get; set; }

        /// <summary>
        /// When not filtering metadata, setting this to true filters out the metadata indices.
        /// </summary>
        public bool FilterIndices { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (Local) { builder = HttpRequest.AddToQueryString(builder, "local"); }
            if (FilterBlocks) { builder = HttpRequest.AddToQueryString(builder, "filter_blocks"); }
            if (FilterIndices) { builder = HttpRequest.AddToQueryString(builder, "filter_indices"); }
            if (FilterMetaData) { builder = HttpRequest.AddToQueryString(builder, "filter_metadata"); }
            if (FilterNodes) { builder = HttpRequest.AddToQueryString(builder, "filter_nodes"); }
            if (FilterRouting) { builder = HttpRequest.AddToQueryString(builder, "filter_routing_table"); }

            if (builder.Length == 0)
                return null;

            return builder.ToString();
        }        
    }
}
