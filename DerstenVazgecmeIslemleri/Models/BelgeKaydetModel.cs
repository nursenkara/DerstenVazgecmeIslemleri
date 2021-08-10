using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniOgrenci.Master.Entities;

namespace DerstenVazgecmeIslemleri
{
    public class BelgeKaydetModel
    {
        public int? BasvuruProgramBelgeID { get; set; }
        public int BelgeTipi{ get; set; } 
        public int BasvuruProgramID { get; set; }
        public bool TcZorunlu { get; set; }
        public bool YuZorunlu { get; set; }
        public int Adim { get; set; }
    }

    public class KodModel {
        public int KodId { get; set; }
        public string Aciklama { get; set; }    
    }
}
