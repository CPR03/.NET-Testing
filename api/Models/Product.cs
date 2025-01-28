using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Product
    {

        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Product Name
        public string Name { get; set; } = string.Empty;

        // Product Description
        public string Description { get; set; } = string.Empty;

        // Customers List
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}