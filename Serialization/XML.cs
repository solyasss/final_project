using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Storage;

namespace Serialization
{
    public class XMLSerialize : ISerialize
    {
        public void Save(List<StorageDevice> devices, string file_path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<StorageDevice>), new Type[] { typeof(Flash), typeof(DVD), typeof(HDD) });
                using (FileStream fs = new FileStream(file_path, FileMode.Create))
                {
                    serializer.Serialize(fs, devices);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with xml serialization: {ex.Message}");
            }
        }

        public List<StorageDevice> Load(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<StorageDevice>), new Type[] { typeof(Flash), typeof(DVD), typeof(HDD) });
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        return (List<StorageDevice>)serializer.Deserialize(fs);
                    }
                }
                else
                {
                    Console.WriteLine("File {path} not found");
                    return new List<StorageDevice>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with xml serialization: {ex.Message}");
                return new List<StorageDevice>();
            }
        }
    }
}