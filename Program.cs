//inserire almeno 3 prodotti diversi
//inserire almeno 5 ordini su almeno 2 utenti diversi

//recuperare la lista di tutti gli ordini effettuati da un cliente
//modificare l’ordine di un cliente
//cancellare un ordine di un cliente
//cancellare un prodotto su cui è attivo almeno un ordine

using csharp_ecommerce_db;


////AGGIUNTA UTENTI DB
//Customer.AddCustomer("Angelo", "Guarnieri", "ag@gmail.com");
//Customer.AddCustomer("Marco", "Rossi", "mr@gmail.com");
//Customer.AddCustomer("Viviana", "Verdi", "vv@gmail.com");


////AGGIUNTA PRODOTTI DB
//Product.AddProduct("Vans", "Scarpe Vans uomo", "41,65");
//Product.AddProduct("Adidas", "Scarpe Adidas uomo", "79,90");
//Product.AddProduct("Fila", "Scarpe Fila donna", "59,99");
//Product.AddProduct("Giorgio Armani", "Scarpe Giorgio Armani donna", "239,69");



//CREAZIONE 5 ORDINI CON UTENTI E PRODOTTI CASUALI
using (EcommerceContext db = new EcommerceContext())
{

    List<Customer> customers = db.Customers.ToList();
    List<Product> products = db.Products.ToList();


    int numOrdine = 0;

    //creare 5 ordini
    do
    {

        //scelta id utente random
        int customerIdRandom = new Random().Next(1, customers.Count);
        customerIdRandom = customers[customerIdRandom].CustomerId;


        List<OrderProduct> orderProducts = new List<OrderProduct>();

        decimal totalPrice = 0;
        int quantity;

        //numero random prodotti singoli per ordine
        int numberProducts = new Random().Next(1, products.Count + 1);


        while (orderProducts.Count() < numberProducts)
        {
            int choiceProductId;
            bool cp;
            do
            {
                cp = false;
                //scelta del id prodotto random dalla lista
                choiceProductId = new Random().Next(0, products.Count);

                foreach (OrderProduct op in orderProducts)
                {
                    if (products[choiceProductId].ProductId == op.ProductId)
                    {
                        cp = true;

                    }
                }

            } while (cp);


            quantity = new Random().Next(1, 5);

            decimal totaleArticolo = products[choiceProductId].Price * quantity;

            totalPrice += totaleArticolo;

            orderProducts.Add(new OrderProduct(products[choiceProductId].ProductId, numOrdine, quantity));

            Console.WriteLine($"\n{products[choiceProductId].Name} X {quantity} aggiunto nel carrello.");
            Console.WriteLine($"{products[choiceProductId].Price} EUR X {quantity} = {totaleArticolo} EUR");
        }

        Order order = new Order(customerIdRandom, DateTime.Now, "in preparazione", totalPrice);
        db.Orders.Add(order);
        db.SaveChanges();


        Order lastOrder = db.Orders.OrderByDescending(orders => orders.OrderId).First();
        int lastOrderId = lastOrder.OrderId;

        foreach (OrderProduct op in orderProducts)
        {
            op.OrderID = lastOrderId;
            db.OrderProducts.Add(op);
            db.SaveChanges();
        }


        numOrdine++;

        Console.WriteLine($"\n- - - Totale carrello: {totalPrice} EUR - - -");
        Console.WriteLine($"Ordine {numOrdine} creato con successo e in preparazione!");
        Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\n\n");


        Thread.Sleep(1000);


    } while (numOrdine < 5);
}