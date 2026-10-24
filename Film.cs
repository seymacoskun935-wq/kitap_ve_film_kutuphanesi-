using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Kitaplik
{
    public class Film : Medya
    {
        public string Yonetmen { get; set; }
        public float ImdbPuani { get; set; }

        public Film()
        {

        }
        public Film(int id, string baslik, int yil, bool durum, string yonetmen, float ımdbPuani) : base(id, baslik, yil, durum)
        {
            Yonetmen = yonetmen;
            ImdbPuani = ımdbPuani;
        }
    }
}
