

//cancellare un ordine di un cliente
//cancellare un prodotto su cui è attivo almeno un ordine

using csharp_ecommerce_db;


////AGGIUNTA UTENTI DB
//Customer.AddCustomer("Angelo", "Guarnieri", "ag@gmail.com");
//Customer.AddCustomer("Marco", "Rossi", "mr@gmail.com");
//Customer.AddCustomer("Viviana", "Verdi", "vv@gmail.com");
//Customer.AddCustomer("Domenico", "Sardi", "ds@gmail.com");
//Customer.AddCustomer("Alessio", "Neri", "an@gmail.com");
//Customer.AddCustomer("Roberta", "Franchi", "rf@gmail.com");
//Customer.AddCustomer("Stella", "Palmisano", "sp@gmail.com");


////AGGIUNTA PRODOTTI DB
//Product.AddProduct("Vans", "Scarpe Vans uomo", "41,65");
//Product.AddProduct("Adidas", "Scarpe Adidas uomo", "79,90");
//Product.AddProduct("Nike", "Scarpe Nike unisex nere", "89,90");
//Product.AddProduct("Fila", "Scarpe Fila donna", "59,99");
//Product.AddProduct("Vans", "Scarpe Vans donna", "49,99");
//Product.AddProduct("Versace Jeans", "Scarpe Versace Unisex", "289,78");
//Product.AddProduct("Giorgio Armani", "Scarpe Giorgio Armani donna", "239,69");



////CREAZIONE 5 ORDINI CON UTENTI E PRODOTTI CASUALI
//using (EcommerceContext db = new EcommerceContext())
//{

//    List<Customer> customers = db.Customers.ToList();
//    List<Product> products = db.Products.ToList();


//    int numberOrder = 0;

//    //creare 5 ordini
//    do
//    {
//        //scelta id utente random
//        int customerIdRandom = new Random().Next(1, customers.Count);

//        //dati utente
//        string customerName = customers[customerIdRandom].Name;
//        string customerSurname = customers[customerIdRandom].Surname;
//        customerIdRandom = customers[customerIdRandom].CustomerId;

//        //creazione lista ordine -prodotti
//        List<OrderProduct> orderProducts = new List<OrderProduct>();

//        decimal totalPrice = 0;
//        int quantity;

//        //numero random prodotti singoli - max 4 prodotti diversi ad ordine
//        int numberProducts = new Random().Next(1, 5);

//        Console.WriteLine($"\n* * * Carrello di {customerName} {customerSurname} * * *");

//        while (orderProducts.Count() < numberProducts)
//        {
//            int choiceProductId;
//            bool cp;
//            do
//            {
//                cp = false;
//                //scelta del id prodotto random dalla lista
//                choiceProductId = new Random().Next(0, products.Count);

//                //controllo se id prodotto è presente nella lista 
//                foreach (OrderProduct op in orderProducts)
//                {
//                    if (products[choiceProductId].ProductId == op.ProductId)
//                    {
//                        cp = true;
//                    }
//                }

//            } while (cp);

//            //quantita singolo prodotto random - max 3 a prodotto
//            quantity = new Random().Next(1, 4);

//            //calcolo prezzo totale a prodotto
//            decimal totaleArticolo = products[choiceProductId].Price * quantity;

//            //calcolo prezzo totale carrello
//            totalPrice += totaleArticolo;

//            //creazione del istanza ordine - prodotti
//            orderProducts.Add(new OrderProduct(products[choiceProductId].ProductId, numberOrder, quantity));

//            Console.WriteLine($"\n{products[choiceProductId].Name} X {quantity} aggiunto nel carrello.");
//            Console.WriteLine($"{products[choiceProductId].Price} EUR X {quantity} = {totaleArticolo} EUR");
//        }

//        //creazione singolo ordine e aggiunta db
//        Order order = new Order(customerIdRandom, DateTime.Now, "in preparazione", totalPrice);
//        db.Orders.Add(order);
//        db.SaveChanges();

//        //recupero del ultimo id ordine dal db
//        Order lastOrder = db.Orders.OrderByDescending(orders => orders.OrderId).First();
//        int lastOrderId = lastOrder.OrderId;

//        //cambio id ordine del istanza e aggiunta pivot nel db
//        foreach (OrderProduct op in orderProducts)
//        {
//            op.OrderID = lastOrderId;
//            db.OrderProducts.Add(op);
//            db.SaveChanges();
//        }

//        numberOrder++;

//        Console.WriteLine($"\n- - - Totale carrello: {totalPrice} EUR - - -");
//        Console.WriteLine($"Ordine {numberOrder} creato con successo e in preparazione!");
//        Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n");

//        Thread.Sleep(1000);

//    } while (numberOrder < 5);
//}



//recuperare la lista di tutti gli ordini effettuati da un cliente
void ListOrder(int customerId)
{
    using (EcommerceContext db = new EcommerceContext())
    {
        Customer customer = db.Customers.Where(customer => customer.CustomerId == customerId).First();
        List<Order> customerOrders = db.Orders.Where(orders => orders.CustomerId == customerId).ToList();

        Console.WriteLine($"{customer.Name} {customer.Surname} ha effettuato {customerOrders.Count} ordini");

        foreach (Order order in customerOrders)
        {
            Console.WriteLine($"\n* * * Ordine n.{order.OrderId} * * *");
            Console.WriteLine($"{order.Date.ToString("d")}\t Totale: {order.Amount}\t ->{order.Status}");
        }
    }
}



ListOrder(8);




//modificare l’ordine di un cliente
