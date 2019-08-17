using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core
{
    public interface IController
    {
        string ControllerName { get; }
        void Load(string saveDirectory);
        void Update();

    }
}
