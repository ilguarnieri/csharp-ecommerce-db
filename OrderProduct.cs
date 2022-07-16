using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace csharp_ecommerce_db
{
    [Table("order_product")]
    internal class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("order_id")]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }



        public OrderProduct(int productId, int orderID, int quantity)
        {
            ProductId = productId;
            OrderID = orderID;
            Quantity = quantity;
        }




        public static List<Product> stampListProduct()
        {
            //stampare lista prodotti con group by sul product id e recupero info dal db dei singoli prodotti

            List<Product> productsList;

            Console.WriteLine("* * * LISTA PRODOTTI POSSIBILI DA CANCELLARE * * *");

            using (EcommerceContext db = new EcommerceContext())
            {
                
                String query = "SELECT cr.product_id AS id, p.name, p.description, p.price FROM products p INNER JOIN (SELECT DISTINCT product_id FROM order_product INNER JOIN products ON order_product.product_id = products.id) cr ON p.id = cr.product_id";

                productsList = db.Products.FromSqlRaw(query).ToList();

                int i = 1;
                foreach (Product product in productsList)
                {
                    Console.WriteLine($"{i}-\t{product.Name}\t{product.Price}\t{product.Description}");

                    i++;
                }                
            }

            return productsList;
        }



    }
}
