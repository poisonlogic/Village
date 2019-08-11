using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimUser<TDef> : IDimUser<TDef> where TDef : IDimDef
    {
        private IDimManager<TDef> _manager;
        public string InstanceId { get; }
        public IEnumerable<string> Tags { get; }
        public IDimInstance<TDef> UsingInstance { get; private set; }

        public IDimManager<TDef> GetManager()
        {
            return _manager;
        }
        public bool TrySetManager(IDimManager<TDef> manager)
        {
            this._manager = manager;
            return true;
        }
        public virtual bool InformManagerOfChange() { return true; }


        public bool TryAssignToInstance(IDimInstance<TDef> instance)
        {
            this.UsingInstance = instance;
            return true;
        }

        public bool TryUnAssignFromInstance(IDimInstance<TDef> instance)
        {
            if (this.UsingInstance.InstanceId == instance.InstanceId)
            {
                this.UsingInstance = null;
                return true;
            }
            return false;
        }
    }
}
