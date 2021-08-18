using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerstenVazgecmeIslemleri.DTOs
{
    [Serializable]
    public class OgrenciDersVazgecmeDTO
    {
        public int OgrenciId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public List<OgrencininDersVazgecmeDTO> OgrencininDersVazgecmeDtosu;
    }
}
