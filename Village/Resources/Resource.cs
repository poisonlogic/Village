using Village.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Resources
{
    public class Resource
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public int BaseLimit { get; set; }
    }
}
