using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village.Core.DIMCUP;
using Village.Resources;
using Village.Social.Jobs;

namespace Village.Core.Administrator
{
    public class VillageAdministrator
    {
        private Dictionary<string, DimPackage> DimPackages;
        private Dictionary<string, IDimManager<IDimDef>> _managers;

        public VillageAdministrator()
        {
            _managers = new Dictionary<string, IDimManager<IDimDef>>();
            _managers.Add("test", new JobManager<JobDef>() as IDimManager<IDimDef>);
        }

        public void Update()
        {
        }

        public IEnumerable<IDimManager<IDimDef>> FindManagersForInstance<TDef, TInst>(TInst inst) 
            where TDef : IDimDef 
            where TInst : IDimInstance<IDimDef>
        {
            foreach (var manager in _managers.Values)
                if(manager.InstanceIsOfType(inst))
                {
                    yield return manager;
                }

        }
    }
}
