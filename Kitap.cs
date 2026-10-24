using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Kitaplik
{
    public class Kitap : Medya
    {
         
        public string Yazar { get; set; }
        public int SayfaSayisi { get; set; }

        public Kitap() { }
        public Kitap(int id, string baslik, int yil, bool durum,string yazar,int sayfaSayisi) : base(id, baslik, yil, durum)
        {
            Yazar = yazar;
            SayfaSayisi = sayfaSayisi;
        }
    }
}
