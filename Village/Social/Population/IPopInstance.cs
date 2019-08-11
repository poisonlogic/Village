using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;

namespace Village.Social.Population
{
    public interface IPopInstance : IDimInstance<BaseDimDef>
    {
        string Label { get; }
    }
}
