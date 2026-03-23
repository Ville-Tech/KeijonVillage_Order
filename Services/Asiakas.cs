namespace VillageNewbies_Projekti.Services
{
    // Asiakas luokka
    public class Asiakas
    {
        // Kysymysmerkit meinaa, että value voi olla null.
        private int id;
        private string? etunimi;
        private string? sukunimi;
        private string? lahiosoite;
        private string postinro;
        private string? sahkoposti;
        private string? puhelin;

        public int ID
        {
            get { return id; }
            set { id = value; }
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
