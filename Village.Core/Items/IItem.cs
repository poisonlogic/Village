using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IItemInstance
    {
        string Id { get; }
        ItemDef ItemDef { get; }
        bool IsDistinct { get; }
        int Count { get; }

        IInventory InInventoryOf();
        decimal GetMass();
        bool HasProperty(string propertyName);
        object GetProperty(string propertyName);
        T GetProperty<T>(string propertyName);
        bool IsSame(IItemInstance item);
        void DestorySelf();
    }
}
