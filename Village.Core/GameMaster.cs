using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Village.Core.Buildings;
using Village.Core.Items;
using Village.Core.Map;
using Village.Core.Time;
using Village.Core.Time.Internal;

namespace Village.Core
{
    public class GameMaster
    {
        public static GameMaster Instance;
        public static long MillsPerTick = 0;

        private Dictionary<string, int> HaulTime;

        private bool inited;
        private IFileHandler _fileHandler;
        public ILogger _logger;
        private List<IController> _controllers;
        private ITimeKeeper _timeKeeper;

        public ITimeKeeper TimeKeeper => _timeKeeper;

        public GameMaster(IFileHandler fileHandler, ILogger logger, IEnumerable<IController> controllers)
        {
            if (Instance != null)
                throw new Exception("Multiple GameMasters not allowed");
            Instance = this;

            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _controllers = controllers?.ToList() ?? throw new ArgumentNullException(nameof(controllers));
            _timeKeeper = GetController<ITimeKeeper>();
            HaulTime = _timeKeeper.ProjectTime(new Dictionary<string, int> { { "HOUR", 1 } });
        }

        public void Init()
        {
            inited = true;
            GetController<IBuildingController>().TryAddBuilding(new MapSpot(0, 0), "APPLE_TREE");
            GetController<IBuildingController>().TryAddBuilding(new MapSpot(3, 0), "STORAGE_CHEST");
            //GetController<IBuildingController>().TryAddBuilding(new MapSpot(-3, 0), "STORAGE_CHEST");
            GetController<IBuildingController>().TryAddBuilding(new MapSpot(0, 3), "STOVE");
        }

        public void Update()
        {
            if (!inited)
                Init();
            _timeKeeper.Tick();
            GetController<IBuildingController>().Update();

            foreach(var inv in GetController<IItemController>().AllInventories)
            {
                _logger.LogError(inv.InventoryUser.Label + " - " + inv.Config.Label + ": " + string.Join(", ", inv.GetAllHeldItems()?.Select(x => x.Label)));
            }

            if (_timeKeeper.IsItTime(HaulTime) > 0)
            {
                HaulTime = _timeKeeper.ProjectTime(new Dictionary<string, int> { { "HOUR", 1 }, { "MIN", 30 } });
                DoHaulTest();
            }
        }

        public void DoHaulTest()
        {
            var items = GetController<IItemController>().FindAllItemsNeedHauling().ToList();
            if (items == null)
                return;

            foreach (var item in items)
            {
                var invs = GetController<IItemController>().FindHaulDestinationForItem(item).OrderBy(x => x.Config.Priority);
                foreach ( var inv in invs)
                {
                    if(GetController<IItemController>().TryTransferItemToInventory(item, item.InInventoryOf(), inv))
                        break;
                }
            }
        }

        public T GetController<T>() where T : IController
        {
            var controler = _controllers.Where(x => typeof(T).IsAssignableFrom(x.GetType())).SingleOrDefault();
            if(controler == null)
                throw new Exception($"Could not find controller of type '{typeof(T).Name}'.");

            return (T)controler;
        }
        
    }
}
