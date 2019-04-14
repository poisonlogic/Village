using PoisonLogic.Village.Core;
using PoisonLogic.Village.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Resources
{
    public interface IProducers
    {
        string Name { get; }
        Guid InstanceId { get; }
        Dictionary<string, int> RawProducedResources { get; }
        Dictionary<string, int> ModProducedResources { get; }
        IEnumerable<string> Tags { get; }
        ModifyerHandler GetModHandler();
        bool TryAddMod(Modifyer mod);
    }
}
