using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimProvider<TDef> where TDef : IDimDef
    {
        string InstanceId { get; }
        IEnumerable<string> Tags { get; }
        IEnumerable<string> ProvidedDefIds { get; }
        IEnumerable<string> ProvidingInstanceIds { get; }
        IEnumerable<IDimInstance<TDef>> ProvidingInstances { get; }


        IDimManager<TDef> GetManager();
        bool TrySetManager(IDimManager<TDef> manager);
        //bool HasNewInstances(out IEnumerable<IDimInstance<TDef>> newInstances);
        bool InformManagerOfChange();
        bool InformManagerOfNewInstances();
        bool TryUnregisterInstance(IDimInstance<TDef> instance);
    }
}
