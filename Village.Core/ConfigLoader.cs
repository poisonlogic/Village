using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Village.Core
{
    public static class ConfigLoader
    {
        public static T LoadConfig<T>(string resourceName)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonString = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            catch(Exception e)
            {
                throw new Exception($"Failed to load config file '{resourceName}'.", e);
            }
        }
    }
}
