using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Social.Population
{
    public interface IPopInstance
    {
        string InstanceId { get; }
        string Label { get; }
        IEnumerable<string> Tags { get; }
    }
}
