using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sena
{
    public class uye
    {
        public string kullanici_adi { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string parola { get; set; }
        public string e_posta { get; set; }
        public string ceptel { get; set; }
        public string uyelik_tipi { get; set; }


    }
    public class ilan
    {

        public string kullanici_adi { get; set; }
        public int ilan_no { get; set; }
        public bool oncelik { get; set; }
        public string ilan_icerigi { get; set; }
        public string ilan_baslıgı { get; set; }
        public string ilan_fiyati { get; set; }
        public string kategori { get; set; }
        public string resim_yolu { get; set; }
    }
    public class goruntulenme
    {
        public int ilan_no { get; set; }
        public int goruntulenme_sayisi { get; set; }
    }
    public class okunan_mesaj
    {
        public string kullanici_adi { get; set; }
        public int mesaj_id { get; set; }
        public DateTime tarih { get; set; }
    }
}
