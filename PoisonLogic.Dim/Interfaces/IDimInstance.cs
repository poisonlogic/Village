using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonLogic.Dim
{
    public interface IDimInstance
    {
        string InstanceId { get; }
        string DefName { get; }
        DimDef DimDef { get; }
        IEnumerable<string> AllTags { get; }
        IDimManager GetManager();
        string SerializeSaveData();

    }
}
