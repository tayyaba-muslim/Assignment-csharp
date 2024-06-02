using System;

public abstract class ToolVehicle
{
    // Properties
    public string RegNo { get; private set; }
    public int VehicleID { get; private set; }
    public string Model { get; private set; }
    public string Brand { get; private set; }
    public decimal BasePrice { get; private set; }
    public string VehicleType { get; private set; }

    // Static Properties
    public static int TotalVehicles { get; private set; }
    public static int TotalTaxPayingVehicles { get; private set; }
    public static int TotalNonTaxPayingVehicles { get; private set; }
    public static decimal TotalTaxCollected { get; private set; }

    // Constructor
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

    // Abstract Method
    public abstract void PayTax();

    // Method for Passing Without Paying Tax
    public void PassWithoutPaying()
    {
        TotalNonTaxPayingVehicles++;
    }

    // Method to Update Tax Statistics
    protected void UpdateTaxStatistics(decimal taxAmount)
    {
        TotalTaxCollected += taxAmount;
        TotalTaxPayingVehicles++;
    }
}
public class Car : ToolVehicle
{
    private const decimal TaxAmount = 2m;

    public Car(int vehicleID, string regNo, string model, string brand, decimal basePrice)
        : base(vehicleID, regNo, model, brand, basePrice, "Car")
    {
    }

    public override void PayTax()
    {
        UpdateTaxStatistics(TaxAmount);
    }
}

public class Bike : ToolVehicle
{
    private const decimal TaxAmount = 1m;

    public Bike(int vehicleID, string regNo, string model, string brand, decimal basePrice)
        : base(vehicleID, regNo, model, brand, basePrice, "Bike")
    {
    }

    public override void PayTax()
    {
        UpdateTaxStatistics(TaxAmount);
    }
}

public class HeavyVehicle : ToolVehicle
{
    private const decimal TaxAmount = 4m;

    public HeavyVehicle(int vehicleID, string regNo, string model, string brand, decimal basePrice)
        : base(vehicleID, regNo, model, brand, basePrice, "HeavyVehicle")
    {
    }

    public override void PayTax()
    {
        UpdateTaxStatistics(TaxAmount);
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<ToolVehicle> vehicles = new List<ToolVehicle>();
        int vehicleCounter = 1;

        while (true)
        {
            Console.WriteLine("Welcome to the Tool Vehicle Tax Management System");
            Console.WriteLine("1. Add a Car");
            Console.WriteLine("2. Add a Bike");
            Console.WriteLine("3. Add a Heavy Vehicle");
            Console.WriteLine("4. Pass a Vehicle Without Paying Tax");
            Console.WriteLine("5. Generate Report");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    vehicles.Add(CreateVehicle(vehicleCounter++, "Car"));
                    break;
                case 2:
                    vehicles.Add(CreateVehicle(vehicleCounter++, "Bike"));
                    break;
                case 3:
                    vehicles.Add(CreateVehicle(vehicleCounter++, "HeavyVehicle"));
                    break;
                case 4:
                    PassVehicleWithoutPaying(vehicles);
                    break;
                case 5:
                    GenerateReport();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static ToolVehicle CreateVehicle(int vehicleID, string vehicleType)
    {
        Console.Write("Enter Registration Number: ");
        string regNo = Console.ReadLine();

        Console.Write("Enter Model: ");
        string model = Console.ReadLine();

        Console.Write("Enter Brand: ");
        string brand = Console.ReadLine();

        Console.Write("Enter Base Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal basePrice))
        {
            Console.WriteLine("Invalid input for base price. Setting to 0.");
            basePrice = 0;
        }

        switch (vehicleType)
        {
            case "Car":
                return new Car(vehicleID, regNo, model, brand, basePrice);
            case "Bike":
                return new Bike(vehicleID, regNo, model, brand, basePrice);
            case "HeavyVehicle":
                return new HeavyVehicle(vehicleID, regNo, model, brand, basePrice);
            default:
                throw new ArgumentException("Invalid vehicle type");
        }
    }

    static void PassVehicleWithoutPaying(List<ToolVehicle> vehicles)
    {
        Console.Write("Enter Vehicle ID to pass without paying tax: ");
        if (!int.TryParse(Console.ReadLine(), out int vehicleID))
        {
            Console.WriteLine("Invalid input. Please enter a valid vehicle ID.");
            return;
        }

        ToolVehicle vehicle = vehicles.Find(v => v.VehicleID == vehicleID);
        if (vehicle != null)
        {
            vehicle.PassWithoutPaying();
            Console.WriteLine($"Vehicle {vehicleID} has passed without paying tax.");
        }
        else
        {
            Console.WriteLine("Vehicle not found.");
        }
    }

    static void GenerateReport()
    {
        Console.WriteLine("Report:");
        Console.WriteLine($"Total Vehicles: {ToolVehicle.TotalVehicles}");
        Console.WriteLine($"Total Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
        Console.WriteLine($"Total Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
        Console.WriteLine($"Total Tax Collected: {ToolVehicle.TotalTaxCollected:C}");
    }
}
