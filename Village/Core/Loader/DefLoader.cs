using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village.Core.Loader
{
    public static class DefLoader
    {
        public static IEnumerable<T> LoadDefs<T>(string dir)
        {
            if (!Directory.Exists(dir))
                throw new Exception(string.Format("DefLoader: Directory does not exist {0}", dir));

            var list = new List<T>();
            var files = Directory.GetFiles(dir).Where(file => file.EndsWith(".def"));



            foreach (var file in files)
            using (StreamReader r = new StreamReader(files.First()))
            {
                string json = r.ReadToEnd();
                list.AddRange( JsonConvert.DeserializeObject<IEnumerable<T>>(json));
            }
            return list;
        }

        public static bool WriteDef<T>(string filePath, IEnumerable<T> defs)
        {
            using (StreamWriter w = new StreamWriter(filePath))
            {
                var json = JsonConvert.SerializeObject(defs);
                w.Write(json);
            }

            return true;
        }
    }
}
