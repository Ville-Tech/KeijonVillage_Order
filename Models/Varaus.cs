namespace VillageNewbies_Projekti.Models
{
    // Kysymysmerkit meinaa, että value voi olla null.
    public class Varaus
    {
        private int varaus_id; // PK
        private int asiakas_id; // FK
        private int mokki_id; // FK
        private DateTime? varattu_pvm;
        private DateTime? vahvistus_pvm;
        private DateTime? varattu_alkupvm;
        private DateTime? varattu_loppupvm;

        public int Varaus_ID
        {
            get { return varaus_id; }
            set { varaus_id = value; }
        }
        public int Asiakas_ID
        {
            get { return asiakas_id; }
            set { asiakas_id = value; }
        }
        public int Mokki_ID
        {
            get { return mokki_id; }
            set { mokki_id = value; }
        }
        public DateTime? Varattu_Pvm
        {
            get { return varattu_pvm; }
            set { varattu_pvm = value; }
        }
        public DateTime? Vahvistus_Pvm
        {
            get { return vahvistus_pvm; }
            set { vahvistus_pvm = value; }
        }
        public DateTime? Varattu_Alkupvm
        {
            get { return varattu_alkupvm; }
            set
            {
                if (value >= DateTime.Today)
                {
                    varattu_alkupvm = value;
                }
            }
        }
        public DateTime? Varattu_Loppupvm
        {
            get { return varattu_loppupvm; }
            set
            {
                if (value >= DateTime.Today && value > varattu_alkupvm)
                {
                    varattu_loppupvm = value;
                }
            }
        }
    }
}
