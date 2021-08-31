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


        public void OgrenciVazgectiSaveOrUpdateAndLog(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {

            string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0}", ogrencininDersVazgecmeDTOsu.OgrenciDersId);
            
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));

            if (dt != null && dt.Rows.Count > 0) 
            {
                OgrenciVazgectiUpdate(ogrencininDersVazgecmeDTOsu);
            }
            else
            {
                OgrenciVazgectiInsert(ogrencininDersVazgecmeDTOsu);
            }
            OgrenciVazgectiLog(ogrencininDersVazgecmeDTOsu.OgrenciDersId); 
        }

          public void DanismanaGonderildiSaveOrUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {

            string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0} and Durum = 1", ogrencininDersVazgecmeDTOsu.OgrenciDersId);
            
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));

            if (dt != null && dt.Rows.Count > 0) 
            {
                DanismanaGonderildiUpdate(ogrencininDersVazgecmeDTOsu);
            }
            else
            {
                DanismanaGonderildiInsert(ogrencininDersVazgecmeDTOsu);
            }
           // DanismanaGonderildiLog(ogrencininDersVazgecmeDTOsu.OgrenciDersId); 
        }

          //public void DanismanOnaySaveOrUpdateAndLog(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
          //{

          //    string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0}", ogrencininDersVazgecmeDTOsu.OgrenciDersId);

          //    DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));

          //    if (dt != null && dt.Rows.Count > 0)
          //    {
          //        DanismanOnayUpdate(ogrencininDersVazgecmeDTOsu);
          //    }
          //    else
          //    {
          //        DanismanOnayInsert(ogrencininDersVazgecmeDTOsu);
          //    }
          //    DanismanOnaylamaLog(ogrencininDersVazgecmeDTOsu);
          //}

          //public void DanismanRedSaveOrUpdateAndLog(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
          //{

          //    string sql = string.Format(@"SELECT * FROM DersVazgecmeDurum WHERE OgrenciDersID ={0}", ogrencininDersVazgecmeDTOsu.OgrenciDersId);

          //    DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));

          //    if (dt != null && dt.Rows.Count > 0)
          //    {
          //        DanismanRedUpdate(ogrencininDersVazgecmeDTOsu);
          //    }
          //    else
          //    {
          //        DanismanRedInsert(ogrencininDersVazgecmeDTOsu);
          //    }
          //    DanismanRedLog(ogrencininDersVazgecmeDTOsu);
          //}
         

        public void OgrenciIsleriSaveorUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sql = string.Format(@"SELECT * FROM DersVazgecmeAktivite WHERE Yil = {0} AND Donem = {1}",
                ogrencininDersVazgecmeDTOsu.Yil,
                ogrencininDersVazgecmeDTOsu.Donem
            );
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));
            if (dt != null && dt.Rows.Count > 0) // kayıt varsa update
            {
                OgrenciIsleriUpdate(ogrencininDersVazgecmeDTOsu);
            }
            else
            {
                OgrenciIsleriInsert(ogrencininDersVazgecmeDTOsu);
            }
           
        }

        public int GetDersVazgecmeAktiviteId()
        {
            int id = 0;
            string sql = string.Format(@"select top 1 DersVazgecmeAktiviteId from DersVazgecmeAktivite");
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(sql));
             if (dt != null && dt.Rows.Count > 0)
            {
                int.TryParse(dt.Rows[0]["DersVazgecmeAktiviteId"].ToString(), out id);
            }
           return id;
        }

        public void DanismanRedLog(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sqlDanismanRedLog = string.Format(@"
                            Insert into DersVazgecmeDurumLog(
                                    Durum,
                                    ReddedenKisi,
                                    ReddetmeTarihi,
                                    OgrenciDersID
                                )
                            values (
                                    @Durum,
                                    @ReddedenKisi,
                                    @ReddetmeTarihi,
                                    @OgrenciDersID
                                )
            ");

            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanRedLog);


            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 3); //DANIŞMAN REDDETTİ...
            OgrenciMaster.Database.AddInParameter(cmdInsert, "ReddedenKisi", DbType.String, ogrencininDersVazgecmeDTOsu.ReddedenKisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "ReddetmeTarihi", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrenciDersId);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void DanismanRedUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sql = string.Format(@"
                Update DersVazgecmeDurum set
                    Durum = 3,
                    GuncelleyenKisi = '{0}',
                    GuncellenenTarih = GetDate()
                where OgrenciDersID = {1}
            ",
             ogrencininDersVazgecmeDTOsu.GuncelleyenKisi,
             ogrencininDersVazgecmeDTOsu.OgrenciDersId
            );
            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sql);
            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);

        }


        public void DanismanRedInsert(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sqlDanismanRed = string.Format(@"
                            Insert into DersVazgecmeDurum(
                                    Durum,
                                    ReddedenKisi,
                                    ReddetmeTarihi,
                                    OgrenciDersID
                                )
                            values (
                                    @Durum,
                                    @ReddedenKisi,
                                    @ReddetmeTarihi,
                                    @OgrenciDersID
                                )
            ");

            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanRed);


            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 3); //danisman reddetti
            OgrenciMaster.Database.AddInParameter(cmdInsert, "ReddedenKisi", DbType.String, ogrencininDersVazgecmeDTOsu.ReddedenKisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "ReddetmeTarihi", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrenciDersId);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void OgrenciGeriAlInsert(int ogrenciDersId)
        {
            string sql = string.Format(@"
           UPDATE DersVazgecmeDurum
           SET  Durum = 4
           WHERE OgrenciDersId = {0}
            ", ogrenciDersId);
            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sql);
       


            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
        }

        public void OgrenciVazgectiLog(int ogrenciDersId)
        {

            string sql = string.Format(@"
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
            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sql);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvurduguTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrenciDersId);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 5);//ogrenci vazgecti


            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void OgrenciVazgectiInsert(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDTO)
        {
            string sql = string.Format(@"insert into DersVazgecmeDurum (OgrenciDersID,Durum) 
values (@OgrenciDersID,5)");
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            OgrenciMaster.Database.AddInParameter(cmd, "OgrenciDersID", DbType.Int32, ogrenciDersVazgecmeDTO.OgrenciDersId);
           
            OgrenciMaster.Database.ExecuteNonQuery(cmd);
        }

        public void OgrenciVazgectiUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {

            string sql = string.Format(@"
                        update DersVazgecmeDurum set
                        OgrenciBasvurduguTarih = GetDate(),
                        Durum = 5,
                        GuncellenenTarih = GetDate(),
                        where OgrenciDersID = {0}
                    ",
                ogrencininDersVazgecmeDTOsu.OgrenciDersId
            );


            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sql);
            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);


        }

        public void DanismanaGonderildiLog(int ogrenciDersId)
        {

            string sqlDanismanaGonderildiDurumLogKayit = string.Format(@"
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
            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanaGonderildiDurumLogKayit);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciBasvurduguTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrenciDersId);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 1);


            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void DanismanaGonderildiInsert(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDTO)
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

        public void DanismanaGonderildiUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
           
                string sqlDanismanaGonderildiUpdate = string.Format(@"
                        update DersVazgecmeDurum set
                        OgrenciBasvurduguTarih = GetDate(),
                        Durum = 1,
                        GuncelleyenKisi = '{1}',
                        GuncellenenTarih = GetDate()
                        where OgrenciDersID = {0}
                    ",
                    ogrencininDersVazgecmeDTOsu.OgrenciDersId,
                    ogrencininDersVazgecmeDTOsu.GuncelleyenKisi
                );


                DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanaGonderildiUpdate);
                OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
        

        }

        public void DanismanOnaylamaLog(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sqlDanismanOnaylamaLog = string.Format(@"
                            Insert into DersVazgecmeDurumLog(
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
            ");

            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanOnaylamaLog);


            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 2); //DANIŞMAN ONAYLADI...
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylayanKisi", DbType.String, ogrencininDersVazgecmeDTOsu.OnaylayanKisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylandigiTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrenciDersId);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }

        public void DanismanOnayUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            //DersVazgecmeDurumlar durum = DersVazgecmeDurumlar.DanismanOnayladi; // ENUM İPTAL EDİLDİ.
            string sql = string.Format(@"
                Update DersVazgecmeDurum set
                    Durum = 2,
                    GuncelleyenKisi = '{0}',
                    GuncellenenTarih = GetDate()
                where OgrenciDersID = {1}
            ",
             ogrencininDersVazgecmeDTOsu.GuncelleyenKisi,
             ogrencininDersVazgecmeDTOsu.OgrenciDersId
            );
            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sql);
            OgrenciMaster.Database.ExecuteNonQuery(cmdUpdate);
        }


       






        public void DanismanOnayInsert(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu) 
        {
            string sqlDanismanOnay = string.Format(@"
                            Insert into DersVazgecmeDurum(
                                    Durum,
                                    OnaylayanKisi,
                                    OnaylandigiTarih,
                                    OgrenciDersID
                                )
                            values (
                                    @Durum,
                                    @OnaylayanKisi,
                                    @OnaylandigiTarih,
                                    @OgrenciDersID
                                )
            ");

            DbCommand cmdInsert = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanOnay);


            OgrenciMaster.Database.AddInParameter(cmdInsert, "Durum", DbType.Int32, 2);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylayanKisi", DbType.String, ogrencininDersVazgecmeDTOsu.OnaylayanKisi);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OnaylandigiTarih", DbType.DateTime, DateTime.Now);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "OgrenciDersID", DbType.Int32, ogrencininDersVazgecmeDTOsu.OgrenciDersId);

            OgrenciMaster.Database.ExecuteNonQuery(cmdInsert);
        }


        public void OgrenciIsleriUpdate(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {
            string sqlBasvuruOncesiTanimlamaOgrenciIsleriUpdate = string.Format(@"
Update DersVazgecmeAktivite 
set
Gano = @Gano,
OgrenciBasvuruBaslangicTarihi = @OgrenciBasvuruBaslangicTarihi,
OgrenciBasvuruBitisTarihi = @OgrenciBasvuruBitisTarihi,
DanismanOnayBaslangicTarihi = @DanismanOnayBaslangicTarihi,
DanismanOnayBitisTarihi = @DanismanOnayBitisTarihi,
AyniAndaVazgecebilecegiDersSayisi = @AyniAndaVazgecebilecegiDersSayisi,
AyniDerstenBaskaDonemdeVazgecebilirMi = @AyniDerstenBaskaDonemdeVazgecebilirMi
where Yil = {0} and Donem = {1}", ogrencininDersVazgecmeDTOsu.Yil, ogrencininDersVazgecmeDTOsu.Donem);
            DbCommand cmdUpdate = OgrenciMaster.Database.GetSqlStringCommand(sqlBasvuruOncesiTanimlamaOgrenciIsleriUpdate);

            OgrenciMaster.Database.AddInParameter(cmdUpdate, "Gano", DbType.Decimal, ogrencininDersVazgecmeDTOsu.OgrenciIslerininBelirledigiGano);
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


        public void OgrenciIsleriInsert(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu)
        {


            string sqlBasvuruOncesiIlkKayitOgrenciIsleri = string.Format(@"
                Insert into DersVazgecmeAktivite(
                        Gano,
                        DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu,
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
                        @DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu,
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
            OgrenciMaster.Database.AddInParameter(cmdInsert, "Gano", DbType.Decimal, ogrencininDersVazgecmeDTOsu.OgrenciIslerininBelirledigiGano);
            OgrenciMaster.Database.AddInParameter(cmdInsert, "DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu", DbType.Int32, ogrencininDersVazgecmeDTOsu.DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu);

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


        public List<OgrencininDersVazgecmeDTO> OgrenciIsleriKayitlariListele()
        {
            string sql = string.Format(@"select 
                                        DersVazgecmeAktiviteID,
                                        Gano,
                                        DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu,
                                        OgrenciBasvuruBaslangicTarihi,       
                                        OgrenciBasvuruBitisTarihi,
                                        DanismanOnayBaslangicTarihi,
                                        DanismanOnayBitisTarihi,
                                        Yil,
                                        Donem
                                        from DersVazgecmeAktivite 
                                        ");
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            List<OgrencininDersVazgecmeDTO> list = new List<OgrencininDersVazgecmeDTO>();

            list = dt.AsEnumerable()
    .Select(row => new OgrencininDersVazgecmeDTO
    {
        DersVazgecmeAktiviteId = row.Field<int>("DersVazgecmeAktiviteID"),
        Gano = row.Field<decimal>("Gano"),
        DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu = row.Field<int>("DerstenVazgecebilmekIcinGanoyaGoreBasvuruDurumu"),
        OgrenciBasvuruBaslangicTarihi = row.Field<DateTime>("OgrenciBasvuruBaslangicTarihi"),
        OgrenciBasvuruBitisTarihi = row.Field<DateTime>("OgrenciBasvuruBitisTarihi"),
        DanismanOnayBaslangicTarihi = row.Field<DateTime>("DanismanOnayBaslangicTarihi"),
        DanismanOnayBitisTarihi = row.Field<DateTime>("DanismanOnayBitisTarihi"),
        Yil = row.Field<int>("Yil"),
        Donem = row.Field<int>("Donem"),

    }).ToList();
            return list;

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
        public List<OgrenciDersGoruntulemeDTO> DerstenVazgecenOgrencileriListele()
        {

            string sql = @"
                select
                    od.OgrenciDersId,
                    o.Ad as OgrenciAd,
                    o.soyad as OgrenciSoyad,
                    d.DersKodu,
                    d.DersAdi
                from DersVazgecmeDurum dvd
                    inner join OgrenciDers od on dvd.OgrenciDersId = od.OgrenciDersId
                    inner join Ogrenci o on o.OgrenciId = od.OgrenciId
                    inner join OgretimUyesi ou on ou.OgretimUyesiId = o.DanismanId
                    inner join Ders d on d.dersId = od.DersId
                where dvd.Durum = 1
            ";
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);
            return dt.AsEnumerable().Select(row => new OgrenciDersGoruntulemeDTO
            {
                OgrenciDersId = row.Field<int>("OgrenciDersId"),
                DersKodu = row.Field<string>("DersKodu"),
                DersAdi = row.Field<string>("DersAdi"),
                OgrenciAd = row.Field<string>("OgrenciAd"),
                OgrenciSoyad = row.Field<string>("OgrenciSoyad")
            }).ToList();
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

       

        public bool OgrenciBasvuruTarihleriArasindaBasvurmusMu()
        {
            bool sonuc = false;

            string sqlBasvuruTarihiArasindaMi = "select * from DersVazgecmeAktivite where GetDate() between OgrenciBasvuruBaslangicTarihi and OgrenciBasvuruBitisTarihi";
            DbCommand cmdSelect = OgrenciMaster.Database.GetSqlStringCommand(sqlBasvuruTarihiArasindaMi);
            DataTable dtSelect = OgrenciMaster.Database.ExecuteDatatable(cmdSelect);
            bool basvurularAcik = dtSelect != null && dtSelect.Rows.Count > 0;

            if (basvurularAcik)
            {
                sonuc = true;
            }
            return sonuc;
   

        }
        

        public bool DanismanOnayTarihleriArasindaMi()
        {
            bool sonuc = false;
            string sql = "select * from DersVazgecmeAktivite where GetDate() between DanismanOnayBaslangicTarihi and DanismanOnayBitisTarihi";
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);
            bool danismanOnaylamaAcik = dt != null && dt.Rows.Count > 0;
            if (danismanOnaylamaAcik)
            {
                sonuc = true;
            }
            return sonuc;
        }

        public OgrenciDersGoruntulemeDTO DanismanAdiniSoyadiniGetir(int danismanId)
        {
            string sqlDanismanAdiniSoyadiniGetir = string.Format(@"select Ad, Soyad from OgretimUyesi where OgretimUyesiID = {0}", danismanId);
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sqlDanismanAdiniSoyadiniGetir);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            List<OgrenciDersGoruntulemeDTO> list = new List<OgrenciDersGoruntulemeDTO>();

            list = dt.AsEnumerable().Select(row => new OgrenciDersGoruntulemeDTO
            {
                DanismanAdi = row.Field<string>("Ad"),
                DanismanSoyadi = row.Field<string>("Soyad"),
            }).ToList();

            return list.FirstOrDefault();
        }


        public bool OgrenciGanosuEsitveBuyukse(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDtosu,string ogrenciId)
        {
            bool sonuc = false;

            string sqlGanosuUygunMu = string.Format(@"select OrtalamaGano from OgrenciOrtalama
where ogrenciId={0} and Yil = {1} and Donem = {2} and OrtalamaGano = {3} and OrtalamaGano > {4}", ogrenciId, ogrenciDersVazgecmeDtosu.Yil, ogrenciDersVazgecmeDtosu.Donem, ogrenciDersVazgecmeDtosu.OgrenciIslerininBelirledigiGano, ogrenciDersVazgecmeDtosu.OgrenciIslerininBelirledigiGano);
            DbCommand cmdSelect = OgrenciMaster.Database.GetSqlStringCommand(sqlGanosuUygunMu);
            DataTable dtSelect = OgrenciMaster.Database.ExecuteDatatable(cmdSelect);
            bool basvurabilir = dtSelect != null && dtSelect.Rows.Count > 0;

            if (basvurabilir)
            {
                sonuc = true;
            }
            return sonuc;

        }

        public bool OgrenciGanosuEsitveKucukse(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDtosu, string ogrenciId)
        {
            bool sonuc = true;

            string sqlGanosuUygunMu = string.Format(@"select OrtalamaGano from OgrenciOrtalama
where ogrenciId={0} and Yil = {1} and Donem = {2} and OrtalamaGano = {3} and OrtalamaGano < {4}", ogrenciId, ogrenciDersVazgecmeDtosu.Yil, ogrenciDersVazgecmeDtosu.Donem, ogrenciDersVazgecmeDtosu.OgrenciIslerininBelirledigiGano, ogrenciDersVazgecmeDtosu.OgrenciIslerininBelirledigiGano);
            DbCommand cmdSelect = OgrenciMaster.Database.GetSqlStringCommand(sqlGanosuUygunMu);
            DataTable dtSelect = OgrenciMaster.Database.ExecuteDatatable(cmdSelect);
            bool basvuramaz = dtSelect != null && dtSelect.Rows.Count > 0;
            //başvuramaz true ise başvuramaz 
            if (basvuramaz)
            {
                sonuc = false;
            }
            return sonuc;

        }

        public bool OgrenciGanosuKucukse(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDtosu, string ogrenciId)
        {
            bool sonuc = true;

            string sqlGanosuUygunMu = string.Format(@"select OrtalamaGano from OgrenciOrtalama
where ogrenciId={0} and Yil = {1} and Donem = {2} and OrtalamaGano < {3}", ogrenciId, ogrenciDersVazgecmeDtosu.Yil, ogrenciDersVazgecmeDtosu.Donem, ogrenciDersVazgecmeDtosu.OgrenciIslerininBelirledigiGano);
            DbCommand cmdSelect = OgrenciMaster.Database.GetSqlStringCommand(sqlGanosuUygunMu);
            DataTable dtSelect = OgrenciMaster.Database.ExecuteDatatable(cmdSelect);
            bool basvuramaz = dtSelect != null && dtSelect.Rows.Count > 0;
            //başvuramaz true ise başvuramaz 
            if (basvuramaz)
            {
                sonuc = false;
            }
            return sonuc;

        }
        public bool OgrenciGanosuBuyukse(OgrencininDersVazgecmeDTO ogrenciDersVazgecmeDtosu, string ogrenciId)
        {
            bool sonuc = false;

            string sqlGanosuUygunMu = string.Format(@"select OrtalamaGano from OgrenciOrtalama
where ogrenciId={0} and Yil = {1} and Donem = {2} and OrtalamaGano > {3}", ogrenciId, ogrenciDersVazgecmeDtosu.Yil, ogrenciDersVazgecmeDtosu.Donem, ogrenciDersVazgecmeDtosu.OgrenciIslerininBelirledigiGano);
            DbCommand cmdSelect = OgrenciMaster.Database.GetSqlStringCommand(sqlGanosuUygunMu);
            DataTable dtSelect = OgrenciMaster.Database.ExecuteDatatable(cmdSelect);
            bool basvurabilir = dtSelect != null && dtSelect.Rows.Count > 0;
            //başvuramaz true ise başvuramaz 
            if (basvurabilir)
            {
                sonuc = true;
            }
            return sonuc;

        }

        public OgrencininDersVazgecmeDTO OgrenciIslerininBelirledigiGano(OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDtosu)
        {
            
            string sql = string.Format(@"select top 1 Gano from DersVazgecmeAktivite where Yil = {0} and Donem = {1}", ogrencininDersVazgecmeDtosu.Yil, ogrencininDersVazgecmeDtosu.Donem);
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);
            List<OgrencininDersVazgecmeDTO> list = new List<OgrencininDersVazgecmeDTO>();

            list = dt.AsEnumerable().Select(row => new OgrencininDersVazgecmeDTO
            {

                OgrenciIslerininBelirledigiGano = row.Field<decimal>("Gano"),
            }).ToList();

            return list.FirstOrDefault();


            

        }
        


        public List<int> GetOgrencininDahaOncedenVazgectigiOgrenciDersIdler(string ogrenciId)
        {
            List<int> red = new List<int>();

            string sql = @"
                select dvd.OgrenciDersId from DersVazgecmeDurum dvd
                inner join ogrenciDers od on dvd.OgrenciDersID = od.OgrenciDersID
                where od.ogrenciId = {0} and dvd.durum = 5
            ";
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(string.Format(sql, ogrenciId)));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    int odId = 0;
                    int.TryParse(rw["OgrenciDersId"].ToString(), out odId);
                    red.Add(odId);
                }
            }

            return red;
        }

        public List<int> GetDanismanaGonderilenOgrenciDersIdler(string ogrenciId)
        {
            List<int> danismanagonderildi = new List<int>();

            string sql = @"
                select dvd.OgrenciDersId from DersVazgecmeDurum dvd
                inner join ogrenciDers od on dvd.OgrenciDersID = od.OgrenciDersID
                where od.ogrenciId = {0} and dvd.durum = 1
            ";
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(string.Format(sql, ogrenciId)));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    int odId = 0;
                    int.TryParse(rw["OgrenciDersId"].ToString(), out odId);
                    danismanagonderildi.Add(odId);
                }
            }

            return danismanagonderildi;
        }

        public List<int> GetOgrencininDahaOncedenVazgectigiVeOnaylananOgrenciDersIdler(string ogrenciId)
        {
            List<int> onay = new List<int>();

            string sql = @"
                select dvd.OgrenciDersId from DersVazgecmeDurum dvd
                inner join ogrenciDers od on dvd.OgrenciDersID = od.OgrenciDersID
                where od.ogrenciId = {0} and dvd.durum = 2
            ";
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(string.Format(sql, ogrenciId)));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    int odId = 0;
                    int.TryParse(rw["OgrenciDersId"].ToString(), out odId);
                    onay.Add(odId);
                }
            }

            return onay;
        }


        public List<int> GetOgrencininDahaOncedenVazgectigiVeDanismanaGonderdigiOgrenciDersIdler(string ogrenciId)
        {

            List<int> danismanaGonderildi = new List<int>();

            string sql = @"
                select dvd.OgrenciDersId from DersVazgecmeDurum dvd
                inner join ogrenciDers od on dvd.OgrenciDersID = od.OgrenciDersID
                where od.ogrenciId = {0} and dvd.durum = 1
            ";
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(string.Format(sql, ogrenciId)));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rw in dt.Rows)
                {
                    int odId = 0;
                    int.TryParse(rw["OgrenciDersId"].ToString(), out odId);
                    danismanaGonderildi.Add(odId);
                }
            }
            return danismanaGonderildi;
        }

    }
}