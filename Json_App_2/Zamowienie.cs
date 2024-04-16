using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_App_2
{
    internal class Zamowienie
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }
        public List<Produkt> Produkty { get; set; }
        public string Dostawa { get; set; }
        public string Platnosc { get; set; }
        public double Suma { get; set; }

        public Zamowienie(int id, string imie, string nazwisko, string adres, List<Produkt> produkty, string dostawa, string platnosc, double suma)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Adres = adres;
            Produkty = produkty;
            Dostawa = dostawa;
            Platnosc = platnosc;
            Suma = suma;
        }

    }
}
