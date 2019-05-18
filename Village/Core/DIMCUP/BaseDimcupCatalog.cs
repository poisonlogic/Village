using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimcupCatalog : IDimcupCatalog<BaseDimcupDef>
    {
        public virtual Dictionary<string, BaseDimcupDef> DefsDictionary { get; }
        public virtual IEnumerable<BaseDimcupDef> AllDefs { get; }
    }
}
