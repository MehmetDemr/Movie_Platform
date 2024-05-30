using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    // Yöneticiyle nerdeyse çoğu şeyi paylaştıkları için kalıtım uygulanıyor..
    public class Kullanici : Yonetici
    {
        public string Uyelik { get; set; }
    }
}
