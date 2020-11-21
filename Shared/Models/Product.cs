using System.Collections.Generic;

namespace openpos.Shared.Models
{
    public partial class Product
    {
        
        public string Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        //public Category Category { get; set; }
        //public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<Category> CategoriesList { get; set; }
    }
}