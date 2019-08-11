using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.DIMCUP
{
    public class BaseDimDef : IDimDef
    {
        public string DefName { get; }
        public string PackageName { get; }
        public IEnumerable<string> Tags { get; }
    }
}
