using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimcupProvider<TDef> : IDimcupProvider<TDef> where TDef : BaseDimcupDef
    {
        protected IDimcupManager<TDef> _manager;
        protected Dictionary<string, IDimcupInstance<TDef>> _providedInstances;

        public string InstanceId { get; }
        public IEnumerable<string> Tags { get; }
        public IEnumerable<string> ProvidedDefIds { get; }
        public IEnumerable<IDimcupInstance<TDef>> ProvidingInstances { get; }
        public IEnumerable<string> ProvidingInstanceIds { get; }
        
        public virtual IDimcupManager<TDef> GetManager()
        {
            return _manager;
        }

        public virtual bool InformManagerOfChange() { return true; }
        public virtual bool InformManagerOfNewInstances() { return true; }
        public virtual bool TrySetManager(IDimcupManager<TDef> manager) { return true; }
        public virtual bool TryUnregisterInstance(IDimcupInstance<TDef> instance)
        {
            if (!_providedInstances.ContainsKey(instance.InstanceId))
                return false;
            else
            {
                _providedInstances.Remove(instance.InstanceId);
                return true;
            }
        }
    }
}
