using Newtonsoft.Json;
using PoisonLogic.Village.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PoisonLogic.Dim
{
    public class Administrator
    {
        private static ILogger _logger;

        public static string PackageDirectory;
        private Dictionary<string, DimPacket> _packets;
        private Dictionary<string, IDimManager> _managers;
        private Dictionary<string, Assembly> _loadedAssemblies;

        public PacketWarehouse PacketWarehouse { get; }
        public List<IDimManager> AllManagers;

        public Administrator(ILogger log, string packageDirectory)
        {
            _logger = log;
            PackageDirectory = packageDirectory;

            PacketWarehouse = new PacketWarehouse(packageDirectory);
        }

        public static void Log(string s)
        {
            _logger.Log(s);
        }


        #region Loading
        public void LoadAllAssemblies()
        {
            _loadedAssemblies = new Dictionary<string, Assembly>();

            foreach (var packet in PacketWarehouse.AllPackets)
                LoadPacketAssembly(packet);
        }

        private void LoadPacketAssembly(DimPacket packet)
        {
            var packetdir = Path.Combine(PacketWarehouse.PacketDirectory, packet.PacketName);
            var foundDllPaths = Directory.GetFiles(packetdir, packet.PacketAssemblyName + ".dll", SearchOption.AllDirectories);
            Administrator.Log($"Looking for dlls in {packetdir} and found {string.Join(", ", foundDllPaths)}");

            if (foundDllPaths.Count() > 1)
                throw new Exception("Found multiple matching dlls");
            if (foundDllPaths.Count() == 0)
                throw new Exception("Found no dlls");

            var dll = foundDllPaths.Single();

            try
            {
                var fileName = dll.Split('\\').Last();
                Administrator.Log($"Loading package '{fileName}'");
                Assembly loadedAssembly = Assembly.LoadFile(dll);
                _loadedAssemblies.Add(packet.PacketName, loadedAssembly);
                Administrator.Log("Loaded");
            }
            catch (FileLoadException loadEx)
            {

            }
            catch (BadImageFormatException imgEx)
            {
                Administrator.Log("Not Dll");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void LoadAllManagers()
        {
            _managers = new Dictionary<string, IDimManager>();

            var managerPackers = PacketWarehouse.AllPackets.Where(x => x.HasManager);
            if (managerPackers == null || managerPackers.Count() == 0)
                throw new Exception("No managers to load");

            foreach (var packet in managerPackers)
            {
                if(!_loadedAssemblies.ContainsKey(packet.PacketName))
                {
                    throw new Exception($"Assembly {packet.PacketName} has not been loaded");
                }
                else
                {
                    var ass = _loadedAssemblies[packet.PacketName];
                    var newManager = ass.CreateInstance(packet.PacketManagerName);
                    if (newManager == null)
                        throw new Exception($"Failed to create new manager of name {packet.PacketManagerName}");
                    else if (!(newManager is IDimManager))
                        throw new Exception($"{packet.PacketManagerName} is not an IDimManager.");

                    Administrator.Log($"Loaded IManager {packet.PacketManagerName}.");
                    _managers.Add(packet.PacketManagerName, newManager as IDimManager);
                }
            }
        }

        public bool ValidateAssembly(Assembly assembly)
        {
            return true;
        }
        #endregion

        public void Update()
        {
            foreach (var manager in _managers.Values)
                manager.Update();
        }

        public void RegisterNewInstance(IDimInstance instance, bool TryRegisterManager)
        {
            var instanceType = instance.GetType();

            if (TryRegisterManager)
            {
                var manager = instance.GetManager();
                var managerName = instance.DimDef.ManagerTypeName;

                if (!_managers.ContainsKey(managerName))
                    throw new Exception($"Instance {instance.InstanceId} is requesting unknown manager of type {managerName}.");

                if (instance.GetManager() != null)
                {
                    if (!_managers.Values.Contains(manager))
                        throw new Exception($"Attempted to register instance which has unknown manager");
                    else if(!manager.GetType().Name.Equals(managerName))
                        throw new Exception($"Instance is already registered to wrong manager");
                }
            }

            // See which interfaces on the instance are DimLinks
            var interfaces = instanceType.GetInterfaces().Where(x => x.IsSubclassOf(typeof(IDimUser)));


            // Link to foreign managers
            //foreach(var manager in _managers.Values)
            //{
            //    var linkableTypes = manager.GetLinkableForeignInstanceTypes();
            //    if (linkableTypes.Any(type => instanceType.IsSubclassOf(type) || instanceType == type))
            //        manager.TryLinkForeignInstance(instance);
            //}
        }

        public IDimManager GetPacketManager(DimPacket packet) => GetPacketManager(packet.PacketName);

        public IDimManager GetPacketManager(string PacketName)
        {
            return _managers[PacketName];
        }
        
        public void ListAllInstances()
        {
            foreach (var ass in _loadedAssemblies.Values)
            {
                Log($"_____{ass.FullName}:");
                var instances = ass.GetExportedTypes();//.Where(x => x.IsAssignableFrom(typeof(IDimInstance)));
                foreach (var inst in instances)
                    Log($"      {inst.Name}");
            }

        }
    }
}
