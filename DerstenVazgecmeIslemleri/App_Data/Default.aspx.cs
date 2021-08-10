using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using UniOgrenci.Master.Web.UI;
using UniOgrenci.Master.Entities;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Reflection;
using System.IO;
using UniOgrenci.Master.Exceptions;
using System.Diagnostics;


namespace DerstenVazgecmeIslemleri
{
    public partial class _Default : OgrenciBasePage<DerstenVazgecmeIslemleriUygulama> 
    {
        public bool ilkmi = false;
        private bool YilDonemDegistiMi = false;
        public IList<JuriUyeleri> JuriUyeleriMulakat
        {
            get
            {
                IList<JuriUyeleri> _result = PageSession["JuriUyeleriMulakat"] as IList<JuriUyeleri>;
                if (_result == null)
                    PageSession["JuriUyeleriMulakat"] = _result = null;
                return _result;
            }
            set
            {
                PageSession["JuriUyeleriMulakat"] = value;
            }
        }
        public OgretimUyesi hocaMulakat
        {
            get
            {
                OgretimUyesi _result = PageSession["hocaMulakat"] as OgretimUyesi;
                if (_result == null)
                    PageSession["hocaMulakat"] = _result = null;
                return _result;
            }
            set
            {
                PageSession["hocaMulakat"] = value;
            }
        }
        public BasvuruProgram CurrentBasvuruProgram
        {
            get
            {
                BasvuruProgram _result = PageSession["CurrentBasvuruProgram"] as BasvuruProgram;
                if (_result == null)
                    PageSession["CurrentBasvuruProgram"] = _result = null;
                return _result;
            }
            set
            {
                PageSession["CurrentBasvuruProgram"] = value;
            }
        }

        public int BasvuruYili
        {
            get
            {
                return yildonem_basvuru.Yil;
            }

        }
        public int BasvuruDonem
        {
            get
            {
                return yildonem_basvuru.Donem;
            }

        }
        public GirisSinavPuanlariListem GirisSinavNotlari
        {
            get
            {
                GirisSinavPuanlariListem _result = PageSession["GirisSinavNotlari"] as GirisSinavPuanlariListem;
                if (_result == null)
                    PageSession["GirisSinavNotlari"] = _result = null;
                return _result;
            }
            set
            {
                PageSession["GirisSinavNotlari"] = value;
            }
        }
        public DegerlendirmeKriterleriListem DegerlendirmeKriterleri
        {
            get
            {
                DegerlendirmeKriterleriListem _result = PageSession["DegerlendirmeKriterleri"] as DegerlendirmeKriterleriListem;
                if (_result == null)
                    PageSession["DegerlendirmeKriterleri"] = _result = null;
                return _result;
            }
            set
            {
                PageSession["DegerlendirmeKriterleri"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ilkmi = true;
            if (!IsPostBack)
            {
                hocaMulakat = null;
                JuriUyeleriMulakat = null;
                acEkran();
            }
            yildonem_basvuru.DonemSelectedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.YilDonemCombo.DonemSelected(yildonem_basvuru_DonemSelectedEventHandler);
            ara_hoca_mulakat.OgretimOyesiSearchedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.SearchOgretimUyesi.OgretimOyesiSearchedEvent(ara_hoca_mulakat_OgretimOyesiSearchedEventHandler);
            ara_hoca_mulakat.OgretimOyesiSelectedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.SearchOgretimUyesi.OgretimOyesiSelectedEvent(ara_hoca_mulakat_OgretimOyesiSelectedEventHandler);
            //firefox için düzenlemeler
            txt_min_mezuniyet4.Enabled = true;
            txt_min_yl_mezuniyet4.Enabled = true;
            txt_yabanci_kontenjan.Enabled = true;
            txt_turk_kontenjan.Enabled = true;
            txt_yedek_kontenjan_turk.Enabled = true;
            txt_yedek_kontenjan_yabanci.Enabled = true;
            txt_min_mezuniyet100.Enabled = true;
            txt_min_yl_mezuniyet100.Enabled = true;
            txt_arastirma_gorevli_kontenjan.Enabled = true;
            txt_min_basari_notu.Enabled = true;
            txt_min_mulakat_notu.Enabled = true;
            txt_ekbilgi.Enabled = true;

            // alan dışı olan kontenjanlar gözüksün mü ?
            SistemParam alanDisiKontenjanlarSP = OgrenciMaster.GetSistemParam(15004);
            if (alanDisiKontenjanlarSP != null)
            {
                if (!alanDisiKontenjanlarSP.BooDeger1.GetValueOrDefault())
                {
                    txt_yedek_kontenjan_turk_alan_disi.Visible = false;
                    txt_yedek_kontenjan_yabanci_alan_disi.Visible = false;
                    txt_turk_kontenjan_alan_disi.Visible = false;
                    txt_yabanci_kontenjan_alan_disi.Visible = false;

                    txt_yedek_kontenjan_turk_alan_disi_LBL.Visible = false;
                    txt_yedek_kontenjan_yabanci_alan_disi_LBL.Visible = false;
                    txt_turk_kontenjan_alan_disi_LBL.Visible = false;
                    txt_yabanci_kontenjan_alan_disi_LBL.Visible = false;
                }
            }
            else
            {
                txt_yedek_kontenjan_turk_alan_disi.Visible = false;
                txt_yedek_kontenjan_yabanci_alan_disi.Visible = false;
                txt_turk_kontenjan_alan_disi.Visible = false;
                txt_yabanci_kontenjan_alan_disi.Visible = false;

                txt_yedek_kontenjan_turk_alan_disi_LBL.Visible = false;
                txt_yedek_kontenjan_yabanci_alan_disi_LBL.Visible = false;
                txt_turk_kontenjan_alan_disi_LBL.Visible = false;
                txt_yabanci_kontenjan_alan_disi_LBL.Visible = false;
            }

        }
        void ara_hoca_mulakat_OgretimOyesiSelectedEventHandler(UniOgrenci.Master.Entities.OgretimUyesi value)
        {
            if (value != null)
            {
                if (value.AkademikUnvan != null && value.AkademikUnvan.KodNo != 0)
                    lbl_hoca_mulakat.Text = value.AkademikUnvan.Aciklama + " " + value.Ad + " " + value.Soyad;
                else
                    lbl_hoca_mulakat.Text = value.Ad + " " + value.Soyad;
                hocaMulakat = value;
            }
            else
            {
                lbl_hoca_mulakat.Text = "";
                hocaMulakat = null;
            }
        }
        void ara_hoca_mulakat_OgretimOyesiSearchedEventHandler(UniOgrenci.Master.Entities.OgretimUyesi o)
        {
            if (o != null)
            {
                if (o.AkademikUnvan != null && o.AkademikUnvan.KodNo != 0)
                    lbl_hoca_mulakat.Text = o.AkademikUnvan.Aciklama + " " + o.Ad + " " + o.Soyad;
                else
                    lbl_hoca_mulakat.Text = o.Ad + " " + o.Soyad;
                hocaMulakat = o;
            }
            else
            {
                lbl_hoca_mulakat.Text = "";
                hocaMulakat = null;
            }
        }
        /// <summary>
        /// XX-XXXX-XX formatında uygulama ID değeri.
        /// ProjeId-UygulamaNo-Ekran No(.aspx sayısı) olarak düşünülmeli.
        /// </summary>
        protected override int UygulamaID
        {
            get { return 10008101; }
        }
        protected void organizasyon_ProgramSelectedEventHandler(int selectedProgramID)
        {
            if (ilkmi)
                acEkran();
        }
        protected void organizasyon_orgSelectedEventHandler(int selectedProgramID)
        {
            acEkran();
        }
        protected void grd_mulakat_hoca_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grd_mulakat_hoca.DataSource = JuriUyeleriMulakat;
        }
        protected void cmb_ogrenci_turu_SelectedIndexChanged(Kod k)
        {
            acEkran();
        }
        private void acEkran()
        {
            try
            {
                if (organizasyon_.SelectedProgramId == -1)
                {
                    temizleEkran();
                }
                else//bilgiler secili
                {
                    doldurEkran();
                }
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message,ex.Kod);
            }
            catch (Exception)
            {
                ltlInfo.Text = base.HataGoster("Ekran doldurulurken bilinmeyen bir hata oluştu.");
            }
        }
        private void temizleEkran()
        {
            try
            {
                pnl_genel.Enabled = false;
                RadTabStrip1.Enabled = false;
                btn_kaydet.Enabled = false;
                RadMultiPage1.Enabled = false;
                date_baslama_tarihi.SelectedDate = null;
                date_obaslama_tarihi.SelectedDate = null;
                date_bitis_tarihi.SelectedDate = null;
                date_obitis_tarihi.SelectedDate = null;
                ChkMezunOlmayaniAl.Checked = false;
                ChkResimZorunlu.Checked = false;
                txt_kontenjan.Text = "0";
                txt_yabanci_kontenjan.Text = "0";
                txt_turk_kontenjan.Text = "0";
                txt_yabanci_kontenjan_alan_disi.Text = "0";
                txt_turk_kontenjan_alan_disi.Text = "0";
                txt_arastirma_gorevli_kontenjan.Text = "0";
                txt_yedek_kontenjan.Value = 0;
                txt_yedek_kontenjan_yabanci.Value = 0;
                txt_yedek_kontenjan_turk.Value = 0;
                txt_yedek_kontenjan_yabanci_alan_disi.Value = 0;
                txt_yedek_kontenjan_turk_alan_disi.Value = 0;
                txt_min_mezuniyet4.Text = "0";
                txt_min_mezuniyet100.Text = "0";
                txt_min_yl_mezuniyet4.Text = "0";
                txt_min_yl_mezuniyet100.Text = "0";
                txt_mulakat_yeri.Text = "";
                date_mulakat_tarihi.SelectedDate = null;
                txt_bilim_sinavi.Text = "";
                date_bilim_sinavi_tarihi.SelectedDate = null;
                ara_hoca_mulakat.Value = "";
                lbl_hoca_mulakat.Text = "";
                cmb_mulakat_hoca_durum.SelectedKodId = -1;
                JuriUyeleriMulakat = OgrenciUygulama.getirJuriUyeleriNull();
                txt_min_basari_notu.Value = 0;
                txt_min_mulakat_notu.Value = 0;
                txt_ekbilgi.Text = "";
                if (ilkmi)
                {
                    GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlariNulll();
                    grd_sinav_notlari.Rebind();
                }
                DegerlendirmeKriterleri = OgrenciUygulama.getirPuanBarajlariNull();
                grd_degerlendirme_kriterleri.Rebind();
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Ekran temizlenirken bilinmeyen bir hata meydana geldi!", 1081004, SourceLevels.Error);
            }
        }
        private void doldurEkran()
        {
            try
            {
                if (organizasyon_.SelectedProgramId == -1)
                    return;

                if (yildonem_basvuru.Donem != -1 && yildonem_basvuru.Yil != -1)
                    pnl_genel.Enabled = true;
                else
                    pnl_genel.Enabled = false;
                btn_kaydet.Enabled = true;
                OrganizasyonProgram op = OgrenciMaster.Database.LoadEntityByID<OrganizasyonProgram>(organizasyon_.SelectedProgramId);
                OrganizasyonView ov = OgrenciMaster.Database.LoadEntityByID<OrganizasyonView>(organizasyon_.SelectedOrganizationId);
                CurrentBasvuruProgram = OgrenciUygulama.getirBasvuruProgram(organizasyon_.SelectedProgramId, op.ProgramTuru.Kodid, BasvuruYili, BasvuruDonem);
                if (CurrentBasvuruProgram == null)
                {
                    CurrentBasvuruProgram = OgrenciUygulama.getirBasvuruProgramDefault(ov.UstBirimID, op.ProgramTuru.Kodid);
                }
                if (CurrentBasvuruProgram != null && CurrentBasvuruProgram.BasvuruProgramid > 0)
                {
                    RadTabStrip1.Enabled = true;
                    RadMultiPage1.Enabled = true;
                    ChkMezunOlmayaniAl.Checked = CurrentBasvuruProgram.MezunOlmayaniAl;
                    ChkResimZorunlu.Checked = CurrentBasvuruProgram.ResimZorunlu;
                    date_baslama_tarihi.SelectedDate = CurrentBasvuruProgram.BaslamaTarihi;
                    date_obaslama_tarihi.SelectedDate = CurrentBasvuruProgram.OnayBaslamaTarihi;
                    date_bitis_tarihi.SelectedDate = CurrentBasvuruProgram.BitisTarihi;
                    date_obitis_tarihi.SelectedDate = CurrentBasvuruProgram.OnayBitisTarihi;
                    txt_kontenjan.Text = CurrentBasvuruProgram.Kontenjan.ToString();
                    txt_yabanci_kontenjan.Text = CurrentBasvuruProgram.YabanciKontenjan.ToString();

                    txt_turk_kontenjan.Text = CurrentBasvuruProgram.TurkKontenjan.ToString();
                    txt_yedek_kontenjan.Text = CurrentBasvuruProgram.YedekKontenjan.ToString();
                    txt_yedek_kontenjan_yabanci.Text = CurrentBasvuruProgram.YedekYabanciKontenjan.ToString();
                    txt_yedek_kontenjan_turk.Text = CurrentBasvuruProgram.TurkYedekKontenjan.ToString();

                    txt_turk_kontenjan_alan_disi.Text = CurrentBasvuruProgram.TurkKontenjanAlanDisi.ToString();
                    txt_yabanci_kontenjan_alan_disi.Text = CurrentBasvuruProgram.YabanciKontenjanAlanDisi.ToString();
                    txt_yedek_kontenjan_yabanci_alan_disi.Text = CurrentBasvuruProgram.YedekYabanciKontenjanAlanDisi.ToString();
                    txt_yedek_kontenjan_turk_alan_disi.Text = CurrentBasvuruProgram.TurkYedekKontenjanAlanDisi.ToString();


                    txt_arastirma_gorevli_kontenjan.Text = CurrentBasvuruProgram.ArastirmaGorevlisiKontenjan.ToString();
                    txt_min_mezuniyet4.Text = CurrentBasvuruProgram.MinimumMezuniyetNotu4sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumMezuniyetNotu4sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_mezuniyet100.Text = CurrentBasvuruProgram.MinimumMezuniyetNotu100sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumMezuniyetNotu100sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_yl_mezuniyet4.Text = CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu4sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu4sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_yl_mezuniyet100.Text = CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu100sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu100sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_basari_notu.Text = CurrentBasvuruProgram.MinimumBasariNotu != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumBasariNotu.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_mulakat_notu.Text = CurrentBasvuruProgram.MinimumMulakatNotu != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumMulakatNotu.GetValueOrDefault()).ToString().Replace(',', '.') : "0";
                    txt_mulakat_yeri.Text = CurrentBasvuruProgram.MulakatYeri;
                    txt_ekbilgi.Text = CurrentBasvuruProgram.EkBilgi;
                    date_mulakat_tarihi.SelectedDate = CurrentBasvuruProgram.MulakatTarihi;
                    txt_bilim_sinavi.Text = CurrentBasvuruProgram.BilimSinaviYeri;
                    date_bilim_sinavi_tarihi.SelectedDate = CurrentBasvuruProgram.BilimSinaviTarihi;
                    ara_hoca_mulakat.Value = "";
                    lbl_hoca_mulakat.Text = "";
                    cmb_mulakat_hoca_durum.SelectedKodId = -1;
                    JuriUyeleriMulakat = OgrenciUygulama.getirMulakatJuriUyeleri(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem);
                    grd_mulakat_hoca.Rebind();
                    GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlari(CurrentBasvuruProgram.BasvuruProgramid);
                    //if (ilkmi)
                    grd_sinav_notlari.Rebind();
                    DegerlendirmeKriterleri = OgrenciUygulama.getirDegerlendirmeKrierleri(CurrentBasvuruProgram.BasvuruProgramid);
                    grd_degerlendirme_kriterleri.Rebind();
                    temizleAlsPuanTuru();
                    doldurAlsPuanTuru();
                }
                else
                {
                    RadTabStrip1.Enabled = false;
                    RadMultiPage1.Enabled = false;
                    ChkResimZorunlu.Checked = false;
                    ChkMezunOlmayaniAl.Checked = false;
                    date_baslama_tarihi.SelectedDate = null;
                    date_obaslama_tarihi.SelectedDate = null;
                    date_bitis_tarihi.SelectedDate = null;
                    date_obitis_tarihi.SelectedDate = null;
                    txt_kontenjan.Text = "0";
                    txt_yabanci_kontenjan.Text = "0";
                    txt_turk_kontenjan.Text = "0";
                    txt_yabanci_kontenjan_alan_disi.Text = "0";
                    txt_turk_kontenjan_alan_disi.Text = "0";
                    txt_arastirma_gorevli_kontenjan.Text = "0";
                    txt_yedek_kontenjan.Value = 0;
                    txt_yedek_kontenjan_yabanci.Value = 0;
                    txt_yedek_kontenjan_turk.Value = 0;
                    txt_yedek_kontenjan_yabanci_alan_disi.Value = 0;
                    txt_yedek_kontenjan_turk_alan_disi.Value = 0;
                    txt_min_mezuniyet4.Text = "0";
                    txt_min_yl_mezuniyet4.Text = "0";
                    txt_min_mezuniyet100.Text = "0";
                    txt_min_yl_mezuniyet100.Text = "0";
                    txt_min_basari_notu.Value = 0;
                    txt_min_mulakat_notu.Value = 0;
                    txt_ekbilgi.Text = "";
                    txt_mulakat_yeri.Text = "";
                    date_mulakat_tarihi.SelectedDate = null;
                    txt_bilim_sinavi.Text = "";
                    date_bilim_sinavi_tarihi.SelectedDate = null;
                    ara_hoca_mulakat.Value = "";
                    lbl_hoca_mulakat.Text = "";
                    cmb_mulakat_hoca_durum.SelectedKodId = -1;
                    JuriUyeleriMulakat = OgrenciUygulama.getirJuriUyeleriNull();
                    grd_mulakat_hoca.Rebind();
                    GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlariNulll();
                    grd_sinav_notlari.Rebind();
                    DegerlendirmeKriterleri = OgrenciUygulama.getirPuanBarajlariNull();
                    grd_degerlendirme_kriterleri.Rebind();
                    temizleAlsPuanTuru();
                }
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex,"Ekran doldurulurken bilinmeyen bir hata meydana geldi!",1081003,SourceLevels.Error);
            }
        }

        private void JuriUyeleriDoldur()
        {
            ara_hoca_mulakat.Value = "";
            lbl_hoca_mulakat.Text = "";
            cmb_mulakat_hoca_durum.SelectedKodId = -1;
            if (CurrentBasvuruProgram != null && CurrentBasvuruProgram.BasvuruProgramid > 0)
            {
                JuriUyeleriMulakat = OgrenciUygulama.getirMulakatJuriUyeleri(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem);
            }
            else
            {
                JuriUyeleriMulakat = OgrenciUygulama.getirJuriUyeleriNull();
            }
            grd_mulakat_hoca.Rebind();
        }

        private void doldurAlsPuanTuru()
        {
            IList<ProgramPuanTuru> _temp = OgrenciUygulama.getirProgramPuanTurleri(CurrentBasvuruProgram.BasvuruProgramid);
            for (int i = 0; i < _temp.Count; ++i)
            {
                if (_temp[i].Kod.Kodid == 942001)//sözel
                    chk_sozel.Checked = true;
                if (_temp[i].Kod.Kodid == 942002)//sayısal
                    chk_sayisal.Checked = true;
                if (_temp[i].Kod.Kodid == 942003)//ea
                    chk_ea.Checked = true;
            }
        }

        private bool kaydetAlsPuanTuru()
        {
            bool sonuc = false;
            ProgramPuanTuru _temp;
            if (chk_sozel.Checked)//kaydet
            {
                _temp = OgrenciUygulama.getirProgramPuanTuru(CurrentBasvuruProgram.BasvuruProgramid, 942001);
                if (_temp == null || _temp.ProgramPuanTuruid < 1)
                {
                    _temp = new ProgramPuanTuru();
                    _temp.BasvuruProgram = CurrentBasvuruProgram;
                    // _temp.ProgramPuanTuruid = 942001;
                    _temp.Kod = OgrenciUygulama.getirkod(942001);
                    sonuc = OgrenciUygulama.kaydetProgramPuanTuru(_temp);
                }
            }
            else//sil
            {
                _temp = OgrenciUygulama.getirProgramPuanTuru(CurrentBasvuruProgram.BasvuruProgramid, 942001);
                if (_temp != null && _temp.ProgramPuanTuruid > 0)
                    sonuc = OgrenciUygulama.silProgramPuanTuru(_temp);
            }
            if (chk_sayisal.Checked)//kaydet
            {
                _temp = OgrenciUygulama.getirProgramPuanTuru(CurrentBasvuruProgram.BasvuruProgramid, 942002);
                if (_temp == null || _temp.ProgramPuanTuruid < 1)
                {
                    _temp = new ProgramPuanTuru();
                    _temp.BasvuruProgram = CurrentBasvuruProgram;
                    _temp.Kod = OgrenciUygulama.getirkod(942002);
                    sonuc = OgrenciUygulama.kaydetProgramPuanTuru(_temp);
                }
            }
            else//sil
            {
                _temp = OgrenciUygulama.getirProgramPuanTuru(CurrentBasvuruProgram.BasvuruProgramid, 942002);
                if (_temp != null && _temp.ProgramPuanTuruid > 0)
                    sonuc = OgrenciUygulama.silProgramPuanTuru(_temp);
            }
            if (chk_ea.Checked)//kaydet
            {
                _temp = OgrenciUygulama.getirProgramPuanTuru(CurrentBasvuruProgram.BasvuruProgramid, 942003);
                if (_temp == null || _temp.ProgramPuanTuruid < 1)
                {
                    _temp = new ProgramPuanTuru();
                    _temp.BasvuruProgram = CurrentBasvuruProgram;
                    _temp.Kod = OgrenciUygulama.getirkod(942003);
                    sonuc = OgrenciUygulama.kaydetProgramPuanTuru(_temp);
                }
            }
            else//sil
            {
                _temp = OgrenciUygulama.getirProgramPuanTuru(CurrentBasvuruProgram.BasvuruProgramid, 942003);
                if (_temp != null && _temp.ProgramPuanTuruid > 0)
                    sonuc = OgrenciUygulama.silProgramPuanTuru(_temp);
            }

            return sonuc;

        }
        private void temizleAlsPuanTuru()
        {
            chk_ea.Checked = false;
            chk_sayisal.Checked = false;
            chk_sozel.Checked = false;
        }
        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                #region kontroller

                if (RadMultiPage1.Enabled && !kontroldegerlendirmekriterleritoplamoran())
                {
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.toplamoran);
                    return;
                }
                if (yildonem_basvuru.Donem == -1 || yildonem_basvuru.Yil == -1)
                {
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.yildonemsecilmedi);
                    return;
                }

                #endregion
                bool sonuc = false;
                int arastirmagorkon = 0;
                if (txt_arastirma_gorevli_kontenjan.Text != "")
                    arastirmagorkon = Convert.ToInt32(txt_arastirma_gorevli_kontenjan.Text);

                #region Kontenjanlar

                int kontenjan = 0;

                if (txt_kontenjan.Text != "")
                    kontenjan = Convert.ToInt32(txt_kontenjan.Text);
                int yabancikontenjan = 0;
                if (txt_yabanci_kontenjan.Text != "")
                    yabancikontenjan = Convert.ToInt32(txt_yabanci_kontenjan.Text);
                int turkkontenjan = 0;
                if (txt_turk_kontenjan.Text != "")
                    turkkontenjan = Convert.ToInt32(txt_turk_kontenjan.Text);
                int yedekkontenjan = 0;
                if (txt_yedek_kontenjan.Text != "")
                    yedekkontenjan = Convert.ToInt32(txt_yedek_kontenjan.Text);
                int yedekyabancikontenjan = 0;
                if (txt_yedek_kontenjan_yabanci.Text != "")
                    yedekyabancikontenjan = Convert.ToInt32(txt_yedek_kontenjan_yabanci.Text);
                int yedekturkkontenjan = 0;
                if (txt_yedek_kontenjan_turk.Text != "")
                    yedekturkkontenjan = Convert.ToInt32(txt_yedek_kontenjan_turk.Text);



                int yabancikontenjanAlanDisi = 0;
                if (txt_yabanci_kontenjan_alan_disi.Text != "")
                    yabancikontenjanAlanDisi = Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text);
                int turkkontenjanAlanDisi = 0;
                if (txt_turk_kontenjan_alan_disi.Text != "")
                    turkkontenjanAlanDisi = Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text);

                int yedekyabancikontenjanAlanDisi = 0;
                if (txt_yedek_kontenjan_yabanci_alan_disi.Text != "")
                    yedekyabancikontenjanAlanDisi = Convert.ToInt32(txt_yedek_kontenjan_yabanci_alan_disi.Text);
                int yedekturkkontenjanAlanDisi = 0;
                if (txt_yedek_kontenjan_turk_alan_disi.Text != "")
                    yedekturkkontenjanAlanDisi = Convert.ToInt32(txt_yedek_kontenjan_turk_alan_disi.Text);

                #endregion


                decimal minnot4 = 0;
                if (txt_min_mezuniyet4.Text != "")
                    minnot4 = Convert.ToDecimal(txt_min_mezuniyet4.Text.Replace('.', ','));
                decimal minnot100 = 0;
                if (txt_min_mezuniyet100.Text != "")
                    minnot100 = Convert.ToDecimal(txt_min_mezuniyet100.Text.Replace('.', ','));
                decimal minylnot4 = 0;
                if (txt_min_yl_mezuniyet4.Text != "")
                    minylnot4 = Convert.ToDecimal(txt_min_yl_mezuniyet4.Text.Replace('.', ','));
                decimal minylnot100 = 0;
                if (txt_min_yl_mezuniyet100.Text != "")
                    minylnot100 = Convert.ToDecimal(txt_min_yl_mezuniyet100.Text.Replace('.', ','));
                decimal minbasarinotu = 0;
                if (txt_min_basari_notu.Text != "")
                    minbasarinotu = Convert.ToDecimal(txt_min_basari_notu.Text.Replace('.', ','));
                decimal minmulakatnotu = 0;
                if (txt_min_mulakat_notu.Text != "")
                    minmulakatnotu = Convert.ToDecimal(txt_min_mulakat_notu.Text.Replace('.', ','));
                if (CurrentBasvuruProgram == null || CurrentBasvuruProgram.BasvuruProgramid < 1)
                    CurrentBasvuruProgram = new BasvuruProgram();
                if (CurrentBasvuruProgram.DefaultDeger)
                {
                    CurrentBasvuruProgram = new BasvuruProgram();
                }
                if (txt_yedek_kontenjan.Text != "")
                    CurrentBasvuruProgram.YedekKontenjan = Convert.ToInt32(txt_yedek_kontenjan.Value);
                if (txt_yedek_kontenjan_yabanci.Text != "")
                    CurrentBasvuruProgram.YedekYabanciKontenjan = Convert.ToInt32(txt_yedek_kontenjan_yabanci.Value);
                kontenjan = yabancikontenjan + turkkontenjan + yabancikontenjanAlanDisi + turkkontenjanAlanDisi;
                yedekkontenjan = yedekyabancikontenjan + yedekturkkontenjan + yedekyabancikontenjanAlanDisi + yedekturkkontenjanAlanDisi;
                //if (CurrentBasvuruProgram.DefaultDeger)
                //{
                //    CurrentBasvuruProgram = new BasvuruProgram();
                //}
                CurrentBasvuruProgram.ResimZorunlu = ChkResimZorunlu.Checked;
                CurrentBasvuruProgram.MezunOlmayaniAl = ChkMezunOlmayaniAl.Checked;
                CurrentBasvuruProgram.ArastirmaGorevlisiKontenjan = arastirmagorkon;
                CurrentBasvuruProgram.BaslamaTarihi = date_baslama_tarihi.SelectedDate;
                CurrentBasvuruProgram.OnayBaslamaTarihi = date_obaslama_tarihi.SelectedDate;
                CurrentBasvuruProgram.BitisTarihi = date_bitis_tarihi.SelectedDate;
                CurrentBasvuruProgram.OnayBitisTarihi = date_obitis_tarihi.SelectedDate;
                CurrentBasvuruProgram.Kontenjan = kontenjan;
                CurrentBasvuruProgram.YedekKontenjan = yedekkontenjan;
                CurrentBasvuruProgram.YedekYabanciKontenjan = yedekyabancikontenjan;
                CurrentBasvuruProgram.TurkYedekKontenjan = yedekturkkontenjan;
                CurrentBasvuruProgram.MinimumMezuniyetNotu4sistem = minnot4;
                CurrentBasvuruProgram.MinimumMezuniyetNotu100sistem = minnot100;
                CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu4sistem = minylnot4;
                CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu100sistem = minylnot100;
                CurrentBasvuruProgram.MinimumBasariNotu = minbasarinotu;
                CurrentBasvuruProgram.MinimumMulakatNotu = minmulakatnotu;
                CurrentBasvuruProgram.MulakatTarihi = date_mulakat_tarihi.SelectedDate;
                CurrentBasvuruProgram.MulakatYeri = txt_mulakat_yeri.Text;
                CurrentBasvuruProgram.EkBilgi = txt_ekbilgi.Text;
                OrganizasyonProgram op = OgrenciUygulama.getorgprg(organizasyon_.SelectedOrganizasyonProgram.OrganizasyonProgramid);
                CurrentBasvuruProgram.OrganizasyonProgram = op;
                CurrentBasvuruProgram.BilimSinaviYeri = txt_bilim_sinavi.Text;
                CurrentBasvuruProgram.BilimSinaviTarihi = date_bilim_sinavi_tarihi.SelectedDate;
                CurrentBasvuruProgram.OgrenciTuru = op.ProgramTuru;
                CurrentBasvuruProgram.YabanciKontenjan = yabancikontenjan;
                CurrentBasvuruProgram.TurkKontenjan = turkkontenjan;

                // Alan dışı konenjanlar
                CurrentBasvuruProgram.TurkKontenjanAlanDisi = turkkontenjanAlanDisi;
                CurrentBasvuruProgram.YabanciKontenjanAlanDisi = yabancikontenjanAlanDisi;
                CurrentBasvuruProgram.TurkYedekKontenjanAlanDisi = yedekturkkontenjanAlanDisi;
                CurrentBasvuruProgram.YedekYabanciKontenjanAlanDisi = yedekyabancikontenjanAlanDisi;

                CurrentBasvuruProgram.YabanciDilSinaviYeri = "";
                CurrentBasvuruProgram.YabanciDilSinaviTarihi = null;
                CurrentBasvuruProgram.TurkceDilSinaviYeri = "";
                CurrentBasvuruProgram.TurkceDilSinaviTarihi = null;
                CurrentBasvuruProgram.OgretimYili = BasvuruYili;
                CurrentBasvuruProgram.OgretimDonemi = OgrenciUygulama.getirkod(BasvuruDonem);


                /*Gelen kaydın akademiktakvim ve akademiktakvimaktivite tablolarına kayıtları atılır...*/
                AkademikTakvim AkademikTakvim = OgrenciUygulama.getAkademikTakvim(CurrentBasvuruProgram.OrganizasyonProgram.OrganizasyonProgramid, yildonem_basvuru.Yil, yildonem_basvuru.Donem);

                if (AkademikTakvim == null)
                {
                    AkademikTakvim = new AkademikTakvim();
                    AkademikTakvim.Sinif = 99;
                    AkademikTakvim.OgretimYili = yildonem_basvuru.Yil;
                    AkademikTakvim.OgretimDonemi = OgrenciUygulama.getDonem(yildonem_basvuru.Donem);
                    AkademikTakvim.OrganizasyonProgramid = CurrentBasvuruProgram.OrganizasyonProgram.OrganizasyonProgramid;
                    OgrenciUygulama.guncelleKaydetAkademikTakvim(AkademikTakvim);
                }

                AkademikTakvimAktivite AkademikTakvimAktivite;
                AkademikTakvimAktivite = OgrenciUygulama.getAkademikTakvimAktivite(AkademikTakvim.AkademikTakvimid);
                if (AkademikTakvimAktivite == null)
                    AkademikTakvimAktivite = new AkademikTakvimAktivite();
                AkademikTakvimAktivite.BaslamaTarihi = CurrentBasvuruProgram.BaslamaTarihi;
                AkademikTakvimAktivite.BitisTarihi = CurrentBasvuruProgram.BitisTarihi;
                AkademikTakvimAktivite.Aktivitekodu = OgrenciUygulama.getAktiviteKodu();
                AkademikTakvimAktivite.AkademikTakvim = AkademikTakvim;

                OgrenciUygulama.guncelleKaydetAkademikTakvimAktivite(AkademikTakvimAktivite);


                if (CurrentBasvuruProgram.BasvuruProgramid > 0)//update
                {
                    sonuc = OgrenciUygulama.guncelleBasvuruProgram(CurrentBasvuruProgram);
                }
                else//kaydet
                {
                    sonuc = OgrenciUygulama.kaydetBasvuruProgram(CurrentBasvuruProgram);
                }
                if (sonuc)
                {
                    sonuc = kaydetGirisSinavNotlari();
                    kaydetDegerlendirmeKriterleri();
                    kaydetAlsPuanTuru();
                }
                if (sonuc)
                {
                    acEkran();
                    ltlInfo.Text = base.BilgiGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.kayittamam);
                }
                else
                {
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.kayithatali);
                }
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message,ex.Kod);
            }
            catch (Exception)
            {
                ltlInfo.Text = base.HataGoster("Yaptığınız değişiklikler kaydedilirken bilinmeyen bir hata meydana geldi!");
            }
        }

        protected void btn_hoca_ekle_mulakat_Click(object sender, EventArgs e)
        {
            if (hocaMulakat == null || hocaMulakat.OgretimUyesiid <= 0)
            {
                ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.hocasec);
                return;
            }
            JuriUyeleri _temp = OgrenciUygulama.getirMulakatJuriUyesi(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem, hocaMulakat.OgretimUyesiid);
            bool sonuc = false;
            if (grd_mulakat_hoca.SelectedItems.Count == 0)//Güncelleme değilse
            {
                if (_temp == null || _temp.JuriUyeleriid < 1)
                    _temp = new JuriUyeleri();
                else
                {
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.hocaeklenmis);
                    return;
                }
            }
            if (cmb_mulakat_hoca_durum.SelectedKod == null)
            {
                ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.juri_durum_secilmemis);
                return;
            }
            else
            {
                ltlInfo.Text = "";
            }
            if (cmb_mulakat_hoca_durum.SelectedKod.Kodid == 943001)
            {
                IList<JuriUyeleri> juriUyeleri = OgrenciUygulama.getirMulakatJuriUyeleri(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem);
                for (int i = 0; i < juriUyeleri.Count; i++)
                {
                    if (juriUyeleri[i].OgretimUyesiDurum.Kodid == 943001)
                    {
                        ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.baskanvar);
                        return;
                    }
                }
            }
            _temp.BasvuruProgram = CurrentBasvuruProgram;
            _temp.Donem = OgrenciUygulama.getirkod(BasvuruDonem);
            _temp.JuriTipi = OgrenciUygulama.getirkod(944001);
            _temp.OgretimUyesi = hocaMulakat;
            if (cmb_mulakat_hoca_durum.SelectedKodId > 0)
                _temp.OgretimUyesiDurum = cmb_mulakat_hoca_durum.SelectedKod;
            _temp.OgretimUyesiUnvan = hocaMulakat.AkademikUnvan;
            _temp.Yil = BasvuruYili;
            if (_temp.JuriUyeleriid > 0)//update
            {
                sonuc = OgrenciUygulama.guncelleJuriUyesi(_temp);
            }
            else//kaydet
            {
                sonuc = OgrenciUygulama.kaydetJuriUyesi(_temp);
            }
            if (sonuc)
            {
                JuriUyeleriDoldur();
                //acEkran();
                ltlInfo.Text = base.BilgiGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.hocaeklendi);
            }
            else
            {
                ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.hocaeklenemedi);
            }
        }

        protected void grd_sinav_notlari_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grd_sinav_notlari.DataSource = GirisSinavNotlari.Liste;
        }

        private bool kaydetGirisSinavNotlari()
        {
            bool sonuc = false;
            GirisSinaviPuanBarajlari _temp;
            if (grd_sinav_notlari.Items.Count < 1)
                sonuc = true;
            for (int i = 0; i < grd_sinav_notlari.Items.Count; i++)
            {
                _temp = OgrenciUygulama.getirGirisSinavBaraji(CurrentBasvuruProgram.BasvuruProgramid, grd_sinav_notlari.Items[i].Cells[2].Text);
                if (_temp == null || _temp.GirisSinaviPuanBarajlariid <= 0)
                    _temp = new GirisSinaviPuanBarajlari();
                _temp.BasvuruProgram = CurrentBasvuruProgram;
                _temp.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_sinav_notlari.Items[i].Cells[2].Text));
                Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_sinav_notlari.Items[i].FindControl("txt_puan1");
                Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_sinav_notlari.Items[i].FindControl("txt_puan2");
                Telerik.Web.UI.RadNumericTextBox txt_yabanci_uyruk = (Telerik.Web.UI.RadNumericTextBox)grd_sinav_notlari.Items[i].FindControl("txt_yabanci_uyruk");
                if (txt_puan1.Text != "")
                    _temp.Puan1 = Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));
                else
                    _temp.Puan1 = 0;
                if (txt_puan2.Text != "")
                    _temp.Puan2 = Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));
                else
                    _temp.Puan2 = 0;
                if (txt_yabanci_uyruk.Text != "")
                    _temp.YabanciUyrukluPuanBaraji = Convert.ToDecimal(txt_yabanci_uyruk.Text.Replace('.', ','));
                else
                    _temp.YabanciUyrukluPuanBaraji = 0;
                if (_temp.GirisSinaviPuanBarajlariid > 0)//update
                {
                    sonuc = OgrenciUygulama.guncelleGirisSinavBaraji(_temp) || sonuc;
                }
                else//kaydet
                {
                    sonuc = OgrenciUygulama.kaydetGirisSinavBaraji(_temp) || sonuc;
                }
            }
            return sonuc;

        }

        private bool kontroldegerlendirmekriterleritoplamoran()
        {
            bool sonuc = false;
            decimal toplam = 0;
            decimal toplam2 = 0;
            for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
            {
                Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan1");
                if (txt_puan1.Text != "")
                    toplam = toplam + Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));

            }
            for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
            {
                Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan2");
                if (txt_puan2.Text != "")
                    toplam2 = toplam2 + Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));

            }


            if (toplam == 100 || grd_degerlendirme_kriterleri.Items.Count < 1)
            {
                if (toplam2 == 0 || toplam2 == 100)
                    sonuc = true;
            }
            return sonuc;
        }

        private void kaydetDegerlendirmeKriterleri()
        {
            try
            {
                IstenenBelgeler _istenenBelgeler;

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 1);

                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                    {
                        _istenenBelgeler = new IstenenBelgeler();
                    }
                    _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));

                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan1");
                    if (txt_puan1.Text != "")
                    {
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));
                    }
                    else
                    {
                        _istenenBelgeler.PuanAgirligi = 0;
                    }
                    _istenenBelgeler.HesaplamaTuru = 1;

                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 2);

                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                    {
                        _istenenBelgeler = new IstenenBelgeler();
                    }
                    _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));

                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan2");
                    if (txt_puan2.Text != "")
                    {
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));
                    }
                    else
                    {
                        _istenenBelgeler.PuanAgirligi = 0;
                    }
                    _istenenBelgeler.HesaplamaTuru = 2;

                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 3);
                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                    {
                        _istenenBelgeler = new IstenenBelgeler();
                    }
                    _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));

                    Telerik.Web.UI.RadNumericTextBox txt_puan3 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan3");
                    if (txt_puan3.Text != "")
                    {
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan3.Text.Replace('.', ','));
                    }
                    else
                    {
                        _istenenBelgeler.PuanAgirligi = 0;
                    }
                    _istenenBelgeler.HesaplamaTuru = 3;

                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Değerlendirme kriterleri güncellenirken/kaydedilirken bilinmeyen bir hata meydana geldi.", 1081001, SourceLevels.Error);
            }
        }

        protected void grd_sinav_notlari_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is Telerik.Web.UI.GridDataItem)
            {
                Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl("txt_puan1");
                Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl("txt_puan2");
                Telerik.Web.UI.RadNumericTextBox txt_yabanci_uyruk = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl("txt_yabanci_uyruk");
                if (e.Item.Cells[7].Text != "" && e.Item.Cells[7].Text != "&nbsp;")
                    txt_puan1.Text = Convert.ToDouble(e.Item.Cells[7].Text).ToString().Replace(',', '.');
                //txt_puan1.Text = Convert.ToDouble(e.Item.Cells[6].Text).ToString();
                if (e.Item.Cells[8].Text != "" && e.Item.Cells[8].Text != "&nbsp;")
                    txt_puan2.Text = Convert.ToDouble(e.Item.Cells[8].Text).ToString().Replace(',', '.');
                if (e.Item.Cells[9].Text != "" && e.Item.Cells[9].Text != "&nbsp;")
                    txt_yabanci_uyruk.Text = Convert.ToDouble(e.Item.Cells[9].Text).ToString().Replace(',', '.'); ;
            }
        }

        protected void grd_degerlendirme_kriterleri_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grd_degerlendirme_kriterleri.DataSource = DegerlendirmeKriterleri.Liste;
        }

        protected void btn_hoca_sil_mulakat_Click(object sender, EventArgs e)
        {
            if (grd_mulakat_hoca.SelectedValue != null && grd_mulakat_hoca.SelectedValue != "")
            {
                JuriUyeleri _temp = OgrenciUygulama.getirMulakatJuriUyesi(grd_mulakat_hoca.SelectedValue);
                if (_temp != null && _temp.JuriUyeleriid > 0)
                {
                    OgrenciUygulama.siljuriUye(_temp);
                    acEkran();
                }
            }
        }

        //protected void btn_turkce_sinav_hoca_sil_Click(object sender, EventArgs e)
        //{
        //    if (grid_turkce_sinav.SelectedValue != null && grid_turkce_sinav.SelectedValue != "")
        //    {
        //        JuriUyeleri _temp = OgrenciUygulama.getirMulakatJuriUyesi(grid_turkce_sinav.SelectedValue);
        //        if (_temp != null && _temp.JuriUyeleriid > 0)
        //        {
        //            OgrenciUygulama.siljuriUye(_temp);
        //            acEkran();
        //        }
        //    }
        //}

        protected void yildonem_basvuru_DonemSelectedEventHandler(int yil, int donem)
        {
            try
            {
                doldurEkran();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message,ex.Kod);
            }
            catch (Exception)
            {
                ltlInfo.Text = base.HataGoster("Ekran doldurulurken bilinmeyen bir hata oluştu.");
            }
        }

        protected void grd_mulakat_hoca_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (grd_mulakat_hoca.SelectedItems.Count > 0)
            {
                GridDataItem item = grd_mulakat_hoca.Items[grd_mulakat_hoca.SelectedItems[0].ItemIndex];
                OgretimUyesi ogretimUyesi = OgrenciMaster.Database.LoadEntityByID<OgretimUyesi>(Convert.ToInt32(item["OgretimUyesiID"].Text));
                JuriUyeleri _temp = OgrenciUygulama.getirMulakatJuriUyesi(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem, ogretimUyesi.OgretimUyesiid);
                hocaMulakat = ogretimUyesi;
                cmb_mulakat_hoca_durum.SelectedKodId = _temp.OgretimUyesiDurum.Kodid;
                ara_hoca_mulakat.OgretimUyesiID = hocaMulakat.OgretimUyesiid;
                ara_hoca_mulakat.Value = hocaMulakat.TCKimlikNo;
            }
        }

        protected void txt_turk_kontenjan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_kontenjan.Text = (Convert.ToInt32(txt_turk_kontenjan.Text) + Convert.ToInt32(txt_yabanci_kontenjan.Text) + Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text)).ToString();
            }
            catch { }
        }

        protected void txt_yabanci_kontenjan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_kontenjan.Text = (Convert.ToInt32(txt_turk_kontenjan.Text) + Convert.ToInt32(txt_yabanci_kontenjan.Text) + Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text)).ToString();
            }
            catch { }
        }

        protected void txt_yedek_kontenjan_turk_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_yedek_kontenjan.Text = (Convert.ToInt32(txt_yedek_kontenjan_turk.Text) + Convert.ToInt32(txt_yedek_kontenjan_turk_alan_disi.Text) + Convert.ToInt32(txt_yedek_kontenjan_yabanci_alan_disi.Text) + Convert.ToInt32(txt_yedek_kontenjan_yabanci.Text)).ToString();
            }
            catch { }
        }

        protected void txt_yedek_kontenjan_yabanci_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_yedek_kontenjan.Text = (Convert.ToInt32(txt_yedek_kontenjan_turk.Text) + Convert.ToInt32(txt_yedek_kontenjan_turk_alan_disi.Text) + Convert.ToInt32(txt_yedek_kontenjan_yabanci_alan_disi.Text) + Convert.ToInt32(txt_yedek_kontenjan_yabanci.Text)).ToString();
            }
            catch { }
        }

        #region exporttoexcel
        void ExportToExcel(IList<JuriUyeleri> reportList, string fileName, string reportName)
        {
            //The Clear method erases any buffered HTML output.
            HttpContext.Current.Response.Clear();
            //The AddHeader method adds a new HTML header and value to the response sent to the client.
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            //The ContentType property specifies the HTTP content type for the response.
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            //Implements a TextWriter for writing information to a string. The information is stored in an underlying StringBuilder.
            using (StringWriter sw = new StringWriter())
            {

                //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    //  Create a form to contain the List
                    System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                    System.Web.UI.WebControls.TableRow row = new System.Web.UI.WebControls.TableRow();

                    System.Web.UI.WebControls.TableRow row_reportName = new System.Web.UI.WebControls.TableRow();
                    TableHeaderCell cell_reportName = new TableHeaderCell();
                    cell_reportName.Text = reportName;
                    row_reportName.Cells.Add(cell_reportName);

                    table.Rows.Add(row_reportName);

                        //TableHeaderCell hcellonay = new TableHeaderCell();
                        //hcellonay.Text = "Onay";
                        //row.Cells.Add(hcellonay);
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = "Ünvan";
                        row.Cells.Add(hcell);
                        TableHeaderCell hcell1 = new TableHeaderCell();
                        hcell1.Text = "Ad";
                        row.Cells.Add(hcell1);
                        TableHeaderCell hcell2 = new TableHeaderCell();
                        hcell2.Text = "Soyad";
                        row.Cells.Add(hcell2);
                        TableHeaderCell hcell3 = new TableHeaderCell();
                        hcell3.Text = "Durum";
                        row.Cells.Add(hcell3);

                    
                    table.Rows.Add(row);
                    //  add each of the data item to the table
                    foreach (JuriUyeleri item in reportList)
                    {
                        System.Web.UI.WebControls.TableRow row1 = new System.Web.UI.WebControls.TableRow();


                        //TableCell onay = new TableCell(); onay.Text = "" + item.OgretimUyesi.Aktif.Aciklama;
                        TableCell Ad = new TableCell(); Ad.Text = "" + item.OgretimUyesi.Ad;
                        TableCell Soyad = new TableCell(); Soyad.Text = "" + item.OgretimUyesi.Soyad;
                        TableCell Durum = new TableCell(); Durum.Text = "" + item.OgretimUyesiDurum.Aciklama.ToString();
                        TableCell Unvan = new TableCell(); Unvan.Text = "" + item.OgretimUyesiUnvan.Aciklama.ToString();

                        //row1.Cells.Add(onay);
                        row1.Cells.Add(Unvan);
                        row1.Cells.Add(Ad);
                        row1.Cells.Add(Soyad);
                        row1.Cells.Add(Durum);

                        table.Rows.Add(row1);
                    }
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ClearContent();

                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
                    HttpContext.Current.Response.Charset = "windows-1254"; //ISO-8859-13 ISO-8859-9  windows-1254

                    HttpContext.Current.Response.Buffer = true;
                    this.EnableViewState = false;
                    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    string header = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\n<head>\n<title></title>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1254\" />\n<style>\n</style>\n</head>\n<body>\n";


                    //  render the table into the htmlwriter
                    table.RenderControl(htw);
                    //  render the htmlwriter into the response

                    HttpContext.Current.Response.Write(header + sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }
        #endregion

        protected void ExcelKayit_Click(object sender, ImageClickEventArgs e)
        {
            ExportToExcel(JuriUyeleriMulakat, "JüriÜyeleri.xls", "Jüri Üyeleri");
        }

        /// <summary>
        /// web servis testidir silmeyiniz. efecan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void webdeneme_Click(object sender, EventArgs e)
        {
            // GetHarcOnay(string ogrenciNo, int ogretimYili, Kod ogretimDonemi)
            Kod a = OgrenciMaster.Database.LoadEntityByID<Kod>(100001);
            //beykoz öğrenci no 0901030048 plato 20110105007, 20090101010
            try
            {
                object response = OgrenciUygulama.OgrenciMaster.HarcIslemleri.GetHarcOnay(ogrno.Text, 2011, a);
                string aciklama = ((UniOgrenci.Master.Entities.HarcOnay)(response)).Aciklama;
                string[] bol = aciklama.Split('.');
                string kayitYapabilirMi = bol[0];
                string krediSayisi = bol[1];
                string kredi = bol[2];

            }
            catch (Exception) 
            {
            
            }
        }
    }
}
