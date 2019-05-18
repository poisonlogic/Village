using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimcupInstance<TDef> where TDef : IDimcupDef
    {
        string InstanceId { get; }
        string DefName { get; }
        IEnumerable<string> Tags { get; }
        IDimcupProvider<TDef> InstanceProvider { get; }
        IEnumerable<IDimcupUser<TDef>> InstanceUsers { get; }

        IDimcupManager<TDef> GetManager(); 
        bool TrySetManager(IDimcupManager<TDef> manager);

        bool HasUserOpening();
        bool CanAcceptUser(IDimcupUser<TDef> user);
        bool TryAddUser(IDimcupUser<TDef> user);
        bool TryRemoveUser(IDimcupUser<TDef> user);

        bool TrySetProvider(IDimcupProvider<TDef> provider);

    }
}
