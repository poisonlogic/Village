using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Crafting;
using Village.Core.Items;
using Village.Core.Items.Internal;
using Village.Core.Map;
using Village.Core.Rendering;

namespace Village.Core.Buildings.Industrial
{
    public class DefaultManufacturingBuilding : BaseManufacturerBuilding
    {
        private bool _spriteFlip;

        public DefaultManufacturingBuilding(ManufacturingBuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : 
            base(def, layerName, anchor, controller, rotation)
        {
        }

        public override string GetSprite()
        {
            return null;
            //var temptext = "MMMM";
            //if (PercentComplete > 0)
            //    temptext = PercentComplete.ToString("0.00");

            //_spriteFlip = !_spriteFlip;

            //return new FakeSprite
            //{
            //    BackColor = ConsoleColor.Black,
            //    MainColor = ConsoleColor.White,
            //    Text = _spriteFlip ? temptext.Substring(0, 2) : temptext.Substring(2, 2)
            //};
        }

        public override void Update()
        {
            if (_currentCrafter != null && _currentCraftingState != null)
                TickCrafting();
            else
            {
                if(!_inputInventory.IsEmpty)
                {
                    var apples = _inputInventory.FindItemsOfDef("APPLE").ToList();
                    if (apples?.Any() ?? false)
                    {
                        var apple = apples.First();
                        var itemContoller = GameMaster.Instance.GetController<IItemController>();
                        if (itemContoller.TryDestoryItems(apple.Id, _inputInventory, 1))
                        {
                            if (TryStartCrafting(ManufacturingBuildingDef.CraftingDefs.First().DefName, new FakeCrafter()) != CraftingResults.Success)
                                throw new Exception("WRONG");
                        }
                    }
                }
            }
        }
    }
}
