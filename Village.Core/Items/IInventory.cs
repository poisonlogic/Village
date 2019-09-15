using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IInventory
    {
        string InventoryId { get; }
        InventoryConfig Config { get; }
        decimal GetCurrentMass();
        bool IsEmpty { get; }
        IItemController Controller { get; }
        IInventoryUser GetInventoryUser();
        IEnumerable<IItemInstance> GetAllHeldItems();
        bool HasItemOfDef(string itemDef);
        bool HasItem(string itemId);
        IItemInstance GetItem(string itemId);
        bool CanAcceptItem(IItemInstance item);

        IEnumerable<IItemInstance> GetItemsNeedHauling();

    }
}
