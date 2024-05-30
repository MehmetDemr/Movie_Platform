using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    class StandartKullanici : TemelKullanici
    {
        public override KullaniciBilgileri HesaplaVeGoster()
        {
            KullaniciBilgileri bilgiler = new KullaniciBilgileri();
            bilgiler.Ucret = HesaplaUcret(100);
            bilgiler.DenemeSuresi = "7 Gün Ücretsiz Deneme Süresi!";
            return bilgiler;
        }

        private double HesaplaUcret(double standartUcret)
        {
            // Standart kullanıcı için ücret hesaplaması
            return standartUcret;
        }
    }
}
