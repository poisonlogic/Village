using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public interface IDimcupDef
    {
        string DefName { get; }
        IEnumerable<string> Tags { get; }
    }
}
