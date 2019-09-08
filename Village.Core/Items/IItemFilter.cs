using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core.Items
{
    public interface IItemFilter
    {
        string GetLable();
        bool CanAcceptItem(IItemInstance item);
    }
}
