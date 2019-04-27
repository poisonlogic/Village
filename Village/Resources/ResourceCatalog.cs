using Village.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Resources
{
    public static class ResourceCatalog 
    {
        public static Dictionary<string, Resource> All;

        public static void Init()
        {
            All = new Dictionary<string, Resource>();
            All.Add("Iorn", new Resource
            {
                Name = "Iron",
                Tags = new List<string> { "Metal" },
                BaseLimit = 5000
            });
        }
    }
}
