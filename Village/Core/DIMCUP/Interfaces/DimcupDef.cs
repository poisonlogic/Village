using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public abstract class DimDef
    {
        public string DefName { get; }
        public string PackageName { get; }
        public IEnumerable<string> Tags { get; }
    }
}
