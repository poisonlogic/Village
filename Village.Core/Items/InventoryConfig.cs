using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public class InventoryConfig
    {
        public string Label;
        public bool CanReceiveItems;
        public bool CanProvideItems;
        public bool RespectsStackLimit;
        public bool HasMassLimit;
        public int MaxMass;
        public int Priority;
        public ItemFilterConfig ItemFilterConfig;
    }
}
