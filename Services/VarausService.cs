using MySqlConnector;
using System;
using System.Collections.Generic;
using VillageNewbies_Projekti.Database;
using VillageNewbies_Projekti.Models;

namespace VillageNewbies_Projekti.Services
{
    public class VarausService
    {
        private TietokantaYhteys db = new TietokantaYhteys();

        public bool OnkoVapaa(int mokkiId, DateTime alku, DateTime loppu, int? ohitaVarausId = null)
        {
            using var conn = db.GetConnection();
            conn.Open();

            string sql = @"
                SELECT COUNT(*) FROM varaus
                WHERE mokki_id = @mokki_id
                  AND varattu_alkupvm < @loppu
                  AND varattu_loppupvm > @alku";

            if (ohitaVarausId.HasValue)
                sql += " AND varaus_id <> @ohita";

            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mokki_id", mokkiId);
            cmd.Parameters.AddWithValue("@alku", alku);
            cmd.Parameters.AddWithValue("@loppu", loppu);
            if (ohitaVarausId.HasValue)
                cmd.Parameters.AddWithValue("@ohita", ohitaVarausId.Value);

            return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
        }

        public List<Varaus> HaeVaraukset()
        {
            var lista = new List<Varaus>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM varaus ORDER BY varattu_alkupvm DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueVaraus(reader));
            return lista;
        }

        public void LisaaVaraus(Varaus varaus)
        {
            if (!varaus.Varattu_Alkupvm.HasValue || !varaus.Varattu_Loppupvm.HasValue)
            {
                throw new ArgumentException("Varauksen alku- ja loppupäivä ovat pakollisia.");
            }
            if (!OnkoVapaa(varaus.Mokki_ID, varaus.Varattu_Alkupvm.Value, varaus.Varattu_Loppupvm.Value))
            {
                throw new InvalidOperationException("Mökki on jo varattu valitulle aikavälille.");
            }

            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO varaus (asiakas_id, mokki_id, varattu_pvm, varattu_alkupvm, varattu_loppupvm)
                VALUES (@asiakas_id, @mokki_id, @varattu_pvm, @varattu_alkupvm, @varattu_loppupvm)",
                conn);
            cmd.Parameters.AddWithValue("@asiakas_id", varaus.Asiakas_ID);
            cmd.Parameters.AddWithValue("@mokki_id", varaus.Mokki_ID);
            cmd.Parameters.AddWithValue("@varattu_pvm", varaus.Varattu_Pvm ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@varattu_alkupvm", varaus.Varattu_Alkupvm);
            cmd.Parameters.AddWithValue("@varattu_loppupvm", varaus.Varattu_Loppupvm);
            cmd.ExecuteNonQuery();
        }

        public void PaivitaVaraus(Varaus varaus)
        {
            if (!varaus.Varattu_Alkupvm.HasValue || !varaus.Varattu_Loppupvm.HasValue)
            {
                throw new ArgumentException("Varauksen alku- ja loppupäivä ovat pakollisia.");
            }
            if (!OnkoVapaa(varaus.Mokki_ID, varaus.Varattu_Alkupvm.Value, varaus.Varattu_Loppupvm.Value, varaus.Varaus_ID))
            {
                throw new InvalidOperationException("Mökki on jo varattu valitulle aikavälille.");
            }

            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                UPDATE varaus SET
                    asiakas_id       = @asiakas_id,
                    mokki_id         = @mokki_id,
                    varattu_alkupvm  = @varattu_alkupvm,
                    varattu_loppupvm = @varattu_loppupvm
                WHERE varaus_id = @varaus_id",
                conn);
            cmd.Parameters.AddWithValue("@asiakas_id", varaus.Asiakas_ID);
            cmd.Parameters.AddWithValue("@mokki_id", varaus.Mokki_ID);
            cmd.Parameters.AddWithValue("@varattu_alkupvm", varaus.Varattu_Alkupvm);
            cmd.Parameters.AddWithValue("@varattu_loppupvm", varaus.Varattu_Loppupvm);
            cmd.Parameters.AddWithValue("@varaus_id", varaus.Varaus_ID);
            cmd.ExecuteNonQuery();
        }

        /// <summary>Poistaa varauksen ja kaikki siihen liittyvät rivit (palvelut, lasku) transaktiossa.</summary>
        public void PoistaVaraus(int varausId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();
            try
            {
                Exec(conn, trans, "DELETE FROM varauksen_palvelut WHERE varaus_id = @id", varausId);
                Exec(conn, trans, "DELETE FROM lasku WHERE varaus_id = @id", varausId);
                Exec(conn, trans, "DELETE FROM varaus WHERE varaus_id = @id", varausId);
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

        private static Varaus LueVaraus(MySqlDataReader r) => new Varaus
        {
            Varaus_ID = r.GetInt32("varaus_id"),
            Asiakas_ID = r.GetInt32("asiakas_id"),
            Mokki_ID = r.GetInt32("mokki_id"),
            Varattu_Pvm = r.IsDBNull(r.GetOrdinal("varattu_pvm")) ? null : r.GetDateTime("varattu_pvm"),
            Vahvistus_Pvm = r.IsDBNull(r.GetOrdinal("vahvistus_pvm")) ? null : r.GetDateTime("vahvistus_pvm"),
            Varattu_Alkupvm = r.IsDBNull(r.GetOrdinal("varattu_alkupvm")) ? null : r.GetDateTime("varattu_alkupvm"),
            Varattu_Loppupvm = r.IsDBNull(r.GetOrdinal("varattu_loppupvm")) ? null : r.GetDateTime("varattu_loppupvm"),
        };
    }
}