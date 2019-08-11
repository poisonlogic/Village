using System;
using System.Collections.Generic;
using System.Text;
using PoisonLogic.Dim;
using PoisonLogic.Village.Stats;

namespace PoisonLogic.Village.Population
{
    public class PopulationManager : IDimManager
    {
        public IEnumerable<IStatUser> GetPopAsStatUsers;

        public IEnumerable<IDimInstance> AllInstances => throw new NotImplementedException();

        public Type InstanceType => throw new NotImplementedException();

        IEnumerable<IDimInstance> IDimManager.AllInstances => throw new NotImplementedException();

        Type IDimManager.InstanceType => throw new NotImplementedException();

        public bool ValidateCanAccept(IDimInstance instance)
        {
            throw new NotImplementedException();
        }
        

        bool IDimManager.TryLinkForeignInstance(IDimInstance instance)
        {
            throw new NotImplementedException();
        }

        void IDimManager.Update()
        {
            throw new NotImplementedException();
        }

        bool IDimManager.ValidateCanAccept(IDimInstance instance)
        {
            throw new NotImplementedException();
        }

        public string PrintState()
        {
            return "Arkansas";
        }
    }
}
