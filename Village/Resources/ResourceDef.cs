using Village.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Village.Core.DIMCUP;

namespace Village.Resources
{
    public class ResourceDef : IDimDef
    {
        public string DefName { get; set; }
        public string PackageName { get; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int BaseLimit { get; set; }
    }
}
