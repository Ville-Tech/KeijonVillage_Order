using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageNewbies_Projekti.Services
{
    // Kysymysmerkit meinaa, että value voi olla null.
    public class Alue
    {
        private int alue_id; // PK
        private string? nimi;

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
    }
}
