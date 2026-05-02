using MySqlConnector;
using System;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class MokkiService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public List<Mokki> HaeMokit()
        {
            var lista = new List<Mokki>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM mokki ORDER BY mokkinimi", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueMokki(reader));
            return lista;
        }

        public List<Mokki> HaeMokitAlueelta(int alueId)
        {
            var lista = new List<Mokki>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM mokki WHERE alue_id = @alue_id ORDER BY mokkinimi", conn);
            cmd.Parameters.AddWithValue("@alue_id", alueId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueMokki(reader));
            return lista;
        }

        public void LisaaMokki(Mokki mokki)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO mokki (alue_id, postinro, mokkinimi, katuosoite, hinta, kuvaus, henkilomaara, varustelu)
                VALUES (@alue_id, @postinro, @mokkinimi, @katuosoite, @hinta, @kuvaus, @henkilomaara, @varustelu)",
                conn);
            LisaaParametrit(cmd, mokki);
            cmd.ExecuteNonQuery();
        }

        public void PaivitaMokki(Mokki mokki)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                UPDATE mokki SET
                    alue_id      = @alue_id,
                    postinro     = @postinro,
                    mokkinimi    = @mokkinimi,
                    katuosoite   = @katuosoite,
                    hinta        = @hinta,
                    kuvaus       = @kuvaus,
                    henkilomaara = @henkilomaara,
                    varustelu    = @varustelu
                WHERE mokki_id = @mokki_id",
                conn);
            LisaaParametrit(cmd, mokki);
            cmd.Parameters.AddWithValue("@mokki_id", mokki.Mokki_ID);
            cmd.ExecuteNonQuery();
        }

        public void PoistaMokki(int mokkiId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                // 1. Hae mökin varausten id:t
                var varausIdt = new List<int>();
                var cmdVaraukset = new MySqlCommand(
                    "SELECT varaus_id FROM varaus WHERE mokki_id = @mokki_id", conn, trans);
                cmdVaraukset.Parameters.AddWithValue("@mokki_id", mokkiId);
                using (var r = cmdVaraukset.ExecuteReader())
                    while (r.Read()) varausIdt.Add(r.GetInt32(0));

                // 2. Poista varauksen_palvelut ja laskut jokaiselle varaukselle
                foreach (int vid in varausIdt)
                {
                    Exec(conn, trans, "DELETE FROM varauksen_palvelut WHERE varaus_id = @id", vid);
                    Exec(conn, trans, "DELETE FROM lasku WHERE varaus_id = @id", vid);
                    Exec(conn, trans, "DELETE FROM varaus WHERE varaus_id = @id", vid);
                }

                // 3. Poista mökki
                Exec(conn, trans, "DELETE FROM mokki WHERE mokki_id = @id", mokkiId);

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        private static void Exec(MySqlConnection conn, MySqlTransaction trans, string sql, int id)
        {
            var cmd = new MySqlCommand(sql, conn, trans);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Mokki LueMokki(MySqlDataReader reader) => new Mokki
        {
            Mokki_ID = reader.GetInt32("mokki_id"),
            Alue_ID = reader.GetInt32("alue_id"),
            Postinro = reader.GetString("postinro"),
            Mokkinimi = reader.IsDBNull(reader.GetOrdinal("mokkinimi")) ? null : reader.GetString("mokkinimi"),
            Katuosoite = reader.IsDBNull(reader.GetOrdinal("katuosoite")) ? null : reader.GetString("katuosoite"),
            Hinta = reader.GetDouble("hinta"),
            Kuvaus = reader.IsDBNull(reader.GetOrdinal("kuvaus")) ? null : reader.GetString("kuvaus"),
            Henkilomaara = reader.GetInt32("henkilomaara"),
            Varustelu = reader.IsDBNull(reader.GetOrdinal("varustelu")) ? null : reader.GetString("varustelu"),
        };

        private static void LisaaParametrit(MySqlCommand cmd, Mokki mokki)
        {
            cmd.Parameters.AddWithValue("@alue_id", mokki.Alue_ID);
            cmd.Parameters.AddWithValue("@postinro", mokki.Postinro);
            cmd.Parameters.AddWithValue("@mokkinimi", mokki.Mokkinimi);
            cmd.Parameters.AddWithValue("@katuosoite", mokki.Katuosoite);
            cmd.Parameters.AddWithValue("@hinta", mokki.Hinta);
            cmd.Parameters.AddWithValue("@kuvaus", mokki.Kuvaus);
            cmd.Parameters.AddWithValue("@henkilomaara", mokki.Henkilomaara);
            cmd.Parameters.AddWithValue("@varustelu", mokki.Varustelu);
        }
    }
}