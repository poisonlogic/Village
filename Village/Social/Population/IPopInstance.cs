using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Social.Population
{
    public interface IPopInstance : IDimcupInstance<BaseDimcupDef>
    {
        string InstanceId { get; }
        string Label { get; }
        IEnumerable<string> Tags { get; }
    }
}
