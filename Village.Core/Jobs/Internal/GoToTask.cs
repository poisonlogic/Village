using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.Map;

namespace Village.Core.Jobs.Internal
{
    public class GoToTask : BaseTask, ITask
    {
        private MapSpot _nextSpot;
        public MapSpot Destination { get; }

        public GoToTask(IJobWorker worker, MapSpot mapSpot) : base(worker)
        {
            Destination = mapSpot ?? throw new ArgumentNullException(nameof(mapSpot));
        }

        public override bool CanCancel()
        {
            return true;
        }

        public override bool CanPause()
        {
            throw new NotImplementedException();
        }

        public override bool CanStart()
        {
            return true;
        }

        public override void DoUpdate()
        {
            if (IsActive && Worker.MoveToSpot(_nextSpot))
                _nextSpot = GetNextNextSpot();
        }

        public override bool IsCompleted()
        {
            return Worker.Position == Destination;
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override void Pause()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            _nextSpot = GetNextNextSpot();
            base.Start();
        }

        public override void UnPause()
        {
            throw new NotImplementedException();
        }

        private MapSpot GetNextNextSpot()
        {
            var current = Worker.Position;

            if(current.X != Destination.X)
            {
                var move = current.X > Destination.X ? -1 : 1;
                return new MapSpot(current.X + move, current.Y);
            }
            else
            {
                var move = current.Y > Destination.Y ? -1 : 1;
                return new MapSpot(current.X, current.Y + move);
            }
        }
    }
}
