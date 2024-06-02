using System;
using System.Collections.Generic;

namespace ToolVehicleTaxManagementSystemwithStaticProperties
{
    public abstract class ToolVehicle
    {
        public int VehicleID { get; set; }
        public string RegisterationNo { get; set; }
        public string ModelYear { get; set; }
        public string Brand { get; set; }
        public decimal PurchasePrice { get; set; }
        public string VehicleType { get; set; }

        public static int TotalVehicles { get; private set; }
        public static int TotalTaxPaidVehicles { get; private set; }
        public static int TotalNonTaxPaidVehicles { get; private set; }
        public static decimal TotalTaxCollection { get; private set; }

        public ToolVehicle(int vehicleID, string registerationNo, string modelYear, string brand, decimal purchasePrice, string vehicleType)
        {
            VehicleID = vehicleID;
            RegisterationNo = registerationNo;
            ModelYear = modelYear;
            Brand = brand;
            PurchasePrice = purchasePrice;
            VehicleType = vehicleType;
            TotalVehicles++;
        }

        public abstract void PayTax();

        public void PassWithoutPaying()
        {
            TotalNonTaxPaidVehicles++;
        }

        protected void UpdateTaxStatistics(decimal taxAmount)
        {
            TotalTaxCollection += taxAmount;
            TotalTaxPaidVehicles++;
        }
    }

    public class Car : ToolVehicle
    {
        private const decimal CarTaxAmount = 2.0m;

        public Car(int vehicleID, string registerationNo, string modelYear, string brand, decimal purchasePrice)
            : base(vehicleID, registerationNo, modelYear, brand, purchasePrice, "Car")
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

        public Bike(int vehicleID, string registerationNo, string modelYear, string brand, decimal purchasePrice)
            : base(vehicleID, registerationNo, modelYear, brand, purchasePrice, "Bike")
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

        public HeavyVehicle(int vehicleID, string registerationNo, string modelYear, string brand, decimal purchasePrice)
            : base(vehicleID, registerationNo, modelYear, brand, purchasePrice, "HeavyVehicle")
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
                        MakeVehicle();
                        break;
                    case 2:
                        PayingTax();
                        break;
                    case 3:
                        PassWithoutPayingTax();
                        break;
                    case 4:
                        PrintStatisticsForAllVehicles();
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
            Console.WriteLine("1. Make a new vehicle");
            Console.WriteLine("2. Paying tax for a vehicle");
            Console.WriteLine("3. Pass without paying tax for a vehicle");
            Console.WriteLine("4. Print report of statistics for all vehicles");
            Console.WriteLine("5. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static int GetUserChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            return -1;
        }

        static void MakeVehicle()
        {
            Console.Write("\nEnter vehicle type (Add Car/Add Bike/Add HeavyVehicle): ");
            string type = Console.ReadLine();
            Console.Write("Enter Vehicle ID: ");
            int vehicleID = int.Parse(Console.ReadLine());
            Console.Write("Enter Registration Number: ");
            string registerationNo = Console.ReadLine();
            Console.Write("Enter Model Year: ");
            string modelYear = Console.ReadLine();
            Console.Write("Enter Brand: ");
            string brand = Console.ReadLine();
            Console.Write("Enter Purchase Price: ");
            decimal purchasePrice = decimal.Parse(Console.ReadLine());

            ToolVehicle vehicle;
            switch (type.ToLower())
            {
                case "car":
                    vehicle = new Car(vehicleID, registerationNo, modelYear, brand, purchasePrice);
                    break;
                case "bike":
                    vehicle = new Bike(vehicleID, registerationNo, modelYear, brand, purchasePrice);
                    break;
                case "heavyvehicle":
                    vehicle = new HeavyVehicle(vehicleID, registerationNo, modelYear, brand, purchasePrice);
                    break;
                default:
                    Console.WriteLine("Invalid vehicle type. Vehicle not created.");
                    return;
            }
            vehicles.Add(vehicle);
            Console.WriteLine($"\n{type} created successfully.\n");
        }

        static void PayingTax()
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

        static void PassWithoutPayingTax()
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

        static void PrintStatisticsForAllVehicles()
        {
            Console.WriteLine($"\nTotal Vehicles: {ToolVehicle.TotalVehicles}");
            Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPaidVehicles}");
            Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPaidVehicles}");
            Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollection:C}\n");
        }
    }
}
