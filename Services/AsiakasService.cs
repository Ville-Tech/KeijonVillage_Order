using MySqlConnector;
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
            {
                var asiakas = LueAsiakas(reader);

                if (asiakas != null)
                    lista.Add(asiakas);
            }

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
                INSERT INTO asiakas (postinro, etunimi, sukunimi, lahiosoite, email, puhelinnro)
                VALUES (@postinro, @etunimi, @sukunimi, @lahiosoite, @email, @puhelinnro)", conn);

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
                    email      = @email,
                    puhelinnro = @puhelinnro
                WHERE asiakas_id = @asiakas_id", conn);

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
                Postinro = r.IsDBNull(r.GetOrdinal("postinro")) ? "" : r.GetString("postinro"),
                Etunimi = r.IsDBNull(r.GetOrdinal("etunimi")) ? "" : r.GetString("etunimi"),
                Sukunimi = r.IsDBNull(r.GetOrdinal("sukunimi")) ? "" : r.GetString("sukunimi"),
                Lahiosoite = r.IsDBNull(r.GetOrdinal("lahiosoite")) ? "" : r.GetString("lahiosoite"),
                Sahkoposti = r.IsDBNull(r.GetOrdinal("email")) ? "" : r.GetString("email"),
                Puhelin = r.IsDBNull(r.GetOrdinal("puhelinnro")) ? "" : r.GetString("puhelinnro"),
            };
        }

        private void LisaaParametrit(MySqlCommand cmd, Asiakas asiakas)
        {
            cmd.Parameters.AddWithValue("@postinro", asiakas.Postinro ?? "");
            cmd.Parameters.AddWithValue("@etunimi", asiakas.Etunimi ?? "");
            cmd.Parameters.AddWithValue("@sukunimi", asiakas.Sukunimi ?? "");
            cmd.Parameters.AddWithValue("@lahiosoite", asiakas.Lahiosoite ?? "");
            cmd.Parameters.AddWithValue("@email", asiakas.Sahkoposti ?? "");
            cmd.Parameters.AddWithValue("@puhelinnro", asiakas.Puhelin ?? "");
        }
    }
}