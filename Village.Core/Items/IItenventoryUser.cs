using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IInventoryUser
    {
        string Id { get; }
        string Label { get; }
        IInventory AllInventories { get; }
    }
}
