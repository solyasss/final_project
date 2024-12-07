using System;
using System.IO;
using Log;
using Newtonsoft.Json;

namespace Storage
{
    [Serializable]
    [JsonObject]
    public class DVD : StorageDevice
    {
        public double write_speed { get; set; } 

        public DVD() { }

        public DVD(string manufacturer, string model, string name, double capacity, int quantity, double speed)
            : base(manufacturer, model, name, capacity, quantity)
        {
            write_speed = speed;
        }

        public override void generate_report(ILog logger)
        {
            string report = $"DVD Disk:\nManufacturer: {Manufacturer}\nModel: {Model}\nName: {Name}\nCapacity: {Capacity} GB\nQuantity: {Quantity}\nWrite Speed: {write_speed}x";
            logger.print(report);
        }

        public override void load()
        {
            string fileName = $"{Model}_dvd.json";
            try
            {
                if (File.Exists(fileName))
                {
                    string jsonData = File.ReadAllText(fileName);
                    var loadedDVD = JsonConvert.DeserializeObject<DVD>(jsonData);
                    Manufacturer = loadedDVD.Manufacturer;
                    Model = loadedDVD.Model;
                    Name = loadedDVD.Name;
                    Capacity = loadedDVD.Capacity;
                    Quantity = loadedDVD.Quantity;
                    write_speed = loadedDVD.write_speed;
                }
                else
                {
                    Console.WriteLine("File {fileName} not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with uploading data from dvd: {ex.Message}");
            }
        }

        public override void save()
        {
            string fileName = $"{Model}_dvd.json";
            try
            {
                string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(fileName, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with saving data dvd: {ex.Message}");
            }
        }
    }
}
