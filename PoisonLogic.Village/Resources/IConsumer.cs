using PoisonLogic.Village.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Resources
{
    public interface IConsumer
    {
        string Name { get; }
        Guid InstanceId { get; }
        Dictionary<Resource, int> ConsumedResources { get; }
        IEnumerable<string> Tags { get; }
    }
}
