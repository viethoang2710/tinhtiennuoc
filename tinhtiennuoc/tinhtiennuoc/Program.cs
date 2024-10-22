using System;
using System.Collections.Generic;
using System.Linq;

class Customer
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int PeopleCount { get; set; }
}

class WaterBillSystem
{
    // List to store customers
    static List<Customer> customers = new List<Customer>();

    // Method to calculate water bill based on customer type and water usage
    public static double CalculateBill(string customerType, double waterUsage, int peopleCount = 1)
    {
        double totalBill = 0;
        double vatRate = 0.10; // VAT 10%

        switch (customerType.ToLower())
        {
            case "household":
                if (waterUsage <= 10 * peopleCount)
                    totalBill = waterUsage * 5.973;
                else if (waterUsage <= 20 * peopleCount)
                    totalBill = waterUsage * 7.052;
                else if (waterUsage <= 30 * peopleCount)
                    totalBill = waterUsage * 8.699;
                else
                    totalBill = waterUsage * 15.929;
                break;

            case "administrative":
                totalBill = waterUsage * 9.955;
                break;

            case "production":
                totalBill = waterUsage * 11.615;
                break;

            case "business":
                totalBill = waterUsage * 22.068;
                break;

            default:
                Console.WriteLine("Invalid customer type.");
                return -1;
        }

        // Add 10% environmental protection fee 
        totalBill += totalBill * vatRate;
        return totalBill;
    }

    // Add a new customer
    static void AddCustomer()
    {
        Console.WriteLine("Enter Customer ID:");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter Customer Name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter Customer Type (Household/Administrative/Production/Business):");
        string type = Console.ReadLine();

        int peopleCount = 1;
        if (type.ToLower() == "household")
        {
            Console.WriteLine("Enter number of people in the household:");
            peopleCount = Convert.ToInt32(Console.ReadLine());
        }

        customers.Add(new Customer { ID = id, Name = name, Type = type, PeopleCount = peopleCount });
        Console.WriteLine("Customer added successfully!\n");
    }

   
    static void ViewWaterBill()
    {
        Console.WriteLine("Enter Customer ID:");
        int id = Convert.ToInt32(Console.ReadLine());

        // Search for the customer by ID
        var customer = customers.FirstOrDefault(c => c.ID == id);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        // Input water meter readings
        Console.WriteLine("Enter last month's water meter reading (in cubic meters):");
        double lastMonthReading = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter this month's water meter reading (in cubic meters):");
        double thisMonthReading = Convert.ToDouble(Console.ReadLine());

        // Calculate water usage
        double waterUsage = thisMonthReading - lastMonthReading;
        if (waterUsage < 0)
        {
            Console.WriteLine("Error: Current meter reading is less than last month's reading.");
            return;
        }

        // Calculate the water bill
        double totalBill = CalculateBill(customer.Type, waterUsage, customer.PeopleCount);

        // Water bill
        if (totalBill >= 0)
        {
            Console.WriteLine("**********WATER BILL SUMMARY**********");
            Console.WriteLine($"Customer Name        : {customer.Name}");
            Console.WriteLine($"Customer Type        : {customer.Type}");
            Console.WriteLine($"Last Month's Reading : {lastMonthReading} cubic meters");
            Console.WriteLine($"This Month's Reading : {thisMonthReading} cubic meters");
            Console.WriteLine($"Water Usage          : {waterUsage} cubic meters");
            Console.WriteLine($"Total Bill (incl. VAT): {totalBill:C2} VND");
            Console.WriteLine("***************************************");
          
        }
    }

    // Sort customers by ID
    static void SortCustomersByID()
    {
        customers = customers.OrderBy(c => c.ID).ToList();
        Console.WriteLine("\nSorted customer list:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.ID}, Name: {customer.Name}, Type: {customer.Type}");
        }
    }

    static void Main(string[] args)
    {
        while (true)
        {
            // Display menu
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. View Water Bill");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Sort Customers by ID");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewWaterBill();
                    break;
                case "2":
                    AddCustomer();
                    break;
                case "3":
                    SortCustomersByID();
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }
}
