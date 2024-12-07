using System;
using System.IO;
using Log;
using Newtonsoft.Json;

namespace Storage
{
    [Serializable]
    [JsonObject]
    public class HDD : StorageDevice
    {
        public int hhd_speed { get; set; } 

        public HDD() { }

        public HDD(string manufacturer, string model, string name, double capacity, int quantity, int h_speed)
            : base(manufacturer, model, name, capacity, quantity)
        {
            hhd_speed = h_speed;
        }

        public override void generate_report(ILog logger)
        {
            string report = $"Removable HDD:\nManufacturer: {Manufacturer}\nModel: {Model}\nName: {Name}\nCapacity: {Capacity} GB\nQuantity: {Quantity}\nSpindle Speed: {hhd_speed} RPM";
            logger.print(report);
        }

        public override void load()
        {
            string fileName = $"{Model}_hdd.json";
            try
            {
                if (File.Exists(fileName))
                {
                    string jsonData = File.ReadAllText(fileName);
                    var loadedHDD = JsonConvert.DeserializeObject<HDD>(jsonData);
                    Manufacturer = loadedHDD.Manufacturer;
                    Model = loadedHDD.Model;
                    Name = loadedHDD.Name;
                    Capacity = loadedHDD.Capacity;
                    Quantity = loadedHDD.Quantity;
                    hhd_speed = loadedHDD.hhd_speed;
                }
                else
                {
                    Console.WriteLine($"File {fileName} not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with uploading data in hdd: {ex.Message}");
            }
        }

        public override void save()
        {
            string fileName = $"{Model}_hdd.json";
            try
            {
                string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(fileName, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with saving date hdd: {ex.Message}");
            }
        }
    }
}
