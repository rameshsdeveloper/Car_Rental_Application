﻿using CarRentalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_CarRental
{
    internal interface ICustomerDetails
    {
        string CustomerId { get; set; } 
        string Name { get; set;  }
        string PhoneNumber { get; set; }
    }
}
