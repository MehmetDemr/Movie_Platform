using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    // Temel kullanıcı sınıfı
    abstract class TemelKullanici
    {
        public string Uyelik { get; set; }
        public abstract KullaniciBilgileri HesaplaVeGoster(); // Hesaplama ve gösterme metodu, alt sınıflarda implemente edilecek
    }
    // Kullanıcı bilgileri sınıf
    class KullaniciBilgileri
    {
        public double Ucret { get; set; }
        public string DenemeSuresi { get; set; }
    }
}
