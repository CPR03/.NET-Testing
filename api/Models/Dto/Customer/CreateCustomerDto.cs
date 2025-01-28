using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Dto.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public int? ProductId {get; set;}
    }
}