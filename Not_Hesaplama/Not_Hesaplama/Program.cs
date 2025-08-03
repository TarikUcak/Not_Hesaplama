namespace Not_Hesaplama
{
    class Program
    {
        static string notlarKlasoru = "notlar";

        static void Main(string[] args)
        {
            Directory.CreateDirectory(notlarKlasoru);

            while (true)
            {
                Console.WriteLine("------ NOT HESAPLAMA PROGRAMINA HOŞGELDİNİZ ------");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("1.Not Ekle");
                Console.WriteLine("2.Ders Sil");
                Console.WriteLine("3.Notları Listele");
                Console.WriteLine("4.Not Hesplama");
                Console.WriteLine("5.Çıkış");
                Console.WriteLine("--------------------------------------------------");
                Console.Write("Lütfen Bir Seçim Yapınız: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        NotEkle();
                        break;
                    case "2":
                        NotSil();
                            break;

                    case "3":
                        NotlariListele();
                        break;
                    case "4":
                        NotHesapla();
                        break;
                    case "5":
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin."); Temizle();
                        break; 
                }
            }
        }

        static void NotEkle()
        {
            Console.Write("\nDers Adı: ");
            string dersAdi = Console.ReadLine();

            string notDosyasi = "notlar/" + dersAdi + ".txt";
            if (File.Exists(dersAdi))
            {
                Console.WriteLine("Bu dersten var.");
            }

            Console.Write("Vize Notu: ");
            string vizeNotu = Console.ReadLine();

            Console.Write("Final Notu: ");
            string finalNotu = Console.ReadLine();

            File.WriteAllText(notDosyasi, "Ders Adı: " +  dersAdi + "\n" +
                "Vize Notu: " + vizeNotu + "\n" +
                "Final Notu: " + finalNotu);

            Console.WriteLine("Ders başarılı bir şekilde eklendi.");
            Temizle();
        }
        
        static void NotSil()
        {
            Console.Write("\nLütfen silmek istediğiniz dersin adını giriniz: ");
            string dersAdi = Console.ReadLine();

            string notDosyasi = "notlar/" + dersAdi + ".txt";
            if (File.Exists(notDosyasi))
            {
                File.Delete(notDosyasi);
                Console.WriteLine($"{dersAdi} dersi başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Bu dersten not bulunmamaktadır.");
            }
            Temizle();
        }

        static void NotlariListele()
        {
            string[] notDosyalari = Directory.GetFiles(notlarKlasoru, "*.txt");
            if (notDosyalari.Length == 0)
            {
                Console.WriteLine("\nHenüz eklenmiş bir ders bulunmamaktadır.");
            }
            else
            {
                Console.WriteLine("\nEklenmiş Dersler:");
                Console.WriteLine("-------------------");
                foreach (string dosya in notDosyalari)
                {
                    string dersAdi = Path.GetFileNameWithoutExtension(dosya);
                    string içerik = File.ReadAllText(dosya);
                    Console.WriteLine($"{içerik}\n");
                }
            }
            Temizle();
        }

        static void NotHesapla()
        {
            Console.Write("\nLütfen notunu hesaplamak istediğiniz dersin adını giriniz: ");
            string dersAdi = Console.ReadLine();
            string notDosyasi = "notlar/" + dersAdi + ".txt";
            if (!File.Exists(notDosyasi))
            {
                Console.WriteLine("Bu dersten not bulunmamaktadır.");
                Temizle();
                return;
            }
            string[] notlar = File.ReadAllLines(notDosyasi);
            string vizeNotu = notlar[1].Split(':')[1].Trim();
            string finalNotu = notlar[2].Split(':')[1].Trim();

            if(finalNotu == "-")
            {
                double ortalama = double.Parse(vizeNotu) * 0.4;
                double gerekenFinal = (60 - ortalama) / 0.6;
                if (gerekenFinal < 50)
                {
                    Console.WriteLine($"Bu dersten geçmek için gereken final notu: {50}"); Temizle(); return;
                }
                Console.WriteLine($"Bu dersten geçmek için gereken final notu: {gerekenFinal}");
            }
            else if (double.TryParse(vizeNotu, out double vize) && double.TryParse(finalNotu, out double final))
            {
                double ortalama = (vize * 0.4) + (final * 0.6);
                Console.WriteLine($"\n{dersAdi} dersinin not ortalaması: {ortalama}");
            }
            else
            {
                Console.WriteLine("Geçersiz not formatı.");
            }

            Console.WriteLine("Not hesaplama işlemi tamamlandı.");
            Temizle();
        }

        static void Temizle()
        {
            Console.ReadKey();
            Console.Clear();               
        }
    }
}