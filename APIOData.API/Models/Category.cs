using System.Collections.Generic;

namespace APIOData.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set;}
    }
}
