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

        public bool RespectsStackLimit {get;}
        public bool HasMassLimit => throw new NotImplementedException();
        public int CurrentMass => throw new NotImplementedException();
        public int MaxMass => throw new NotImplementedException();
        public IItemController Controller { get; }
        
        public BaseInventory(IItemController controller, IInventoryUser user)
        {
            _items = new Dictionary<string, IItemInstance>();
            Controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _user = user ?? throw new ArgumentNullException(nameof(user));
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

        public bool CanAcceptItem(IItemInstance id)
        {
            throw new NotImplementedException();
        }
    }
}
