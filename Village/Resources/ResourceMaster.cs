using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Resources
{
    public class ResourceMaster
    {
        private List<IProducer> _producers;
        private List<IConsumer> _consumers;

        private Dictionary<string, ResourceDetails> _resources;

        private List<ResourceRequest> _pendingResourceRequests;

        private class ResourceDetails
        {
            public string ResourceName;
            public int StoredValue;
            public int MaxStoreValue;
            public Resource Resource { get { return ResourceCatalog.All[ResourceName]; } }
        }

        public IEnumerable<IProducer> Producers { get { return _producers; } }
        public IEnumerable<IConsumer> Consumers { get { return _consumers; } }

        public ResourceMaster()
        {
            _producers = new List<IProducer>();
            _consumers = new List<IConsumer>();
            _resources = new Dictionary<string, ResourceDetails>();
        }

        public bool TryRegisterNewResource(string resName)
        {
            if(_resources.ContainsKey(resName))
                return false;

            _resources.Add(resName, new ResourceDetails
            {
                ResourceName = resName,
                StoredValue = 0,
                MaxStoreValue = ResourceCatalog.All[resName].BaseLimit
            });

            return true;
        }

        public bool TryRegiseringProducer(IProducer producer, bool throwExceptions = false)
        {
            if (_producers.Contains(producer))
            {
                if (throwExceptions) throw new Exception("Attempted to register dupicate producers");
                return false;
            }

            var unregisteredResources = producer.AllProducedResources.Where(x => !_resources.ContainsKey(x));
            if (unregisteredResources.Any())
                foreach (var unres in unregisteredResources)
                    TryRegisterNewResource(unres);
            
            _producers.Add(producer);

            return true;
        }

        public bool TryRegiseringConsumer(IConsumer consumer, bool throwExceptions = false)
        {
            if (_consumers.Contains(consumer))
            {
                if (throwExceptions) throw new Exception("Attempted to register dupicate consumers");
                return false;
            }

            var unregisteredResources = consumer.AllConsumedResources.Where(x => !_resources.ContainsKey(x));
            if (unregisteredResources.Any())
                foreach (var unres in unregisteredResources)
                    TryRegisterNewResource(unres);

            _consumers.Add(consumer);

            return true;
        }

        public bool CanFullFillRequest(ResourceRequest request)
        {
            switch(request.Type)
            {
                case ResourceRequestType.Produced:
                    foreach (var res in request.Exchanges)
                        if (_resources[res.Key].StoredValue + res.Value > _resources[res.Key].MaxStoreValue)
                            return false;
                    break;

                case ResourceRequestType.Consume:
                    foreach (var res in request.Exchanges)
                        if (_resources[res.Key].StoredValue < res.Value)
                            return false;
                    break;
            }
            return true;
        }

        private void DoRequest(ResourceRequest request)
        {
            foreach(var exchange in request.Exchanges)
            {
                if (!_resources.ContainsKey(exchange.Key))
                    TryRegisterNewResource(exchange.Key);

                _resources[exchange.Key].StoredValue += exchange.Value;
            }
        }

        public bool SubitRequest(ResourceRequest request)
        {
            if (CanFullFillRequest(request))
            {
                DoRequest(request);
                return true;
            }
            else
                return false;
        }

        private void RegisterPendingRequest(ResourceRequest request)
        {
            if (_pendingResourceRequests == null)
                _pendingResourceRequests = new List<ResourceRequest>();
            _pendingResourceRequests.Add(request);
        }
    }
}
