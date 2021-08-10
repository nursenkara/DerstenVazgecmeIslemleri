using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using UniOgrenci.Master.Entities;
using UniOgrenci.Master.Utils;
using Telerik.Web.UI;
using UniOgrenci.Master.OgrUygulamalar;
using UniOgrenci.Master.OgrUygulamalar.Model;
using UniOgrenci.Master.Enums;
namespace DerstenVazgecmeIslemleri
{
    
    public partial class DerstenVazgecmeIslemleriUygulama
    {
        #region Genel - Gorunurluk ayarlari ve kod bilgileri 
        public void TabGorunurlukAyariYap(RadTabStrip comp)
        {
            var showBelgeler = OgrenciMaster.Database.LoadEntityByID<SistemParam>(15022);
            var showDisiplinBilgileri = OgrenciMaster.Database.LoadEntityByID<SistemParam>(15036);
            var showBasvuruKontrolListesi = OgrenciMaster.Database.LoadEntityByID<SistemParam>(15031);

            /*
             * Degerlendirme Araclari 0 
               Sinav Bilgileri 1 
               Sinav Puan Barajlari 2
               Belgeler 3
               Disiplin Bilgileri 4
               3Basvuru kontrol listeleri 5
             */
            if (showBelgeler != null && showBelgeler.BooDeger1.HasValue && showBelgeler.BooDeger1.Value)
            {
                comp.FindTabByText("Belgeler").Visible = true;
                
            }
            if (showDisiplinBilgileri != null && showDisiplinBilgileri.BooDeger1.HasValue && showDisiplinBilgileri.BooDeger1.Value)
            {
                comp.FindTabByText("Disiplin Bilgileri").Visible = true;
            }
            if (showBasvuruKontrolListesi != null && showBasvuruKontrolListesi.BooDeger1.HasValue && showBasvuruKontrolListesi.BooDeger1.Value)
            {
                comp.FindTabByText("Başvuru Kontrol Listeleri").Visible = true;
            }

#if DEBUG
            comp.FindTabByText("Belgeler").Visible = true;
            comp.FindTabByText("Disiplin Bilgileri").Visible = true;
            comp.FindTabByText("Başvuru Kontrol Listeleri").Visible = true;
#endif
        }
        
        /// <summary>
        ///  163	7	0	Özgeçmiş
        ///  163	8	0	Sertifika
        ///  163	9	0	Başarı Belgesi
        ///  163	10	0	Referans Mektubu
        ///  163	11	0	Yökten Denklik/Tanınırlık Belgesi
        ///  163	12	0	TÖMER Belgesi
        ///  163	13	0	Diploma ve Transkriptlerin Konsolosluk Çevirileri        
        /// </summary>
        const int BELGE_KOD_GRUP = 163;
        #endregion
        #region BelgelerTab Fonksiyonlari
        /// <summary>
        /// KodGrup 163 u getirir.
        /// </summary>
        /// <returns></returns>        
        public DataTable BelgeleriGetir()
        {
            DataTable dt = null;
            try
            {
                StringBuilder _sb = new StringBuilder();
                _sb.AppendFormat(@"
SELECT TOP 200
KodId
,Aciklama                                                                        
FROM Kod k 
WHERE KodGrup = {0} 
AND KodNo > 0 ", BELGE_KOD_GRUP);
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                return dt;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }
        }
        public List<BasvuruProgramBelge> ProgramaGoreBelgeleriGetir(int BasvuruProgramId)
        {
            
            List<BasvuruProgramBelge> list = new List<BasvuruProgramBelge>();
            try
            {

                string q = @"SELECT BasvuruProgramBelgeID,BasvuruProgramID,BelgeTuru,ZorunluMu,YabancidaZorunlu 
                             FROM ens.BasvuruProgramBelge WHERE BasvuruProgramID = @BasvuruProgramId ";
                DbCommand c = OgrenciMaster.Database.GetSqlStringCommand(q);
                OgrenciMaster.Database.AddInParameter(c, "@BasvuruProgramId", DbType.Int32, BasvuruProgramId);

                var dt = OgrenciMaster.Database.ExecuteDatatable(c, true);

                foreach (var item in dt.Rows) { 
                    var row = item as DataRow;
                    list.Add(new BasvuruProgramBelge()
                    {
                        BasvuruProgramBelgeID = row[0].ToInt().Value,
                        BasvuruProgramID = row[1].ToInt().Value,
                        BelgeTuru = row[2].ToInt().Value,
                        ZorunluMu = row[3].ToBool().Value,
                        YabancidaZorunlu = row[4].ToBool().Value,
                    });
                    

                }
                return list;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;

            }
        }
        public bool BelgeleriKaydet(BelgeKaydetModel model)
        {
            bool result = false;
            try
            {
                StringBuilder _sb = new StringBuilder();
                if (model.Adim == 0)
                {
                    _sb.Append(@"
                    INSERT INTO ens.BasvuruProgramBelge
                    (BasvuruProgramID,BelgeTuru,ZorunluMu,YabancidaZorunlu)
                    VALUES(@BasvuruProgramID,@BelgeTuru,@ZorunluMu,@YabancidaZorunlu)    
                    ");
                }
                else
                {
                    _sb.Append(@"
                    INSERT INTO ens.BasvuruProgramBelge
                    (BasvuruProgramID,BelgeTuru,ZorunluMu,YabancidaZorunlu,Adim)
                    VALUES(@BasvuruProgramID,@BelgeTuru,@ZorunluMu,@YabancidaZorunlu,@Adim)    
                    ");
                }
                
                
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, model.BasvuruProgramID);
                OgrenciMaster.Database.AddInParameter(_cmd, "@BelgeTuru", DbType.Int32, model.BelgeTipi);
                OgrenciMaster.Database.AddInParameter(_cmd, "@ZorunluMu", DbType.Boolean, model.TcZorunlu);
                OgrenciMaster.Database.AddInParameter(_cmd, "@YabancidaZorunlu", DbType.Boolean, model.YuZorunlu);
                if (model.Adim != 0) 
                { OgrenciMaster.Database.AddInParameter(_cmd, "@Adim", DbType.Int32, model.Adim); }
                
                var count = OgrenciMaster.Database.ExecuteNonQuery(_cmd, true);
                if (count > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }
        }
        public bool EskiBelgeSil(int basvuruProgramId)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"DELETE FROM ens.BasvuruProgramBelge where BasvuruProgramID = @BasvuruProgramID;");
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, basvuruProgramId);
                OgrenciMaster.Database.ExecuteNonQuery(_cmd, true);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }

        }
        /// <summary>
        /// Sistem parametrelerine gore tab gorunurluklerini ayarlar.
        /// ParamID=15022 Bool1 = True ise - Belgeler 
        /// ParamID=15036 Bool1 = True ise - Disiplin Bilgileri 
        /// ParamID= 15031 Bool1 = True ise - Başvuru Kontrol Listeleri
        /// <remarks> Debugta gorunurluk kontrolu yapilirken butun table gorunur.</remarks>
        /// </summary>
        
        /// <summary>
        /// Disiplin bilgileri bolumune ait bilesenlerin baslangic veri yukleme islemelerini icerir. 
        /// <example> Combobox datalarinin doldurulmasi vb. </example>
        /// </summary>
        /// <param name="comps"></param>
        public void DisiplinBilgileriBolumuInit(params object[] comps) {

            foreach (var comp in comps) {
                if (comp is RadComboBox) {
                    var c = comp as RadComboBox;
                    switch (c.ID) {
                        case "cmbDisiplinTuru":
                            OgrUygulamaCls.PopulateComboFromKod(new OgrUygulamaKod()
                            {
                                KomboAdi = c,
                                KodGrup = 225,
                                SecinizGorunsun = true,
                            });
                            break;
                        case "cmbBasvurulabilecekProgramTuru":
                            OgrUygulamaCls.PopulateComboFromKod(new OgrUygulamaKod()
                            {
                                KomboAdi = c,
                                KodGrup = 915,
                                SecinizGorunsun = true,                                
                            });
                            break;
                        default:break;
                    }
                }
            }        
        }

        /// <summary>
        /// Basvuru Kontrol Listeleri bolumune ait bilesenlerin baslangic veri yukleme islemelerini icerir. 
        /// <example> Combobox datalarinin doldurulmasi vb. </example>
        /// </summary>
        /// <param name="comps"></param>
        public void BasvuruKontrolListeleriInit(params object[] comps)
        {
            foreach (var comp in comps)
            {
                //Mevcutta bir sey yok. Gelirse burasi doldurulabilir.                
            }
        }
        #endregion
        #region DisiplinBilgileriTab Fonksiyonlari       
        public List<ProgramDisiplinBasvuru> ProgramaGoreDisiplinBilgileriGetir(int BasvuruProgramId)
        {

            List<ProgramDisiplinBasvuru> list = new List<ProgramDisiplinBasvuru>();
            try
            {
                StringBuilder _sb = new StringBuilder();
                _sb.AppendFormat(@"
SELECT 
[BasvuruProgramID]
,[BasvurulabilecekDisiplinKodID]
,[MaxBasvuruDisiplinSayisi]
,[BasvurulabilecekProgramTuruID]
,[MaxBasvuruProgramTuruSayisi] 
FROM [ens].[ProgramDisiplinBasvuru]
WHERE BasvuruProgramID =  {0}
", BasvuruProgramId);
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                var dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);

                foreach (var item in dt.Rows)
                {
                    var row = item as DataRow;
                    
                    list.Add(new ProgramDisiplinBasvuru()
                    {
                        BasvuruProgramID = row[0].ToInt().Value,
                        BasvurulabilecekDisiplinKodID = row[1].ToInt(),
                        MaxBasvuruDisiplinSayisi = row[2].ToInt(),
                        BasvurulabilecekProgramTuruID = row[3].ToInt(),
                        MaxBasvuruProgramTuruSayisi = row[4].ToInt()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;

            }
        }
        public string ProgramDisiplinBilgileriGetir(int OrganizasyonProgramId)
        {

            
            try
            {
                StringBuilder _sb = new StringBuilder();
                _sb.AppendFormat(@"
                                    SELECT  DisipKod.Aciklama
	                                    FROM [ens].[ProgramDisiplin] pd
                                        INNER JOIN Kod DisipKod on DisipKod.KodId=pd.DisiplinKodID
                                    WHERE OrganizasyonProgramID =  {0}
                                    ", OrganizasyonProgramId);
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                var dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                string result = "";
                foreach (var row in dt.Rows)
                {
                    var item = (row as DataRow).ItemArray;
                    result = item[0].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;

            }
        }
        public bool DisiplinBilgileriKaydet(DisiplinKaydetModel model)
        {
            bool result = false;
            try
            {
                StringBuilder _sb = new StringBuilder();
                _sb.Append(@"
INSERT INTO ens.ProgramDisiplinBasvuru
(BasvuruProgramID
,BasvurulabilecekDisiplinKodID
,MaxBasvuruDisiplinSayisi
,BasvurulabilecekProgramTuruID
,MaxBasvuruProgramTuruSayisi)
VALUES(
@BasvuruProgramID
,@BasvurulabilecekDisiplinKodID
,@MaxBasvuruDisiplinSayisi
,@BasvurulabilecekProgramTuruID
,@MaxBasvuruProgramTuruSayisi)    
");
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, model.BasvuruProgramID);
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvurulabilecekDisiplinKodID", DbType.Int32, model.BasvurulabilecekDisiplinKodID);
                OgrenciMaster.Database.AddInParameter(_cmd, "@MaxBasvuruDisiplinSayisi", DbType.Int32, model.MaxBasvuruDisiplinSayisi);
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvurulabilecekProgramTuruID", DbType.Int32, model.BasvurulabilecekProgramTuruID);
                OgrenciMaster.Database.AddInParameter(_cmd, "@MaxBasvuruProgramTuruSayisi", DbType.Int32, model.MaxBasvuruProgramTuruSayisi);
                var count = OgrenciMaster.Database.ExecuteNonQuery(_cmd, true);
                if (count > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }
        }
        public bool EskiDisiplinBilgileriSil(int basvuruProgramId)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"DELETE FROM ens.ProgramDisiplinBasvuru where BasvuruProgramID = @BasvuruProgramID;");
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, basvuruProgramId);
                OgrenciMaster.Database.ExecuteNonQuery(_cmd, true);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }

        }
        #endregion 
        #region BasvuruKontrolListeleriTab Fonksiyonlari        
        public List<BasvurudaKontrolKriter> ProgramaGoreBasvuruKontrolListeleriGetir(int BasvuruProgramId)
        {

            List<BasvurudaKontrolKriter> list = new List<BasvurudaKontrolKriter>();
            try
            {
                StringBuilder _sb = new StringBuilder();
                _sb.AppendFormat(@"
SELECT 
[BasvurudaKontrolKriterID]
,[BasvuruProgramID]
,[MinimumMezuniyetNotu4sistem]
,[MinimumMezuniyetNotu100sistem]
,[MinimumYuksekLisansMezuniyetNotu4sistem]
,[MinimumYuksekLisansMezuniyetNotu100sistem] 
FROM [ens].[BasvurudaKontrolKriter]
WHERE BasvuruProgramID = {0}
", BasvuruProgramId);
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                var dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);

                foreach (var item in dt.Rows)
                {
                    var row = (item as DataRow).ItemArray;
                    list.Add(new BasvurudaKontrolKriter()
                    {
                        BasvuruProgramID = row[1].ToInt().Value,
                        MinimumMezuniyetNotu4sistem = row[2].ToBool().Value,
                        MinimumMezuniyetNotu100sistem = row[3].ToBool().Value,
                        MinimumYuksekLisansMezuniyetNotu4sistem = row[4].ToBool().Value,
                        MinimumYuksekLisansMezuniyetNotu100sistem = row[5].ToBool().Value,
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }
        }
        public bool BasvuruKontrolListeleriKaydet(BasvuruKontrolKaydetModel model)
        {
            bool result = false;
            try
            {
                StringBuilder _sb = new StringBuilder();
                _sb.Append(@"
INSERT INTO [ens].[BasvurudaKontrolKriter]
([BasvuruProgramID]
,[MinimumMezuniyetNotu4sistem]
,[MinimumMezuniyetNotu100sistem]
,[MinimumYuksekLisansMezuniyetNotu4sistem]
,[MinimumYuksekLisansMezuniyetNotu100sistem]) 
	VALUES
(@BasvuruProgramID
 ,@MinimumMezuniyetNotu4sistem
 ,@MinimumMezuniyetNotu100sistem
 ,@MinimumYuksekLisansMezuniyetNotu4sistem
 ,@MinimumYuksekLisansMezuniyetNotu100sistem)  
");
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(_sb.ToString());
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, model.BasvuruProgramID);
                OgrenciMaster.Database.AddInParameter(_cmd, "@MinimumMezuniyetNotu4sistem", DbType.Boolean, model.MinimumMezuniyetNotu4sistem);
                OgrenciMaster.Database.AddInParameter(_cmd, "@MinimumMezuniyetNotu100sistem", DbType.Boolean, model.MinimumMezuniyetNotu100sistem);
                OgrenciMaster.Database.AddInParameter(_cmd, "@MinimumYuksekLisansMezuniyetNotu4sistem", DbType.Boolean, model.MinimumYuksekLisansMezuniyetNotu4sistem);
                OgrenciMaster.Database.AddInParameter(_cmd, "@MinimumYuksekLisansMezuniyetNotu100sistem", DbType.Boolean, model.MinimumYuksekLisansMezuniyetNotu100sistem);                

                var count = OgrenciMaster.Database.ExecuteNonQuery(_cmd, true);
                if (count > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EskiBasvuruKontrolListeleriSil(int basvuruProgramId)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"DELETE FROM ens.BasvurudaKontrolKriter where BasvuruProgramID = @BasvuruProgramID;");
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, basvuruProgramId);
                OgrenciMaster.Database.ExecuteNonQuery(_cmd, true);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }

        }
        public string DisiplinTipiKontrol(int basvuruProgramId)
        {
            try
            {
                string result = null;
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"
SELECT 
k.Aciklama
FROM [ens].[ProgramDisiplinBasvuru] pdb
INNER JOIN dbo.Kod k on k.KodID = pdb.BasvurulabilecekDisiplinKodID
WHERE BasvuruProgramID = @BasvuruProgramID
");
                OgrenciMaster.Database.AddInParameter(_cmd, "@BasvuruProgramID", DbType.Int32, basvuruProgramId);
                var dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                foreach(var row in dt.Rows) {                    
                    var item = (row as DataRow).ItemArray;
                    result = item[0].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                throw ex;
            }

        }
        #endregion
    }
}

