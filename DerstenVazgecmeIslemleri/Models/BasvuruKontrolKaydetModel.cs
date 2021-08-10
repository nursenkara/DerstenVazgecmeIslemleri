using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniOgrenci.Master.Entities;

namespace DerstenVazgecmeIslemleri
{
    public class BasvuruKontrolKaydetModel
    {
        public int BasvurudaKontrolKriterID { get; set; }
        public int BasvuruProgramID { get; set; }
        public bool MinimumMezuniyetNotu4sistem { get; set; }
        public bool MinimumMezuniyetNotu100sistem { get; set; }
        public bool MinimumYuksekLisansMezuniyetNotu4sistem { get; set; }
        public bool MinimumYuksekLisansMezuniyetNotu100sistem { get; set; }
    } 
}
