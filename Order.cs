using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_ecommerce_db
{
    [Table("orders")]
    internal class Order
    {
        [Key]
        [Column("id")]
        public int OrderId { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Required]
        [Column("amount")]
        public int Amount { get; set; }

        [Required]
        [Column("status")]
        public bool Status { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderProduct> OrdersProducts { get; set; }
    }
}
