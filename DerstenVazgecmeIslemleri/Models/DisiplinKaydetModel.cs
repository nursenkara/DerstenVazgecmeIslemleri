using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniOgrenci.Master.Entities;

namespace DerstenVazgecmeIslemleri
{
    public class DisiplinKaydetModel
    {
        public int ProgramDisiplinBasvuruID { get; set; }
        public int BasvuruProgramID { get; set; }
        public int? BasvurulabilecekDisiplinKodID { get; set; }
        public int? MaxBasvuruDisiplinSayisi { get; set; }
        public int? BasvurulabilecekProgramTuruID { get; set; }
        public int? MaxBasvuruProgramTuruSayisi { get; set; }         
    }  
}
