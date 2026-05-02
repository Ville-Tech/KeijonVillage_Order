using System.Linq;

namespace VillageNewbies_Projekti.Models
{
    public class Asiakas
    {
        public int Asiakas_ID { get; set; }
        public string Postinro { get; set; } = "";
        public string? Etunimi { get; set; }
        public string? Sukunimi { get; set; }
        public string? Lahiosoite { get; set; }
        public string? Sahkoposti { get; set; }
        public string? Puhelin { get; set; }

        public string KokoNimi => $"{Etunimi} {Sukunimi}".Trim();
    }
}