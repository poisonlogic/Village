using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimInstance<TDef> where TDef : IDimDef
    {
        string InstanceId { get; }
        string DefName { get; }
        IEnumerable<string> Tags { get; }
        IDimProvider<TDef> InstanceProvider { get; }
        IEnumerable<IDimUser<TDef>> InstanceUsers { get; }

        IDimManager<TDef> GetManager(); 
        bool TrySetManager(IDimManager<TDef> manager);

        bool HasUserOpening();
        bool CanAcceptUser(IDimUser<TDef> user);
        bool TryAddUser(IDimUser<TDef> user);
        bool TryRemoveUser(IDimUser<TDef> user);

        bool TrySetProvider(IDimProvider<TDef> provider);

    }
}
