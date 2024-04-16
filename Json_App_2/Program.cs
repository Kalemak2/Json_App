using Json_App_2;
using System;
using System.IO;
using System.Text.Json;

namespace Ksiegarnia
{
    class Program
    {
        public static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            List<Produkt> produkts;
            List<Zamowienie> zamowienie;

            string productpath = @"produkty.json";
            string orderspath = @"zamowienia.json";

            try
            {
                if (!File.Exists(productpath))
                    File.Create(productpath).Close();
                if (!File.Exists(orderspath))
                    File.Create(orderspath).Close();

                string json = File.ReadAllText(productpath);
                produkts = JsonSerializer.Deserialize<List<Produkt>>(json);

                string json1 = File.ReadAllText(orderspath);
                zamowienie = JsonSerializer.Deserialize<List<Zamowienie>>(json1);

                Console.Write($"Witaj w Biedronce! \n1.Dodaj zamówienie!\n2.Pokaż wszystkie produkty\n3.Pokaż zamówienia\nTwój wybór: ");
                int numbers;
                if (int.TryParse(Console.ReadLine(), out numbers))
                {
                    switch (numbers)
                    {
                        case 1:
                            Console.Clear();
                            DodajZamowienie(produkts);
                            break;
                        case 2:
                            Console.Clear();
                            ShowProducts(produkts);
                            break;
                        case 3:
                            Console.Clear();
                            ShowOrders(zamowienie);
                            break;
                        default:
                            Console.WriteLine("Wybrano nieprawidłową opcję.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wybrano nieprawidłową opcję.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    static void ShowProducts(List<Produkt> produkt)
        {
            Console.Clear();
            if (produkt.Count == 0)
            {
                Console.WriteLine("Brak książek w bibliotece.");
            }
            else
            {
                foreach (var produkty in produkt)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine($"ID: {produkty.Id}");
                    Console.WriteLine($"Nazwa: {produkty.Nazwa}");
                    Console.WriteLine($"Producent: {produkty.Producent}");
                    Console.WriteLine($"Cena: {produkty.Cena}");
                    Console.WriteLine($"Kategoria: {produkty.Kategoria}");
                    Console.WriteLine($"Ilosc: {produkty.Ilosc}");
                    Console.WriteLine($"DataDostawy: {produkty.DataDostawy}");
                    Console.WriteLine("-----------------------");
                }
            }
        }

        static void ShowOrders(List<Zamowienie> zamowienie)
        {
            foreach (var zamowienia in zamowienie)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine($"ID: {zamowienia.Id}");
                Console.WriteLine($"Nazwa: {zamowienia.Imie}");
                Console.WriteLine($"Nazwisko: {zamowienia.Nazwisko}");
                Console.WriteLine($"Adres: {zamowienia.Adres}");
                Console.WriteLine($"Dostawa: {zamowienia.Dostawa}");
                Console.WriteLine($"Platnosc: {zamowienia.Platnosc}"); 
                Console.WriteLine($"Suma: {zamowienia.Suma}"); 
                Console.WriteLine("-----------------------");
            }
        }
        static void DodajZamowienie(List<Produkt> produkty)

        {

            Console.Clear();
            Console.WriteLine("Dodawanie nowego zamówienia");

            Console.Write("Podaj imie: ");
            string imie = Console.ReadLine();
            Console.Write("Podaj nazwisko: ");
            string nazwisko = Console.ReadLine();
            Console.Write("Podaj adres: ");
            string adres = Console.ReadLine();

            List<Produkt> wybraneProdukty = new List<Produkt>();
            List<Zamowienie> zamowienie = new List<Zamowienie>();

            ShowProducts(produkty);
            double suma = 0;
            while (true)
            {

                Console.Write("Podaj ID produktu, który chcesz dodać (0 aby zakończyć): ");
                int idProduktu = Convert.ToInt32(Console.ReadLine());

                if (idProduktu == 0)
                {
                    break;
                }

                Produkt wybranyProdukt = produkty.Find(p => p.Id == idProduktu);

                if (wybranyProdukt != null)
                {
                    Console.Write("Podaj ilość: ");
                    int ilosc = Convert.ToInt32(Console.ReadLine());

                    suma += (wybranyProdukt.Cena * ilosc);
                    
                    wybraneProdukty.Add(new Produkt(wybranyProdukt.Id, wybranyProdukt.Nazwa, wybranyProdukt.Producent, wybranyProdukt.Cena, wybranyProdukt.Kategoria, ilosc, wybranyProdukt.DataDostawy));
                    suma += cenaProduktu(wybranyProdukt) * ilosc;
                }
                else
                {
                    Console.WriteLine("Nie znaleziono produktu o podanym ID.");
                }
            }

            Console.Write("Wybierz sposób dostawy (1 - Kurier(20zł), 2 - Odbiór osobisty(0zł): ");
            string sposobDostawy = Convert.ToInt32(Console.ReadLine()) == 1 ? "Kurier" : "Odbiór osobisty";
            if (sposobDostawy == "Kurier")
            {
                suma += 20;
            }

            Console.Write("Wybierz sposób płatności (1 - Karta(2zł), 2 - Gotówka(0zł): ");
            string sposobPlatnosci = Convert.ToInt32(Console.ReadLine()) == 1 ? "Karta" : "Gotówka";
            if(sposobPlatnosci == "Karta")
            {
                suma += 2;
            }
            Zamowienie noweZamowienie = new Zamowienie(zamowienie.Count + 1, imie, nazwisko, adres, wybraneProdukty, sposobDostawy, sposobPlatnosci, suma);
            zamowienie.Add(noweZamowienie);

            string json = JsonSerializer.Serialize<List<Zamowienie>>(zamowienie);

            string orderspath = @"zamowienia.json";
            File.WriteAllText(orderspath, json);
            Console.WriteLine($"Wartość twojego zamówienia wynosi: {suma}\n");
            Console.WriteLine("Zamówienie zostało utworzone.");
        }


    }
}
