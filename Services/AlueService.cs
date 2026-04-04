using MySqlConnector;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class AlueService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public List<Alue> HaeAlueet()
        {
            var lista = new List<Alue>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM alue", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new Alue
                {
                    Alue_ID = reader.GetInt32("alue_id"),
                    Nimi = reader.IsDBNull(reader.GetOrdinal("nimi")) ? null : reader.GetString("nimi"),
                });
            return lista;
        }
    }
}