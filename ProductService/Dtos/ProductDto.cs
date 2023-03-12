using System;

namespace ProductBusiness.Dtos
{
    public class ProductDto
    {
        public String Name { get; set; }
        public String Group { get; set; }

        public String Description { get; set; }
        public decimal Price { get; set; }
    }
}
