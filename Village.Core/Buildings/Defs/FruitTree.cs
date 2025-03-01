﻿using System;
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
        public FruitTree( BuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(def, layerName, anchor, controller, rotation)
        {
            _stageLengths = new List<Dictionary<string, int>>();
            _stageLengths.Add(new Dictionary<string, int> { { "HOUR", 1 } });
            _stageLengths.Add(new Dictionary<string, int> { { "HOUR", 1 } });
            _stageLengths.Add(new Dictionary<string, int> { { "HOUR", 1 } });

            var outputConfif = new InventoryConfig()
            {
                CanProvideItems = true,
                CanReceiveItems = false,
                HasMassLimit = false,
                MaxMass = -1,
                RespectsStackLimit = false
            };

            var timeKeeper = GameMaster.Instance.GetController<ITimeKeeper>();
            _timeKeeper = timeKeeper;
            _finishDate = timeKeeper.ProjectTime(_stageLengths[0]);
            _outputInventory = new DefaultInventory(GameMaster.Instance.GetController<IItemController>(), this, outputConfif);
        }
        
        IInventory IInventoryUser.AllInventories => throw new NotImplementedException();

        public override string GetSprite()
        {
            if (_outputInventory.GetAllHeldItems().Any())
                return BuildingDef.DefName + "Tree2";

            return BuildingDef.DefName + "Tree";
            
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
                _growthStage = 4;
            }
            if (_growthStage == 4 && _outputInventory.IsEmpty)
            {
                _growthStage = 2;
                _finishDate = _timeKeeper.ProjectTime(new Dictionary<string, int> { { "HOUR", 10 } });
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
