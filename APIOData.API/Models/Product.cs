using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIOData.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public int? FeatureId { get; set; }
        public DateTime? Created { get; set; }
        public virtual Category Category { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
