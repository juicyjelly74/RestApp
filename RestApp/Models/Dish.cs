using System.Data.Entity;

namespace RestApp.Models
{
    public class Dish
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public string Category { get;  set;}
       
    }
    public class RestaurantDBContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
    }
}