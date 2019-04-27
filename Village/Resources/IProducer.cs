using Village.Core;
using Village.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Resources
{
    public interface IProducer
    {
        string Name { get; }
        Guid InstanceId { get; }

        IEnumerable<string> AllProducedResources { get; }

        IEnumerable<string> Tags { get; }
        ModifyerHandler GetModHandler();
        bool TryAddMod(Modifyer mod);
    }
}
