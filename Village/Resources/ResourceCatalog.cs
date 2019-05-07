using Village.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Resources
{
    public static class ResourceCatalog 
    {
        public static Dictionary<string, ResourceDef> All;

        public static void Init()
        {
            All = new Dictionary<string, ResourceDef>();
            All.Add("Iorn", new ResourceDef
            {
                Name = "Iron",
                Tags = new List<string> { "Metal" },
                BaseLimit = 5000
            });
        }
    }
}
