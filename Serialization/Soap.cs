using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using Storage;

namespace Serialization
{
    public class SoapSerialize : ISerialize
    {
        public void Save(List<StorageDevice> devices, string file_path)
        {
            try
            {
                using (FileStream fs = new FileStream(file_path, FileMode.Create))
                {
                    SoapFormatter formatter = new SoapFormatter();
                    formatter.Serialize(fs, devices);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with soap serialization: {ex.Message}");
            }
        }

        public List<StorageDevice> Load(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        SoapFormatter formatter = new SoapFormatter();
                        return (List<StorageDevice>)formatter.Deserialize(fs);
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
                Console.WriteLine("Error with soap serialization: {ex.Message}");
                return new List<StorageDevice>();
            }
        }
    }
}