using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Dto.Product
{
    public class UpdateProductDto
    {

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}