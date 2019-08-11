using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonLogic.Dim
{
    public interface IDimCatalog<out T> where T : DimDef
    {
        IEnumerable<T> AllKnownDefs { get; }
        bool IsDefKnown(string dimDefName);
        bool TryRegisterNewDef(DimDef def);
    }
}
