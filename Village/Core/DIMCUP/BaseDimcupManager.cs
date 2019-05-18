using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public abstract class BaseDimcupManager<TDef> : IDimcupManager<TDef> where TDef : IDimcupDef
    {
        protected Dictionary<string, IDimcupInstance<TDef>> _instances;
        protected Dictionary<string, IDimcupUser<TDef>> _registeredUsers;
        protected Dictionary<string, IDimcupProvider<TDef>> _registeredProviders;

        public IDimcupCatalog<TDef> Catalog { get; }
        public IEnumerable<IDimcupInstance<TDef>> AllInstances => _instances.Select(x => x.Value); 
        public IEnumerable<IDimcupUser<TDef>> AllRegisteredUsers => _registeredUsers.Select(x => x.Value);
        public IEnumerable<IDimcupProvider<TDef>> AllRegisteredProviders => _registeredProviders.Select(x => x.Value);

        public BaseDimcupManager()
        {
            _instances = new Dictionary<string, IDimcupInstance<TDef>>();
            _registeredUsers = new Dictionary<string, IDimcupUser<TDef>>();
            _registeredProviders = new Dictionary<string, IDimcupProvider<TDef>>();
        }

        public virtual bool TryRegisterNewInstance(IDimcupInstance<TDef> instance)
        {
            if (_instances.ContainsKey(instance.InstanceId))
                return false;
            else
            {
                _instances.Add(instance.InstanceId, instance);
                return instance.TrySetManager(this);
            }
        }

        public virtual bool TryRegisterProvider(IDimcupProvider<TDef> provider)
        {
            if (_registeredProviders.ContainsKey(provider.InstanceId))
                return false;
            else
            {
                _registeredProviders.Add(provider.InstanceId, provider);
                return provider.TrySetManager(this);
            }
        }

        public virtual bool TryUnregisterProvider(IDimcupProvider<TDef> provider)
        {
            if (!_registeredProviders.ContainsKey(provider.InstanceId))
                return false;
            _registeredProviders.Remove(provider.InstanceId);
            return true;
        }

        public virtual bool TryRegisterUser(IDimcupUser<TDef> user)
        {
            if (_registeredUsers.ContainsKey(user.InstanceId))
                return false;
            else
            {
                _registeredUsers.Add(user.InstanceId, user);
                return user.TrySetManager(this);
            }
        }

        public virtual bool TryUnregisterUser(IDimcupUser<TDef> user)
        {
            if (!_registeredUsers.ContainsKey(user.InstanceId))
                return false;
            _registeredUsers.Remove(user.InstanceId);
            return true;

        }

        public virtual bool DisposeOfInstance(string instanceId)
        {
            if (!_instances.ContainsKey(instanceId))
                return false;

            var instance = _instances[instanceId];
            instance.InstanceProvider.TryUnregisterInstance(instance);

            foreach (var user in instance.InstanceUsers)
                user.TryUnAssignFromInstance(instance);

            return true;

        }

        public virtual bool TryDestroyInstance(IDimcupInstance<TDef> instance)
        {
            if (!_instances.ContainsKey(instance.InstanceId))
                return false;
            _instances.Remove(instance.InstanceId);
            return true;
        }

        public abstract bool TryTransferInstance(IDimcupInstance<TDef> instance);
        public abstract void InformOfInstanceChange(IDimcupInstance<TDef> instance);
        public abstract void InformOfUserChange(IDimcupUser<TDef> instance);
        public abstract void InformOfProviderChange(IDimcupProvider<TDef> instance);
    }
}
