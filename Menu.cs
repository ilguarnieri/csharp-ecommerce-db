using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_ecommerce_db
{
    internal class Menu
    {
        //MENU PRINCIPALE
        public static void MainMenu()
        {
            Console.WriteLine("* * * MENÙ * * *");

            Console.WriteLine("\n1. Crea 5 ordini casuali");
            Console.WriteLine("2. Lista utenti");
            Console.WriteLine("3. Cancella un prodotto dal DB");
            Console.WriteLine("4. Exit\n");

            int choice;
            choice = Menu.loopChoice(4);

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Order.AddCasualOrder();
                    Console.WriteLine("\nPremi qualsiasi tasto per tornare al menù...");
                    Console.ReadKey();
                    Console.Clear();
                    Menu.MainMenu();
                    break;
                case 2:
                    Console.Clear();
                    Customer.userChoice();
                    break;
                case 3:
                    Console.Clear();
                    Menu.menuDeleteProduct();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("A PRESTO!");
                    Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n");
                    break;
            }

        }




        //MENU CANCELLAZIONE PRODOTTO
        public static void menuDeleteProduct()
        {
            List<Product> products = OrderProduct.stampListProduct();

            if (products.Count > 0)
            {
                Console.WriteLine("\nQuale prodotto vuoi definitivamente dal Database?");

                int Choice = Menu.loopChoice(products.Count);

                Console.Clear();

                Console.WriteLine($"Confermi che vuoi eliminare {products[Choice - 1].Name}? (y/n)\n");

                string confirm = Console.ReadLine();

                switch (confirm)
                {
                    case "y":
                        Product.removeProduct(products[Choice - 1]);
                        Console.Clear();
                        Console.WriteLine("Prodotto cancellato!");
                        break;
                    case "n":
                        Console.Clear();
                        Console.WriteLine("Prodotto non cancellato.");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Input non corretto");
                        break;
                }

            }
            else
            {
                Console.WriteLine("\n\nNon ci sono prodotti da cancellare...");
            }

            Console.WriteLine("\nPremi qualsiasi tasto per toranre indietro...");
            Console.ReadKey();
            Console.Clear();
            Menu.MainMenu();
        }





        //MENU INFO SPECIFICHE UTENTE
        public static void menuCustomerInfo(Customer user, int userId)
        {
            Console.WriteLine($"* * * {user.Name.ToUpper()} {user.Surname.ToUpper()} * * *");

            Console.WriteLine("\n1. Lista ordini");
            Console.WriteLine("2. Modifica lo stato di un ordine");
            Console.WriteLine("3. Cancella un ordine");
            Console.WriteLine("4. Torna al menù\n");

            int choice;
            List<Order> customerOrders;
            choice = Menu.loopChoice(5);
            

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Order.ListOrder(userId);
                    Console.WriteLine("\nPremi qualsiasi tasto per toranre indietro...");
                    Console.ReadKey();
                    Console.Clear();
                    Menu.menuCustomerInfo(user, userId);
                    break;
                case 2:
                    Console.Clear();
                    customerOrders = Order.ListOrder(userId);
                    Menu.modifyOrder(user, userId, customerOrders);
                    break;
                case 3:
                    Console.Clear();
                    customerOrders = Order.ListOrder(userId);
                    Menu.deleteOrder(user, userId, customerOrders);
                    break;
                case 4:
                    Console.Clear();
                    Menu.MainMenu();
                    break;

            }
        }




        //mMENU ELIMINAZIONE ORDINE
        public static void deleteOrder(Customer user, int userId, List<Order> customerOrders)
        {
            if (customerOrders.Count > 0)
            {
                Console.WriteLine("\nQuale ordine vorresti eliminare?");
                int choice;
                choice = Menu.loopChoice(customerOrders.Count);

                Order orderSelect = customerOrders[choice - 1];

                Console.Clear();
                Console.WriteLine($"Sei sicuro di voler elimianre l'ordine n.{orderSelect.OrderId}? (y/n)\n");
                string confirm = Console.ReadLine();

                switch (confirm)
                {
                    case "y":
                        Order.removeOrder(orderSelect);
                        Console.Clear();
                        Console.WriteLine("Ordine cancellato!");
                        break;
                    case "n":
                        Console.Clear();
                        Console.WriteLine("Ordine non cancellato.");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Input non corretto");
                        break;
                }
            }

            Console.WriteLine("\nPremi qualsiasi tasto per tornare al menù utente...");
            Console.ReadKey();
            Console.Clear();
            Menu.menuCustomerInfo(user, userId);
        }




        //MENU MODIFICA STATUS ORDINE
        public static void modifyOrder(Customer user, int userId, List<Order> customerOrders)
        {

            if(customerOrders.Count > 0)
            {
                Console.WriteLine("\nQuale ordine vorresti modificare?");
                int choice;
                choice = Menu.loopChoice(customerOrders.Count);

                Order orderSelect = customerOrders[choice - 1];

                Console.Clear();

                Console.WriteLine($"* * * Ordine n.{orderSelect.OrderId} di {user.Name} {user.Surname} * * *");
                Console.WriteLine($"{orderSelect.Date.ToString("d")}\t Totale: {orderSelect.Amount}\t ->{orderSelect.Status}\n");

                Console.WriteLine("\nIn che stato è l'ordine?");
                Console.WriteLine("1. In preparazione");
                Console.WriteLine("2. Spedito");
                Console.WriteLine("3. Consegnato\n");

                int choice2;
                choice2 = Menu.loopChoice(3);

                switch (choice2)
                {
                    case 1:
                        orderSelect.Status = "In preparazione";
                        break;
                    case 2:
                        orderSelect.Status = "Spedito";
                        break;
                    case 3:
                        orderSelect.Status = "Consegnato";
                        break;
                }

                Order.modifyOrderStatus(orderSelect);
                Console.Clear();
                Console.WriteLine("Ordine modificato con successo!\n");
                Console.WriteLine($"* * * Ordine n.{orderSelect.OrderId} di {user.Name} {user.Surname} * * *");
                Console.WriteLine($"{orderSelect.Date.ToString("d")}\t Totale: {orderSelect.Amount}\t ->{orderSelect.Status}\n");
                
            }

            Console.WriteLine("\nPremi qualsiasi tasto per tornare al menù utente...");
            Console.ReadKey();
            Console.Clear();
            Menu.menuCustomerInfo(user, userId);

        }






        //---------------------------------------- CONTROLLO INPUT MENU ----------------------------------------------------
        public static int loopChoice(int options)
        {
            int choice;
            do
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out choice);

                if (String.IsNullOrEmpty(input) || !isNumber)
                {
                    input = "0";
                }

                choice = Convert.ToByte(input);

                if (choice == 0 || choice > options)
                {
                    Console.WriteLine("La voce di menu selezionata non esite!");
                    Console.WriteLine("Inserisci una voce valida...\n");
                }
            } while (choice == 0 || choice > options);

            return choice;
        }

    }
}
