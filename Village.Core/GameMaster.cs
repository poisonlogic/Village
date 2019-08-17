using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;
using Village.Core.Time;
using Village.Core.Time.Internal;

namespace Village.Core
{
    public class GameMaster
    {
        private IFileHandler _fileHandler;
        private ILogger _logger;
        private IMapController _mapController;
        private ITimeKeeper _timeKeeper;

        public GameMaster(IFileHandler fileHandler, ILogger logger, IMapController mapController, ITimeKeeper timeKeeper)
        {
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapController = mapController ?? throw new ArgumentNullException(nameof(mapController));
            _timeKeeper = timeKeeper ?? throw new ArgumentNullException(nameof(timeKeeper));
        }

        public void Update()
        {
            _mapController.Renderer.DrawMap(_mapController);
            _timeKeeper.Tick();
            _logger.LogError(_timeKeeper.Print());
        }

        public void LoadAllControllers()
        {
            //foreach (var con in _controllers)
            //{
            //    try
            //    {
            //        con.Load(_fileHandler.GetSaveDirectory());
            //    }
            //    catch(Exception e)
            //    {
            //        _logger.LogError($"Error on loading controller {con.ControllerName}", e);
            //    }

            //}
        }
    }
}
