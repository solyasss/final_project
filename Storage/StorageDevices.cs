using Log;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Storage
{
    [Serializable]
    [XmlInclude(typeof(Flash))]
    [XmlInclude(typeof(DVD))]
    [XmlInclude(typeof(HDD))]
    [JsonObject]
    public abstract class StorageDevice
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public double Capacity { get; set; } 
        public int Quantity { get; set; }

        protected StorageDevice() { }

        protected StorageDevice(string manufacturer, string model, string name, double capacity, int quantity)
        {
            Manufacturer = manufacturer;
            Model = model;
            Name = name;
            Capacity = capacity;
            Quantity = quantity;
        }

        public abstract void generate_report(ILog logger);
        public abstract void load();
        public abstract void save();
    }
}