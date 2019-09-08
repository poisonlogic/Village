using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IInventory
    {
        bool RespectsStackLimit { get; }
        bool HasMassLimit { get; }
        int CurrentMass { get; }
        int MaxMass { get; }

        IItemController Controller { get; }
        IInventoryUser GetInventoryUser();
        IEnumerable<IItemInstance> GetAllHeldItems();
        bool HasItemOfDef(string itemDef);
        IItemInstance GetItem(string itemId);
        bool CanAcceptItem(IItemInstance id);

    }
}
