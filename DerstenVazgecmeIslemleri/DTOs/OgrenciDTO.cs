using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerstenVazgecmeIslemleri.DTOs
{
    public class OgrenciDTO:KullanicilarDTO
    {
        public int No { get; set; }
        public DateTime GonderimTarihi { get; set; }

    }
}
