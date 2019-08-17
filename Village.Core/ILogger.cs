using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core
{
    public interface ILogger
    {
        void LogError(string message);
        void LogError(string message, Exception e);
    }
}
