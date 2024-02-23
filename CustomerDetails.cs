using CarRentalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_CarRental
{
    internal class CustomerDetails : ICustomerDetails
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public CustomerType CustomerType { get; }
        public CarType CarType { get; }
        public bool AdditionalService { get; }
        public double AdditionalServicePrice { get; }

        public CustomerDetails(string customerId, string name, string phoneNumber, CustomerType customerType, CarType carType, bool additionalService, double additionalServicePrice)
        {
            CustomerId = customerId;
            Name = name;
            PhoneNumber = phoneNumber;
            CustomerType = customerType;
            CarType = carType;
            AdditionalService = additionalService;
            AdditionalServicePrice = additionalServicePrice;
        }


    }
}
