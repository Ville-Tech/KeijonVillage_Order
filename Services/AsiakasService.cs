using MySqlConnector;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class AsiakasService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public List<Asiakas> HaeAsiakkaat()
        {
            var lista = new List<Asiakas>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM asiakas", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueAsiakas(reader));
            return lista;
        }

        public Asiakas? HaeAsiakasId(int asiakasId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "SELECT * FROM asiakas WHERE asiakas_id = @asiakas_id", conn);
            cmd.Parameters.AddWithValue("@asiakas_id", asiakasId);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return LueAsiakas(reader);
            return null;
        }

        public void LisaaAsiakas(Asiakas asiakas)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO asiakas (postinro, etunimi, sukunimi, lahiosoite, sahkoposti, puhelin)
                VALUES (@postinro, @etunimi, @sukunimi, @lahiosoite, @sahkoposti, @puhelin)",
                conn);
            LisaaParametrit(cmd, asiakas);
            cmd.ExecuteNonQuery();
        }

        public void PaivitaAsiakas(Asiakas asiakas)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                UPDATE asiakas SET
                    postinro   = @postinro,
                    etunimi    = @etunimi,
                    sukunimi   = @sukunimi,
                    lahiosoite = @lahiosoite,
                    sahkoposti = @sahkoposti,
                    puhelin    = @puhelin
                WHERE asiakas_id = @asiakas_id",
                conn);
            LisaaParametrit(cmd, asiakas);
            cmd.Parameters.AddWithValue("@asiakas_id", asiakas.Asiakas_ID);
            cmd.ExecuteNonQuery();
        }

        public void PoistaAsiakas(int asiakasId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "DELETE FROM asiakas WHERE asiakas_id = @asiakas_id", conn);
            cmd.Parameters.AddWithValue("@asiakas_id", asiakasId);
            cmd.ExecuteNonQuery();
        }

        private Asiakas LueAsiakas(MySqlDataReader r)
        {
            return new Asiakas
            {
                Asiakas_ID = r.GetInt32("asiakas_id"),
                Postinro = r.GetString("postinro"),
                Etunimi = r.IsDBNull(r.GetOrdinal("etunimi")) ? null : r.GetString("etunimi"),
                Sukunimi = r.IsDBNull(r.GetOrdinal("sukunimi")) ? null : r.GetString("sukunimi"),
                Lahiosoite = r.IsDBNull(r.GetOrdinal("lahiosoite")) ? null : r.GetString("lahiosoite"),
                Sahkoposti = r.IsDBNull(r.GetOrdinal("sahkoposti")) ? null : r.GetString("sahkoposti"),
                Puhelin = r.IsDBNull(r.GetOrdinal("puhelin")) ? null : r.GetString("puhelin"),
            };
        }

        private void LisaaParametrit(MySqlCommand cmd, Asiakas asiakas)
        {
            cmd.Parameters.AddWithValue("@postinro", asiakas.Postinro);
            cmd.Parameters.AddWithValue("@etunimi", asiakas.Etunimi);
            cmd.Parameters.AddWithValue("@sukunimi", asiakas.Sukunimi);
            cmd.Parameters.AddWithValue("@lahiosoite", asiakas.Lahiosoite);
            cmd.Parameters.AddWithValue("@sahkoposti", asiakas.Sahkoposti);
            cmd.Parameters.AddWithValue("@puhelin", asiakas.Puhelin);
        }
    }
}