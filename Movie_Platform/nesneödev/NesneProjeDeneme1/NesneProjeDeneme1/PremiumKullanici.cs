using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    class PremiumKullanici : TemelKullanici
    {
        public override KullaniciBilgileri HesaplaVeGoster()
        {
            KullaniciBilgileri bilgiler = new KullaniciBilgileri();
            bilgiler.Ucret = HesaplaUcret(100, 0.25);
            bilgiler.DenemeSuresi = "7 Gün Ücretsiz Deneme Süresi!";
            return bilgiler;
        }

        private double HesaplaUcret(double standartUcret, double artisOrani)
        {
            // Premium kullanıcı için ücret hesaplaması
            return standartUcret * (1 + artisOrani);
        }
    }
}
