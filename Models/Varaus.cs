namespace VillageNewbies_Projekti.Models
{
    public class Varaus
    {
        public int Varaus_ID { get; set; }
        public int Asiakas_ID { get; set; }
        public int Mokki_ID { get; set; }
        public DateTime? Varattu_Pvm { get; set; }
        public DateTime? Vahvistus_Pvm { get; set; }
        public DateTime? Varattu_Alkupvm { get; set; }
        public DateTime? Varattu_Loppupvm { get; set; }
    }
}