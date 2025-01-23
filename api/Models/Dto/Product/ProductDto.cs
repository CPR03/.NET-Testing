using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Dto
{   

    //NOTE - Will exclude the 'id' column to return of API
    public class ProductDto
    {
        // Primary Key
        // public int Id { get; set; }

        // Product Name
        public string Name { get; set; } = string.Empty;

        // Product Description
        public string Description { get; set; } = string.Empty;
    }
}