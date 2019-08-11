using System;
using System.Collections.Generic;
using System.Text;

namespace PoisonLogic.Dim
{
    public class DimPacket
    {
        public string PacketName;
        public string PacketAssemblyName;
        public bool HasAssembly => !string.IsNullOrEmpty(PacketAssemblyName);
        public string PacketManagerName;
        public bool HasManager => !string.IsNullOrEmpty(PacketManagerName);
    }
}
