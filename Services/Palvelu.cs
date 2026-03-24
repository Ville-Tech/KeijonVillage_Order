using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageNewbies_Projekti.Services
{
    // Kysymysmerkit meinaa, että value voi olla null.
    public class Palvelu
    {
        private int palvelu_id; // PK
        private int alue_id; // FK
        private string? nimi;
        private string? kuvaus;
        private double hinta;
        private double alv;

        public int Palvelu_ID
        {
            get { return palvelu_id; }
            set { palvelu_id = value; }
        }
        public int Alue_ID
        {
            get { return alue_id; }
            set { alue_id = value; }
        }
        public string? Nimi
        {
            get { return nimi; }
            set { nimi = value; }
        }
        public string? Kuvaus
        {
            get { return kuvaus; }
            set { kuvaus = value; }
        }
        public double Hinta
        {
            get { return hinta; }
            set { hinta = value; }
        }
        public double Alv
        {
            get { return alv; }
            set { alv = value; }
        }
    }
}
