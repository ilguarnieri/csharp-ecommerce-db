using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_ecommerce_db
{
    internal class Menu
    {

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

                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("A PRESTO!");
                    Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n");
                    break;
            }

        }




        //info utente
        public static void menuCustomerInfo(Customer user, int userId)
        {
            Console.WriteLine($"* * * {user.Name} {user.Surname} * * *");

            Console.WriteLine("\n1. Lista ordini");
            Console.WriteLine("2. Modifica un ordine");
            Console.WriteLine("3. Cancella un ordine");
            Console.WriteLine("4. Torna al menù\n");

            int choice;
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


                    break;
                case 3:
                    Console.Clear();
                    Order.deleteOrder(user, userId);
                    break;
                case 4:
                    Console.Clear();
                    Menu.MainMenu();
                    break;

            }
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
