using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace csharp_ecommerce_db
{
    [Table("customers")]
    internal class Customer
    {
        [Key]
        [Column("id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("surname")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        public List<Order> Orders { get; set; }


        public Customer(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }


        public static void AddCustomer(string name, string surname, string email)
        {
            using (EcommerceContext db = new EcommerceContext())
            {
                Customer cust = new Customer(name, surname, email);
                db.Add(cust);
                db.SaveChanges();
                Console.WriteLine($"\n{name} {surname} aggiunto con successo!");
            }
        }



        //SELEZIONE UTENTE
        public static void userChoice()
        {
            using(EcommerceContext db = new EcommerceContext())
            {
                List<Customer> customers = db.Customers.ToList();

                Console.WriteLine("* * * LISTA UTENTI * * *");

                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}- {customers[i].Name} {customers[i].Surname}");
                }


                Console.WriteLine("\nSeleziona un utente");

                int choice = Menu.loopChoice(customers.Count);
                Console.Clear();

                Menu.menuCustomerInfo(customers[choice - 1], customers[choice - 1].CustomerId);
            }
        }


    }
}
