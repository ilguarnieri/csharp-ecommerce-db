using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_ecommerce_db
{
    [Table("products")]
    internal class Product
    {
        [Key]
        [Column("id")]
        public int ProductId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [Column("price", TypeName = "double(6,2)")]
        public double Price { get; set; }

        public List<OrderProduct> OrdersProducts { get; set; }


        public Product(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
