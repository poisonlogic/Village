using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimCatalog<T> where T : DimDef
    {
        Type DefType { get; }
        Type InstanceType { get; }
        Type ManagerType { get; }
        Type CatalogType { get; }
        Type UserType { get; }
        Type ProviderType { get; }

        Dictionary<string, T> DefsDictionary { get; }
        IEnumerable<T> AllDefs { get; }
    }
}
