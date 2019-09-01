using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;
using Village.Core.Rendering;
using Village.Core.Time;

namespace Village.Core.Buildings.Defs
{
    public class FruitTree : BaseBuilding, IBuilding
    {
        private int _growthStage;
        private List<Dictionary<string, int>> _stageLengths;

        private ITimeKeeper _timeKeeper;
        private Dictionary<string, int> _finishDate;
        public FruitTree( BuildingDef def, string layerName, MapSpot anchor, IMapController controller, MapRotation rotation) : base(layerName, def, anchor, controller, rotation)
        {
            _stageLengths = new List<Dictionary<string, int>>();
            _stageLengths.Add(new Dictionary<string, int> { { "DAY", 24 } });
            _stageLengths.Add(new Dictionary<string, int> { { "DAY", 24 } });

            var timeKeeper = GameMaster.Instance.GetController<ITimeKeeper>();
            _timeKeeper = timeKeeper;
            _finishDate = timeKeeper.ProjectTime(_stageLengths[0]);
        }

        public override ISprite GetSprite()
        {
            if (_growthStage < 1)
                return new FakeSprite { Text = ". ", BackColor = ConsoleColor.Green, MainColor = ConsoleColor.Black } as ISprite;
            if (_growthStage < 2)
                return new FakeSprite { Text = "i ", BackColor = ConsoleColor.Green, MainColor = ConsoleColor.Black } as ISprite;

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
        }
    }
}
