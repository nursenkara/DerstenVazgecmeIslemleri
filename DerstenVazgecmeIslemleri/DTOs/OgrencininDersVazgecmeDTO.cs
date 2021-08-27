using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DerstenVazgecmeIslemleri.DTOs
{
    [Serializable]
    public class OgrencininDersVazgecmeDTO
    {
        public int DersVazgecmeAktiviteId { get; set; }
        public int OgrenciDersId { get; set; }
        public string DersAdi { get; set; }
        public string DersKodu { get; set; }
        public int Durum { get; set; }
        public string IlkEkleyenKisi { get; set; }
        public DateTime IlkEklemeTarihi { get; set; }
        public string GuncelleyenKisi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
        public string OnaylayanKisi { get; set; }
        public DateTime OnaylamaTarihi { get; set; }
        public string ReddedenKisi { get; set; }
        public DateTime ReddetmeTarihi { get; set; }
        public DateTime OgrencininBasvurduguTarih { get; set; }
        public int AyniAndaVazgecebilecegiDersSayisi { get; set; }
        public int OgrencininVazgectigiDersSayisi { get; set; }
        public Boolean AyniDerstenBaskaDonemdeVazgecebilirMi  { get; set; }
        public string IslemiYapanKullanici { get; set; }
        public DateTime OgrenciBasvuruBaslangicTarihi  { get; set; }
        public DateTime OgrenciBasvuruBitisTarihi  { get; set; }
        public DateTime DanismanOnayBaslangicTarihi  { get; set; }
        public DateTime DanismanOnayBitisTarihi  { get; set; }
        public int Yil { get; set; }
        public int Donem { get; set; }
        public int DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu { get; set; }
        public decimal OgrenciIslerininBelirledigiGano { get; set; }
    }
}
