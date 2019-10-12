using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Items.Internal;

namespace Village.Core.Items
{
    public class DefaultInventory : BaseInventory
    {
        public DefaultInventory(IItemController controller, IInventoryUser user, InventoryConfig config) : base(controller, user, config)
        {
        }
    }
}
