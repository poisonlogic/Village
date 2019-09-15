using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Items;
using Village.Core.Map;

namespace Village.Core.Buildings.Defs
{
    public class AppleTreeDef : BuildingDef
    {
        public int GrowTime;
    }

    public class StorageContainerDef : BuildingDef
    {
        public InventoryConfig InventoryConfig;
    }
}
