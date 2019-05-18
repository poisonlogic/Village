using Village.Map;
using Village.Resources;
using System.Collections.Generic;
using System.Linq;
using Village.Social.Population;
using Village.Map.MapStructures;
using Village.Social.Jobs;
using System;

namespace Village.Core
{
    public class VillageManager
    {
        public VillageMap Map { get; private set; }
        public PopulationManager PopulationManager { get; }
        //public MapStructManager MapStructManager { get; }
        public JobManager<JobDef> JobManager { get; }
        public ResourceManager ResourceManager { get; }

        //public IEnumerable<IJobProvider> AllIJobProviders { get { return MapStructManager.AllMapStructs.Where(x => x is IJobProvider).Select(x => x as IJobProvider); } }
        //public IEnumerable<IJobWorker> AllIJobWorkers { get { return MapStructManager.AllMapStructs.Where(x => x is IJobWorker).Select(x => x as IJobWorker); } }

        public VillageManager()
        {
            this.Map = new VillageMap(20, 20);
            PopulationManager = new PopulationManager();
            //MapStructManager = new MapStructManager(Map);
            JobManager = new JobManager<JobDef>();
            //this.ResourceManager = new ResourceMaster();
        }

        public bool TryAddVillager(IPopInstance newGuy)
        {
            if (!this.PopulationManager.TryAddNewVillager(newGuy))
                return false;
            if (newGuy is IJobWorker<JobDef>)
                this.JobManager.TryRegisterNewWorker(newGuy as IJobWorker<JobDef>);
            return true;
        }

        public bool TryAddNewMapStruct(IMapStructInstance<MapStructDef> mapStruct)
        {
            //if (!this.MapStructManager.TryAddStructure(mapStruct))
            //    return false;
            if (mapStruct is IResourceUser)
                this.ResourceManager.TryRegiseringUser(mapStruct as IResourceUser);
            return true;
        }

        public void TryFindJobsForAll()
        {
            //foreach (IJobWorker worker in AllIJobWorkers)
            //    if (!worker.HasJob)
            //        JobManager.FindOpenJobForAndHire(worker);
        }

        public void PrintPop()
        {
            Console.WriteLine("Printing Population: ");
            Console.WriteLine(PopulationManager.PrintState());
        }

        public void PrintJobs()
        {
            Console.WriteLine("Printing Jobs: ");
            Console.WriteLine(JobManager.PrintState());
        }
    }
}
