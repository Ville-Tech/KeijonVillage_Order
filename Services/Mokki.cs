using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace VillageNewbies_Projekti.Services
{
    public class Mokki
    {
        // Kysymysmerkit meinaa, että value voi olla null.
        private int id;
        private int alue_id;
        private string postinro;
        private string? mokkinimi;
        private string? katuosoite;
        private double hinta;
        private string? kuvaus;
        private int henkilomaara;
        private string? varustelu;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int Alue_ID
        {
            get { return alue_id; }
            set { alue_id = value; }
        }
        public string Postinro
        {
            get { return postinro; }
            set
            {
                if (value.Length == 5 && value.All(char.IsDigit))
                {
                    postinro = value;
                }
            }
        }
        public string? Mokkinimi
        {
            get { return mokkinimi; }
            set { mokkinimi = value; }
        }
        public string? Katuosoite
        {
            get { return katuosoite; }
            set { katuosoite = value; }
        }
        public double Hinta
        {
            get { return hinta; }
            set 
            {
                if(value >= 0)
                {
                    hinta = value;
                }
            }
        }
        public string? Kuvaus
        {
            get { return kuvaus; }
            set { kuvaus = value; }
        }
        public int Henkilomaara
        {
            get { return henkilomaara; }
            set 
            {
                if (value >= 0)
                {
                    henkilomaara = value; 
                }
            }
        }
        public string? Varustelu
        {
            get { return varustelu; }
            set { varustelu = value; }
        }
    }
}