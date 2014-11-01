using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.ClusterState.Models
{
    public class StateRequest
    {
        internal const string LOCAL = "local";
        internal const string FILTER_BLOCKS = "filter_blocks";
        internal const string FILTER_INDICES = "filter_indices";
        internal const string FILTER_METADATA = "filter_metadata";
        internal const string FILTER_NODES = "filter_nodes";
        internal const string FILTER_ROUTING_TABLE = "filter_routing_table";

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
    }
}
