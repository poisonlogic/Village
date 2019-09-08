using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Items;

namespace Village.Core.Buildings.Industrial
{
    public interface IConsumerBuilding : IInventoryUser
    {
        IInventory GetInputInventory();
    }
}
