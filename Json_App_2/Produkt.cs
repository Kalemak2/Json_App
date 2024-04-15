using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json_App_2
{
    internal class Produkt
    {

        public int Id { get; set; }

        public string Nazwa {  get; set; }
        public string Producent { get; set;}

        public double Cena { get; set; }

        public string Kategoria { get; set; }

        public int Ilosc {  get; set; }

        public DateTime DataDostawy { get; set; }

        public Produkt(int id, string nazwa, string producent, double cena, string kategoria, int ilosc, DateTime dataDostawy)
        {
            Id = id;
            Nazwa = nazwa;
            Producent = producent;
            Cena = cena;
            Kategoria = kategoria;
            Ilosc = ilosc;
            DataDostawy = dataDostawy;
        }

    }


}
