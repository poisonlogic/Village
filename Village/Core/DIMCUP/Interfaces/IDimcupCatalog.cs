using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimcupCatalog<T> where T : IDimcupDef
    {
        Dictionary<string, T> DefsDictionary { get; }
        IEnumerable<T> AllDefs { get; }
    }
}
