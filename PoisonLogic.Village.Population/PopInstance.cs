using PoisonLogic.Dim;
using PoisonLogic.Village.Stats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Population
{
    public class PopInstance : DimBaseInstance<PopDef>, IStatUser
    {
        private PopulationManager _populationManager => _manager as PopulationManager;
        
        public PopDef PopDef => DimDef as PopDef;
        public string Label { get; }

        string IStatUser.InstanceId => throw new NotImplementedException();

        IEnumerable<string> IStatUser.Tags => throw new NotImplementedException();

        string IDimUser.InstanceId => throw new NotImplementedException();

        public override string SerializeSaveData()
        {
            throw new NotImplementedException();
        }

        void IDimUser.InformOfChange<TMan>()
        {
            throw new NotImplementedException();
        }

        IDimManager IDimUser.GetManager()
        {
            throw new NotImplementedException();
        }
    }
}
