using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimCatalog<TDef> : IDimCatalog<TDef> where TDef : BaseDimDef
    {
        public virtual Dictionary<string, TDef> DefsDictionary { get; }
        public virtual IEnumerable<TDef> AllDefs { get; }

        public virtual Type DefType => typeof(BaseDimDef);

        public Type InstanceType => typeof(BaseDimInstance<TDef>);

        public Type ManagerType => typeof(BaseDimInstance<TDef>);

        public Type CatalogType => typeof(BaseDimCatalog<TDef>);

        public Type UserType => typeof(BaseDimUser<TDef>);

        public Type ProviderType => typeof(BaseDimProvider<TDef>);
    }
}
