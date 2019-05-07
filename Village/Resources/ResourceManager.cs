using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Resources
{
    public class ResourceManager
    {
        private Dictionary<string, IResourceUser> _resourceUsers;
        private Dictionary<string, ResourceDetails> _resources;

        private List<ResourceRequest> _pendingResourceRequests;

        private class ResourceDetails
        {
            public string ResourceName;
            public int StoredValue;
            public int MaxStoreValue;
            public ResourceDef Resource { get { return ResourceCatalog.All[ResourceName]; } }
        }
        
        public ResourceManager()
        {
            _resources = new Dictionary<string, ResourceDetails>();
            _resourceUsers = new Dictionary<string, IResourceUser>();
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

        public bool TryRegiseringUser(IResourceUser user, bool throwExceptions = false)
        {
            if (_resourceUsers.ContainsKey(user.InstanceId))
            {
                if (throwExceptions) throw new Exception("Attempted to register dupicate producers");
                return false;
            }

            var unregisteredResources = user.AllResourceIds.Where(x => !_resources.ContainsKey(x));
            if (unregisteredResources.Any())
                foreach (var unres in unregisteredResources)
                    TryRegisterNewResource(unres);
            
            _resourceUsers.Add(user.InstanceId, user);

            return true;
        }
        
        public bool CanFullFillRequest(ResourceRequest request)
        {
            //switch(request.Type)
            //{
            //    case ResourceRequestType.Produced:
            //        foreach (var res in request.Exchanges)
            //            if (_resources[res.Key].StoredValue + res.Value > _resources[res.Key].MaxStoreValue)
            //                return false;
            //        break;

            //    case ResourceRequestType.Consume:
            //        foreach (var res in request.Exchanges)
            //            if (_resources[res.Key].StoredValue < res.Value)
            //                return false;
            //        break;
            //}
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
