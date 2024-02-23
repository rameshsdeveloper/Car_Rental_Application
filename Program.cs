using Assignment2_CarRental;
using System;
using System.Collections.Generic;

namespace CarRentalSystem
{
    class Program
    {
        static readonly List<CustomerDetails> reservations = new List<CustomerDetails>();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit) //Menu choices for the application
            {
                Console.WriteLine("Choose an option below:");
                Console.WriteLine("1. Create a reservation");
                Console.WriteLine("2. List all reservations");
                Console.WriteLine("3. Clear all reservations");
                Console.WriteLine("4. Exit the program");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreateReservation();
                            break;
                        case 2:
                            ListAllReservations();
                            break;
                        case 3:
                            ClearAllReservations();
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else //Error Handling
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                Console.WriteLine();
            }
        }

        static void CreateReservation() // Creating new reservation with customer details
        {
            try
            {
                Console.WriteLine("Enter customer information:");
                Console.Write("Customer ID: ");
                string? customerId = Console.ReadLine();
                Console.Write("Name: ");
                string? name = Console.ReadLine();
                Console.Write("Phone Number: ");
                string? phoneNumber = Console.ReadLine();
                //Choose the Customer Type
                Console.Write("Customer Type (0 for Regular, 1 for Premium, 2 for VIP): ");
                int customerType;
                if (!int.TryParse(Console.ReadLine(), out customerType) || customerType < 0 || customerType > 2)
                {
                    Console.WriteLine("Invalid customer type. Reservation failed.");
                    return;
                }

                //Car types and rental amount
                Console.WriteLine("Choose the number corresponding to the car type the customer wants:");
                Console.WriteLine("1. Economy - $29.99/day");
                Console.WriteLine("2. Standard - $49.99/day");
                Console.WriteLine("3. Luxury - $79.99/day");
                int carChoice;
                if (!int.TryParse(Console.ReadLine(), out carChoice) || carChoice < 1 || carChoice > 3)
                {
                    Console.WriteLine("Invalid car choice. Reservation failed.");
                    return;
                }

                CarType carType = (CarType)(carChoice - 1);

                bool additionalService = false;
                double additionalServicePrice = 0;

                // Offers one additional service depending on the customer type
                switch ((CustomerType)customerType)
                {
                    case CustomerType.Regular:
                        Console.WriteLine("Does the customer want GPS Navigation - $9.99/day? (yes/no)");
                        additionalService = Console.ReadLine() == "yes";
                        additionalServicePrice = 9.99;
                        break;
                    case CustomerType.Premium:
                        Console.WriteLine("Does the customer want Child Car Seat - $14.99/day? (yes/no)");
                        additionalService = Console.ReadLine() == "yes";
                        additionalServicePrice = 14.99;
                        break;
                    case CustomerType.VIP:
                        Console.WriteLine("Does the customer want Chauffeur Service - $99.99/day? (yes/no)");
                        additionalService = Console.ReadLine() == "yes";
                        additionalServicePrice = 99.99;
                        break;
                }

                CustomerDetails customerDetails = new CustomerDetails(customerId!, name!, phoneNumber!, (CustomerType)customerType, carType, additionalService, additionalServicePrice);
                reservations.Add(customerDetails);

                Console.WriteLine("Thank you! The reservation was successful.");
            }
            catch (Exception ex) //Error Handling
            {
                Console.WriteLine($"Error: {ex.Message}\nPlease try again.\n");
            }
        }

        static void ListAllReservations() // List the all reservations
        {
            try
            {
                if (reservations.Count == 0)
                {
                    Console.WriteLine("No reservations found.");
                    return;
                }

                for (int i = 0; i < reservations.Count; i++)
                {
                    Console.WriteLine($"Reservation {i + 1}:");
                    Console.WriteLine($"Customer ID: {HideCustomerId(reservations[i].CustomerId)}");
                    Console.WriteLine($"Name: {reservations[i].Name}");
                    Console.WriteLine($"Phone Number: {reservations[i].PhoneNumber}");
                    Console.WriteLine($"Customer Type: {reservations[i].CustomerType}");
                    Console.WriteLine($"Car Type: {reservations[i].CarType}");
                    if (reservations[i].AdditionalService)
                    {
                        Console.WriteLine($"Additional Service: {GetAdditionalServiceName(reservations[i].CustomerType)}");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"Total Price: ${CalculateTotalPrice()}");
            }
            catch (Exception ex) //Error handling
            {
                Console.WriteLine($"Error: {ex.Message}\nPlease try again.\n");
            }
        }

        static void ClearAllReservations() // Clear all reservations
        {
            reservations.Clear();
            Console.WriteLine("All reservations cleared.");
        }

        static string HideCustomerId(string customerId) // Display CustomerID with given format
        {
            return "XXX" + customerId.Substring(3);
        }

        static string GetAdditionalServiceName(CustomerType customerType) //Offers one additional service depending on the customer type
        {
            switch (customerType)
            {
                case CustomerType.Regular:
                    return "GPS Navigation";
                case CustomerType.Premium:
                    return "Child Car Seat";
                case CustomerType.VIP:
                    return "Chauffeur Service";
                default:
                    return "Unknown";
            }
        }

        static double CalculateTotalPrice() // Calculate the Total price 
        {
            double totalPrice = 0;
            foreach (var reservation in reservations)
            {
                double carRentalPrice = 0;
                switch (reservation.CarType)
                {
                    case CarType.Economy:
                        carRentalPrice = 29.99;
                        break;
                    case CarType.Standard:
                        carRentalPrice = 49.99;
                        break;
                    case CarType.Luxury:
                        carRentalPrice = 79.99;
                        break;
                }

                double additionalServicePrice = reservation.AdditionalService ? reservation.AdditionalServicePrice : 0;
                totalPrice += carRentalPrice + additionalServicePrice;
            }
            return totalPrice;
        }
    }

    enum CustomerType //enum for CustomerType
    {
        Regular,
        Premium,  
        VIP
    }

    enum CarType //enum for CarType
    {
        Economy,
        Standard,
        Luxury
    }
}
