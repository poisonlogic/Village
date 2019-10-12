using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Core.Items.Internal
{
    /*
     * Cool inventory settings
     * Filter White and black list
     *      def name
     *      taxonmy
     *      
     * Limit by count
     *  either each or total
     *  
     *  dynanimc priority based on current count
     * 
     * 
     * 
     * 
     */


    public abstract class BaseInventory : IInventory
    {
        private Dictionary<string, IItemInstance> _items;
        private IInventoryUser _user;

        private List<string> _filterIds;

        public string InventoryId { get; }
        public InventoryConfig Config { get; }
        public IItemController Controller { get; }

        public bool IsEmpty => !_items.Any();
        public IInventoryUser InventoryUser => _user;

        public BaseInventory(IItemController controller, IInventoryUser user, InventoryConfig config)
        {
            InventoryId = Guid.NewGuid().ToString();
            _items = new Dictionary<string, IItemInstance>();
            Controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            Config = config ?? throw new ArgumentNullException(nameof(config));
            _filterIds = new List<string>();

            if(Config.ItemFilterConfig != null)
                _filterIds.Add(Controller.CreateFilterFromConfig(Config.ItemFilterConfig));
            Controller.RegisterNewInventory(this);
        }

        public decimal GetCurrentMass()
        {
            return _items.Values.Sum(X => X.GetMass());
        }

        public IEnumerable<IItemFilter> GetItemFilters()
        {
            foreach (var filterId in _filterIds)
                yield return Controller.GetItemFilter(filterId);
        }

        public IEnumerable<IItemInstance> GetAllHeldItems()
        {
            var items = _items.Keys.Select(x => Controller.GetItem(x));
            return items;
        }

        public bool HasItem(string id)
        {
            return _items.Keys.Contains(id);
        }

        public IItemInstance FindItem(string id)
        {
            if (_items.Keys.Contains(id))
                return Controller.GetItem(id);
            throw new Exception($"Does not contain item '{id}'");
        }
        
        public IEnumerable<IItemInstance> FindItemsOfDef(string defName)
        {
            foreach (var item in _items.Values.Where(x => x.ItemDef.DefName == defName))
                yield return item;
        }

        internal void AddItemInstance(IItemInstance item)
        {
            _items.Add(item.Id, item);
        }

        internal void RemoveItemInstance(IItemInstance item)
        {
            _items.Remove(item.Id);
        }

        public bool HasItemOfDef(string itemDef)
        {
            throw new NotImplementedException();
        }


        public bool CanAcceptItemOfDef(ItemDef itemDef)
        {
            if (_filterIds.Any())
            {
                foreach (var filter in GetItemFilters())
                    if (!filter.CanAcceptItemOfDef(itemDef))
                        return false;
            }

            return Config.CanReceiveItems;

        }

        public bool CanAcceptItem(IItemInstance item)
        {
            if (Config.HasMassLimit)
            {
                var curtMass = GetCurrentMass();
                var itemMass = item.GetMass();
                if (curtMass + itemMass > Config.MaxMass)
                    return false;
            }

            if (!CanAcceptItemOfDef(item.ItemDef))
                return false;

            return Config.CanReceiveItems;

        }

        public IEnumerable<IItemInstance> FindItemsNeedHauling()
        {
            if (!Config.CanProvideItems)
                return null;
            if (!Config.CanReceiveItems)
                return _items.Values;

            return null;
        }
    }
}
