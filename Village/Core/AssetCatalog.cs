using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core
{
    public abstract class AssetCatalog<T> 
        where T : IAsset
    {
        private Dictionary<string, T> _all = new Dictionary<string, T>();



        public abstract bool TryLoadAllAssets();
        
    }
}
