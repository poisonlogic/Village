using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimInstance<TDef> : IDimInstance<TDef> where TDef : IDimDef
    {
        protected IDimProvider<TDef> _provider;
        protected IDimManager<TDef> _manager;
        protected Dictionary<string, IDimUser<TDef>> _users;

        public virtual string InstanceId { get; }
        public virtual TDef Def { get ;}
        public virtual string DefName { get; }
        public virtual IEnumerable<string> Tags { get; }
        public virtual IDimProvider<TDef> InstanceProvider { get; }
        public virtual IEnumerable<IDimUser<TDef>> InstanceUsers { get; }
        public virtual bool IsContinuous { get; }

        public BaseDimInstance(IDimProvider<TDef> provider, IDimManager<TDef> manager, TDef def)
        {
            this.Def = def;
            this.InstanceId = Guid.NewGuid().ToString();
            this._provider = provider;
            this._manager = manager;
            this._users = new Dictionary<string, IDimUser<TDef>>();
        }

        public virtual IDimManager<TDef> GetManager()
        {
            return _manager;
        }

        public virtual bool TrySetManager(IDimManager<TDef> manager)
        {
            _manager = manager;
            return true;
        }


        public virtual bool HasUserOpening()
        {
            throw new NotImplementedException();
        }

        public virtual bool CanAcceptUser(IDimUser<TDef> user)
        {
            throw new NotImplementedException();
        }

        public virtual bool TryAddUser(IDimUser<TDef> user)
        {
            if (!this._users.ContainsKey(user.InstanceId))
                this._users.Add(user.InstanceId, user);
            return true;
        }

        public virtual bool TryRemoveUser(IDimUser<TDef> user)
        {
            if (this._users.ContainsKey(user.InstanceId))
                this._users.Remove(user.InstanceId);
            return true;
        }

        public bool TrySetProvider(IDimProvider<TDef> provider)
        {
            this._provider = provider;
            return true;
        }
    }
}
