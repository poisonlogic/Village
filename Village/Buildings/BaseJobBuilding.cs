//using System;
//using System.Collections.Generic;
//using System.Text;
//using Village.Core.DIMCUP;
//using Village.Map;
//using Village.Map.MapStructures;
//using Village.Social.Jobs;

//namespace Village.Buildings
//{
//    public class BaseJobBuilding : BaseMapStructInstance, IJobProvider
//    {
//        public BaseJobBuilding(JobBuildingDef def, int x, int y) : base(def, x, y)
//        {

//        }

//        public IEnumerable<string> ProvidedJobDefNames => throw new NotImplementedException();

//        public IEnumerable<string> ProvidedDefIds => throw new NotImplementedException();

//        public IEnumerable<string> ProvidingInstanceIds => throw new NotImplementedException();

//        public IEnumerable<IDimInstance<JobDef>> ProvidingInstances => throw new NotImplementedException();

//        public IDimManager<JobDef, IDimInstance<JobDef>, IDimCatalog<JobDef>, IDimUser<JobDef>, IDimProvider<JobDef>> GetManager<t>()
//        {
//            throw new NotImplementedException();
//        }

//        public bool HasNewInstances(out IEnumerable<IDimInstance<JobDef>> newInstances)
//        {
//            throw new NotImplementedException();
//        }

//        public bool InformManagerOfChange()
//        {
//            throw new NotImplementedException();
//        }

//        public bool InformManagerOfNewInstances()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
