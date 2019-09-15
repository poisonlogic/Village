using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Core.Items.Internal
{
    internal class ItemController : IItemController, IController
    {
        private Dictionary<string, ItemDef> _defs;
        private Dictionary<string, IItemInstance> _items;
        private Dictionary<string, IInventory> _inventories;

        public IEnumerable<IInventory> AllInventories => _inventories.Values;

        public ItemDef GetDef(string defName)
        {
            return _defs[defName];
        }

        public ItemController()
        {
            _defs = DefLoader.LoadDefCatalog<ItemDef>("Village.Core.Items.Defs.ItemDefs.json");
            _items = new Dictionary<string, IItemInstance>();
            _inventories = new Dictionary<string, IInventory>();
        }

        public ItemDef GetDefByName(string name)
        {
            if (!_defs.ContainsKey(name))
                return null;
            return _defs[name];
        }

        public void RegisterNewInventory(IInventory inventory)
        {
            if (_inventories.ContainsKey(inventory.InventoryId))
                throw new Exception($"Inventory '{inventory.InventoryId}' already registered.");
            _inventories.Add(inventory.InventoryId, inventory);
        }

        public IInventory FindInventory(string id)
        {
            if (_inventories.ContainsKey(id))
                return _inventories[id];
            return null;
        }

        public IItemInstance CreateNewItem(ItemDef def, IInventory inventory)
        {
            return CreateNewItems(def, inventory, 1);
        }

        public IItemInstance CreateNewItems(ItemDef def, IInventory inventory, int count)
        {
            var item = DefLoader.CreateInstanct<IItemInstance>(def, this, inventory);
            (item as BaseItem).SetCount(count);
            _items.Add(item.Id, item);
            (inventory as BaseInventory).AddItemInstance(item);
            return item;
        }

        public IItemInstance CreateNewInstanceLike(IItemInstance item, IInventory inventory, int count)
        {
            var newItem = DefLoader.CreateInstanct<IItemInstance>(item.ItemDef, this, inventory);
            (newItem as BaseItem).SetCount(count);
            _items.Add(newItem.Id, newItem);
            (inventory as BaseInventory).AddItemInstance(newItem);
            return newItem;
        }

        private void TransferBetweenInstances(IItemInstance toItem, IItemInstance fromItem, int count)
        {
            if (count >= fromItem.Count)
                throw new Exception($"Can not transfer {count} from instance with {fromItem.Count}.");

            if(toItem.InInventoryOf().Config.RespectsStackLimit && toItem.Count + count > toItem.ItemDef.StackLimit)
                throw new Exception($"Can not transfer {count} due to stack limit.");


            (toItem as BaseItem).SetCount(toItem.Count + count);
            (fromItem as BaseItem).SetCount(fromItem.Count - count);

        }

        public void DeleteItem(string id)
        {
            if (!_items.ContainsKey(id))
                throw new Exception($"Item '{id}' not registered with ItemController.");
            _items.Remove(id);
            //TODO find reall way of deleting
        }

        public IItemInstance GetItem(string Id)
        {
            if (!_items.ContainsKey(Id))
                throw new Exception($"Item '{Id}' not registered with ItemController.");
            return _items[Id];
        }

        public bool CanTransferItemsToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory, int count)
        {
            if (!newInventory.CanAcceptItem(item))
                return false; // Item not accepted by new inventory

            if (newInventory.Config.HasMassLimit && newInventory.GetCurrentMass() + item.GetMass() > newInventory.Config.MaxMass)
                return false; // Would put contaner over mass limit

            if (!oldInventory.GetAllHeldItems().Where(x => x.Equals(item)).Any())
                return false; // Item not found in in old inventory

            if (newInventory.HasItem(item.Id))
                return false; // Item already in new inventory

            if (item.Count - count < 0)
                return false; // Can not transfer more than is in stack
            
            var same = newInventory.GetAllHeldItems().Where(x => x.IsSame(item)).SingleOrDefault();
            if(same != null)
            {
                if (newInventory.Config.RespectsStackLimit && same.Count + count > item.ItemDef.StackLimit)
                    return false; // Would put new inventory over stack limit
            }
            else
            {
                if (newInventory.Config.RespectsStackLimit && count > item.ItemDef.StackLimit)
                    return false; // Would put new inventory over stack limit
            }

            return true;
        }

        public bool TryTransferItemsToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory, int count)
        {
            if (!CanTransferItemsToInventory(item, oldInventory, newInventory, count))
                return false;

            var fullStack = item.Count == count;

            var existingItem = newInventory.GetAllHeldItems().Where(x => x.IsSame(item)).SingleOrDefault();
            if(existingItem == null) // No matching item in new inventory
            {
                if(fullStack)
                {
                    (oldInventory as BaseInventory).RemoveItemInstance(item);
                    (newInventory as BaseInventory).AddItemInstance(item);
                    (item as BaseItem).SetCurrentInventory(newInventory);
                    return true;
                }
                else
                {
                    var newItem = CreateNewInstanceLike(item, newInventory, count);
                    (item as BaseItem).SetCount(item.Count - count);
                    return true;
                }
            }
            else
            {
                if (fullStack)
                {
                    (oldInventory as BaseInventory).RemoveItemInstance(item);
                    (existingItem as BaseItem).SetCount(existingItem.Count + item.Count);
                    DeleteItem(item.Id);
                    return true;
                }
                else
                {
                    TransferBetweenInstances(existingItem, item, count);
                    return true;
                }
            }
        }

        public bool CanTransferItemToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory)
        {
            return CanTransferItemsToInventory(item, oldInventory, newInventory, item.Count);
        }

        public bool TryTransferItemToInventory(IItemInstance item, IInventory oldInventory, IInventory newInventory)
        {
            return TryTransferItemsToInventory(item, oldInventory, newInventory, item.Count);
        }

        public IItemInstance ProduceItemInInventory(string Id, IInventory inventory, int count)
        {
            throw new NotImplementedException();
        }

        public bool CanAllItemBeConsumedFromInventory(string id, IInventory inventory)
        {
            throw new NotImplementedException();
        }

        public bool CanItemBeConsumedFromInventory(string id, IInventory inventory, int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItemInstance> FindAllItemsNeedHauling()
        {
            foreach(var inv in _inventories.Values)
            {
                var needsHaul = inv.GetItemsNeedHauling();
                if (needsHaul != null)
                    foreach (var item in needsHaul)
                        yield return item;
            }

        }

        public IEnumerable<IInventory> FindHaulDestinationForItem(IItemInstance item)
        {
            foreach (var inventory in _inventories.Values)
                if (inventory.CanAcceptItem(item))
                    yield return inventory;
        }
    }
}
