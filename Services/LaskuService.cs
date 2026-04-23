using MySqlConnector;
using System;
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
            {
                lista.Add(new Lasku
                {
                    Lasku_ID = reader.GetInt32("lasku_id"),
                    Varaus_ID = reader.GetInt32("varaus_id"),
                    Summa = reader.GetDouble("summa"),
                    Alv = reader.GetDouble("alv"),
                    Maksettu = reader.GetInt32("maksettu")
                });
            }
            return lista;
        }

        public bool OnkoLaskuJoOlemassa(int varausId)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("SELECT COUNT(*) FROM lasku WHERE varaus_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", varausId);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void LuoLaskuVarauksesta(int varausId)
        {
            if (OnkoLaskuJoOlemassa(varausId))
                throw new Exception("Lasku on jo olemassa tälle varaukselle!");

            var varausService = new VarausService();
            var mokkiService = new MokkiService();
            var vpService = new VarauksenPalvelutService();
            var palveluService = new PalveluService();

            var v = varausService.HaeVaraukset().Find(x => x.Varaus_ID == varausId);
            var m = mokkiService.HaeMokit().Find(x => x.Mokki_ID == v.Mokki_ID);

            int paivat = (v.Varattu_Loppupvm.Value - v.Varattu_Alkupvm.Value).Days;
            if (paivat <= 0) paivat = 1;

            decimal mokkiHinta = (decimal)(m?.Hinta ?? 0) * paivat;
            decimal palveluHinta = 0;

            var vpLista = vpService.HaeVarauksenPalvelut(varausId);
            var pKaikki = palveluService.HaePalvelut();

            foreach (var vp in vpLista)
            {
                var p = pKaikki.Find(x => x.Palvelu_ID == vp.Palvelu_ID);
                palveluHinta += (decimal)(p?.Hinta ?? 0) * vp.Lkm;
            }

            decimal verotonYhteensa = mokkiHinta + palveluHinta;
            decimal alvOsuus = verotonYhteensa * 0.24m;

            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO lasku (varaus_id, summa, alv, maksettu) VALUES (@vid, @s, @a, @m)", conn);
            cmd.Parameters.AddWithValue("@vid", varausId);
            cmd.Parameters.AddWithValue("@s", (double)verotonYhteensa);
            cmd.Parameters.AddWithValue("@a", (double)alvOsuus);
            cmd.Parameters.AddWithValue("@m", 0);
            cmd.ExecuteNonQuery();
        }

        public void MerkitseMaksetuksi(int id)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("UPDATE lasku SET maksettu = 1 WHERE lasku_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}