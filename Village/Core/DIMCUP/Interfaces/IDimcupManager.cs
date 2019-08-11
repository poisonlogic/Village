using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimManager<TDef> where TDef : IDimDef
    {
        IDimCatalog<TDef> Catalog { get; }
        IEnumerable<IDimInstance<TDef>> AllInstances { get; }
        IEnumerable<IDimUser<TDef>> AllRegisteredUsers { get; }
        IEnumerable<IDimProvider<TDef>> AllRegisteredProviders { get; }

        Type TypeOfInstances { get; }
        Type TypeOfUsers { get; }
        Type TypeOfProviders { get; }

        bool InstanceIsOfType(IDimInstance<IDimDef> instance);
        bool UserIsOfType(IDimUser<IDimDef> user);
        bool ProviderIsOfType(IDimProvider<IDimDef> provider);

        bool TryRegisterUser(IDimUser<TDef> user);
        bool TryRegisterProvider(IDimProvider<TDef> provider);
        bool TryRegisterNewInstance(IDimInstance<TDef> instance);

        bool TryDestroyInstance(IDimInstance<TDef> instance);
        bool TryTransferInstance(IDimInstance<TDef> instance);
        bool TryUnregisterUser(IDimUser<TDef> user);
        bool TryUnregisterProvider(IDimProvider<TDef> provider);

        void InformOfInstanceChange(IDimInstance<TDef> instance);
        void InformOfUserChange(IDimUser<TDef> user);
        void InformOfProviderChange(IDimProvider<TDef> provider);

    }
}
