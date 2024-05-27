using System;
using System.Collections.Generic;

namespace ToolVehicleTaxManagementSystemwithStaticProperties
{

    public abstract class ToolVehicle
    {
        public int VehicleID { get; set; }
        public string RegNo { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public decimal BasePrice { get; set; }
        public string VehicleType { get; set; }

        public static int TotalVehicles { get; private set; }
        public static int TotalTaxPayingVehicles { get; private set; }
        public static int TotalNonTaxPayingVehicles { get; private set; }
        public static decimal TotalTaxCollected { get; private set; }

        public ToolVehicle(int vehicleID, string regNo, string model, string brand, decimal basePrice, string vehicleType)
        {
            VehicleID = vehicleID;
            RegNo = regNo;
            Model = model;
            Brand = brand;
            BasePrice = basePrice;
            VehicleType = vehicleType;
            TotalVehicles++;
        }

        public abstract void PayTax();

        public void PassWithoutPaying()
        {
            TotalNonTaxPayingVehicles++;
        }

        protected void UpdateTaxStatistics(decimal taxAmount)
        {
            TotalTaxCollected += taxAmount;
            TotalTaxPayingVehicles++;
        }
    }

    public class Car : ToolVehicle
    {
        private const decimal CarTaxAmount = 2.0m;

        public Car(int vehicleID, string regNo, string model, string brand, decimal basePrice)
            : base(vehicleID, regNo, model, brand, basePrice, "Car")
        {
        }

        public override void PayTax()
        {
            UpdateTaxStatistics(CarTaxAmount);
        }
    }

    public class Bike : ToolVehicle
    {
        private const decimal BikeTaxAmount = 1.0m;

        public Bike(int vehicleID, string regNo, string model, string brand, decimal basePrice)
            : base(vehicleID, regNo, model, brand, basePrice, "Bike")
        {
        }

        public override void PayTax()
        {
            UpdateTaxStatistics(BikeTaxAmount);
        }
    }

    public class HeavyVehicle : ToolVehicle
    {
        private const decimal HeavyVehicleTaxAmount = 4.0m;

        public HeavyVehicle(int vehicleID, string regNo, string model, string brand, decimal basePrice)
            : base(vehicleID, regNo, model, brand, basePrice, "HeavyVehicle")
        {
        }

        public override void PayTax()
        {
            UpdateTaxStatistics(HeavyVehicleTaxAmount);
        }
    }

    class Program
    {
        static List<ToolVehicle> vehicles = new List<ToolVehicle>();

        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
                int choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        CreateVehicle();
                        break;
                    case 2:
                        PayTax();
                        break;
                    case 3:
                        PassWithoutPaying();
                        break;
                    case 4:
                        PrintStatistics();
                        break;
                    case 5:
                        Console.WriteLine("\nExiting... \n");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("Vehicle Tax Management System \n");
            Console.WriteLine("1. Create a new vehicle");
            Console.WriteLine("2. Pay tax for a vehicle");
            Console.WriteLine("3. Pass without paying tax for a vehicle");
            Console.WriteLine("4. Print report of statistics");
            Console.WriteLine("5. Exit");
            Console.Write("\n Enter your choice: ");
        }

        static int GetUserChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            return -1;
        }

        static void CreateVehicle()
        {
            Console.Write("\nEnter vehicle type (Car/Bike/HeavyVehicle):");
            string type = Console.ReadLine();
            Console.Write("\nEnter Vehicle ID: ");
            int vehicleID = int.Parse(Console.ReadLine());
            Console.Write("Enter Registration Number: ");
            string regNo = Console.ReadLine();
            Console.Write("Enter Model: ");
            string model = Console.ReadLine();
            Console.Write("Enter Brand: ");
            string brand = Console.ReadLine();
            Console.Write("Enter Base Price: ");
            decimal basePrice = decimal.Parse(Console.ReadLine() + "\n");

            ToolVehicle vehicle;
            switch (type.ToLower())
            {
                case "car":
                    vehicle = new Car(vehicleID, regNo, model, brand, basePrice);
                    break;
                case "bike":
                    vehicle = new Bike(vehicleID, regNo, model, brand, basePrice);
                    break;
                case "heavyvehicle":
                    vehicle = new HeavyVehicle(vehicleID, regNo, model, brand, basePrice);
                    break;
                default:
                    Console.WriteLine("Invalid vehicle type. Vehicle not created.");
                    return;
            }
            vehicles.Add(vehicle);
            Console.WriteLine($"\n{type} created successfully.\n");
        }

        static void PayTax()
        {
            ToolVehicle vehicle = GetVehicleByID();
            if (vehicle != null)
            {
                vehicle.PayTax();
                Console.WriteLine("Tax paid successfully.");
            }
            else
            {
                Console.WriteLine("\nVehicle not found.\n");
            }
        }

        static void PassWithoutPaying()
        {
            ToolVehicle vehicle = GetVehicleByID();
            if (vehicle != null)
            {
                vehicle.PassWithoutPaying();
                Console.WriteLine("Vehicle passed without paying tax.");
            }
            else
            {
                Console.WriteLine("\nVehicle not found.\n");
            }
        }

        static ToolVehicle GetVehicleByID()
        {
            Console.Write("\nEnter Vehicle ID: ");
            int vehicleID = int.Parse(Console.ReadLine());
            return vehicles.Find(v => v.VehicleID == vehicleID);
        }

        static void PrintStatistics()
        {
            Console.WriteLine($"\nTotal Vehicles: {ToolVehicle.TotalVehicles}");
            Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
            Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
            Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollected:C}\n");
        }
    }

}
