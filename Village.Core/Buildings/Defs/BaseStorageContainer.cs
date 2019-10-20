using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Items;
using Village.Core.Items.Internal;
using Village.Core.Map;
using Village.Core.Rendering;

namespace Village.Core.Buildings.Defs
{
    public class BaseStorageContainer : BaseBuilding, IInventoryUser
    {
        public IInventory AllInventories => throw new NotImplementedException();
        public StorageContainerDef StorageContainerDef { get; }
        public IItemController ItemController { get; }
        public IInventory Inventory { get; }

        public BaseStorageContainer(StorageContainerDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(def, layerName, anchor, controller, rotation)
        {
            StorageContainerDef = def ?? throw new ArgumentNullException(nameof(def));
            ItemController = GameMaster.Instance.GetController<IItemController>();
            Inventory = new DefaultInventory(ItemController, this, def.InventoryConfig);
        }

        public override void Update()
        {
        }

        public override string GetSprite()
        {
            return StorageContainerDef.DefName + StorageContainerDef.Sprites.First();
            return null;
            //if(Inventory.GetAllHeldItems().Any())
            //    return new FakeSprite()
            //    {
            //        BackColor = ConsoleColor.White,
            //        MainColor = ConsoleColor.Red,
            //        Text = "[]"
            //    };

            //return new FakeSprite()
            //{
            //    BackColor = ConsoleColor.White,
            //    MainColor = ConsoleColor.Blue,
            //    Text = "{}"
            //};
        }
    }
}
