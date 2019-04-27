using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Buildings
{
    public interface IBuilding
    {
        Guid InstanceId { get; }
        string Name { get; }

    }
}
