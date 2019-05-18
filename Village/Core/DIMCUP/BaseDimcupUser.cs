using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimcupUser<TDef> : IDimcupUser<TDef> where TDef : IDimcupDef
    {
        private IDimcupManager<TDef> _manager;
        public string InstanceId { get; }
        public IEnumerable<string> Tags { get; }
        public IDimcupInstance<TDef> UsingInstance { get; private set; }

        public IDimcupManager<TDef> GetManager()
        {
            return _manager;
        }
        public bool TrySetManager(IDimcupManager<TDef> manager)
        {
            this._manager = manager;
            return true;
        }
        public virtual bool InformManagerOfChange() { return true; }


        public bool TryAssignToInstance(IDimcupInstance<TDef> instance)
        {
            this.UsingInstance = instance;
            return true;
        }

        public bool TryUnAssignFromInstance(IDimcupInstance<TDef> instance)
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
