using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using UniOgrenci.Master;
using Unipa.Framework.UnipaMaster;
using UniOgrenci.Master.Entities;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using UniOgrenci.Master.Exceptions;
using System.Diagnostics;
using DerstenVazgecmeIslemleri.Resources.Lang;
using DerstenVazgecmeIslemleri.DTOs;
using UniOgrenci.Master.Models;

namespace DerstenVazgecmeIslemleri
{
    /// <summary>
    /// Uygulamanın business logic'ini gerçekleştirecek olan sınıf.
    /// </summary>
    [Serializable]
    public partial class DerstenVazgecmeIslemleriUygulama : OgrenciUygulama
    {
        public DerstenVazgecmeIslemleriUygulama(UnipaMaster unipaMaster, int uygulamaId, int projeId) : base(unipaMaster, projeId, uygulamaId) { }


        public void DerstenVazgecmeDurumSaveOrUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            //öğrencidersid ve öğrenciid(öğrenciders veya öğrenci tablosu) çekerek bulabilirsin)

            // string sql = string.Format("");

            string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0}", ogrencininDersVazgecmeDTOsu.OgrenciDersId);
            //kayıt varsa var olan kaydı update edeceksin kayıt yoksa insert edeceksin.
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));

            if (dt != null && dt.Rows.Count > 0) // kayıt varsa update
            {
                DersVazgecmeDurumOgrenciUpdate(ogrencininDersVazgecmeDTOsu);
            }
            else
            {
                DersVazgecmeDurumOgrenciInsert(ogrencininDersVazgecmeDTOsu);
            }
            // DersVazgecmeDurumLogKayitInsert(); // tüm işlemler bittikten sonra yapılacak.
        }
        //1 --> 

        public void DanismanOnaylamaDurumSaveOrUpdate2(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu, string ogrenciId)
        {
            string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum dvd inner join OgrenciDers od on dvd.OgrenciDersId = od.OgrenciDersId 
inner join Ogrenci o on o.OgrenciId = od.OgrenciId
WHERE dvd.Durum = {0} and  o.OgrenciId = {1}
", 2, ogrenciId);
            //kayıt varsa var olan kaydı update edeceksin kayıt yoksa insert edeceksin
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));


            if (dt != null && dt.Rows.Count > 0)
            {
                //update

                DersVazgecmeDanismaninOnayGuncellemesi(ogrencininDersVazgecmeDTOsu);

            }
            else
            {
                DersVazgecmeDanismanOnaylamaKayitInsert(ogrencininDersVazgecmeDTOsu, ogrenciId);
                // insert


            }
            DanismanOnaylamaLog(ogrencininDersVazgecmeDTOsu, ogrenciId);
            //log
        }

        public void OgrenciIsleriSaveorUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sql = string.Format(@"SELECT * FROM DersVazgecmeAktivite WHERE Yil = {0} AND Donem = {1}",
                ogrencininDersVazgecmeDTOsu.Yil,
                ogrencininDersVazgecmeDTOsu.Donem
            );
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));
            if (dt != null && dt.Rows.Count > 0) // kayıt varsa update
            {
                BasvuruOncesiTanimlamaOgrenciIsleriUpdate(ogrencininDersVazgecmeDTOsu);
            }
            else
            {
                BasvuruOncesiIlkTanimlamaOgrenciIsleriInsert(ogrencininDersVazgecmeDTOsu);
            }
            //logu istenmedi
        }

        //butona 






        //DANIŞMANA GONDERİLDİ...
        public void DersVazgecmeDurumOgrenciInsert(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDTO)
        {
            string sql = string.Format(@"
                insert into DersVazgecmeDurum (
	                OgrenciBasvurduguTarih,
	                OgrenciDersID,
	                Durum,
	                IlkEkleyen,
	                IlkEklemeTarihi
                ) values (
	                GetDate(),
	                @OgrenciDersID,
	                1,
	                @IlkEkleyen,
	                GetDate()
                );
            ");
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            OgrenciMaster.Database.AddInParameter(cmd, "OgrenciDersID", DbType.Int32, ogrenciDersVazgecmeDTO.OgrenciDersId);
            OgrenciMaster.Database.AddInParameter(cmd, "IlkEkleyen", DbType.String, ogrenciDersVazgecmeDTO.IlkEkleyenKisi);
            OgrenciMaster.Database.ExecuteNonQuery(cmd);
        }
        //update
        public void BasvuruOncesiTanimlamaOgrenciIsleriUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sqlBasvuruOncesiTanimlamaOgrenciIsleriUpdate = @"
Update DersVazgecmeAktivite 
set
Gano = @Gano,
OgrenciBasvuruBaslangicTarihi = @OgrenciBasvuruBaslangicTarihi,
OgrenciBasvuruBitisTarihi = @OgrenciBasvuruBitisTarihi,
DanismanOnayBaslangicTarihi = @DanismanOnayBaslangicTarihi,
DanismanOnayBitisTarihi = @DanismanOnayBitisTarihi,
Yil = @Yil,
Donem = @Donem,
AyniAndaVazgecebilecegiDersSayisi = @AyniAndaVazgecebilecegiDersSayisi,
AyniDerstenBaskaDonemdeVazgecebilirMi = @AyniDerstenBaskaDonemdeVazgecebilirMi
";
            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlBasvuruOncesiTanimlamaOgrenciIsleriUpdate);

            OgrenciMaster.Database.AddInParameter(cmdUpdate, "Gano", DbType.Decimal, ogrencininDersVazgecmeDTOsu.Gano);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "OgrenciBasvuruBaslangicTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.OgrenciBasvuruBaslangicTarihi);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "OgrenciBasvuruBitisTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.OgrenciBasvuruBitisTarihi);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "DanismanOnayBaslangicTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.DanismanOnayBaslangicTarihi);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "DanismanOnayBitisTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.DanismanOnayBitisTarihi);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "Yil", DbType.Int32, ogrencininDersVazgecmeDTOsu.Yil);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "Donem", DbType.Int32, ogrencininDersVazgecmeDTOsu.Donem);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "AyniAndaVazgecebilecegiDersSayisi", DbType.Int32, ogrencininDersVazgecmeDTOsu.AyniAndaVazgecebilecegiDersSayisi);
            OgrenciMaster.Database.AddInParameter(cmdUpdate, "AyniDerstenBaskaDonemdeVazgecebilirMi", DbType.Boolean, ogrencininDersVazgecmeDTOsu.AyniDerstenBaskaDonemdeVazgecebilirMi);

            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);

        }

        //update komutu da olması lazım
        public void BasvuruOncesiIlkTanimlamaOgrenciIsleriInsert(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {


            string sqlBasvuruOncesiIlkKayitOgrenciIsleri = string.Format(@"
                Insert into DersVazgecmeAktivite(
                        Gano,
                        OgrenciBasvuruBaslangicTarihi,
                        OgrenciBasvuruBitisTarihi,
                        DanismanOnayBaslangicTarihi,
                        DanismanOnayBitisTarihi,
                        Yil,
                        Donem,
                        AyniAndaVazgecebilecegiDersSayisi,
                        AyniDerstenBaskaDonemdeVazgecebilirMi
                    )
                values (
                        @Gano,
                        @OgrenciBasvuruBaslangicTarihi,
                        @OgrenciBasvuruBitisTarihi,
                        @DanismanOnayBaslangicTarihi,
                        @DanismanOnayBitisTarihi,
                        @Yil,
                        @Donem,
                        @AyniAndaVazgecebilecegiDersSayisi,
                        @AyniDerstenBaskaDonemdeVazgecebilirMi
                    )
            ");
            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlBasvuruOncesiIlkKayitOgrenciIsleri);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Gano", DbType.Decimal, ogrencininDersVazgecmeDTOsu.Gano);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvuruBaslangicTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.OgrenciBasvuruBaslangicTarihi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvuruBitisTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.OgrenciBasvuruBitisTarihi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "DanismanOnayBaslangicTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.DanismanOnayBaslangicTarihi)
                ;
            OgrenciMaster.Database.AddInParameter(cmdInsert, "DanismanOnayBitisTarihi", DbType.DateTime, ogrencininDersVazgecmeDTOsu.DanismanOnayBitisTarihi)
               ;
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Yil", DbType.Int32, ogrencininDersVazgecmeDTOsu.Yil);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Donem", DbType.Int32, ogrencininDersVazgecmeDTOsu.Donem);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "AyniAndaVazgecebilecegiDersSayisi", DbType.Int32, ogrencininDersVazgecmeDTOsu.AyniAndaVazgecebilecegiDersSayisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "AyniDerstenBaskaDonemdeVazgecebilirMi", DbType.Boolean, ogrencininDersVazgecmeDTOsu.AyniDerstenBaskaDonemdeVazgecebilirMi);


            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }
        // DANIŞMAN ONAYLADI -- insert yapılmayacak sadece update ile durumu değiştirilecek.
        public void DersVazgecmeDanismanOnaylamaKayitInsert(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu, string ogrenciId)
        {
            string sqlDersVazgecmeDanismanOnaylamaKayitInsert = string.Format(@"
                Insert into DersVazgecmeDurum(
                        Durum,
                        OnaylayanKisi,
                        OnaylandigiTarih,
                        OgrenciDersID,
                        VazgectigiDersSayisi
                    )
                values (
                        @Durum,
                        @OnaylayanKisi,
                        @OnaylandigiTarih,
                        @OgrenciDersID,
                        @VazgectigiDersSayisi
                    )
select od from DersVazgecmeDurum dvd inner join OgrenciDers od on dvd.OgrenciDersId = od.OgrenciDersId 
where od.OgrenciId = ", ogrenciId);

            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDanismanOnaylamaKayitInsert);
            if (OgrenciVazgecebilecegiDersSayisiniAstiMi(ogrencininDersVazgecmeDTOsu.OgrencininVazgectigiDersSayisi))
            {
                //true ise aşmadı
                OgrenciMaster.Database.AddInParameter(cmdInsert, "VazgectigiDersSayisi", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrencininVazgectigiDersSayisi);
            }
            else
            {
                Console.WriteLine(ogrencininDersVazgecmeDTOsu.AyniAndaVazgecebilecegiDersSayisi +
                    " ders vazgeçebilirsiniz..." + (ogrencininDersVazgecmeDTOsu.OgrencininVazgectigiDersSayisi - ogrencininDersVazgecmeDTOsu.AyniAndaVazgecebilecegiDersSayisi) + " ders çıkarmalısınız...");
                return;
            }
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanOnayladi;
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 2); //DANIŞMAN ONAYLADI...
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylayanKisi", DbType.String, ogrencininDersVazgecmeDTOsu.OnaylayanKisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylandigiTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrenciDersId);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }


        public void DersVazgecmeDanismaninOnayGuncellemesi(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanOnayladi;
            string sqlDersVazgecmeDanismaninOnayGuncellemesi = string.Format(@"Update DersVazgecmeDurum set
Durum={0},
GuncelleyenKisi={1} ,
GuncellenenTarih={2}", 2, ogrencininDersVazgecmeDTOsu.GuncelleyenKisi, DateTime.Now);


            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDanismaninOnayGuncellemesi);

            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
        }


        //öğrencinin aldığı dersleri listele kesin kayıtlı

        public List<OgrencininDersVazgecmeDTO> OgrencininAldigiDersleriListele(string ogrenciId)
        {
            string sqlOgrencininKesinKayitliOlduguDersler = string.Format(@"
                    select
                        od.OgrenciDersId,
                        d.dersKodu,
                        d.DersAdi
                    from ogrenci o
                        inner join OgrenciDers od on od.OgrenciID = o.OgrenciID
                        inner join ders d on d.DersID = od.DersID
                        left join YonetmelikHarfNot yhn on yhn.YonetmelikHarfNotID = od.HarfNotID
                        left join YonetmelikHarfNot yhn2 on yhn2.YonetmelikHarfNotID = od.DersAlisSekliID
                    where od.KY_DevamEdiyor = 0
                        and o.OgrenciID =(
                            select o.ogrenciID
                            from UnipaMasterDB_Test..Kullanici k
                                inner join UnipaMasterDB_Test..KullaniciProfil kp on k.KullaniciID = kp.KullaniciID
                                inner join Ogrenci o on o.OgrenciID = kp.EkBilgi1
                            where o.OgrenciID = {0})
                      order by
                        od.OgretimYili,
                        od.OgretimDonemi asc  ", ogrenciId);
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sqlOgrencininKesinKayitliOlduguDersler);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            List<OgrencininDersVazgecmeDTO> list = new List<OgrencininDersVazgecmeDTO>();

            list = dt.AsEnumerable()
    .Select(row => new OgrencininDersVazgecmeDTO
    {
        OgrenciDersId = row.Field<int>("OgrenciDersId"),
        DersKodu = row.Field<string>("DersKodu"),
        DersAdi = row.Field<string>("DersAdi")
    }).ToList();
            return list;

        }

        //danışmana gönderildi ve danışmanın sayfasında listelenecek
        public List<OgrenciDersGoruntulemeDTO> DerstenVazgecenOgrencileriListele(string ogrenciId)
        {
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
            //DURUMA GÖRE DVD TABLOSUNDAN VAZGEÇENLERİ SEÇECEĞİZ...

            string sqlDerstenVazgecenOgrencileriListele = string.Format(@"
select od.OgrenciDersId,o.Ad,d.DersKodu,d.DersAdi 
from DersVazgecmeDurum dvd inner join OgrenciDers od 
on dvd.OgrenciDersId = od.OgrenciDersId
inner join Ogrenci o
on o.OgrenciId = od.OgrenciId
inner join OgretimUyesi ou
on ou.OgretimUyesiId = o.DanismanId
inner join Ders d
on d.dersId = od.DersId
where dvd.Durum = {0} and o.OgrenciId = {1}", 1, ogrenciId);

            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sqlDerstenVazgecenOgrencileriListele);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            List<OgrenciDersGoruntulemeDTO> list = new List<OgrenciDersGoruntulemeDTO>();

            OgrenciDersGoruntulemeDTO ogrenciAdSoyad = OgrenciAdiveSoyadiniGetir(ogrenciId);


            list = dt.AsEnumerable()
    .Select(row => new OgrenciDersGoruntulemeDTO
    {
        DersKodu = row.Field<string>("DersKodu"),
        DersAdi = row.Field<string>("DersAdi"),
        OgrenciAd = ogrenciAdSoyad.OgrenciAd,
        OgrenciSoyad = ogrenciAdSoyad.OgrenciSoyad
    }).ToList();
            return list;
        }

        public void OgrenciGeriAlInsert()
        {
        }
        public OgrenciDersGoruntulemeDTO OgrenciAdiveSoyadiniGetir(string ogrenciId)
        {
            string sqlOgrenciAdiniSoyadiniGetir = string.Format(@"select Ad, Soyad from ogrenci where OgrenciId = {0}", ogrenciId);
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sqlOgrenciAdiniSoyadiniGetir);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            List<OgrenciDersGoruntulemeDTO> list = new List<OgrenciDersGoruntulemeDTO>();

            list = dt.AsEnumerable().Select(row => new OgrenciDersGoruntulemeDTO
            {
                OgrenciAd = row.Field<string>("Ad"),
                OgrenciSoyad = row.Field<string>("Soyad"),
            }).ToList();

            return list.FirstOrDefault();
        }

        //öğrenci vazgeçti danışmana gönderildi logu
        public void DersVazgecmeDurumLogKayitInsert()
        {
            //            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
            //            string sqlDersVazgecmeDurumLogKayitInsert = string.Format(@"
            //               INSERT INTO DersVazgecmeDurumLog(
            //        OgrenciBasvurduguTarih,
            //        OgrenciDersID,
            //        Durum,
            //    )
            //VALUES (
            //        @OgrenciBasvurduguTarih,
            //        @OgrenciDersID,
            //        @Durum,
            //    )
            //            ");
            //            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDurumLogKayitInsert);
            //            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvurduguTarih", DbType.DateTime, DateTime.Now);
            //            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrenciId);
            //            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 1);


            //            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void DersVazgecmeDurumOgrenciUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            try
            {
                //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
                string sqlDersVazgecmeDurumUpdateOgrenci = string.Format(@"
                        update DersVazgecmeDurum set
                        OgrenciBasvurduguTarih = GetDate(),
                        Durum = 1,
                        GuncelleyenKisi = {1},
                        GuncellenenTarih = GetDate()
                        where OgrenciDersID = {0}
                    ",
                    ogrencininDersVazgecmeDTOsu.OgrenciDersId,
                    ogrencininDersVazgecmeDTOsu.GuncelleyenKisi
                );

                //ganoya bakmaya gerek yok zaten update işlemi bu ganosu uymasaydı zaten hiç kayıt yapamazdı...
                DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDurumUpdateOgrenci);
                OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
            }
            catch
            {
                Console.WriteLine("Başvuru tarihleri arasında değil ve/veya öğrenci vazgeçebileceği ders sayısını aştı.");
            }

            //danışman onayı bekleniyor diye bir durum daha olması lazım danışman onayladıktan sonra onaylandı durumuna geçmesi lazım

        }


        public bool OgrenciVazgecebilecegiDersSayisiniAstiMi(int vazgectigiDersSayisi)
        {
            bool sonuc = false;
            string sql = string.Format(@" SELECT COUNT(1)
                            FROM DersVazgecmeAktivite dva INNER JOIN DersVazgecmeDurum dvd ON dva.DersVazgecmeDurumID = dvd.DersVazgecmeDurumID
           
                            WHERE dva.AyniAndaVazgecebilecegiDersSayisi < @vazgectigiDersSayisi and dva.AyniAndaVazgecebilecegiDersSayisi = @vazgectigiDersSayisi ");

            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            var dt = new DataTable();

            try
            {
                _cmd.Parameters.Add(new SqlParameter("vazgectigiDersSayisi", vazgectigiDersSayisi));

                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                if (dt.Rows.Count > 0)
                    sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            return sonuc;
        }



        public byte GetAyniAndaVazgecebilecegiDersSayisi()
        {
            byte num = 0;
            string sql = string.Format(@"select top 1 AyniAndaVazgecebilecegiDersSayisi from DersVazgecmeAktivite");
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));
            if (dt != null && dt.Rows.Count > 0)
            {
                byte.TryParse(dt.Rows[0]["AyniAndaVazgecebilecegiDersSayisi"].ToString(), out num);
            }
            return num;
        }

        public bool OgrenciAyniDerstenBaskaDonemdeVazgecebilirMi(bool ayniDerstenBaskaDonemdeVazgecebilirMi)
        {
            bool sonuc = false;
            string sql = string.Format(@" SELECT COUNT(1)
                            FROM DersVazgecmeAktivite
           
                            WHERE AyniDerstenBaskaDonemdeVazgecebilirMi = @ayniDerstenBaskaDonemdeVazgecebilirMi and dva.AyniAndaVazgecebilecegiDersSayisi = @vazgectigiDersSayisi ");

            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            var dt = new DataTable();

            try
            {
                _cmd.Parameters.Add(new SqlParameter("ayniDerstenBaskaDonemdeVazgecebilirMi", ayniDerstenBaskaDonemdeVazgecebilirMi));

                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                if (dt.Rows.Count > 0)
                    sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            return sonuc;
        }
        //Öğrenci başvuru tarihleri arasında başvurmuş mu?

        public bool OgrenciBasvuruTarihleriArasindaBasvurmusMu(string ogrenciId)
        {
            bool sonuc = false;
            string sql = string.Format(@" SELECT COUNT(1)
                            FROM DersVazgecmeAktivite dva INNER JOIN DersVazgecmeDurum dvd on 
dva.DersVazgecmeDurumId = dvd.DersVazgecmeDurumId 
inner join OgrenciDers  od on od.OgrenciDersId = dvd.OgrenciDersId
           
                            WHERE  dvd.OgrenciBasvurduguTarih between  dva.OgrenciBasvuruBaslangicTarihi and dva.OgrenciBasvuruBitisTarihi and od.OgrenciId = " + ogrenciId);
            //SORU: Bu sorguyu doğru yapmış mıyım?

            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            var dt = new DataTable();

            try
            {
                // _cmd.Parameters.Add(new SqlParameter("ayniDerstenBaskaDonemdeVazgecebilirMi", ayniDerstenBaskaDonemdeVazgecebilirMi));

                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                if (dt.Rows.Count > 0)
                    sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            return sonuc;

        }
        //Danışman onay tarihleri arasında onaylamış mı?


        public bool DanismanOnayTarihleriArasindaOnaylamisMi(DateTime danismaninOnayladigiTarih)
        {
            bool sonuc = false;
            string sql = string.Format(@" SELECT COUNT(1)
                            FROM DersVazgecmeAktivite dva INNER JOIN DersVazgecmeDurum dvd on 
dva.DersVazgecmeDurumId = dvd.DersVazgecmeDurumId
           
                            WHERE  dvd.OnaylandigiTarih between dva.DanismanOnayBaslangicTarihi and 
dva.DanismanOnayBitisTarihi");


            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            var dt = new DataTable();

            try
            {


                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                if (dt.Rows.Count > 0)
                    sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }
            return sonuc;

        }




        //Ganoya göre öğrenci başvurabilir mi?
        public bool DerstenVazgecebilmekIcinGanosunaGoreBasvuruDurumu(decimal gano)
        {
            bool sonuc = false;
            string sql = string.Format(@" SELECT COUNT(1)
                            FROM DersVazgecmeAktivite dva INNER JOIN DersVazgecmeDurum dvd on 
dva.DersVazgecmeDurumId = dvd.DersVazgecmeDurumId
           
                            WHERE  dva.Gano < @gano and dva.Gano = @gano");
            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            var dt = new DataTable();

            try
            {
                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                if (dt.Rows.Count > 0)
                {

                    string sql2 = string.Format(@" Update DersVazgecmeAktivite set

DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu = {0}", 1);//1 in anlamı başvurabilir
                    DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql2);
                    var dt2 = new DataTable();

                    sonuc = true;
                }
            }
            catch (Exception)
            {

                string sql2 = string.Format(@" Update DersVazgecmeAktivite set

DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu = {0}", 2); //Başvuramaz.

                DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql2);
                var dt2 = new DataTable();

                sonuc = false;
            }
            return sonuc;

        }
        //danışman onayladı log tablosuna da kaydedilecek.
        public void DanismanOnaylamaLog(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu, string ogrenciId)
        {
            string sqlDanismanOnaylamaLog = string.Format(@"
                Insert into DersVazgecmeDurum(
                        Durum,
                        OnaylayanKisi,
                        OnaylandigiTarih,
                        OgrenciDersID,
                        VazgectigiDersSayisi
                    )
                values (
                        @Durum,
                        @OnaylayanKisi,
                        @OnaylandigiTarih,
                        @OgrenciDersID,
                        @VazgectigiDersSayisi
                    )
select od from DersVazgecmeDurum dvd inner join OgrenciDers od on dvd.OgrenciDersId = od.OgrenciDersId 
where od.OgrenciId = ", ogrenciId);

            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanOnaylamaLog);

            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanOnayladi;
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 2); //DANIŞMAN ONAYLADI...
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylayanKisi", DbType.String, ogrencininDersVazgecmeDTOsu.OnaylayanKisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylandigiTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrenciDersId);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }



    }
}