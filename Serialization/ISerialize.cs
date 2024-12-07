using System;
using System.Collections.Generic;
using Storage;

namespace Serialization
{
    public interface ISerialize
    {
        void Save(List<StorageDevice> devices, string path);
        List<StorageDevice> Load(string path);
    }
}