using System;
using System.Collections.Generic;
using System.Linq;
using Serialization;
using Storage;

namespace Price
{
    public class PriceList  //управляю списком устройств хранения
    {
        private List<StorageDevice> dev = new List<StorageDevice>();  // список устройств

        public void add_device(StorageDevice device)
        {
            dev.Add(device);
        }

        public void remove_device(Func<StorageDevice, bool> pred)  // pred для поиска устройств которые нужно удалить
        {
            dev.RemoveAll(new Predicate<StorageDevice>(pred));
        }

        public List<StorageDevice> Search(Func<StorageDevice, bool> pred) //поиск устройств хранения в списке
        {
            return dev.Where(pred).ToList();
        }

        public void print(Log.ILog logger)
        {
            if (dev.Count == 0)
            {
                logger.print("The list of devices is empty");
                return;
            }

            for (int i = 0; i < dev.Count; i++)  //перебираю устройства и вывожу инфо 
            {
                logger.print($"Index: {i}");
                dev[i].generate_report(logger); // отчет для устройств
                logger.print("-------------------------------");
            }
        }

        public void update(int index, Action<StorageDevice> update_action)  //обновляю устройство в списке по индексу 
        {
            if (index >= 0 && index < dev.Count)
            {
                update_action(dev[index]);
            }
            else
            {
                Console.WriteLine("Cant find device with such name");
            }
        }

        public void save(ISerialize serializer, string file_path)
        {
            serializer.Save(dev, file_path);
        }

        public void load(ISerialize serializer, string file_path)
        {
            dev = serializer.Load(file_path);
        }

        public int get_device_count() //возвращаю количество устройств в списке
        {
            return dev.Count;
        }

        public StorageDevice GetDevice(int index)  //возвращаю устройство из списка по индексу
        {
            if (index >= 0 && index < dev.Count)
            {
                return dev[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Cant find device with such index");
            }
        }
    }
}
