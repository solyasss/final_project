using System;
using System.IO;
using Log;
using Newtonsoft.Json;

namespace Storage
{
    [Serializable]
    [JsonObject]
    public class Flash : StorageDevice
    {
        public double usb_speed { get; set; } 

        public Flash() { }

        public Flash(string manufacturer, string model, string name, double capacity, int quantity, double u_speed)
            : base(manufacturer, model, name, capacity, quantity)
        {
            usb_speed = u_speed;
        }

        public override void generate_report(ILog logger)
        {
            string report = $"Flash Memory:\nManufacturer: {Manufacturer}\nModel: {Model}\nName: {Name}\nCapacity: {Capacity} GB\nQuantity: {Quantity}\nUSB Speed: {usb_speed} MB/s";
            logger.print(report);
        }

        public override void load()
        {
            string fileName = $"{Model}_flash.json";
            try
            {
                if (File.Exists(fileName))
                {
                    string jsonData = File.ReadAllText(fileName);
                    var loadedFlash = JsonConvert.DeserializeObject<Flash>(jsonData);
                    Manufacturer = loadedFlash.Manufacturer;
                    Model = loadedFlash.Model;
                    Name = loadedFlash.Name;
                    Capacity = loadedFlash.Capacity;
                    Quantity = loadedFlash.Quantity;
                    usb_speed = loadedFlash.usb_speed;
                }
                else
                {
                    Console.WriteLine("File{fileName} not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with uploading data in flash: {ex.Message}");
            }
        }

        public override void save()
        {
            string fileName = $"{Model}_flash.json";
            try
            {
                string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(fileName, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with saving data flash: {ex.Message}");
            }
        }
    }
}
