using Village.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.DIMCUP;

namespace Village.Resources
{

    public interface IResourceInstance //: IDimInstance<>
    {
        string InstanceId { get; }
        ResourceDef ResourceDef { get; }


        int CurrentlyHeld { get; set; }
        float Modifyer { get; set; }


        //public ResourceInstance(Resource resource, ResourceManager manager)
        //{
        //    InstanceId = Guid.NewGuid();
        //    this.Resource = resource;
        //    this.Manager = manager;

        //}
    }
}
