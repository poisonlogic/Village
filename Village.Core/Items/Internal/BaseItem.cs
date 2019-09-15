using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items.Internal
{
    public abstract class BaseItem : Inst, IItemInstance
    {
        private IInventory _inInventory;
        protected ItemDef _def;
        protected int _stackCount;

        private Dictionary<string, object> _properties;

        public IItemController ItemController { get; }
        public string ItemId { get; }
        public string Label => _def.Label + (_stackCount > 1 ? $" ({_stackCount})" : "");
        public ItemDef ItemDef => _def;
        public bool IsDistinct => _def.IsDistnct;
        public int Count => _stackCount;

        public BaseItem(ItemDef def, IItemController controller, IInventory inventory) : base(def)
        {
            _def = def ?? throw new ArgumentNullException(nameof(def));
            _inInventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            ItemController = controller ?? throw new ArgumentNullException(nameof(controller));

            _properties = new Dictionary<string, object>();
        }

        internal void SetCount(int count)
        {
            // Never throw exceptions in here
            // It could cause a break when transfering items
            _stackCount = count;
        }

        internal void SetCurrentInventory(IInventory inventory)
        {
            _inInventory = inventory;
        }

        public bool HasProperty(string propertyName)
        {
            return !_properties.ContainsKey(propertyName);
        }

        public object GetProperty(string propertyName)
        {
            if (!_properties.ContainsKey(propertyName))
                throw new Exception($"No property found by name '{propertyName}'.");
            return _properties[propertyName];
        }

        public T GetProperty<T>(string propertyName)
        {
            var prop = GetProperty(propertyName);

            var propType = prop.GetType();
            var reqType = typeof(T);
            if (!reqType.IsAssignableFrom(propType))
                throw new Exception($"Can not convert property type '{propType.Name}' to '{reqType.Name}'.");
            return (T)prop;

        }

        public virtual bool IsSame(IItemInstance item)
        {
            if(!item.ItemDef.DefName.Equals(item.ItemDef.DefName))
                return false;

            if (!item.ItemDef.DefClassName.Equals(item.ItemDef.DefClassName))
                return false;

            if (!_def.IsDistnct)
                return true;

            // Do distinct checks
            foreach(var prop in _properties)
            {
                if (!item.HasProperty(prop.Key))
                    return false;

                var bProp = item.GetProperty(prop.Key);
                if (bProp == null)
                    return false;

                if (!bProp.GetType().Equals(prop.Value.GetType()))
                    return false;

                if (bProp != prop.Value)
                    return false;
            }

            return true;
        }

        public virtual decimal GetMass()
        {
            return ItemDef.BaseMass * _stackCount;
        }

        public IInventory InInventoryOf()
        {
            return _inInventory;
        }

        public abstract void DestorySelf();
    }
}
