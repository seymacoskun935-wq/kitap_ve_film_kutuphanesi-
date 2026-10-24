namespace Kitaplik
{
    internal class Program
    {
        static List<Medya> kutuphane = Medya.JsondanYukle(); 
        static void Main(string[] args)
        {
            Console.WriteLine("*****                              *****");
            Console.WriteLine("**** KİŞİSEL KİTAP & FİLM TAKİPÇİSİ ****");
            Console.WriteLine("*****                              *****");

            while (true) 
            {
                Console.WriteLine("Hoş geldiniz, ne yapmak istersiniz?");
                Secim(); 
            }
           
            
        }
        public static void Secim()
        {
            Console.WriteLine("1- Yeni Kitap / Film ekle " +
                       "2- Listele (okunanlar/izlenenler " +
                       "3- Durum Güncelle (Okundu/ izlendi bilgisini değiştirir) " +
                       "4- Ara " +
                       "5- Sil " +
                       "6- Tüm Medyayı sil "  );
                     



                      

            int secim;
            if(!int.TryParse(Console.ReadLine(), out secim))
            {
                Console.WriteLine("Lütfen sayı girin");
                return;
            }
            if(secim <1 || secim > 6)
            {
                Console.WriteLine("geçersiz değer");
            }
            

          
            
                switch (secim)
                {
                    case 1:
                        Ekle();
                        break;
                    case 2:
                        Listele();
                        break;
                    case 3:
                        Guncelle();
                        break;
                    case 4:
                        Ara();
                        break;
                    case 5:
                        Sil();
                        break;
                    case 6:
                        TümünüSil();
                        break;

                }
             

        }

        public static void Ekle()
        {
            Console.WriteLine("Ne eklemek istersiniz?  1- Kitap  2- Film");
            int ekle = Convert.ToInt32(Console.ReadLine());
           
            Console.Write("Baslik: ");
            string baslik = Console.ReadLine();
            Console.Write("Yil: ");
            int yil = int.Parse(Console.ReadLine());
            Console.Write("bitirdin mi?: 1- evet, 2-hayır ");
            string durum = Console.ReadLine();
            bool okunduMu = false;
            if(durum == "1")
            {
                okunduMu = true;
            }
            else if (durum == "2")
            {
                okunduMu = false;
            }
            else
            {
                Console.WriteLine("Geçersiz seçim yaptınız, otomatik olarak hayır kabul edildi");
            }



            if (ekle == 1)
            {
                Console.Write("Yazar: ");
                string yazar = Console.ReadLine();
                Console.Write("Sayfa sayisi: ");
                int sayfasayisi = int.Parse(Console.ReadLine());

                Kitap yeniKitap = new Kitap(kutuphane.Count + 1, baslik, yil, okunduMu , yazar, sayfasayisi);
                kutuphane.Add(yeniKitap);

            }
            else
            {
                Console.Write("Imdb: ");
                float imdb = float.Parse(Console.ReadLine());
                Console.Write("Yonetmen: ");
                string yonetmen = Console.ReadLine();

                Film yeniFilm = new Film(kutuphane.Count + 1, baslik, yil, okunduMu , yonetmen, imdb);
                kutuphane.Add(yeniFilm);
            }

            Medya.JsonaKaydet(kutuphane);
        }

        public static void Listele()
        {
            Console.WriteLine("-----< Tüm Kütüphane >-----");

            if(kutuphane.Count  == 0)
            {
                Console.WriteLine("Kütüphanenizde henüz hiç medya yok");
                return;
            }

            foreach (var medya in kutuphane)
            {
                string durumYazi = medya.Durum ? "Okundu/İzlendi" : "Henüz Bitmedi";

                if(medya is Kitap k)
                {
                    Console.WriteLine($"[KİTAP] ID: {k.Id} | Başlık: {k.Baslik} | Yazar: {k.Yazar} | Sayfa: {k.SayfaSayisi} | {durumYazi}");
                }

                else if (medya is Film f)
                {
                    Console.WriteLine($"[FİLM] ID: {f.Id} | Başlık: {f.Baslik} | Yonetmen: {f.Yonetmen} | IMDB: {f.ImdbPuani} | {durumYazi}");
                }
            }
        }

        public static void Guncelle()
        {
            Console.WriteLine("-----< Durum Güncelleme >-----");
            Listele();
            Console.WriteLine("Güncellemek istediğiniz medyanın numarasını (ID) girin: ");
          
            if (!int.TryParse(Console.ReadLine(), out int guncelle))
            {
                Console.WriteLine("Lütfen geçerli bir sayı girin");
                return;
            }
            Medya bulunanMedya = kutuphane.FirstOrDefault(m => m.Id == guncelle);

            if (bulunanMedya == null)
            {
                Console.WriteLine("Bu numaraya sahip bir kitap yada film bulunamadı");
                return;
            }
            string mevcutDurumStr = bulunanMedya.Durum ? "Okundu / İzlendi" : "Henüz bitmedi";
            Console.WriteLine($"Bulunan eser: {bulunanMedya.Baslik} | Mevcut durum: {mevcutDurumStr}");

            Console.Write("Yeni durum ne olsun? 1-Okundu/İzlendi, 2-Henüz Bitmedi: ");
            string yeniSecim = Console.ReadLine();

            if(yeniSecim == "1")
            {
                bulunanMedya.Durum = true;
                Console.WriteLine("Durum 'Okundu/İzlendi' olarak guncellendi!");
            }
            else if (yeniSecim == "2")
            {
                bulunanMedya.Durum = false;
                Console.WriteLine("Durum 'Henüz Bitmedi' olarak guncellendi!");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim yaptınız, değişiklik yapılamadı");
                return;
            }

            Medya.JsonaKaydet(kutuphane);
        }
        public static void Ara()
        {
            Console.WriteLine("Aramak istediğiniz film / kitabın adını giriniz: ");
            string ara = Console.ReadLine();


            Console.WriteLine("Aradığınız medya bulundu işte bilgileri: ");
            var aranan = kutuphane.Where(a => a.Baslik.ToLower().Contains( ara.ToLower())) .ToList();

            if (aranan.Count == 0)
            {
                Console.WriteLine("Aradığınız medya bulunamadı");
            }
            foreach ( var a in aranan)
            {
                string durumYazi = a.Durum ? "Okundu/İzlendi" : "Henüz Bitmedi";

                if (a is Kitap k)
                {
                    Console.WriteLine($"[KİTAP] ID: {k.Id} | Başlık: {k.Baslik} | Yazar: {k.Yazar} | Sayfa: {k.SayfaSayisi} | {durumYazi  }");
                }

                else if (a is Film f)
                {
                    Console.WriteLine($"[FİLM] ID: {f.Id} | Başlık: {f.Baslik} | Yonetmen: {f.Yonetmen} | IMDB: {f.ImdbPuani} | {durumYazi  }");
                }
               
            }
        }
        public static void Sil()
        {
            Listele();

            Console.WriteLine("Silmek istediğiniz medyanın numarasını (ID) giriniz: ");

            if (!int.TryParse(Console.ReadLine(), out int sil))
            {
                Console.WriteLine("Lütfen geçerli bir sayı girin");
                return;
            }
             Medya bulunanMedya = kutuphane.FirstOrDefault(m => m.Id == sil);
          
            

            if (bulunanMedya == null)
            {
                Console.WriteLine("Bu numaraya sahip bir kitap yada film bulunamadı");
                return;
            }
            kutuphane.Remove(bulunanMedya);
            Console.WriteLine($" {bulunanMedya.Id} numaralı medya başarıyla silindi");
           
            Medya.JsonaKaydet(kutuphane);
                      

        }
        public static void TümünüSil()
        {
            kutuphane.Clear();
            Medya.JsonaKaydet(kutuphane);
        }
    }
}
