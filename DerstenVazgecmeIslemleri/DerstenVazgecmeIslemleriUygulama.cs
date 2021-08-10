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

namespace DerstenVazgecmeIslemleri
{
    /// <summary>
    /// Uygulamanın business logic'ini gerçekleştirecek olan sınıf.
    /// </summary>
    [Serializable]
    public partial class DerstenVazgecmeIslemleriUygulama : OgrenciUygulama
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unipaMaster"></param>
        /// <param name="uygulamaId"></param>
        /// <param name="projeId"></param>
        public DerstenVazgecmeIslemleriUygulama(UnipaMaster unipaMaster, int uygulamaId, int projeId) : base(unipaMaster, projeId, uygulamaId) { }

        public BasvuruProgram getirBasvuruProgram(object programID, object ogrenciTuru, int yil, int donem)
        {
            try
            {
                string sql = @"SELECT *
                                                                       FROM ens.BasvuruProgram
                                                                       WHERE OrganizasyonProgramID = {0} AND OgrenciTuru = {1} AND OgretimYili = {2} AND OgretimDonemi = {3}
                                                                       ORDER BY BasvuruProgramID DESC";
                sql = string.Format(sql, programID, ogrenciTuru, yil, donem);
                return OgrenciMaster.Database.Select<BasvuruProgram>(sql).ToList().FirstOrDefault();

                //                BasvuruProgram bp = new BasvuruProgram();
                //                bp = string.Format(@"SELECT *
                //                                                                       FROM ens.BasvuruProgram
                //                                                                       WHERE OrganizasyonProgramID = ? AND OgrenciTuru = ? AND OgretimYili = ? AND OgretimDonemi = ?
                //                                                                       ORDER BY BasvuruProgramID DESC", programID, ogrenciTuru, yil, donem)).ToList().FirstOrDefault();

            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.ProgramGetirilirkenHata, 1081001, SourceLevels.Error);
            }
        }

        public BasvuruProgram getirBasvuruProgramDefault(object organizasyonID, object ogrenciTuru)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<BasvuruProgram>(@"SELECT *
                                                                             FROM ens.BasvuruProgram
                                                                             WHERE OrganizasyonID = ? AND OgrenciTuru = ? AND DefaultDeger = 1", organizasyonID, ogrenciTuru);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.ProgramGetirilirkenHata, 1081002, SourceLevels.Error);
            }
        }

        public IList<JuriUyeleri> getirMulakatJuriUyeleri(object basvuruprogramID, object yil, object donem)
        {
            try
            {
                return OgrenciMaster.Database.Select<JuriUyeleri>(@"SELECT *
                                                                    FROM ens.JuriUyeleri
                                                                    WHERE BasvuruProgramID = ? AND Yil = ? AND Donem = ? AND JuriTipi = 944001", basvuruprogramID, yil, donem);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081003, SourceLevels.Error);
            }
        }

        public IList<JuriUyeleri> getirTurkceSinavJuriUyeleri(object basvuruprogramID, object yil, object donem)
        {
            try
            {
                return OgrenciMaster.Database.Select<JuriUyeleri>(@"SELECT *
                                                                    FROM ens.JuriUyeleri
                                                                    WHERE BasvuruProgramID = ? AND Yil = ? AND Donem = ? AND JuriTipi = 944002", basvuruprogramID, yil, donem);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081004, SourceLevels.Error);
            }
        }

        public JuriUyeleri getirMulakatJuriUyesi(object basvuruprogramID, object yil, object donem, object hocaID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<JuriUyeleri>(@"SELECT *
                                                                          FROM ens.JuriUyeleri
                                                                          WHERE BasvuruProgramID = ? AND Yil = ? AND Donem = ? AND JuriTipi = 944001 AND OgretimUyesiID = ?",
                                                                        basvuruprogramID, yil, donem, hocaID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081005, SourceLevels.Error);
            }
        }

        public JuriUyeleri getirMulakatJuriUyesi(object juriuyeID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<JuriUyeleri>(@"SELECT *
                                                                          FROM ens.JuriUyeleri
                                                                          WHERE JuriUyeleriID = ?", juriuyeID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081006, SourceLevels.Error);
            }
        }

        public JuriUyeleri getirTurkceSinavJuriUyesi(object basvuruprogramID, object yil, object donem, object hocaID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<JuriUyeleri>(@"SELECT *
                                                                          FROM ens.JuriUyeleri
                                                                          WHERE BasvuruProgramID = ? AND Yil = ? AND Donem = ? AND JuriTipi = 944002 AND OgretimUyesiID = ?", basvuruprogramID, yil, donem, hocaID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081007, SourceLevels.Error);
            }
        }

        public GirisSinaviPuanBarajlari getirGirisSinavBaraji(object basvuruprogramID, object sinavID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<GirisSinaviPuanBarajlari>(@"SELECT *
                                                                                       FROM ens.GirisSinaviPuanBarajlari
                                                                                       WHERE BasvuruProgramID = ? AND SinavTuru = ?", basvuruprogramID, sinavID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.SinavBarajPuan, 1081008, SourceLevels.Error);
            }
        }

        public IstenenBelgeler getirIstenenBelge(object basvuruprogramID, object belgeID, object hesapturu)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<IstenenBelgeler>(@"SELECT *
                                                                              FROM ens.IstenenBelgeler
                                                                              WHERE BasvuruProgramID = ? AND BelgeID = ? AND ISNULL(HesaplamaTuru, 1) = ?",
                                                                            basvuruprogramID, belgeID, hesapturu);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.BelgeHatasi, 1081009, SourceLevels.Error);
            }
        }

        public ProgramPuanTuru getirProgramPuanTuru(object basvuruprogramID, object puanturu)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<ProgramPuanTuru>(@"SELECT *
                                                                              FROM ens.ProgramPuanTuru
                                                                              WHERE BasvuruProgramID = ? AND PuanTuruID = ?", basvuruprogramID, puanturu);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.PuanTuruHata, 1081010, SourceLevels.Error);
            }
        }

        public DataTable getirProgramTuru(int? OrganizasyonID)
        {
            string sql = "";
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "tr-TR")
                sql = @"SELECT distinct op.ProgramTuru, k.Aciklama
                        FROM organizasyon o, OrganizasyonProgram op, kod k
                        WHERE o.OrganizasyonID = op.OrganizasyonID AND k.KodID = op.ProgramTuru AND o.UstBirim = (SELECT UstBirim
                                                                                                                  FROM Organizasyon o2
                                                                                                                  WHERE o2.OrganizasyonID = " + OrganizasyonID + ")";
            else
                sql = @"SELECT DISTINCT op.ProgramTuru, k.Aciklama
                        FROM organizasyon o, OrganizasyonProgram op, kodResx k
                        WHERE o.OrganizasyonID = op.OrganizasyonID AND k.KodID = op.ProgramTuru AND o.UstBirim = (SELECT UstBirim
                                                                                                                  FROM Organizasyon o2
                                                                                                                  WHERE o2.OrganizasyonID = " + OrganizasyonID + ")" +
                       " AND k.Kultur = '" + System.Threading.Thread.CurrentThread.CurrentUICulture.Name + "'";

            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);

            return OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
        }

        public Kod getirkod(object kodID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<Kod>(@"SELECT *
                                                                  FROM kod
                                                                  WHERE kodID = ?", kodID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KodTuruHata, 1081011, SourceLevels.Error);
            }
        }

        public OrganizasyonProgram getorgprg(int id)
        {
            try
            {
                return OgrenciMaster.Database.LoadEntityByID<OrganizasyonProgram>(id);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.ProgramGetirilirkenHata, 1081012, SourceLevels.Error);
            }
        }

        public Organizasyon getOrganizasyon(int OrganizasyonProgramID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<Organizasyon>(@"SELECT *
                                                                           FROM  Organizasyon where OrganizasyonID IN (SELECT OrganizasyonID
                                                                                                                       FROM Organizasyonprogram
                                                                                                                       WHERE organizasyonProgramID = ?)", OrganizasyonProgramID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Organizasyon getirilirken hata oluştu.", 1081027, SourceLevels.Error);
            }
        }

        public IList<ProgramPuanTuru> getirProgramPuanTurleri(object basvuruprogramID)
        {
            try
            {
                return OgrenciMaster.Database.Select<ProgramPuanTuru>(@"SELECT *
                                                                        FROM kod k, ens.ProgramPuanTuru pt
                                                                        WHERE pt.PuanTuruID = k.KodID AND pt.BasvuruProgramID = ?", basvuruprogramID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.ProgramTurHatasi, 1081013, SourceLevels.Error);
            }
        }

        public IList<JuriUyeleri> getirJuriUyeleriNull()
        {
            try
            {
                return OgrenciMaster.Database.Select<JuriUyeleri>(@"SELECT *
                                                                    FROM ens.JuriUyeleri
                                                                    WHERE BasvuruProgramID = -1");
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081014, SourceLevels.Error);
            }
        }

        public bool kaydetGirisSinavBaraji(GirisSinaviPuanBarajlari _sinavBaraji)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Save(_sinavBaraji);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public bool guncelleGirisSinavBaraji(GirisSinaviPuanBarajlari _sinavBaraji)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Update(_sinavBaraji);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public bool kaydetProgramPuanTuru(ProgramPuanTuru aa)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Save(aa);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public bool silProgramPuanTuru(ProgramPuanTuru aa)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.DeleteDbEntity(aa);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public bool siljuriUye(JuriUyeleri aa)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.DeleteDbEntity(aa);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public Kod getAktiviteKodu()
        {
            try
            {
                return OgrenciMaster.Database.LoadEntityByID<Kod>(918032);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.AktiviteHatasi, 1081015, SourceLevels.Error);
            }
        }

        public bool kaydetBasvuruProgram(BasvuruProgram _basvuruProgram)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Save(_basvuruProgram);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public AkademikTakvim getAkademikTakvim(int orgProgID, int yil, int donem)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<AkademikTakvim>(@"SELECT *
                                                                             FROM AkademikTakvim
                                                                             WHERE OrganizasyonProgramID = ? AND OgretimYili = ? AND OgretimDonemi = ? ", orgProgID, yil, donem);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.AkademikTakvimHata, 1081016, SourceLevels.Error);
            }
        }

        public AkademikTakvimAktivite getAkademikTakvimAktivite(int takvimID)
        {
            try
            {
                return OgrenciMaster.Database.SelectSingle<AkademikTakvimAktivite>(@"SELECT *
                                                                                     FROM AkademikTakvimAktivite
                                                                                     WHERE AktiviteID = 918032 AND AkademikTakvimID = ? ", takvimID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.AkademikTakvimAktivitesi, 1081017, SourceLevels.Error);
            }
        }

        public void guncelleKaydetAkademikTakvimAktivite(AkademikTakvimAktivite aka)
        {
            try
            {
                OgrenciMaster.Database.SaveOrUpdate(aka);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.AkGuncelleme, 1081018, SourceLevels.Critical);
            }
        }

        public void guncelleKaydetAkademikTakvim(AkademikTakvim ak)
        {
            try
            {
                OgrenciMaster.Database.SaveOrUpdate(ak);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.SaveError, 1081020, SourceLevels.Critical);
            }
        }

        public Kod getDonem(int donemID)
        {
            try
            {
                return OgrenciMaster.Database.LoadEntityByID<Kod>(donemID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.DonemGetirmeHatasi, 1081021, SourceLevels.Error);
            }
        }

        public BasvuruProgram getirBasvuruProgram(object organizasyonID, object ogrenciTuru, object yil, object donem, string asd)
        {
            try
            {
                //            return OgrenciMaster.Database.SelectSingle<BasvuruProgram>(@"SELECT *
                //                                                                         FROM ens.BasvuruProgram
                //                                                                         WHERE OrganizasyonID = ? AND OgrenciTuru = ? AND OgretimYili = ? AND OgretimDonemi = ?
                //                                                                         ORDER BY BasvuruProgramID DESC", organizasyonID, ogrenciTuru, yil, donem);
                string sql = @"SELECT *
                                                                       FROM ens.BasvuruProgram
                                                                       WHERE OrganizasyonID = {0} AND OgrenciTuru = {1} AND OgretimYili = {2} AND OgretimDonemi = {3}
                                                                       ORDER BY BasvuruProgramID DESC";
                sql = string.Format(sql, organizasyonID, ogrenciTuru, yil, donem);
                return OgrenciMaster.Database.Select<BasvuruProgram>(sql).ToList().FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw(ex);
            }
        }

        public bool guncelleBasvuruProgram(BasvuruProgram _basvuruProgram)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Update(_basvuruProgram);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public bool kaydetJuriUyesi(JuriUyeleri _juriUyesi)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Save(_juriUyesi);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public bool guncelleJuriUyesi(JuriUyeleri _juriUyesi)
        {
            bool sonuc = false;

            try
            {
                OgrenciMaster.Database.Update(_juriUyesi);
                sonuc = true;
            }
            catch (Exception)
            {
                sonuc = false;
            }

            return sonuc;
        }

        public GirisSinavPuanlariListem getirPuanBarajlari(object _basvuruProgramID)
        {
            //            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID
            //                                                                        , Aciklama
            //                                                                        , ISNULL((SELECT REPLACE(Puan1, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                          WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), 0) AS 'Puan1'
            //                                                                        , ISNULL((SELECT REPLACE(Puan2, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                          WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), 0) AS 'Puan2'
            //                                                                        ,ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                          WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0') AS 'YabanciUyrukluPuanBaraji' 
            //                                                                         ,ISNULL((SELECT TOP 1 GecerlilikTarihi
            //                                                                         FROM ens.GirisSinaviPuanBarajlari WHERE
            //                                                                         BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID),'') as 'GecerlilikTarihi'                                                                         
            //                                                                         FROM Kod k WHERE KodGrup IN (20, 23) AND KodNo > 0  and k.Gizli=0");


            //            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID
            //                                                                        , Aciklama
            //                                                                        , ISNULL((SELECT REPLACE(Puan1, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), 0) AS 'Puan1'
            //                                                                        , ISNULL((SELECT REPLACE(Puan2, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), 0) AS 'Puan2'
            //                                                                        ,ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), '0') AS 'YabanciUyrukluPuanBaraji' 
            //                                                                        ,convert(date,(SELECT TOP 1 cast(GecerlilikTarihi as varchar) FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID ),105) as 'GecerlilikTarihi'                                                                         
            //                                                                        FROM Kod k 
            //                                                                        WHERE KodGrup IN (20, 23) AND KodNo > 0  and k.Gizli=0
            //                                                                        ");



            /**/
            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID
                                , Aciklama
                                , ISNULL((SELECT REPLACE(Puan1, '.', ',') FROM ens.GirisSinaviPuanBarajlari
                                WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), 0) AS 'Puan1'
                                , ISNULL((SELECT REPLACE(Puan2, '.', ',') FROM ens.GirisSinaviPuanBarajlari
                                WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), 0) AS 'Puan2'
                                ,ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',') FROM ens.GirisSinaviPuanBarajlari
                                WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0') AS 'YabanciUyrukluPuanBaraji' 
                                ,convert(date,(SELECT TOP 1 cast(GecerlilikTarihi as varchar) FROM ens.GirisSinaviPuanBarajlari WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID ),105) as 'GecerlilikTarihi'                                                                         
                                FROM Kod k 
                                WHERE KodGrup IN (20, 23) AND KodNo > 0  and k.Gizli=0");



            //            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID
            //                                                                        , Aciklama
            //                                                                        , ISNULL((SELECT REPLACE(Puan1, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), 0) AS 'Puan1'
            //                                                                        , ISNULL((SELECT REPLACE(Puan2, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), 0) AS 'Puan2'
            //                                                                        ,ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), '0') AS 'YabanciUyrukluPuanBaraji' 
            //                                                                        ,ISNULL((SELECT TOP 1 cast (GecerlilikTarihi as varchar) FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID ),105) as 'GecerlilikTarihi'                                                                         
            //                                                                        FROM Kod k 
            //                                                                        WHERE KodGrup IN (20, 23) AND KodNo > 0  and k.Gizli=0
            //                                                                        ");

            //            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID
            //                                                                        , Aciklama
            //                                                                        , ISNULL((SELECT REPLACE(Puan1, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), 0) AS 'Puan1'
            //                                                                        , ISNULL((SELECT REPLACE(Puan2, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), 0) AS 'Puan2'
            //                                                                        ,ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',') FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID), '0') AS 'YabanciUyrukluPuanBaraji' 
            //                                                                        ,(convert(date,(SELECT TOP 1 cast(GecerlilikTarihi as varchar) FROM ens.GirisSinaviPuanBarajlari
            //                                                                        WHERE BasvuruProgramID = 1370 AND SinavTuru = k.KodID ))) as 'GecerlilikTarihi'                                                                         
            //                                                                        FROM Kod k 
            //                                                                        WHERE KodGrup IN (20, 23) AND KodNo > 0  and k.Gizli=0
            //                                                                        ");


            DataTable dt = new DataTable();

            try
            {
                _cmd.Parameters.Add(new SqlParameter("_basvuruProgramID", _basvuruProgramID));
                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " | inner => " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                dt = null;
            }

            return new GirisSinavPuanlariListem(dt);
        }

        public GirisSinavPuanlariListem getirPuanBarajlariTobb(object _basvuruProgramID)
        {
            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID, (CASE KodID WHEN 20003 THEN 'TOBB ETÜ Yabancı Dil Sınavı' ELSE Aciklama END) AS Aciklama,
                                                                                 ISNULL((SELECT REPLACE(Puan1, '.', ',')
                                                                                         FROM ens.GirisSinaviPuanBarajlari 
                                                                                         WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), 0) AS 'Puan1',
                                                                                 ISNULL((SELECT REPLACE(Puan2, '.', ',')
                                                                                         FROM ens.GirisSinaviPuanBarajlari 
                                                                                         WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), 0) AS 'Puan2',
                                                                                 ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',')
                                                                                         FROM ens.GirisSinaviPuanBarajlari 
                                                                                         WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0') AS
                                                                                         'YabanciUyrukluPuanBaraji' ,null as 'GecerlilikTarihi'
                                                                          FROM Kod k 
                                                                          WHERE KodID IN(20003, 20014, 20016, 20022, 20023, 23002, 23004) and k.Gizli=0");
            DataTable dt = new DataTable();

            try
            {
                _cmd.Parameters.Add(new SqlParameter("_basvuruProgramID", _basvuruProgramID));
                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
            }
            catch (Exception)
            {
                dt = null;
            }

            return new GirisSinavPuanlariListem(dt);
        }

        public DegerlendirmeKriterleriListem getirDegerlendirmeKrierleri(object _basvuruProgramID)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"
                    SELECT
                        KodID,
                        Aciklama,
                        ISNULL(
                            (
                                SELECT
                                    REPLACE(PuanAgirligi, '.', ',')
                                FROM
                                    ens.IstenenBelgeler
                                WHERE
                                    BasvuruProgramID = @_basvuruProgramID
                                    AND BelgeID = k.KodID
                                    AND ISNULL(HesaplamaTuru, 1) = 1
                            ),
                            0
                        ) AS 'Puan1',
                        ISNULL(
                            (
                                SELECT
                                    REPLACE(PuanAgirligi, '.', ',')
                                FROM
                                    ens.IstenenBelgeler
                                WHERE
                                    BasvuruProgramID = @_basvuruProgramID
                                    AND BelgeID = k.KodID
                                    AND ISNULL(HesaplamaTuru, 1) = 2
                            ),
                            0
                        ) AS 'Puan2',
                        ISNULL(
                            (
                                SELECT
                                    REPLACE(PuanAgirligi, '.', ',')
                                FROM
                                    ens.IstenenBelgeler
                                WHERE
                                    BasvuruProgramID = @_basvuruProgramID
                                    AND BelgeID = k.KodID
                                    AND ISNULL(HesaplamaTuru, 1) = 3
                            ),
                            0
                        ) AS 'Puan3',
                        ISNULL(
                            (
                                SELECT
                                    REPLACE(PuanAgirligi, '.', ',')
                                FROM
                                    ens.IstenenBelgeler
                                WHERE
                                    BasvuruProgramID = @_basvuruProgramID
                                    AND BelgeID = k.KodID
                                    AND ISNULL(HesaplamaTuru, 1) = 4
                            ),
                            0
                        ) AS 'Puan4',
                        ISNULL(
                            (
                                SELECT
                                    REPLACE(PuanAgirligi, '.', ',')
                                FROM
                                    ens.IstenenBelgeler
                                WHERE
                                    BasvuruProgramID = @_basvuruProgramID
                                    AND BelgeID = k.KodID
                                    AND ISNULL(HesaplamaTuru, 1) = 5
                            ),
                            0
                        ) AS 'Puan5',
                        ISNULL(
                            (
                                SELECT
                                    REPLACE(PuanAgirligi, '.', ',')
                                FROM
                                    ens.IstenenBelgeler
                                WHERE
                                    BasvuruProgramID = @_basvuruProgramID
                                    AND BelgeID = k.KodID
                                    AND ISNULL(HesaplamaTuru, 1) = 6
                            ),
                            0
                        ) AS 'Puan6'
                    FROM
                        Kod k
                    WHERE
                        KodGrup IN (310)
                        AND KodNo > 0
                        AND Gizli = 0
                ");
                _cmd.Parameters.Add(new SqlParameter("_basvuruProgramID", _basvuruProgramID));

                return new DegerlendirmeKriterleriListem(OgrenciMaster.Database.ExecuteDatatable(_cmd, true));
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.DegerlendirmeHatasi, 1081022, SourceLevels.Error);
            }
        }

        public GirisSinavPuanlariListem getirPuanBarajlariNulll()
        {
            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT 
                                                                        KodID
                                                                        , Aciklama, isnull((SELECT REPLACE(Puan1, '.', ',')
                                                                        FROM ens.GirisSinaviPuanBarajlari
                                                                        WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0')
                                                                        AS 'Puan1'
                                                                        ,ISNULL((SELECT REPLACE(Puan2, '.', ',') FROM ens.GirisSinaviPuanBarajlari
                                                                         WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0') AS 'Puan2'
                                                                        ,ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',')
                                                                         FROM ens.GirisSinaviPuanBarajlari
                                                                         WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0') AS 'YabanciUyrukluPuanBaraji' 
                                                                        ,ISNULL((SELECT TOP 1 GecerlilikTarihi
                                                                         FROM ens.GirisSinaviPuanBarajlari WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID),'') as 'GecerlilikTarihi'                                                                        
                                                                        FROM Kod k WHERE KodGrup IN (-1) AND KodNo > 0 and k.Gizli=0");
            DataTable dt = new DataTable();
            try
            {
                _cmd.Parameters.Add(new SqlParameter("_basvuruProgramID", "-1"));
                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
            }
            catch (Exception)
            {
                dt = null;
            }

            return new GirisSinavPuanlariListem(dt);
        }

        public DegerlendirmeKriterleriListem getirPuanBarajlariNull()
        {
            DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT KodID, Aciklama, ISNULL((SELECT REPLACE(Puan1, '.', ',')
                                                                                                          FROM ens.GirisSinaviPuanBarajlari
                                                                                                          WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0')
                                                                                                                AS 'Puan1', ISNULL((SELECT REPLACE(Puan2, '.', ',')
                                                                                                                                    FROM ens.GirisSinaviPuanBarajlari
                                                                                                                                    WHERE BasvuruProgramID = @_basvuruProgramID AND
                                                                                                                                          SinavTuru = k.KodID), '0') AS 'Puan2'
                                                                                 ISNULL((SELECT REPLACE(YabanciUyrukluPuanBaraji, '.', ',')
                                                                                         FROM ens.GirisSinaviPuanBarajlari
                                                                                         WHERE BasvuruProgramID = @_basvuruProgramID AND SinavTuru = k.KodID), '0') AS
                                                                                               'YabanciUyrukluPuanBaraji' FROM Kod k WHERE KodGrup IN (-1) AND KodNo > 0  and Gizli=0");
            DataTable dt = new DataTable();

            try
            {
                _cmd.Parameters.Add(new SqlParameter("_basvuruProgramID", "-1"));
                dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
            }
            catch (Exception)
            {
                dt = null;
            }

            return new DegerlendirmeKriterleriListem();
        }

        public DataTable getBirim(int OrganizasyonID)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT *
                                                                              FROM vwOrganizasyon
                                                                              WHERE UstBirimID IN (SELECT UstBirimID
                                                                                                   FROM vwOrganizasyon
                                                                                                   WHERE OrganizasyonID = @OrganizasyonID) AND BirimID IS NOT NULL AND AltBirimID IS NULL ");
                _cmd.Parameters.Add(new SqlParameter("OrganizasyonID", OrganizasyonID));
                DataTable dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                DataRow newRow = dt.NewRow();
                newRow["OrganizasyonID"] = -1;
                newRow["BirimAdi"] = "Seçiniz";

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["OrganizasyonID"].ToString() != "-1")
                        dt.Rows.InsertAt(newRow, 0);
                }

                return dt;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Birim Getirirken bir hata meydana geldi.", 1081023, SourceLevels.Error);
            }
        }

        public DataTable getAltBirim(int OrganizasyonID)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT *
                                                                              FROM vwOrganizasyon
                                                                              WHERE BirimID IN (SELECT BirimID
                                                                                                FROM vwOrganizasyon
                                                                                                WHERE OrganizasyonID = @OrganizasyonID) AND BirimID IS NOT NULL AND AltBirimID IS NOT NULL ");
                _cmd.Parameters.Add(new SqlParameter("OrganizasyonID", OrganizasyonID));
                DataTable dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                DataRow newRow = dt.NewRow();
                newRow["OrganizasyonID"] = -1;
                newRow["AltBirimAdi"] = "Seçiniz";

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["OrganizasyonID"].ToString() != "-1")
                        dt.Rows.InsertAt(newRow, 0);
                }

                return dt;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Alt Birim Getirirken bir hata meydana geldi.", 1081024, SourceLevels.Error);
            }
        }

        public DataTable getProgram(int OrganizasyonID)
        {
            try
            {
                DbCommand _cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT *
                                                                              FROM OrganizasyonProgram
                                                                              WHERE OrganizasyonID = @OrganizasyonID");
                _cmd.Parameters.Add(new SqlParameter("OrganizasyonID", OrganizasyonID));
                DataTable dt = OgrenciMaster.Database.ExecuteDatatable(_cmd, true);
                DataRow newRow = dt.NewRow();
                newRow["OrganizasyonProgramID"] = -1;
                newRow["Aciklama"] = "Seçiniz";

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["OrganizasyonProgramID"].ToString() != "-1")
                        dt.Rows.InsertAt(newRow, 0);
                }

                return dt;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Program Getirirken bir hata meydana geldi.", 1081025, SourceLevels.Error);
            }
        }

        public IList<JuriUyeleri> getirJuriUyeleri(int ProgramID, int Yil, int Donem)
        {
            try
            {
                return OgrenciMaster.Database.Select<JuriUyeleri>(@"SELECT ju.*
                                                                    FROM ens.JuriUyeleri ju INNER JOIN ens.BasvuruProgram bp ON ju.BasvuruProgramID = bp.BasvuruProgramID
                                                                    WHERE Yil = ? AND Donem = ? and bp.OrganizasyonProgramID = ?", Yil, Donem, ProgramID);
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHatasi, 1081014, SourceLevels.Error);
            }
        }

        public BasvuruAlim GetBasvuruAlim(int Yil, int Donem)
        {
            return OgrenciMaster.Database.SelectSingle<BasvuruAlim>(@"SELECT *
                                                                      FROM BasvuruAlim
                                                                      WHERE OgretimYili = ? AND OgretimDonemi = ? AND BasvuruAlimTuru = 802001", Yil, Donem);
        }

        /// <summary>
        /// WorkItem 176542
        /// </summary>
        /// <param name="Yil"></param>
        /// <param name="Donem"></param>
        /// <returns></returns>
        public void AtamaKaynakHedefKontrolu(int BasvuruYil, int BasvuruDonem, int KaynakYil, int KaynakDonem)
        {
            const string stringMessage = "Gelecek veya aynı döneme ait kopyalama yapamazsınız.";
            if (KaynakYil > BasvuruYil)
            {
                throw new Exception(stringMessage);
            }
            var basvuruDonemSira = OgrenciMaster.Database.SelectSingle<DonemSirasi>(@"SELECT * FROM DonemSirasi WHERE DonemID = ?", BasvuruDonem);
            var kaynakDonemSira = OgrenciMaster.Database.SelectSingle<DonemSirasi>(@"SELECT * FROM DonemSirasi WHERE DonemID = ?", KaynakDonem);

            if (KaynakYil == BasvuruYil && basvuruDonemSira.Sira <= kaynakDonemSira.Sira)
            {
                throw new Exception(stringMessage);
            }

        }


    }
}
