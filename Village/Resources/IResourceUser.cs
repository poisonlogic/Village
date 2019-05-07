using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Resources
{
    public interface IResourceUser
    {
        string Name { get; }
        string InstanceId { get; }
        bool HasActiveRequest { get; }
        IEnumerable<string> AllResourceIds { get; }
        IEnumerable<ResourceRequest> GetAllActiveRequest();

    }
}
