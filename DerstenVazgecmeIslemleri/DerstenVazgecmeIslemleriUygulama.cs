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
using DerstenVazgecmeIslemleri.Enums;

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


            int ogrenciDersId = 42082108;
            int ogrenciId = 1826127;
            ogrencininDersVazgecmeDTOsu.OgrenciDersId = ogrenciDersId; // dışarıdan
            string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0} AND Durum = {1}", ogrenciDersId, 1);
            //kayıt varsa var olan kaydı update edeceksin kayıt yoksa insert edeceksin.
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));
            DersVazgecmeDurumLogKayitInsert(ogrenciDersId);//log
            if (dt != null && dt.Rows.Count > 0)
            {
                //update
                //log
                DersVazgecmeDurumOgrenciUpdate(ogrencininDersVazgecmeDTOsu, ogrenciId);
                //LOG
            }
            else
            {
                DersVazgecmeyiYapanOgrencininIlkKaydi(ogrenciDersId);
                // insert
                // log

            }

        }
   //1 --> 

        public void DanismanOnaylamaDurumSaveOrUpdate2(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            int ogrenciDersId = 42082108;
            int ogrenciId = 1826127;
            ogrencininDersVazgecmeDTOsu.OgrenciDersId = ogrenciDersId; // dışarıdan
            string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0} AND Durum = {1}", ogrenciDersId, 2);
            //kayıt varsa var olan kaydı update edeceksin kayıt yoksa insert edeceksin
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));
            DanismanOnaylamaLog(ogrenciDersId);//log
            if (dt != null && dt.Rows.Count > 0)
            {
                //update
                //log
                DersVazgecmeDanismaninOnayGuncellemesi(ogrencininDersVazgecmeDTOsu);
                //LOG
            }
            else
            {
                DersVazgecmeDanismanOnaylamaKayitInsert(ogrencininDersVazgecmeDTOsu, ogrenciId);
                // insert
                // log

            }

        }



        //butona 






        //DANIŞMANA GONDERİLDİ...
        public void DersVazgecmeyiYapanOgrencininIlkKaydi(int ogrenciDersId)
        {

            string sqlDersVazgecmeOgrenciIlkKayit = string.Format(@"INSERT INTO DersVazgecmeDurum(
        OgrenciBasvurduguTarih,
        OgrenciDersID,
        Durum,
    )
values(
        @ogrenciBasvurduguTarih,
        @ogrenciDersId,
        @durum,
    )");
            
            
            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeOgrenciIlkKayit);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "ogrenciDersID", DbType.Int32, ogrenciDersId);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "ogrenciBasvurduguTarih", DbType.DateTime, DateTime.Now);
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
            OgrenciMaster.Database.AddInParameter(cmdInsert, "durum", DbType.Int32, 1);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

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
        //DANIŞMAN ONAYLADI
        public void DersVazgecmeDanismanOnaylamaKayitInsert(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu, int ogrenciId)
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
GuncellenenTarih={2} where ", 2, ogrencininDersVazgecmeDTOsu.GuncelleyenKisi, DateTime.Now);


            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDanismaninOnayGuncellemesi);

            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
        }


        //öğrencinin aldığı dersleri listele kesin kayıtlı

        public List<OgrencininDersVazgecmeDTO> OgrencininAldigiDersleriListele(int ogrenciId)
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
        public List<OgrenciDersGoruntulemeDTO> DerstenVazgecenOgrencileriListele(int ogretimUyesiId, int ogrenciDersId)
        {
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
            //DURUMA GÖRE DVD TABLSOUNDAN VAZGEÇENLERİ SEÇECEĞİZ
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
where ou.OgretimUyesiId ={0} and dvd.Durum = {1}", ogretimUyesiId, 1);
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sqlDerstenVazgecenOgrencileriListele);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            List<OgrenciDersGoruntulemeDTO> list = new List<OgrenciDersGoruntulemeDTO>();
            OgrenciDersGoruntulemeDTO ogrenciAdSoyad = OgrenciAdiveSoyadiniGetir(ogrenciDersId);

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
        public OgrenciDersGoruntulemeDTO OgrenciAdiveSoyadiniGetir(int ogrenciDersId)
        {
            string sqlOgrenciAdiniSoyadiniGetir = string.Format(@"
select o.Ad,o.Soyad from OgrenciDers od 
inner join Ogrenci o on o.OgrenciID = od.OgrenciID
where od.OgrenciDersID = {0}", ogrenciDersId);
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
        public void DersVazgecmeDurumLogKayitInsert(int ogrenciId)
        {
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
            string sqlDersVazgecmeDurumLogKayitInsert = string.Format(@"
               INSERT INTO DersVazgecmeDurumLog(
        OgrenciBasvurduguTarih,
        OgrenciDersID,
        Durum,
    )
VALUES (
        @OgrenciBasvurduguTarih,
        @OgrenciDersID,
        @Durum,
    )
            ");
            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDurumLogKayitInsert);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvurduguTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrenciId);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 1);


            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void DersVazgecmeDurumOgrenciUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu, int ogrenciId)
        {
              
            try {
                if(OgrenciBasvuruTarihleriArasindaBasvurmusMu(ogrenciId) && OgrenciVazgecebilecegiDersSayisiniAstiMi(ogrencininDersVazgecmeDTOsu.OgrencininVazgectigiDersSayisi))
            {
                //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanaGonderildi;
                string sqlDersVazgecmeDurumUpdateOgrenci = string.Format(@"
Update dvd set
OgrenciBasvurduguTarih={0},
OgrenciDersID={1} ,
Durum = {2},
from DersVazgecmeDurum dvd
inner join OgrenciDers od 
on dvd.OgrenciDersId = od.OgrenciDersId
where od.OgrenciId = {3}", DateTime.Now, ogrencininDersVazgecmeDTOsu.OgrenciDersId, 1, ogrenciId);
               
                //ganoya bakmaya gerek yok zaten update işlemi bu ganosu uymasaydı zaten hiç kayıt yapamazdı...
                DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlDersVazgecmeDurumUpdateOgrenci);

                OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
            }
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

        public bool OgrenciBasvuruTarihleriArasindaBasvurmusMu(int ogrenciId)
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
                    //VazgecebilmekIcinGanoyaGoreBasvuruDurumu basvuruDurumu = VazgecebilmekIcinGanoyaGoreBasvuruDurumu.Basvurabilir;
                    string sql2 = string.Format(@" Update DersVazgecmeAktivite set

DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu = {0}",1);//1 in anlamı başvurabilir
                    DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql2);
                    var dt2 = new DataTable();

                    sonuc = true;
                }
            }
            catch (Exception)
            {
                //VazgecebilmekIcinGanoyaGoreBasvuruDurumu basvuruDurumu = VazgecebilmekIcinGanoyaGoreBasvuruDurumu.Basvuramaz;
                string sql2 = string.Format(@" Update DersVazgecmeAktivite set

DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu = {0}",2); //Başvuramaz.
               
                DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql2);
                var dt2 = new DataTable();

                sonuc = false;
            }
            return sonuc;

        }
        //danışman onayladı log tablosuna da kaydedilecek.
        public void DanismanOnaylamaLog(int ogrenciId)
        {
            string sqlDanismanOnaylamaLog = string.Format(@"
               INSERT INTO DersVazgecmeDurumLog(
        OgrenciBasvurduguTarih,
        OgrenciDersID,
        Durum,
    )
VALUES (
        @OgrenciBasvurduguTarih,
        @OgrenciDersID,
        @Durum,
    )
            ");
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanOnayladi;
            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanOnaylamaLog);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvurduguTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrenciId);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 2);//danışman onayladı


            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);

        }


    }
}