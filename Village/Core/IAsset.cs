using System;
using System.Collections.Generic;
using System.Text;

namespace Village.Core
{
    public interface IAsset
    {
        bool Instanceable { get; }
        string AssetName { get; }
        IEnumerable<string> Tags { get; }
    }
}
