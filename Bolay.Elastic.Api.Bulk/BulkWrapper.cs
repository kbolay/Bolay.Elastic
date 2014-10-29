using Bolay.Elastic.Api.Bulk.Models;
using Bolay.Elastic.Api.Bulk.Request;
using Bolay.Elastic.Api.Bulk.Response;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolay.Elastic.Api.Bulk
{
    public class BulkWrapper : IDisposable
    {
        private static readonly TimeSpan _UNLIMITED_TIMESPAN = TimeSpan.FromMilliseconds(-1);

        private BulkWrapperConfiguration _config;
        private IBulkRepository _bulkRepository;
        private BlockingCollection<BulkRequest> _bulkRequestsQueue;
        private BlockingCollection<BulkActionBase> _bulkActionsQueue;
        private ConcurrentBag<BulkActionFailure> _failures;

        private Task _dequeueActionsTask;
        private List<Task> _sendRequestTasks;

        public IEnumerable<BulkActionFailure> Failures 
        {
            get { return _failures; }
        }

        public BulkWrapper(IBulkRepository bulkRepository, BulkWrapperConfiguration configuration)
        {
            if (bulkRepository == null)
            {
                throw new Exception("bulkRepository");
            }

            if (configuration == null) 
            {
                throw new ArgumentNullException("configuration");
            }

            _config = configuration;
            _bulkRepository = bulkRepository;

            _bulkActionsQueue = new BlockingCollection<BulkActionBase>(_config.MaxActionQueueSize);
            _bulkRequestsQueue = new BlockingCollection<BulkRequest>(_config.MaxRequestQueueSize);
            _failures = new ConcurrentBag<BulkActionFailure>();
        }

        public void AddBulkAction(BulkActionBase action, TimeSpan timeout = default(TimeSpan))
        {
            AddToActionQueue(action, timeout);
        }

        public async Task AddBulkActionAsync(BulkActionBase action, TimeSpan timeout = default(TimeSpan))
        {
            AddToActionQueue(action, timeout);
        }

        public void AddBulkActions(IEnumerable<BulkActionBase> actions, TimeSpan timeout = default(TimeSpan))
        { 
            AddRangeToActionQueue(actions, timeout);
        }

        public async Task AddBulkActionsAsync(IEnumerable<BulkActionBase> actions, TimeSpan timeout = default(TimeSpan))
        {
            AddRangeToActionQueue(actions, timeout);
        }

        public void ClearFailures()
        {
            _failures = new ConcurrentBag<BulkActionFailure>();
        }

        public void Finish(TimeSpan timeout = default(TimeSpan))
        {
            _bulkActionsQueue.CompleteAdding();
            _dequeueActionsTask.Wait();

            _bulkRequestsQueue.CompleteAdding();

            if(timeout != default(TimeSpan))
            {
                if(!Task.WaitAll(_sendRequestTasks.ToArray(), timeout))
                {
                    throw new TimeoutException("Failed to finish all bulk requests before timeout.");
                }
            }
            else
            {
                Task.WaitAll(_sendRequestTasks.ToArray());
            }            
        }

        public void Dispose()
        {
            Finish();
        }

        private void StartProducerConsumerPattern()
        {
            _dequeueActionsTask = Task.Factory.StartNew(() => 
                { 
                    CreateBulkRequests().Wait(); 
                });

            _sendRequestTasks = new List<Task>();
            for(int i = 0; i < _config.MaxConcurrentHttpRequests; i++)
            {
                _sendRequestTasks.Add(Task.Factory.StartNew(() =>
                    {
                        SendRequests().Wait();
                    }));
            }
        }

        private void AddToActionQueue(BulkActionBase action, TimeSpan timeout)
        {
            if (timeout == default(TimeSpan))
            {
                timeout = _UNLIMITED_TIMESPAN;
            }

            if (!_bulkActionsQueue.TryAdd(action, timeout))
            {
                throw new TimeoutException("Failed to add action to the queue.");
            }
        }

        private void AddRangeToActionQueue(IEnumerable<BulkActionBase> actions, TimeSpan timeout)
        {
            bool keepTime = false;
            TimeSpan calculatedTimeout = _UNLIMITED_TIMESPAN;

            if(timeout != _UNLIMITED_TIMESPAN && timeout != default(TimeSpan))
            {
                keepTime = true;
                calculatedTimeout = timeout;
            }

            Stopwatch timer = new Stopwatch();
            foreach(BulkActionBase action in actions)
            {
                if(keepTime)
                {
                    timer.Start();
                    calculatedTimeout = timeout - timer.Elapsed;

                    if(calculatedTimeout.TotalMilliseconds <= 0)
                    {
                        throw new TimeoutException("Failed to add actions to the queue within the timeout.");
                    }
                }

                AddToActionQueue(action, calculatedTimeout);
            }
        }
        
        private async Task CreateBulkRequests()
        {
            List<BulkActionBase> actions = new List<BulkActionBase>();
            foreach(BulkActionBase action in _bulkActionsQueue.GetConsumingEnumerable())
            {
                actions.Add(action);

                if(actions.Count == _config.BatchSize)
                {
                    _bulkRequestsQueue.Add(new BulkRequest(actions.Take(_config.BatchSize)));
                    actions.Clear();
                }
            }

            if(_bulkActionsQueue.IsAddingCompleted && actions.Any())
            {
                _bulkRequestsQueue.Add(new BulkRequest(actions));
            }
        }

        private async Task SendRequests()
        {
            foreach(BulkRequest request in _bulkRequestsQueue.GetConsumingEnumerable())
            {
                BulkResponse response = _bulkRepository.DoBulkRequest(request);
                if(response != null && response.HadErrors)
                {
                    foreach(ActionResponse actionResponse in response.Items.Where(x => !x.Succesful))
                    {
                        BulkActionBase action = request.Actions.FirstOrDefault(x => 
                            x.Action.Equals(actionResponse.Action, StringComparison.OrdinalIgnoreCase) &&
                            x.Index.Equals(actionResponse.Index, StringComparison.OrdinalIgnoreCase) &&
                            x.Type.Equals(actionResponse.Type, StringComparison.OrdinalIgnoreCase) &&
                            x.DocumentId.Equals(actionResponse.DocumentId, StringComparison.OrdinalIgnoreCase));

                        _failures.Add(new BulkActionFailure(action, actionResponse));
                    }
                }
            }
        }
    }
}
