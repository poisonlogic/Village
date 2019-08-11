using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonLogic.Dim
{
    public interface IDimUser
    {
        string InstanceId { get; }
        void InformOfChange<TMan>() where TMan : IDimManager;
        IDimManager GetManager();

        //IDimManager<>

        //TInst LinkedInstance { get; }
        //IDimManager<TDef> LinkedManager { get; }

        //string LinkId { get; }
        //IEnumerable<string> LinkTags { get; }


        //IDimManager<TDef> GetManager();
        //bool TrySetManager(IDimManager<TDef> manager);
        ////bool HasNewInstances(out IEnumerable<IDimInstance<TDef>> newInstances);
        //bool InformManagerOfChange();
        //bool InformManagerOfNewInstances();
        //bool TryUnregisterInstance(IDimInstance<TDef> instance);
    }
}
