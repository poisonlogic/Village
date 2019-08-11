using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public abstract class BaseDimManager<TDef> : IDimManager<TDef> where TDef : IDimDef
    {
        protected Dictionary<string, IDimInstance<TDef>> _instances;
        protected Dictionary<string, IDimUser<TDef>> _registeredUsers;
        protected Dictionary<string, IDimProvider<TDef>> _registeredProviders;

        public abstract Type TypeOfInstances { get; }
        public abstract Type TypeOfUsers { get; }
        public abstract Type TypeOfProviders { get; }

        public IDimCatalog<TDef> Catalog { get; }
        public IEnumerable<IDimInstance<TDef>> AllInstances => _instances.Select(x => x.Value); 
        public IEnumerable<IDimUser<TDef>> AllRegisteredUsers => _registeredUsers.Select(x => x.Value);
        public IEnumerable<IDimProvider<TDef>> AllRegisteredProviders => _registeredProviders.Select(x => x.Value);

        public BaseDimManager()
        {
            _instances = new Dictionary<string, IDimInstance<TDef>>();
            _registeredUsers = new Dictionary<string, IDimUser<TDef>>();
            _registeredProviders = new Dictionary<string, IDimProvider<TDef>>();
        }

        public virtual bool TryRegisterNewInstance(IDimInstance<TDef> instance)
        {
            if (_instances.ContainsKey(instance.InstanceId))
                return false;
            else
            {
                _instances.Add(instance.InstanceId, instance);
                return instance.TrySetManager(this);
            }
        }

        public virtual bool TryRegisterProvider(IDimProvider<TDef> provider)
        {
            if (_registeredProviders.ContainsKey(provider.InstanceId))
                return false;
            else
            {
                _registeredProviders.Add(provider.InstanceId, provider);
                return provider.TrySetManager(this);
            }
        }

        public virtual bool TryUnregisterProvider(IDimProvider<TDef> provider)
        {
            if (!_registeredProviders.ContainsKey(provider.InstanceId))
                return false;
            _registeredProviders.Remove(provider.InstanceId);
            return true;
        }

        public virtual bool TryRegisterUser(IDimUser<TDef> user)
        {
            if (_registeredUsers.ContainsKey(user.InstanceId))
                return false;
            else
            {
                _registeredUsers.Add(user.InstanceId, user);
                return user.TrySetManager(this);
            }
        }

        public virtual bool TryUnregisterUser(IDimUser<TDef> user)
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

        public virtual bool TryDestroyInstance(IDimInstance<TDef> instance)
        {
            if (!_instances.ContainsKey(instance.InstanceId))
                return false;
            _instances.Remove(instance.InstanceId);
            return true;
        }

        public abstract bool TryTransferInstance(IDimInstance<TDef> instance);
        public abstract void InformOfInstanceChange(IDimInstance<TDef> instance);
        public abstract void InformOfUserChange(IDimUser<TDef> instance);
        public abstract void InformOfProviderChange(IDimProvider<TDef> instance);

        public bool InstanceIsOfType(IDimInstance<IDimDef> instance)
        {
            return instance.GetType().IsSubclassOf(TypeOfInstances) || TypeOfInstances == instance.GetType();
        }

        public bool UserIsOfType(IDimUser<IDimDef> user)
        {
            return user.GetType().IsSubclassOf(TypeOfUsers) || TypeOfUsers == user.GetType();
        }

        public bool ProviderIsOfType(IDimProvider<IDimDef> provider)
        {
            throw new NotImplementedException();
        }
    }
}
