namespace VillageNewbies_Projekti.Models
{
    // Asiakas luokka
    public class Asiakas
    {
        // Kysymysmerkit meinaa, että value voi olla null.
        private int asiakas_id; // PK
        private string postinro; // FK
        private string? etunimi;
        private string? sukunimi;
        private string? lahiosoite;
        private string? sahkoposti;
        private string? puhelin;

        public int Asiakas_ID
        {
            get { return asiakas_id; }
            set { asiakas_id = value; }
        }
        public string? Etunimi
        {
            get { return etunimi; }
            set { etunimi = value; }
        }
        public string? Sukunimi
        {
            get { return sukunimi; }
            set { sukunimi = value; }
        }
        public string? Lahiosoite
        {
            get { return lahiosoite; }
            set { lahiosoite = value; }
        }
        public string Postinro
        {
            get { return postinro; }
            // Asetetaan postinro vain jos se on 5 merkkiä pitkä, sekä merkit ovat numeroita.
            set
            {
                if (value.Length == 5 && value.All(char.IsDigit))
                {
                    postinro = value;
                }
            }
        }
        public string? Sahkoposti
        {
            get { return sahkoposti; }
            set { sahkoposti = value; }
        }
        public string? Puhelin
        {
            get { return puhelin; }
            set { puhelin = value; }
        }
    }
}
