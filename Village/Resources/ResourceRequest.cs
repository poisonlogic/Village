using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Resources
{
    public enum ResourceRequestType
    {
        UNSET = -1,
        Produced = 0,
        Consume = 1
    }

    public class ResourceRequest
    {
        public Dictionary<string, int> Exchanges;
        public Guid RequesterId;
        public ResourceRequestType Type;
        public int Priority;
    }
}
