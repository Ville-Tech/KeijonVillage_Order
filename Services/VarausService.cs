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

        public bool OnkoVapaa(int mokkiId, DateTime alku, DateTime loppu)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(@"
                SELECT COUNT(*) FROM varaus
                WHERE mokki_id = @mokki_id
                  AND varattu_alkupvm < @loppu
                  AND varattu_loppupvm > @alku",
                conn);
            cmd.Parameters.AddWithValue("@mokki_id", mokkiId);
            cmd.Parameters.AddWithValue("@alku", alku);
            cmd.Parameters.AddWithValue("@loppu", loppu);
            return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
        }

        public List<Varaus> HaeVaraukset()
        {
            var lista = new List<Varaus>();
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM varaus", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(LueVaraus(reader));
            return lista;
        }

        public void LisaaVaraus(Varaus varaus)
        {
            if (!varaus.Varattu_Alkupvm.HasValue || !varaus.Varattu_Loppupvm.HasValue) return;
            if (!OnkoVapaa(varaus.Mokki_ID, varaus.Varattu_Alkupvm.Value, varaus.Varattu_Loppupvm.Value)) return;

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

        public void PoistaVaraus(int varausId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand(
                "DELETE FROM varaus WHERE varaus_id = @varaus_id", conn);
            cmd.Parameters.AddWithValue("@varaus_id", varausId);
            cmd.ExecuteNonQuery();
        }

        private Varaus LueVaraus(MySqlDataReader r)
        {
            return new Varaus
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
}