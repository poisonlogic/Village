using System;
using System.Collections.Generic;
using System.Text;
using Village.Core;

namespace Village.ConsoleApp.Classes
{
    public class FileHandler : IFileHandler
    {
        public string GetSaveDirectory()
        {
            return @"C:\temp\VillageSave";
        }
    }
}
