using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Village.Core
{
    public interface IModifyerHandlerHolder
    {
        IEnumerable<string> Tags { get; }
    }
}
