using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Buildings.Industrial;
using Village.Core.Items;
using Village.Core.Items.Internal;
using Village.Core.Map;
using Village.Core.Rendering;
using Village.Core.Time;

namespace Village.Core.Buildings.Defs
{
    public class FruitTree : BaseBuilding, IBuilding, IProducerBuilding
    {
        private int _growthStage;
        private List<Dictionary<string, int>> _stageLengths;
        private BaseInventory _outputInventory;

        private ITimeKeeper _timeKeeper;
        private Dictionary<string, int> _finishDate;
        public FruitTree( BuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(layerName, def, anchor, controller, rotation)
        {
            _stageLengths = new List<Dictionary<string, int>>();
            _stageLengths.Add(new Dictionary<string, int> { { "DAY", 24 } });
            _stageLengths.Add(new Dictionary<string, int> { { "DAY", 24 } });
            _stageLengths.Add(new Dictionary<string, int> { { "DAY", 24 } });

            var timeKeeper = GameMaster.Instance.GetController<ITimeKeeper>();
            _timeKeeper = timeKeeper;
            _finishDate = timeKeeper.ProjectTime(_stageLengths[0]);
            _outputInventory = new BaseInventory(GameMaster.Instance.GetController<IItemController>(), this);
        }
        
        IInventory IInventoryUser.AllInventories => throw new NotImplementedException();

        public override ISprite GetSprite()
        {
            if (_growthStage < 1)
                return new FakeSprite { Text = ". ", BackColor = ConsoleColor.Green, MainColor = ConsoleColor.Black } as ISprite;
            if (_growthStage < 2)
                return new FakeSprite { Text = "i ", BackColor = ConsoleColor.Green, MainColor = ConsoleColor.Black } as ISprite;

            if(_outputInventory.GetAllHeldItems().Any())
                return new FakeSprite { Text = "Yo", BackColor = ConsoleColor.Green, MainColor = ConsoleColor.Red } as ISprite;
            
            return new FakeSprite { Text = "Y ", BackColor = ConsoleColor.Green, MainColor = ConsoleColor.Red } as ISprite;
        }

        public override void Update()
        {
            if (_growthStage < _stageLengths.Count && _timeKeeper.IsItTime(_finishDate) >= 0)
            {
                _growthStage++;
                if(_growthStage < _stageLengths.Count)
                    _finishDate = _timeKeeper.ProjectTime(_stageLengths[_growthStage]);
            }
            if(_growthStage == 3)
            {
                ProduceApple();
            }
        }

        private void ProduceApple()
        {
            var itemCon = GameMaster.Instance.GetController<IItemController>();
            var appleDef = itemCon.GetDef("APPLE");
            var apple = itemCon.CreateNewItem(appleDef, _outputInventory);
        }
        
        IInventory IProducerBuilding.GetOutputInventory()
        {
            return _outputInventory;
        }
    }
}
