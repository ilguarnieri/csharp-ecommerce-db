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
        [Column("price", TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        public List<OrderProduct> OrdersProducts { get; set; }


        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }


        public static void AddProduct(string nameProduct, string description, string price)
        {
            using(EcommerceContext db = new EcommerceContext())
            {
                Product prd = new Product(nameProduct, description, decimal.Parse(price));
                db.Add(prd);
                db.SaveChanges();
            }
        }



        //ELIMINA PRODOTTO DAL DB
        public static void removeProduct(Product product)
        {
            using (EcommerceContext db = new EcommerceContext())
            {
                db.Remove(product);
                db.SaveChanges();
            }
        }


    }
}
