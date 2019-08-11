using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonLogic.Dim
{
    public abstract class DimDef
    {
        public string DefName;
        public string SourcePackageName;
        public string ManagerTypeName;
        public string InstanceTypeName;
        public IEnumerable<string> Tag;
    }
}
