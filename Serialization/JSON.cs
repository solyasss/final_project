using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Storage;

namespace Serialization
{
    public class JSONSerialize : ISerialize
    {
        public void Save(List<StorageDevice> devices, string path)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented
                };
                string json_string = JsonConvert.SerializeObject(devices, settings);
                File.WriteAllText(path, json_string);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with saving: {ex.Message}");
            }
        }

        public List<StorageDevice> Load(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string json_string_2 = File.ReadAllText(path);
                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };
                    return JsonConvert.DeserializeObject<List<StorageDevice>>(json_string_2, settings);
                }
                else
                {
                    Console.WriteLine("File {path} not found");
                    return new List<StorageDevice>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with uploading in file: {ex.Message}");
                return new List<StorageDevice>();
            }
        }
    }
}