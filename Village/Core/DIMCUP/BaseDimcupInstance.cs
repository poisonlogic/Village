using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimcupInstance<TDef> : IDimcupInstance<TDef> where TDef : IDimcupDef
    {
        protected IDimcupProvider<TDef> _provider;
        protected IDimcupManager<TDef> _manager;
        protected Dictionary<string, IDimcupUser<TDef>> _users;

        public virtual string InstanceId { get; }
        public virtual TDef Def { get ;}
        public virtual string DefName { get; }
        public virtual IEnumerable<string> Tags { get; }
        public virtual IDimcupProvider<TDef> InstanceProvider { get; }
        public virtual IEnumerable<IDimcupUser<TDef>> InstanceUsers { get; }
        public virtual bool IsContinuous { get; }

        public BaseDimcupInstance(IDimcupProvider<TDef> provider, IDimcupManager<TDef> manager, TDef def)
        {
            this.Def = def;
            this.InstanceId = Guid.NewGuid().ToString();
            this._provider = provider;
            this._manager = manager;
            this._users = new Dictionary<string, IDimcupUser<TDef>>();
        }

        public virtual IDimcupManager<TDef> GetManager()
        {
            return _manager;
        }

        public virtual bool TrySetManager(IDimcupManager<TDef> manager)
        {
            _manager = manager;
            return true;
        }


        public virtual bool HasUserOpening()
        {
            throw new NotImplementedException();
        }

        public virtual bool CanAcceptUser(IDimcupUser<TDef> user)
        {
            throw new NotImplementedException();
        }

        public virtual bool TryAddUser(IDimcupUser<TDef> user)
        {
            if (!this._users.ContainsKey(user.InstanceId))
                this._users.Add(user.InstanceId, user);
            return true;
        }

        public virtual bool TryRemoveUser(IDimcupUser<TDef> user)
        {
            if (this._users.ContainsKey(user.InstanceId))
                this._users.Remove(user.InstanceId);
            return true;
        }

        public bool TrySetProvider(IDimcupProvider<TDef> provider)
        {
            this._provider = provider;
            return true;
        }
    }
}
