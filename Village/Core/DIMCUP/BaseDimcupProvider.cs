using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimProvider<TDef> : IDimProvider<TDef> where TDef : BaseDimDef
    {
        protected IDimManager<TDef> _manager;
        protected Dictionary<string, IDimInstance<TDef>> _providedInstances;

        public string InstanceId { get; }
        public IEnumerable<string> Tags { get; }
        public IEnumerable<string> ProvidedDefIds { get; }
        public IEnumerable<IDimInstance<TDef>> ProvidingInstances { get; }
        public IEnumerable<string> ProvidingInstanceIds { get; }
        
        public virtual IDimManager<TDef> GetManager()
        {
            return _manager;
        }

        public virtual bool InformManagerOfChange() { return true; }
        public virtual bool InformManagerOfNewInstances() { return true; }
        public virtual bool TrySetManager(IDimManager<TDef> manager) { return true; }
        public virtual bool TryUnregisterInstance(IDimInstance<TDef> instance)
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
