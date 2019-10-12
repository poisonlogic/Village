using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Crafting
{
    public class CraftingDef : Def
    {
        public Dictionary<string,float> InputTaxonomy;
        public string OutputItemDefName;
        public bool OutputHasQuality;
        public int OutputCount;
        public float TotalWork;
        public float BaseWorkPerTick;

    }
}
