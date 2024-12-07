using System;
using Log;
using Price;
using Serialization;
using Storage;

namespace storage_final
{
    class Program
    {
        static void Main(string[] args)
        {
            var price_list = new PriceList();
            ILog logger = new ConsoleLog();
            ISerialize serializer = null;
            string file_path = "devices_list";

            while (true)
            {
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1. Add device");
                Console.WriteLine("2. Remove device");
                Console.WriteLine("3. Print device list");
                Console.WriteLine("4. Update device parameters");
                Console.WriteLine("5. Search device");
                Console.WriteLine("6. Save data to file");
                Console.WriteLine("7. Load data from file");
                Console.WriteLine("0. Exit");

                Console.Write("Enter action number: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        add_device(price_list);
                        break;
                    case "2":
                        remove_device(price_list);
                        break;
                    case "3":
                        price_list.print(logger);
                        break;
                    case "4":
                        update_device(price_list);
                        break;
                    case "5":
                        search_device(price_list, logger);
                        break;
                    case "6":
                        serializer = choose_serializer();
                        if (serializer != null)
                        {
                            file_path = get_file_path(serializer);
                            price_list.save(serializer, file_path);
                        }
                        break;
                    case "7":
                        serializer = choose_serializer();
                        if (serializer != null)
                        {
                            file_path = get_file_path(serializer);
                            price_list.load(serializer, file_path);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static ISerialize choose_serializer()
        {
            Console.WriteLine("\nSelect serialization method:");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. SOAP");
            Console.WriteLine("3. XML");

            Console.Write("Enter method number: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return new JSONSerialize();
                case "2":
                    return new SoapSerialize();
                case "3":
                    return new XMLSerialize();
                default:
                    Console.WriteLine("Invalid choice.");
                    return null;
            }
        }

        static string get_file_path(ISerialize serializer)
        {
            if (serializer is JSONSerialize)
                return "devices_list.json";
            else if (serializer is SoapSerialize)
                return "devices_list.soap";
            else if (serializer is XMLSerialize)
                return "devices_list.xml";
            else
                return "devices_list.dat";
        }

        static void add_device(PriceList price_list)
        {
            Console.WriteLine("\nSelect device type to add:");
            Console.WriteLine("1. Flash");
            Console.WriteLine("2. DVD");
            Console.WriteLine("3. Removable HDD");

            Console.Write("Enter type number: ");
            string type_choice = Console.ReadLine();

            try
            {
                Console.Write("Manufacturer: ");
                string manufacturer = Console.ReadLine();
                Console.Write("Model: ");
                string model = Console.ReadLine();
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Capacity: ");
                double capacity = double.Parse(Console.ReadLine());
                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                switch (type_choice)
                {
                    case "1":
                        Console.Write("USB speed: ");
                        double usb_speed = double.Parse(Console.ReadLine());
                        var flash = new Flash(manufacturer, model, name, capacity, quantity, usb_speed);
                        price_list.add_device(flash);
                        flash.save();
                        break;
                    case "2":
                        Console.Write("Write speed: ");
                        double write_speed = double.Parse(Console.ReadLine());
                        var dvd = new DVD(manufacturer, model, name, capacity, quantity, write_speed);
                        price_list.add_device(dvd);
                        dvd.save();
                        break;
                    case "3":
                        Console.Write("Spindle speed: ");
                        int spindle_speed = int.Parse(Console.ReadLine());
                        var hdd = new HDD(manufacturer, model, name, capacity, quantity, spindle_speed);
                        price_list.add_device(hdd);
                        hdd.save();
                        break;
                    default:
                        Console.WriteLine("Invalid device type choice");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        static void remove_device(PriceList price_list)
        {
            try
            {
                Console.Write("Enter device index to remove: ");
                int index = int.Parse(Console.ReadLine());
                var device = price_list.GetDevice(index);
                price_list.remove_device(d => d == device);
                Console.WriteLine("Device removed");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        static void update_device(PriceList price_list)
        {
            try
            {
                Console.Write("Enter device index to update: ");
                int index = int.Parse(Console.ReadLine());
                var device = price_list.GetDevice(index);

                Console.WriteLine("Select parameter to update:");
                Console.WriteLine("1. Manufacturer");
                Console.WriteLine("2. Model");
                Console.WriteLine("3. Name");
                Console.WriteLine("4. Capacity");
                Console.WriteLine("5. Quantity");
                Console.WriteLine("6. Special parameter");

                Console.Write("Enter parameter number: ");
                string param_choice = Console.ReadLine();

                Console.Write("Enter new value: ");
                string new_value = Console.ReadLine();

                price_list.update(index, d =>
                {
                    switch (param_choice)
                    {
                        case "1":
                            d.Manufacturer = new_value;
                            break;
                        case "2":
                            d.Model = new_value;
                            break;
                        case "3":
                            d.Name = new_value;
                            break;
                        case "4":
                            d.Capacity = double.Parse(new_value);
                            break;
                        case "5":
                            d.Quantity = int.Parse(new_value);
                            break;
                        case "6":
                            if (d is Flash flash)
                            {
                                flash.usb_speed = double.Parse(new_value);
                            }
                            else if (d is DVD dvd)
                            {
                                dvd.write_speed = double.Parse(new_value);
                            }
                            else if (d is HDD hdd)
                            {
                                hdd.hhd_speed = int.Parse(new_value);
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid parameter choice");
                            break;
                    }
                });

                Console.WriteLine("Device updated");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }

        static void search_device(PriceList price_list, ILog logger)
        {
            Console.WriteLine("Select search criteria:");
            Console.WriteLine("1. By manufacturer");
            Console.WriteLine("2. By model");
            Console.WriteLine("3. By capacity greater than specified");
            Console.WriteLine("4. By quantity less than specified");

            Console.Write("Enter criterion number: ");
            string criterion = Console.ReadLine();

            Console.Write("Enter search value: ");
            string search_value = Console.ReadLine();

            var results = new System.Collections.Generic.List<StorageDevice>();

            try
            {
                switch (criterion)
                {
                    case "1":
                        results = price_list.Search(d => d.Manufacturer.Equals(search_value, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "2":
                        results = price_list.Search(d => d.Model.Equals(search_value, StringComparison.OrdinalIgnoreCase));
                        break;
                    case "3":
                        double capacity = double.Parse(search_value);
                        results = price_list.Search(d => d.Capacity > capacity);
                        break;
                    case "4":
                        int quantity = int.Parse(search_value);
                        results = price_list.Search(d => d.Quantity < quantity);
                        break;
                    default:
                        Console.WriteLine("Invalid criteria choice");
                        return;
                }

                if (results.Count > 0)
                {
                    logger.print("Search results:");
                    foreach (var device in results)
                    {
                        device.generate_report(logger);
                        logger.print("-------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No devices found");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }
    }
}
