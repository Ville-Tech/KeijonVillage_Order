using MySqlConnector;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class LaskuService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public List<Lasku> HaeLaskut()
        {
            var lista = new List<Lasku>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM lasku", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueLasku(reader));
            return lista;
        }

        public List<Lasku> HaeLaskutVarauksenMukaan(int varausId)
        {
            var lista = new List<Lasku>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT * FROM lasku WHERE varaus_id = @varaus_id", conn);
            cmd.Parameters.AddWithValue("@varaus_id", varausId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueLasku(reader));
            return lista;
        }

        public void LisaaLasku(Lasku lasku)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO lasku (varaus_id, summa, alv, maksettu)
                VALUES (@varaus_id, @summa, @alv, @maksettu)",
                conn);
            LisaaParametrit(cmd, lasku);
            cmd.ExecuteNonQuery();
        }

        public void MerkitseMaksetuksi(int laskuId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "UPDATE lasku SET maksettu = 1 WHERE lasku_id = @lasku_id", conn);
            cmd.Parameters.AddWithValue("@lasku_id", laskuId);
            cmd.ExecuteNonQuery();
        }

        public void PaivitaLasku(Lasku lasku)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                UPDATE lasku SET
                    varaus_id = @varaus_id,
                    summa     = @summa,
                    alv       = @alv,
                    maksettu  = @maksettu
                WHERE lasku_id = @lasku_id",
                conn);
            LisaaParametrit(cmd, lasku);
            cmd.Parameters.AddWithValue("@lasku_id", lasku.Lasku_ID);
            cmd.ExecuteNonQuery();
        }

        public void PoistaLasku(int laskuId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "DELETE FROM lasku WHERE lasku_id = @lasku_id", conn);
            cmd.Parameters.AddWithValue("@lasku_id", laskuId);
            cmd.ExecuteNonQuery();
        }

        private Lasku LueLasku(MySqlDataReader r)
        {
            return new Lasku
            {
                Lasku_ID = r.GetInt32("lasku_id"),
                Varaus_ID = r.GetInt32("varaus_id"),
                Summa = r.GetDouble("summa"),
                Alv = r.GetDouble("alv"),
                Maksettu = r.GetInt32("maksettu"),
            };
        }

        private void LisaaParametrit(MySqlCommand cmd, Lasku lasku)
        {
            cmd.Parameters.AddWithValue("@varaus_id", lasku.Varaus_ID);
            cmd.Parameters.AddWithValue("@summa", lasku.Summa);
            cmd.Parameters.AddWithValue("@alv", lasku.Alv);
            cmd.Parameters.AddWithValue("@maksettu", lasku.Maksettu);
        }
    }
}