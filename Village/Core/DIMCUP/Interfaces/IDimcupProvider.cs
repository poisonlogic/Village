using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimcupProvider<TDef> where TDef : IDimcupDef
    {
        string InstanceId { get; }
        IEnumerable<string> Tags { get; }
        IEnumerable<string> ProvidedDefIds { get; }
        IEnumerable<string> ProvidingInstanceIds { get; }
        IEnumerable<IDimcupInstance<TDef>> ProvidingInstances { get; }


        IDimcupManager<TDef> GetManager();
        bool TrySetManager(IDimcupManager<TDef> manager);
        //bool HasNewInstances(out IEnumerable<IDimcupInstance<TDef>> newInstances);
        bool InformManagerOfChange();
        bool InformManagerOfNewInstances();
        bool TryUnregisterInstance(IDimcupInstance<TDef> instance);
    }
}
