using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VillageNewbies_Projekti.Database
{
    public class TietokantaYhteys
    {
        /*
         * MUUTA SALASANAT JA KÄYTTÄJÄT OMIISI ENNEN AJAMISTA.
         * MUISTA MYÖS JOS PUSHAAT GITHUBIIN NIIN NÄMÄ ASETTAA NÄMÄ.
         * DATABASEN NIMEÄ EI TARVITSE MUUTTAA.
        */
        private string connectionString = "Server=localhost;Port=3306;Database=vn;User=root;Password=omasalasanasi";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
