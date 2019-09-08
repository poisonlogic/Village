using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Items.Internal;

namespace Village.Core.Items
{
    public class DefaultItem : BaseItem
    {
        public DefaultItem(ItemDef def, IItemController controller, IInventory inventory) : base(def, controller, inventory)
        {

        }

        public override void DestorySelf()
        {
            throw new NotImplementedException();
        }
    }
}
