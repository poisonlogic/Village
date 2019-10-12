using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IInventory
    {
        string InventoryId { get; }
        InventoryConfig Config { get; }
        IItemController Controller { get; }
        IInventoryUser InventoryUser { get; }
        bool IsEmpty { get; }


        bool HasItemOfDef(string itemDef);
        bool HasItem(string itemId);
        bool CanAcceptItem(IItemInstance item);
        bool CanAcceptItemOfDef(ItemDef item);

        decimal GetCurrentMass();
        IEnumerable<IItemFilter> GetItemFilters();
        IEnumerable<IItemInstance> GetAllHeldItems();
        IItemInstance FindItem(string itemId);
        IEnumerable<IItemInstance> FindItemsOfDef(string defName);
        IEnumerable<IItemInstance> FindItemsNeedHauling();
    }
}
