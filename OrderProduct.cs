using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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





        public static void stampList()
        {
            //stampare lista prodotti con group by sul product id e recupero info dal db dei singoli prodotti
            using (EcommerceContext db = new EcommerceContext())
            {
                
            }
        }


    }
}
