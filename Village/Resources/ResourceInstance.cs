using Village.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Resources
{

    public class ResourceInstance :IModifyerHandlerHolder
    {
        public Guid InstanceId { get; }
        public Resource Resource { get; }
        //public ResourceManager Manager { get; }
        public ModifyerHandler Mods { get; }
        public int CurrentlyHeld { get; set; }
        public float NetProducedPerCycle { get; set; }
        public float Modifyer { get; set; }

        public IEnumerable<string> Tags => throw new NotImplementedException();

        public IEnumerable<string> GetTags()
        {
            return Resource.Tags;
        }

        //public ResourceInstance(Resource resource, ResourceManager manager)
        //{
        //    InstanceId = Guid.NewGuid();
        //    this.Resource = resource;
        //    this.Manager = manager;

        //}
    }
}
