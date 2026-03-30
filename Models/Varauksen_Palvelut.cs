using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageNewbies_Projekti.Models
{
    public class Varauksen_Palvelut
    {
        private int varaus_id; // FK
        private int palvelu_id; // FK
        private int lkm;

        public int Varaus_ID
        {
            get { return varaus_id; }
            set { varaus_id = value; }
        }
        public int Palvelu_ID
        {
            get { return palvelu_id; }
            set { palvelu_id = value; }
        }
        public int Lkm
        { 
            get { return lkm; }
            set { lkm = value; } 
        }
    }
}
