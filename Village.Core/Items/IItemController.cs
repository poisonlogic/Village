using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    /*
     *  All like items share the same id
     *  Inventories hold a count of how many of a given id they have
     *  When a new unique item is made it gets it's own id
     *  IItem works more as a definition of an item than an instance
     *  All apples will have one IItem instance thus one id
     *  Items dropped on ground will make a ItemStack with an inventory
     *  ----------BAD IDEA: Rotting food would create a new "unique" item per tick 
     *                      because one property would be chaning to a new value
     *                      
     *  What to do with non-compariable properies: Avg? Round up?
     *      Call them Instance properties. Make Inst an interface. Let them define it.
     *      Whats the point of IItem then aside from the def?
     *      Properties could (would be cool in the future) be highly specific to one instance (Nutrition per apple based on tree's values)
     *      Gives a lot less weight to IItem.
     *      Going back to dispersed with custom merge logic;
     *      
     *  _____ CURRENT STATE ________
     *  Item instance represents one type of item definned by def and properties
     *  IItemInstance holds a count of how many of that Item there are.
     *  IItemInstances can be merged and split
     *  All Items must exists within an inventory
     */
    public interface IItemController : IController
    {
        IEnumerable<IInventory> AllInventories { get; }
        IItemInstance GetItem(string id);
        ItemDef GetDef(string defName);

        void RegisterNewInventory(IInventory inventory);
        IInventory FindInventory(string inventoryId);

        IItemInstance CreateNewItem(ItemDef itemDef, IInventory inventory);
        IItemInstance CreateNewItems(ItemDef itemDef, IInventory inventory, int count);
        IItemInstance CreateNewInstanceLike(IItemInstance item, IInventory inventory, int count);
        void DeleteItem(string id);

        bool CanTransferItemToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory);
        bool CanTransferItemsToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory, int count);
        bool TryTransferItemToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory);
        bool TryTransferItemsToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory, int count);

        // Consumed means that item or number of items will be destoryed
        bool CanAllItemBeConsumedFromInventory(string id, IInventory inventory);
        bool CanItemBeConsumedFromInventory(string id, IInventory inventory, int count);

        IEnumerable<IItemInstance> FindAllItemsNeedHauling();
        IEnumerable<IInventory> FindHaulDestinationForItem(IItemInstance item);
    }
}
