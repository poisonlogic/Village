using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Crafting;

namespace Village.Core.Buildings.Industrial
{
    public interface IManufacturingBuilding : IConsumerBuilding, IProducerBuilding
    {
        ManufacturingBuildingDef ManufacturingBuildingDef { get; }
        IEnumerable<CraftingDef> GetAllCrafting();
        CraftingDef CurrentCraftingDef { get; }
        float PercentComplete { get; }
        ICrafter GetCurrentCrafter();
        string TryStartCrafting(string craftingDefName, ICrafter crafter);
        void TickCrafting();
        void StopCrafting();
    }
}
