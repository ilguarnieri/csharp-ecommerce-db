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
        [Column("amount", TypeName = "decimal(8,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderProduct> OrdersProducts { get; set; }


        public Order(int customerId, DateTime date, string status)
        {
            Date = date;
            Status = status;
            CustomerId = customerId;
        }
    }
}
