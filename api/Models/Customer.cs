using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Customer
    {
        public int Id { get; set; }

        // Customer Name
        public string Name { get; set; } = string.Empty;

        // Customer Email
        public string Email { get; set; } = string.Empty;
    }
}