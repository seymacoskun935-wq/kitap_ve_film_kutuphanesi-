using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;

namespace Kitaplik
{

    [JsonDerivedType(typeof(Kitap), typeDiscriminator: "kitap")]
    [JsonDerivedType(typeof(Film), typeDiscriminator: "film")]
    public class Medya
    {
        private static string yol = "Kitaplik.json";
        public Medya() { }
        public int Id { get; set; }
        public string Baslik { get; set; }
        public int Yil { get; set; }
        public bool Durum { get; set; }

        public Medya( int id, string baslik, int yil, bool durum)
        {
            Id = id;
            Baslik = baslik;
            Yil = yil;
            Durum = durum;
        }

        public static void JsonaKaydet(List<Medya> bölüm)
        {
            var kayıt = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize(bölüm, kayıt);
            File.WriteAllText(yol, json);
        }

        public static List<Medya> JsondanYukle()
        {
            if(File.Exists(yol))
            {
                string json = File.ReadAllText(yol);
                List<Medya> liste = JsonSerializer.Deserialize<List<Medya>>(json);
                return liste;
            }
            return new List<Medya>();
        }

       
        
    }
}
