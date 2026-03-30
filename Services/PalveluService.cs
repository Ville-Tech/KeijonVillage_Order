using MySqlConnector;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class PalveluService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public List<Palvelu> HaePalvelut()
        {
            var lista = new List<Palvelu>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM palvelu", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LuePalvelu(reader));
            return lista;
        }

        public List<Palvelu> HaePalvelutAlueelta(int alueId)
        {
            var lista = new List<Palvelu>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT * FROM palvelu WHERE alue_id = @alue_id", conn);
            cmd.Parameters.AddWithValue("@alue_id", alueId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LuePalvelu(reader));
            return lista;
        }

        public void LisaaPalvelu(Palvelu palvelu)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO palvelu (alue_id, nimi, kuvaus, hinta, alv)
                VALUES (@alue_id, @nimi, @kuvaus, @hinta, @alv)",
                conn);
            LisaaParametrit(cmd, palvelu);
            cmd.ExecuteNonQuery();
        }

        public void PaivitaPalvelu(Palvelu palvelu)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                UPDATE palvelu SET
                    alue_id = @alue_id,
                    nimi    = @nimi,
                    kuvaus  = @kuvaus,
                    hinta   = @hinta,
                    alv     = @alv
                WHERE palvelu_id = @palvelu_id",
                conn);
            LisaaParametrit(cmd, palvelu);
            cmd.Parameters.AddWithValue("@palvelu_id", palvelu.Palvelu_ID);
            cmd.ExecuteNonQuery();
        }

        public void PoistaPalvelu(int palveluId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "DELETE FROM palvelu WHERE palvelu_id = @palvelu_id", conn);
            cmd.Parameters.AddWithValue("@palvelu_id", palveluId);
            cmd.ExecuteNonQuery();
        }

        private Palvelu LuePalvelu(MySqlDataReader r)
        {
            return new Palvelu
            {
                Palvelu_ID = r.GetInt32("palvelu_id"),
                Alue_ID = r.GetInt32("alue_id"),
                Nimi = r.IsDBNull(r.GetOrdinal("nimi")) ? null : r.GetString("nimi"),
                Kuvaus = r.IsDBNull(r.GetOrdinal("kuvaus")) ? null : r.GetString("kuvaus"),
                Hinta = r.GetDouble("hinta"),
                Alv = r.GetDouble("alv"),
            };
        }

        private void LisaaParametrit(MySqlCommand cmd, Palvelu palvelu)
        {
            cmd.Parameters.AddWithValue("@alue_id", palvelu.Alue_ID);
            cmd.Parameters.AddWithValue("@nimi", palvelu.Nimi);
            cmd.Parameters.AddWithValue("@kuvaus", palvelu.Kuvaus);
            cmd.Parameters.AddWithValue("@hinta", palvelu.Hinta);
            cmd.Parameters.AddWithValue("@alv", palvelu.Alv);
        }
    }
}