using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneProjeDeneme1
{
    public class Filmler
    {
        public int Id { get; set; }
        public string FilmAdi { get; set; }
        public string Yonetmen { get; set; }
        public string Oyuncular { get; set; } 
        public string Tur { get; set; }
        public DateTime YayinYili { get; set; }
        public double DegerlendirmePuani { get; set; }
        public int YoneticiId { get; set; } // Film ile yöneticiyi ilişkilendirmek için kullanılacak alan

    }
}
