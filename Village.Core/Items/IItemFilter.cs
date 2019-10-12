using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IItemFilter
    {
        string FilterId { get; }
        string GetLable();
        ItemFilterConfig FilterConfig { get; }
        bool CanAcceptItem(IItemInstance item);
        bool CanAcceptItemOfDef(ItemDef item);
    }
}
