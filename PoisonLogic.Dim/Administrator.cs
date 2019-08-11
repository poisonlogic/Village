//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace PoisonLogic.Dim
//{
//    public class Administrator
//    {
//        public static string PackageDirectory;

//        private Dictionary<string, IDimManager> _managers;
//        private List<Assembly> _loadedAssemblies;

//        public List<IDimManager> AllManagers;

//        public Administrator(string packageDirectory)
//        {
//            _managers = new Dictionary<string, IDimManager>();
//            PackageDirectory = packageDirectory;
//        }

//        public void LoadAllManagers()
//        {
//            if (_loadedAssemblies == null)
//                throw new Exception("Packages must be loaded before managers");

//            foreach (var ass in _loadedAssemblies)
//            {
//                var allTypes = ass.GetTypes();
//                var managers = ass.GetTypes().Where(type => type is IDimManager);
//                if (managers.Any())
//                {
//                    if (managers.Count() > 1)
//                        throw new Exception($"Package {ass.FullName} includes more that one manager. Only one IDimManager is allowed per package.");

//                    var managerType = managers.Single();

//                    Console.WriteLine($"Loading manager: {managerType.FullName}");
//                    var manager = ass.CreateInstance(managerType.FullName) as IDimManager;
//                    if (manager != null)
//                        _managers.Add(ass.FullName, manager);
//                }
//                else
//                {
//                    Console.WriteLine($"No managers found in {ass.FullName}");
//                }
//            }
//        }

//        public void Update()
//        {
//            foreach (var manager in _managers.Values)
//                manager.Update();
//        }

//        public void RegisterNewInstance(IDimInstance instance)
//        {
//            var instanceType = instance.GetType();
            
//            foreach(var manager in _managers.Values)
//            {
//                var linkableTypes = manager.GetLinkableForeignInstanceTypes();
//                if (linkableTypes.Any(type => instanceType.IsSubclassOf(type) || instanceType == type))
//                    manager.TryLinkForeignInstance(instance);
//            }
//        }

//        public void LoadAllPackages()
//        {
//            _loadedAssemblies = new List<Assembly>();
//            foreach (string dll in Directory.GetFiles(PackageDirectory, "*.dll", SearchOption.AllDirectories))
//            {
//                try
//                {
//                    var fileName = dll.Split('\\').Last();
//                    Console.WriteLine($"Loading package '{fileName}'");
//                    Assembly loadedAssembly = Assembly.LoadFile(dll);
//                    _loadedAssemblies.Add(loadedAssembly);
//                    Console.WriteLine("Loaded");
//                }
//                catch (FileLoadException loadEx)
//                { Console.Write("Already Loaded"); } // The Assembly has already been loaded.
//                catch (BadImageFormatException imgEx)
//                { Console.Write("Not Dll"); } // If a BadImageFormatException exception is thrown, the file is not an assembly.

//            }
//        }
//    }
//}
