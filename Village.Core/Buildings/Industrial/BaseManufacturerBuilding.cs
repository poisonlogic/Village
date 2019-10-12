using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Crafting;
using Village.Core.Items;
using Village.Core.Map;

namespace Village.Core.Buildings.Industrial
{
    public abstract class BaseManufacturerBuilding : BaseBuilding, IManufacturingBuilding, IInventoryUser
    {
        protected ManufacturingBuildingDef _manufacturingBuildingDef;
        protected ICrafter _currentCrafter;
        protected CraftingState _currentCraftingState;

        protected IInventory _outputInventory;
        protected IInventory _inputInventory;

        public ManufacturingBuildingDef ManufacturingBuildingDef => _manufacturingBuildingDef;
        public CraftingDef CurrentCraftingDef => _currentCraftingState?.CraftingDef;
        public float PercentComplete => _currentCraftingState?.PercentDone ?? -1;
        public IInventory AllInventories => throw new NotImplementedException();
        public bool IsCrafting => _currentCraftingState != null;

        internal BaseManufacturerBuilding(ManufacturingBuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) 
            : base(def, layerName, anchor, controller, rotation)
        {
            _manufacturingBuildingDef = def ?? throw new ArgumentNullException(nameof(def));

            _outputInventory = new DefaultInventory(GameMaster.Instance.GetController<IItemController>(), this, _manufacturingBuildingDef.OutputConfig);
            _inputInventory = new DefaultInventory(GameMaster.Instance.GetController<IItemController>(), this, _manufacturingBuildingDef.InputConfig);
            _currentCraftingState = null;
        }


        public IEnumerable<CraftingDef> GetAllCrafting()
        {
            return _manufacturingBuildingDef.CraftingDefs;
        }

        public ICrafter GetCurrentCrafter()
        {
            return _currentCrafter;
        }

        public IInventory GetInputInventory()
        {
            return _inputInventory;
        }

        public IInventory GetOutputInventory()
        {
            return _outputInventory;
        }

        public string TryStartCrafting(string craftingDefName, ICrafter crafter)
        {

            // TODO: Logic for filtering crafters
            //if (false)
            //    return false;

            if (!_manufacturingBuildingDef.CraftingDefs.Where(x => x.DefName == craftingDefName).Any())
                throw new Exception("Crafting def '{craftingDefName}' not found.");

            _currentCrafter = crafter;
            _currentCraftingState = new CraftingState(_manufacturingBuildingDef.CraftingDefs.Single(x => x.DefName == craftingDefName));
            return CraftingResults.Success;
        }

        public void TickCrafting()
        {
            if (!IsCrafting)
                return;

            if (_currentCrafter == null)
                throw new Exception("Attempted to tick crafting, but no crafter.");

            if (_currentCraftingState == null)
                throw new Exception("Attempted to tick crafting, but no crafting state.");

            // TODO: Logic for crafting work per tick;
            _currentCraftingState.AddWork(_currentCraftingState.CraftingDef.BaseWorkPerTick, _currentCrafter);

            if (_currentCraftingState.PercentDone >= 1)
            {
                var itemController = GameMaster.Instance.GetController<IItemController>();
                var def = itemController.GetDef(_currentCraftingState.CraftingDef.OutputItemDefName);
                itemController.CreateNewItems(def, _outputInventory, _currentCraftingState.CraftingDef.OutputCount);

                _currentCraftingState = null;
                _currentCrafter = null;
            }
        }

        public void StopCrafting()
        {
            _currentCrafter = null;
            _currentCraftingState = null;
        }
    }
}
