using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Core.Items.Internal
{
    public class BaseInventory : IInventory
    {
        private Dictionary<string, IItemInstance> _items;
        private IInventoryUser _user;
        private List<IItemFilter> _filters;

        public string InventoryId { get; }
        public InventoryConfig Config { get; }
        public IItemController Controller { get; }

        public bool IsEmpty => !_items.Any();

        public BaseInventory(IItemController controller, IInventoryUser user, InventoryConfig config)
        {
            InventoryId = Guid.NewGuid().ToString();
            _items = new Dictionary<string, IItemInstance>();
            Controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            Config = config ?? throw new ArgumentNullException(nameof(config));

            Controller.RegisterNewInventory(this);
        }

        public decimal GetCurrentMass()
        {
            return _items.Values.Sum(X => X.GetMass());
        }

        public IEnumerable<IItemInstance> GetAllHeldItems()
        {
            var items = _items.Keys.Select(x => Controller.GetItem(x));
            return items;
        }

        public IInventoryUser GetInventoryUser()
        {
            return _user;
        }

        public bool HasItem(string id)
        {
            return _items.Keys.Contains(id);
        }

        public IItemInstance GetItem(string id)
        {
            if (_items.Keys.Contains(id))
                return Controller.GetItem(id);
            throw new Exception($"Does not contain item '{id}'");
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

        public bool CanAcceptItem(IItemInstance item)
        {
            if (Config.HasMassLimit)
            {
                var curtMass = GetCurrentMass();
                var itemMass = item.GetMass();
                if (curtMass + itemMass > Config.MaxMass)
                    return false;
            }

            return Config.CanReceiveItems;

        }

        public IEnumerable<IItemInstance> GetItemsNeedHauling()
        {
            if (!Config.CanProvideItems)
                return null;
            if (!Config.CanReceiveItems)
                return _items.Values;

            return null;
        }
    }
}
