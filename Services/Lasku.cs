using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageNewbies_Projekti.Services
{
    public class Lasku
    {
        private int lasku_id; // PK
        private int varaus_id; // FK
        private double summa;
        private double alv;
        private int maksettu;

        public int Lasku_ID
        {
            get { return lasku_id; }
            set { lasku_id = value; }
        }
        public int Varaus_ID
        {
            get { return varaus_id; }
            set { varaus_id = value; }
        }
        public double Summa
        {
            get { return summa; }
            set { summa = value; }
        }
        public double Alv
        {
            get { return alv; }
            set { alv = value; }
        }
        public int Maksettu
        {
            get { return maksettu; }
            set { maksettu = value; }
        }
    }
}
