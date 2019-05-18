using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimcupUser<TDef> where TDef : IDimcupDef
    {
        string InstanceId { get; }
        IEnumerable<string> Tags { get; }
        IDimcupInstance<TDef> UsingInstance { get; }

        IDimcupManager<TDef> GetManager();
        bool TrySetManager(IDimcupManager<TDef> manager);
        bool InformManagerOfChange();

        bool TryAssignToInstance(IDimcupInstance<TDef> instance);
        bool TryUnAssignFromInstance(IDimcupInstance<TDef> instance);
    }
}
