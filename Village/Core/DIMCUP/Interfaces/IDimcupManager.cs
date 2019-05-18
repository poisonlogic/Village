using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimcupManager<TDef> where TDef : IDimcupDef
    {
        IDimcupCatalog<TDef> Catalog { get; }
        IEnumerable<IDimcupInstance<TDef>> AllInstances { get; }
        IEnumerable<IDimcupUser<TDef>> AllRegisteredUsers { get; }
        IEnumerable<IDimcupProvider<TDef>> AllRegisteredProviders { get; }

        bool TryRegisterUser(IDimcupUser<TDef> user);
        bool TryRegisterProvider(IDimcupProvider<TDef> provider);
        bool TryRegisterNewInstance(IDimcupInstance<TDef> instance);

        bool TryDestroyInstance(IDimcupInstance<TDef> instance);
        bool TryTransferInstance(IDimcupInstance<TDef> instance);
        bool TryUnregisterUser(IDimcupUser<TDef> user);
        bool TryUnregisterProvider(IDimcupProvider<TDef> provider);

        void InformOfInstanceChange(IDimcupInstance<TDef> instance);
        void InformOfUserChange(IDimcupUser<TDef> user);
        void InformOfProviderChange(IDimcupProvider<TDef> provider);

    }
}
