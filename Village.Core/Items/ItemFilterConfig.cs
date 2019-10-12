using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public class ItemFilterConfig
    {
        public bool WhiteList;
        public int Priority;
        public List<string> Taxonomies;
        public List<string> ItemDefNames;
    }
}
