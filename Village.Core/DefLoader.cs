using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Village.Core
{
    public class Def
    {
        public string DefName;
        public string DefClassName;
        public string InstClassName;
    }

    public class Inst
    {
        private string _id;
        private Def _def;

        public string Id => _id ?? throw new Exception("Instance has not been Inited");
        public Def Def => _def ?? throw new Exception("Instance has not been Inited");
        public Inst(Def def)
        {
            _def = def ?? throw new ArgumentNullException(nameof(def));
            _id = Guid.NewGuid().ToString();
        }
    }

    public static class DefLoader
    {
        public static T CreateInstanct<T>(Def def, params object[] args)
        {
            if (def == null)
                throw new ArgumentNullException(nameof(def));
            //try
            //{
                if (def.InstClassName == null)
                    throw new Exception("InstClassName is not defined");

                var type = Type.GetType(def.InstClassName);
                if (type == null)
                    throw new Exception($"No type found for with name '{def.InstClassName}'");

                if (!type.IsSubclassOf(typeof(Inst)))
                    throw new Exception($"Stated type name '{def.InstClassName}' does not inherent from type Init.");


                var constructors = type.GetConstructors().ToList();
                if(!(constructors?.Any() ?? false))
                    throw new Exception($"Failed to find constructor for class '{def.InstClassName}'.");

                if (constructors.Count() > 1)
                    throw new Exception($"InstClass '{def.InstClassName}' has more than one constructor. All Instance classes must have only one contructor.");

                var constructor = constructors.First();
                var constructorParameters = constructor.GetParameters();
                var tempParms = args.Prepend(def).ToArray();

                if (constructorParameters.Count() != tempParms.Count())
                    throw new Exception($"Constructor arguments for class '{def.InstClassName}' does not match the arguments provided. Arguments must be provided to CreateInstance<T> in the same order as the constuctor.");

                for (int n = 0; n < constructorParameters.Count(); n++)
                {
                    var parm = constructorParameters[n];
                    var conParmType = parm.ParameterType;
                    var tempParmType = tempParms[n].GetType();

                    if (!conParmType.IsAssignableFrom(tempParmType)) 
                        throw new Exception($"Constructor arguments for class '{def.InstClassName}' does not match the arguments provided. '{conParmType.Name}' is not assignable from '{tempParmType.Name}'. Arguments must be provided to CreateInstance<T> in the same order as the constuctor.");
                }

                var inst = (T)Activator.CreateInstance(type, args: tempParms);

                return inst;
            //}
            //catch(Exception e)
            //{
            //    throw new Exception($"Failed to create instance of def named '{def.DefName}'", e);
            //}
        }

        public static Dictionary<string, T> LoadDefCatalog<T>(string resourceName) where T : Def
        {
            try
            {
                var outDic = new Dictionary<string, T>();
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonString = reader.ReadToEnd();
                    var jObject = JArray.Parse(jsonString);

                    foreach(var ob in jObject)
                    {
                        var typeName = ob.Value<string>("DefClassName");
                        var defName = ob.Value<string>("DefName");

                        if (string.IsNullOrEmpty(defName))
                            throw new Exception("DefName not defined in one or more defs.");

                        if(string.IsNullOrEmpty(typeName))
                            throw new Exception("DefClassName not defined in one or more defs.");

                        var defType = assembly.GetType(typeName);

                        if (defType == null)
                            throw new Exception($"Failed to find class '{typeName}'.");

                        if (!defType.IsSubclassOf(typeof(Def)))
                            throw new Exception($"Stated type name '{typeName}' does not inherent from type Def.");

                        if (outDic.ContainsKey(defName))
                            throw new Exception($"A def with name '{defName}' has already been loaded.");

                        var def = ob.ToObject(defType);
                        outDic.Add(defName, (T)def);
                    }
                }
                return outDic;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load def file '{resourceName}'.", e);
            }
        }
    }
}
