//inserire almeno 3 prodotti diversi
//inserire almeno 5 ordini su almeno 2 utenti diversi
//recuperare la lista di tutti gli ordini effettuati da un cliente
//modificare l’ordine di un cliente
//cancellare un ordine di un cliente
//cancellare un prodotto su cui è attivo almeno un ordine

using csharp_ecommerce_db;

using (EcommerceContext db = new EcommerceContext())
{
    //creazione prodotti
    Product prodotto1 = new Product("Vans", "Scarpe Vans uomo", 41.65);
    Product prodotto2 = new Product("Adidas", "Scarpe Adidas uomo", 79.90);
    Product prodott4 = new Product("Fila", "Scarpe Fila donna", 59.99);
    Product prodotto3 = new Product("Giorgio Armani", "Scarpe Giorgio Armani donna", 239.48);

    db.Add(prodotto1);
    db.Add(prodotto2);
    db.Add(prodotto3);
    db.SaveChanges();
    Console.WriteLine("Prodotti aggiunti nel db");

    //creazione utenti
    Customer utente1 = new Customer("Angelo", "Guarnieri", "ag@gmail.com");
    Customer utente2 = new Customer("Marco", "Rossi", "mr@gmail.com");
    Customer utente3 = new Customer("Viviana", "Verdi", "vv@gmail.com");

    db.Add(utente1);
    db.Add(utente2);
    db.Add(utente3);
    db.SaveChanges();
    Console.WriteLine("Utenti aggiunti nel db");



}