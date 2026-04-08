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

            var cmd = new MySqlCommand("SELECT alue_id, nimi FROM alue ORDER BY nimi", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Varmistetaan ettei tule null-olioita listaan
                if (reader.IsDBNull(reader.GetOrdinal("alue_id")))
                    continue;

                var alue = new Alue
                {
                    Alue_ID = reader.GetInt32("alue_id"),
                    Nimi = reader.IsDBNull(reader.GetOrdinal("nimi"))
                        ? ""
                        : reader.GetString("nimi")
                };

                lista.Add(alue);
            }

            return lista;
        }

        public void LisaaAlue(Alue alue)
        {
            using var conn = db.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO alue (nimi) VALUES (@nimi)", conn);

            cmd.Parameters.AddWithValue("@nimi", alue.Nimi ?? "");
            cmd.ExecuteNonQuery();
        }

        public void PaivitaAlue(Alue alue)
        {
            using var conn = db.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "UPDATE alue SET nimi = @nimi WHERE alue_id = @alue_id", conn);

            cmd.Parameters.AddWithValue("@nimi", alue.Nimi ?? "");
            cmd.Parameters.AddWithValue("@alue_id", alue.Alue_ID);

            cmd.ExecuteNonQuery();
        }

        public void PoistaAlue(int alueId)
        {
            using var conn = db.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "DELETE FROM alue WHERE alue_id = @alue_id", conn);

            cmd.Parameters.AddWithValue("@alue_id", alueId);
            cmd.ExecuteNonQuery();
        }
    }
}