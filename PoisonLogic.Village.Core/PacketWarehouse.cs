using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using PoisonLogic.Dim;

namespace PoisonLogic.Village.Core
{
    public class PacketWarehouse
    {
        private Dictionary<string, DimPacket> _packets;

        public string PacketDirectory { get; }
        public IEnumerable<DimPacket> AllPackets
        {
            get
            {
                if (_packets == null)
                    throw new Exception("Packets have not been loaded yet.");
                return _packets.Values;
            }
        }


        public PacketWarehouse(string packetDirectory)
        {
            PacketDirectory = packetDirectory;
        }

        public void LoadAllPackets()
        {
            _packets = new Dictionary<string, DimPacket>();

            var packetDirectories = Directory.GetDirectories(PacketDirectory);
            foreach (var pd in packetDirectories)
            {
                Administrator.Log($"Loading packet in directory [{pd}]");
                try
                {
                    LoadIdividualPacket(pd);
                }
                catch(Exception e)
                {
                    Administrator.Log("Failed to load packet due to exception: " + e.Message);
                }
            }
        }

        private void LoadIdividualPacket(string directory)
        {
            var packetJson = Directory.GetFiles(directory, "PacketData.json", SearchOption.AllDirectories);
            
            if (packetJson.Count() == 0)
                throw new Exception($"Failed to find PacketData.json in directory {directory}");
            else if(packetJson.Count() > 1)
                throw new Exception($"Found multiple PacketData.json in directory {directory}. There should only be one.");

            DimPacket packetData;
            try
            {
                packetData = JsonConvert.DeserializeObject<DimPacket>(File.ReadAllText(packetJson[0]));
            }
            catch
            {
                throw new Exception("Failed to deserialize packetjson");
            }

            Administrator.Log($"Loading Idividual Packet [{packetData.PacketName}]");
           
            if(ValidatePacket(directory, packetData))
                _packets.Add(packetData.PacketName, packetData);
        }

        public bool ValidatePacket(string sourceDir, DimPacket packet, bool throwError = true)
        {
            var directoryName = sourceDir.Split(Path.DirectorySeparatorChar).Last();

            if (!directoryName.Equals(packet.PacketName))
            {
                if (throwError)
                    throw new Exception($"Packet '{packet.PacketName}' was found in directory '{directoryName}'. The packet's directory name should match it's PacketName");
                return false;
            }

            if (_packets.ContainsKey(packet.PacketName))
            {
                if (throwError)
                    throw new Exception($"Packet a with name '{packet.PacketName}' has already been loaded.");
                return false;
            }

            if (packet.HasAssembly)
            {

                var foundDlls = Directory.GetFileSystemEntries(sourceDir, packet.PacketAssemblyName + ".dll", SearchOption.AllDirectories);
                if (!foundDlls.Any())
                    throw new Exception($"Failed to find any dll with name '{packet.PacketAssemblyName}.dll'");
                if(foundDlls.Count() > 1)
                        throw new Exception($"Found multiple dlls with name {packet.PacketAssemblyName}.dll in packet directory '{sourceDir}'. Only one should exist across all sub-directories.");

            }
            return true;

        }


        public IEnumerable<string> GetAllManagerNames()
        {
            if (_packets == null || _packets.Count() == 0)
                throw new Exception("No packets loaded");
            foreach (var packet in _packets.Values)
                if (packet.HasManager)
                    yield return packet.PacketManagerName;
        }


    }
}
