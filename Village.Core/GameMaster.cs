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

        private IFileHandler _fileHandler;
        private ILogger _logger;
        private List<IController> _controllers;
        private ITimeKeeper _timeKeeper;

        public GameMaster(IFileHandler fileHandler, ILogger logger, IEnumerable<IController> controllers)
        {
            if (Instance != null)
                throw new Exception("Multiple GameMasters not allowed");
            Instance = this;

            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _controllers = controllers?.ToList() ?? throw new ArgumentNullException(nameof(controllers));
            _timeKeeper = GetController<ITimeKeeper>();
            //_mapController = mapController ?? throw new ArgumentNullException(nameof(mapController));
            //_timeKeeper = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));
            //_buildingController = buildingController ?? throw new ArgumentNullException(nameof(buildingController));
            //_itemController = itemController ?? throw new ArgumentNullException(nameof(itemController));
            GetController<IBuildingController>().AddBuilding(new MapSpot(0, 0), "APPLE_TREE");
        }

        public void Update()
        {
            _timeKeeper.Tick();
            GetController<IBuildingController>().Update();
            _logger.LogError(_timeKeeper.Print("[HOUR]:[MIN]:[SEC] [WEEK] the [DAY]th, [SEAS], [YEAR]"));
            var map = GetController<IMapController>();
            map.Renderer.DrawMap(map);
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
