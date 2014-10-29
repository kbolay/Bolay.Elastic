using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk.Models
{
    public class BulkWrapperConfiguration
    {
        private const int _DEFAULT_BATCH_SIZE = 250;
        private const int _DEFAULT_MAX_CONCURRENT_HTTP_REQUESTS = 10;
        private const int _DEFAULT_REQUEST_QUEUE_MAX_SIZE = 100;
        private const int _DEFAULT_ACTION_QUEUE_MAX_SIZE = 50000;

        public readonly Uri ClusterUri;
        public readonly int BatchSize;
        public readonly int MaxConcurrentHttpRequests;
        public readonly int MaxRequestQueueSize;
        public readonly int MaxActionQueueSize;

        public BulkWrapperConfiguration(
            int batchSize = _DEFAULT_BATCH_SIZE,
            int maxConcurrentHttpRequests = _DEFAULT_MAX_CONCURRENT_HTTP_REQUESTS,
            int maxRequestsQueueSize = _DEFAULT_REQUEST_QUEUE_MAX_SIZE,
            int maxActionsQueueSize = _DEFAULT_ACTION_QUEUE_MAX_SIZE)
        {
            if (batchSize <= 0)
            {
                throw new ArgumentOutOfRangeException("batchSize", "batchSize must be greater than zero.");
            }

            if (maxConcurrentHttpRequests <= 0)
            {
                throw new ArgumentOutOfRangeException("maxConcurrentHttpRequests", "maxConcurrentHttpRequests must be greater than zero.");
            }

            if (maxRequestsQueueSize <= 0)
            {
                throw new ArgumentOutOfRangeException("maxRequestsQueueSize", "maxRequestsQueueSize must be greater than zero.");
            }

            if (maxActionsQueueSize <= 0)
            {
                throw new ArgumentOutOfRangeException("maxActionsQueueSize", "maxActionsQueueSize must be greater than zero.");
            }

            BatchSize = batchSize;
            MaxConcurrentHttpRequests = maxConcurrentHttpRequests;
            MaxRequestQueueSize = maxRequestsQueueSize;
            MaxActionQueueSize = maxActionsQueueSize;
        }
    }
}
