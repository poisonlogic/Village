using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Crafting;
using Village.Core.Items;

namespace Village.Core.Buildings.Industrial
{
    public class ManufacturingBuildingDef : BuildingDef
    {
        // TODO: Think about how to link CraftingDefs and crafting stations
        public List<CraftingDef> CraftingDefs;
        public InventoryConfig OutputConfig;
        public InventoryConfig InputConfig;
    }
}
