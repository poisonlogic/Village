using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public class ItemDef : Def
    {
        public string Label;
        public bool IsDistnct;
        public int StackLimit;
        public string Taxonomy;
        public decimal BaseValue;
        public decimal BaseMass;
    }
}
