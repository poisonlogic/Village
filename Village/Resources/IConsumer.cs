using Village.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Resources
{
    public interface IConsumer
    {
        string Name { get; }
        Guid InstanceId { get; }

        IEnumerable<string> AllConsumedResources { get; }

        Dictionary<Resource, int> ConsumedResources { get; }
        IEnumerable<string> Tags { get; }

    }
}
