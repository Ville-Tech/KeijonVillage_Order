using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageNewbies_Projekti.Services
{
    // Kysymysmerkit meinaa, että value voi olla null.
    public class Posti
    {
        private string postinro; // PK
        private string? toimipaikka;

        public string Postinro
        {
            get { return postinro; }
            set
            {
                if (value.Length == 5 && value.All(char.IsDigit))
                {
                    postinro = value;
                }
            }
        }
        public string? Toimipaikka
        {
            get { return toimipaikka; }
            set { toimipaikka = value; }
        }
    }
}
