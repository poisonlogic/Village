using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Resources
{
    public enum ResourceUserType
    {
        UNSET = -1,
        Producer =0,
        Consumer = 1,
        Processor = 2
    }

    public interface IResourceUser
    {
        string Name { get; }
        Guid InstanceId { get; }
        ResourceUserType Type { get; }
        bool HasActiveRequest { get; }
        List<string> AllResourceIds { get; }
        List<ResourceRequest> GetAllActiveRequest();

    }
}
