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

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [Column("amount", TypeName = "decimal(8,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; }

        [Required]
        [Column("customer_id")]

        public List<OrderProduct> OrdersProducts { get; set; }


        public Order(int customerId, DateTime date, string status, decimal amount)
        {
            Date = date;
            Status = status;
            CustomerId = customerId;
            this.Amount = amount;
        }




        //recuperare la lista di tutti gli ordini effettuati da un cliente
        public static List<Order> ListOrder(int userId)
        {
            List<Order> customerOrders;

            using (EcommerceContext db = new EcommerceContext())
            {
                Customer customer = db.Customers.Where(customer => customer.CustomerId == userId).First();
                customerOrders = db.Orders.Where(orders => orders.CustomerId == userId).ToList();

                Console.WriteLine($"{customer.Name} {customer.Surname} ha effettuato {customerOrders.Count} ordini");

                int i = 1;
                foreach (Order order in customerOrders)
                {
                    Console.WriteLine($"\n {i}\t* * * Ordine n.{order.OrderId} * * *");
                    Console.WriteLine($"\t{order.Date.ToString("d")}\t Totale: {order.Amount}\t ->{order.Status}");

                    i++;
                }
            }

            return customerOrders;
        }



        //rimuove ordine dal DB
        public static void removeOrder(Order order)
        {
            using (EcommerceContext db = new EcommerceContext())
            {
                db.Remove(order);
                db.SaveChanges();
            }
        }




        //modifica stato ordine DB
        public static void modifyOrderStatus(Order order)
        {
            using (EcommerceContext db = new EcommerceContext())
            {
                db.Update(order);
                db.SaveChanges();
            }
        }






        //CREAZIONE 5 ORDINI CON UTENTI E PRODOTTI CASUALI
        public static void AddCasualOrder()
        {
            using (EcommerceContext db = new EcommerceContext())
            {

                List<Customer> customers = db.Customers.ToList();
                List<Product> products = db.Products.ToList();


                int numberOrder = 0;

                //creare 5 ordini
                do
                {
                    //scelta id utente random
                    int customerIdRandom = new Random().Next(1, customers.Count + 1);

                    //dati utente
                    string customerName = customers[customerIdRandom - 1].Name;
                    string customerSurname = customers[customerIdRandom - 1].Surname;
                    customerIdRandom = customers[customerIdRandom - 1].CustomerId;

                    //creazione lista ordine -prodotti
                    List<OrderProduct> orderProducts = new List<OrderProduct>();

                    decimal totalPrice = 0;
                    int quantity;

                    //numero random prodotti singoli - max 4 prodotti diversi ad ordine
                    int numberProducts = new Random().Next(1, 5);

                    Console.WriteLine($"\n* * * Carrello di {customerName} {customerSurname} * * *");

                    while (orderProducts.Count < numberProducts)
                    {
                        int choiceProductId;
                        bool cp;
                        do
                        {
                            cp = false;
                            //scelta del id prodotto random dalla lista
                            choiceProductId = new Random().Next(0, products.Count);

                            //controllo se id prodotto è presente nella lista 
                            foreach (OrderProduct op in orderProducts)
                            {
                                if (products[choiceProductId].ProductId == op.ProductId)
                                {
                                    cp = true;
                                }
                            }

                        } while (cp);

                        //quantita singolo prodotto random - max 3 a prodotto
                        quantity = new Random().Next(1, 4);

                        //calcolo prezzo totale a prodotto
                        decimal totaleArticolo = products[choiceProductId].Price * quantity;

                        //calcolo prezzo totale carrello
                        totalPrice += totaleArticolo;

                        //creazione del istanza ordine - prodotti
                        orderProducts.Add(new OrderProduct(products[choiceProductId].ProductId, numberOrder, quantity));

                        Console.WriteLine($"\n{products[choiceProductId].Name} X {quantity} aggiunto nel carrello.");
                        Console.WriteLine($"{products[choiceProductId].Price} EUR X {quantity} = {totaleArticolo} EUR");
                    }

                    //creazione singolo ordine e aggiunta db
                    Order order = new Order(customerIdRandom, DateTime.Now, "in preparazione", totalPrice);
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //recupero del ultimo id ordine dal db
                    Order lastOrder = db.Orders.OrderByDescending(orders => orders.OrderId).First();
                    int lastOrderId = lastOrder.OrderId;

                    //cambio id ordine del istanza e aggiunta pivot nel db
                    foreach (OrderProduct op in orderProducts)
                    {
                        op.OrderID = lastOrderId;
                        db.OrderProducts.Add(op);
                        db.SaveChanges();
                    }

                    numberOrder++;

                    Console.WriteLine($"\n- - - Totale carrello: {totalPrice} EUR - - -");
                    Console.WriteLine($"Ordine {numberOrder} creato con successo e in preparazione!");
                    Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n");

                    Thread.Sleep(1000);

                } while (numberOrder < 5);
            }
        }


    }    
}
