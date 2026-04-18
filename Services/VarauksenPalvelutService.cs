using MySqlConnector;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class VarauksenPalvelutService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public List<Varauksen_Palvelut> HaeVarauksenPalvelut(int varausId)
        {
            var lista = new List<Varauksen_Palvelut>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT * FROM varauksen_palvelut WHERE varaus_id = @varaus_id", conn);
            cmd.Parameters.AddWithValue("@varaus_id", varausId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new Varauksen_Palvelut
                {
                    Varaus_ID = reader.GetInt32("varaus_id"),
                    Palvelu_ID = reader.GetInt32("palvelu_id"),
                    Lkm = reader.GetInt32("lkm")
                });
            return lista;
        }

        public void LisaaVarauksenPalvelu(Varauksen_Palvelut vp)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO varauksen_palvelut (varaus_id, palvelu_id, lkm)
                VALUES (@varaus_id, @palvelu_id, @lkm)
                ON DUPLICATE KEY UPDATE lkm = lkm + @lkm",
                conn);
            cmd.Parameters.AddWithValue("@varaus_id", vp.Varaus_ID);
            cmd.Parameters.AddWithValue("@palvelu_id", vp.Palvelu_ID);
            cmd.Parameters.AddWithValue("@lkm", vp.Lkm);
            cmd.ExecuteNonQuery();
        }

        public void PoistaVarauksenPalvelu(int varausId, int palveluId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                DELETE FROM varauksen_palvelut 
                WHERE varaus_id = @varaus_id AND palvelu_id = @palvelu_id", conn);
            cmd.Parameters.AddWithValue("@varaus_id", varausId);
            cmd.Parameters.AddWithValue("@palvelu_id", palveluId);
            cmd.ExecuteNonQuery();
        }
    }
}