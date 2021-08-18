using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerstenVazgecmeIslemleri.DTOs
{
    [Serializable]
    public class OgrenciDersGoruntulemeDTO
    {
        public int OgrenciDersId { get; set; }
        public string OgrenciAd { get; set; }
        public string OgrenciSoyad { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
     

    }
}
