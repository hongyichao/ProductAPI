using System;

namespace ProductEntity
{
    public class Product
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public decimal Price { get; set;}
        public DateTimeOffset CreatedTimestamp { get; set; }
        public DateTimeOffset UpdatedTimestamp { get; set; }
    }
}
