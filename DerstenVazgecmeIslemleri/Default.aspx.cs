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
using AjaxControlToolkit;
using UniOgrenci.Master.Web.UI;
using UniOgrenci.Master.Entities;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Reflection;
using System.IO;
using UniOgrenci.Master.Exceptions;
using System.Diagnostics;
using DerstenVazgecmeIslemleri.Resources;
using DerstenVazgecmeIslemleri.Resources.Lang;
using UniOgrenci.Master.Enums;
using DerstenVazgecmeIslemleri.asd;
using System.Text;
using System.Globalization;
using UniOgrenci.Master.Utils;
using System.Data.Common;


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
            set { PageSession["JuriUyeleriMulakat"] = value; }
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
            set { PageSession["hocaMulakat"] = value; }
        }

        public BasvuruProgram CurrentBasvuruProgram
        {
            get
            {
                BasvuruProgram _result = Session["CurrentBasvuruProgram"] as BasvuruProgram;

                if (_result == null)
                    Session["CurrentBasvuruProgram"] = _result = null;

                return _result;
            }
            set { Session["CurrentBasvuruProgram"] = value; }
        }



        public SistemParam SistemParamBelgelerSekmesi
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15022); }
        }
        public SistemParam SistemParamDisiplinSekmesi
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15036); }
        }
        public SistemParam SistemParamBasvuruKontrolSekmesi
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15031); }
        }
        public SistemParam SistemParamUcretDurumu
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15020); }
        }

        public SistemParam SistemParamBarajNotlarinaBakilacak
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15021); }
        }
        public SistemParam SistemParamUstBirimKontrolu
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15023); }
        }
        public SistemParam SistemParamMulakatSeviyeKontrolu
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15032); }
        }
        public SistemParam SistemParamOncekiDonemdenAktarimYapilsinMi
        {
            get { return OgrenciMaster.Database.LoadEntityByID<SistemParam>(15034); }
        }

        public SistemParam SistemParamBelgeKontrolu
        {
            get { return Session["SistemParamBelgeKontrolu_DerstenVazgecmeIslemleri"] as SistemParam; }
            set { Session["SistemParamBelgeKontrolu_DerstenVazgecmeIslemleri"] = value; }
        }

        public List<Kod> KodGrupBelgeKontrolu
        {
            get { return Session["KodGrupBelgeKontrolu_DerstenVazgecmeIslemleri"] as List<Kod>; }
            set { Session["KodGrupBelgeKontrolu_DerstenVazgecmeIslemleri"] = value; }
        }
        //BasvuruProgramina ait belgeler listesi. Page load yapildiginda doldurulur.
        public List<BasvuruProgramBelge> BasvuruProgramBelgeList
        {
            get { return (Session["BasvuruProgramBelgeList_DerstenVazgecmeIslemleri"] as List<BasvuruProgramBelge>); }
            set { Session["BasvuruProgramBelgeList_DerstenVazgecmeIslemleri"] = value; }
        }
        //BasvuruProgramina ait Kontrol Kriterleri listesi. Page load yapildiginda doldurulur.
        public List<BasvurudaKontrolKriter> BasvuruKontrolKriterList
        {
            get { return (Session["BasvuruKontrolKriterList_DerstenVazgecmeIslemleri"] as List<BasvurudaKontrolKriter>); }
            set { Session["BasvuruKontrolKriterList_DerstenVazgecmeIslemleri"] = value; }
        }
        //BasvuruProgramina ait Disiplin Basvuru listesi. Page load yapildiginda doldurulur.        
        public List<ProgramDisiplinBasvuru> ProgramDisiplinBasvuruList
        {
            get { return (Session["ProgramDisiplinBasvuruList_DerstenVazgecmeIslemleri"] as List<ProgramDisiplinBasvuru>); }
            set { Session["ProgramDisiplinBasvuruList_DerstenVazgecmeIslemleri"] = value; }
        }


        public int BasvuruYili
        {
            get { return yildonem_basvuru.Yil; }
        }

        public int BasvuruDonem
        {
            get { return yildonem_basvuru.Donem; }
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
            set { PageSession["GirisSinavNotlari"] = value; }
        }

        public List<OrganizasyonProgram> altBirimListesi
        {
            get { return Session["DerstenVazgecmeIslemleri_OrganizasyonProgram"] as List<OrganizasyonProgram>; }
            set { Session["DerstenVazgecmeIslemleri_OrganizasyonProgram"] = value; }
        }

        public int currentNumber
        {
            get
            {
                if (Session["DerstenVazgecmeIslemleri_currentNumber"] == null)
                    Session["DerstenVazgecmeIslemleri_currentNumber"] = 0;

                return Convert.ToInt32(Session["DerstenVazgecmeIslemleri_currentNumber"].ToString());
            }
            set { Session["DerstenVazgecmeIslemleri_currentNumber"] = value; }
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
            set { PageSession["DegerlendirmeKriterleri"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                bool DigerProgramlaraJuriAtamasiYap = OgrenciMaster.GetSistemParamDeger(15007, false, SistemParamLevel.Deger1);


                if (!DigerProgramlaraJuriAtamasiYap)
                    pnlDigerBirimlerdenJuriAtama.Visible = false;

                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Ege ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.EgeTest)
                {
                    grd_degerlendirme_kriterleri.Columns[3].HeaderText = "Yüzde katkı oranı(Yabancı Uyruklu Muaf Olanlar İçin)";
                    grd_degerlendirme_kriterleri.Columns[4].HeaderText = "Yüzde katkı oranı(Yabancı Uyruklu Muaf Olmayanlar İçin)";
                }

                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Marmara ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.MarmaraTest)
                    pnlBasvuruSecimi.Visible = false;

                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                {
                    trMilliSporcu.Visible = true;
                    txt_milli_sporcu_alan_disi_kontenjan.Visible = true;
                    lbl_milli_sporcu_alan_disi_kontenjan.Visible = true;
                    txt_milli_sporcu_alan_ici_kontenjan.Visible = true;
                    lbl_milli_sporcu_alan_ici_kontenjan.Visible = true;
                }

                ilkmi = true;

                pnlCaniasDeneme.Visible = false;

                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.Okan || OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.OkanTest)
                    pnlCaniasDeneme.Visible = false;

                //Ilgili Program icin zorunlu belgeleri al                


                if (!IsPostBack)
                {

                    //Disiplin bilgileri bolumu icindeki bilesenleri alarak baslangic yukleme ayarlarini yapar.
                    OgrenciUygulama.DisiplinBilgileriBolumuInit(cmbDisiplinTuru, cmbBasvurulabilecekProgramTuru);
                    //Disiplin bilgileri bolumu icindeki bilesenleri alarak baslangic yukleme ayarlarini yapar.
                    OgrenciUygulama.BasvuruKontrolListeleriInit();

                    OgrenciUygulama.TabGorunurlukAyariYap(RadTabStrip1);
                    //Work item 176542
                    if (!(SistemParamOncekiDonemdenAktarimYapilsinMi != null &&
                        SistemParamOncekiDonemdenAktarimYapilsinMi.BooDeger1.HasValue &&
                        SistemParamOncekiDonemdenAktarimYapilsinMi.BooDeger1.Value))
                    {
                        pnlKaynaktanAktar.Visible = false;
                    }

                    this.KodSistemParamDoldur();
                    hocaMulakat = null;
                    JuriUyeleriMulakat = null;
                    acEkran();
                    cmbBasvuruSeciminiBaslat();

                    pnlUcretDurumu.Visible = false;
                    if (SistemParamUcretDurumu != null && SistemParamUcretDurumu.BooDeger1.HasValue)
                        pnlUcretDurumu.Visible = (bool)SistemParamUcretDurumu.BooDeger1;

                    chkBasvurudaBarajNotlariGozuksun.Visible = false;
                    if (SistemParamBarajNotlarinaBakilacak != null && SistemParamBarajNotlarinaBakilacak.BooDeger1.HasValue)
                        chkBasvurudaBarajNotlariGozuksun.Visible = (bool)SistemParamBarajNotlarinaBakilacak.BooDeger1;

                    pnlChk.Visible = false;
                    if (SistemParamUstBirimKontrolu != null && SistemParamUstBirimKontrolu.BooDeger1.HasValue)
                        pnlChk.Visible = (bool)SistemParamUstBirimKontrolu.BooDeger1;

#if DEBUG
                    pnlUcretDurumu.Visible = chkBasvurudaBarajNotlariGozuksun.Visible = pnlChk.Visible = true;
#endif

                    //BelgeKontroluEkran();
                }

                yildonem_basvuru.DonemSelectedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.YilDonemCombo.DonemSelected(yildonem_basvuru_DonemSelectedEventHandler);


                ara_hoca_mulakat.OgretimOyesiSearchedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.SearchOgretimUyesi.OgretimOyesiSearchedEvent(
                                                                     ara_hoca_mulakat_OgretimOyesiSearchedEventHandler);
                ara_hoca_mulakat.OgretimOyesiSelectedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.SearchOgretimUyesi.OgretimOyesiSelectedEvent(
                                                                     ara_hoca_mulakat_OgretimOyesiSelectedEventHandler);

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
                        if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                        {
                            txt_milli_sporcu_alan_disi_kontenjan.Visible = false;
                            lbl_milli_sporcu_alan_disi_kontenjan.Visible = false;
                        }
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

                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                    {
                        txt_milli_sporcu_alan_ici_kontenjan.Visible = false;
                        lbl_milli_sporcu_alan_ici_kontenjan.Visible = false;
                    }
                }

                SistemParam spDegerlendirmeFarkli = OgrenciMaster.GetSistemParam(15010);
                if (spDegerlendirmeFarkli != null && spDegerlendirmeFarkli.BooDeger1 == true)
                    grd_degerlendirme_kriterleri.Columns[5].Visible = true;
                else
                    grd_degerlendirme_kriterleri.Columns[5].Visible = false;

                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.TobbETU || OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.TobbETUTest)
                {
                    grd_sinav_notlari.Columns.FindByUniqueName("TemplateColumn1").Visible = grd_degerlendirme_kriterleri.Columns.FindByUniqueName("TemplateColumn1").Visible = false;
                    grd_sinav_notlari.Columns.FindByUniqueName("TemplateColumn").HeaderText = "Minimum Puan Barajı (T.C Uyruklu)";
                }

                if (SistemParamBarajNotlarinaBakilacak != null && SistemParamBarajNotlarinaBakilacak.BooDeger1.HasValue && SistemParamBarajNotlarinaBakilacak.BooDeger1 == (bool)true)
                    grd_degerlendirme_kriterleri.Columns[6].Visible = true;
                else
                    grd_degerlendirme_kriterleri.Columns[6].Visible = false;

                if (SistemParamMulakatSeviyeKontrolu != null && SistemParamMulakatSeviyeKontrolu.BooDeger1.HasValue && SistemParamMulakatSeviyeKontrolu.BooDeger1 == (bool)true)
                    grd_degerlendirme_kriterleri.Columns[7].Visible = true;
                else
                    grd_degerlendirme_kriterleri.Columns[7].Visible = false;

                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.Ege || OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.EgeTest)
                {
                    ChkTurkYabanciOrtak.Visible = true;
                    lblTurkYabanciOrtak.Visible = true;
                }

            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810001);
            }
        }

        private void cmbBasvuruSeciminiBaslat()
        {
            //cmbAltBirim.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seçiniz", "-1"));
            //cmbBirim.Items.Insert(0,new RadComboBoxItem("Seçiniz", "-1"));
            //cmbProgram.Items.Insert(0,new RadComboBoxItem("Seçiniz", "-1"));
            cmbBasvuruSecimi.Items.Add(DerstenVazgecmeIslemleriResources.EnsBasvuruIslem);
            cmbBasvuruSecimi.Items.Add(DerstenVazgecmeIslemleriResources.YatayGecisHata);
            cmbBasvuruSecimi.Items.Add(DerstenVazgecmeIslemleriResources.YUBasvuruHatasi);
        }

        void ara_hoca_mulakat_OgretimOyesiSelectedEventHandler(UniOgrenci.Master.Entities.OgretimUyesi value) { }

        void ara_hoca_mulakat_OgretimOyesiSearchedEventHandler(UniOgrenci.Master.Entities.OgretimUyesi o) { }

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
            try
            {
                //this.BelgeKontroluEkran();

                if (ilkmi)
                    acEkran();

                //cmbBirim.SelectedValue = organizasyon_.SelectedOrganizationId.ToString();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810004);
            }
        }

        protected void organizasyon_orgSelectedEventHandler(int selectedProgramID)
        {
            try
            {
                acEkran();

            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810005);
            }
        }

        protected void grd_mulakat_hoca_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                grd_mulakat_hoca.DataSource = JuriUyeleriMulakat;
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810006);
            }
        }

        protected void cmb_ogrenci_turu_SelectedIndexChanged(Kod k)
        {
            try
            {
                acEkran();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810007);
            }
        }

        private void acEkran()
        {
            try
            {
                //BelgeKontroluEkran();

                if (organizasyon_.SelectedProgramId == -1 && !chkUstBirimBazinda.Checked)
                {
                    temizleEkran();
                    cmbBirim.DataSource = OgrenciUygulama.getBirim(organizasyon_.SelectedOrganizationId);
                    cmbBirim.DataBind();
                }
                else//bilgiler secili
                    doldurEkran();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception)
            {
                ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleriResources.EkranHatasi);
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
                //İstanbul Kemerburgaz Üniversitesi fotograf yukleme alanı, secili ve disable özelligi 
                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Kemerburgaz ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.KemerburgazTest)
                {
                    ChkResimZorunlu.Checked = true;
                    ChkResimZorunlu.Enabled = false;
                    //ChkResimZorunlu.Enabled;
                }
                else
                {
                    ChkResimZorunlu.Checked = false;
                    ChkResimZorunlu.Enabled = true;
                }
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
                if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                {
                    txt_milli_sporcu_alan_ici_kontenjan.Value = 0;
                    txt_milli_sporcu_alan_disi_kontenjan.Value = 0;
                }
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
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.EkranTemizleHata, 1081004, SourceLevels.Error);
            }
        }

        private void programYukle()
        {
            if (Organizasyon1.SelectedOrganizasyon != null)
            {
                cmbProgramTuru.Items.Clear();
                cmbProgramTuru.ClearSelection();
                OrganizasyonView ov = OgrenciMaster.Database.LoadEntityByID<OrganizasyonView>(Organizasyon1.SelectedOrganizationId);
                cmbProgramTuru.DataSource = OgrenciUygulama.getirProgramTuru(ov.UstBirimID);
                cmbProgramTuru.DataTextField = "Aciklama";
                cmbProgramTuru.DataValueField = "ProgramTuru";
                cmbProgramTuru.DataBind();
                cmbProgramTuru.Items.Insert(0, new RadComboBoxItem("Seçiniz", "-1"));
                cmbProgramTuru.SelectedValue = "-1";
            }
        }

        private void doldurEkran()
        {
            try
            {

                if (organizasyon_.SelectedProgramId == -1 && !chkUstBirimBazinda.Checked)
                    return;

                if (Organizasyon1.SelectedOrganizationId == -1 && chkUstBirimBazinda.Checked)
                {
                    temizleEkran();
                    return;
                }

                if (yildonem_basvuru.Donem != -1 && yildonem_basvuru.Yil != -1)
                {
                    pnl_genel.Enabled = true;
                }
                else
                {
                    pnl_genel.Enabled = false;
                }

                btn_kaydet.Enabled = true;


                OrganizasyonView ov = new OrganizasyonView();
                OrganizasyonProgram op = new OrganizasyonProgram();

                if (!chkUstBirimBazinda.Checked)
                {
                    // BursluUcretli bazında gidicez, panel gorunurse program turunede gore 
                    op = OgrenciMaster.Database.LoadEntityByID<OrganizasyonProgram>(organizasyon_.SelectedProgramId);
                    ov = OgrenciMaster.Database.LoadEntityByID<OrganizasyonView>(organizasyon_.SelectedOrganizationId);
                    if (SistemParamUcretDurumu != null && SistemParamUcretDurumu.BooDeger1.HasValue && SistemParamUcretDurumu.BooDeger1 == (bool)true)
                    {
                        string s = string.Format(@"SELECT *
                                                   FROM ens.BasvuruProgram
                                                   WHERE OrganizasyonProgramID = {0} AND OgretimYili = {1} AND OgretimDonemi = {2} ",
                                                 organizasyon_.SelectedProgramId, BasvuruYili, BasvuruDonem);

                        if (kodcomboUcretDurumu.SelectedKodId != -1)
                            s += " AND BursluUcretli = " + kodcomboUcretDurumu.SelectedKodId.ToString() + " ";

                        s += " ORDER BY BasvuruProgramID DESC ";

                        CurrentBasvuruProgram = OgrenciMaster.Database.SelectSingle<BasvuruProgram>(s);
                    }
                    else
                         CurrentBasvuruProgram = OgrenciUygulama.getirBasvuruProgram(organizasyon_.SelectedProgramId, op.ProgramTuru.Kodid, BasvuruYili, BasvuruDonem);
                }
                else
                {
                    ov = OgrenciMaster.Database.LoadEntityByID<OrganizasyonView>(Organizasyon1.SelectedOrganizationId);
                    if (SistemParamUcretDurumu != null && SistemParamUcretDurumu.BooDeger1.HasValue && SistemParamUcretDurumu.BooDeger1 == (bool)true)
                    {
                        string sql = string.Format(@"SELECT *
                                                     FROM ens.BasvuruProgram
                                                     WHERE OrganizasyonID = {0} AND OgretimYili = {1} AND OgretimDonemi = {2}", Convert.ToInt32(ov.UstBirimID), BasvuruYili, BasvuruDonem);

                        if (cmbProgramTuru.SelectedValue != "-1")
                            sql += " AND OgrenciTuru = " + cmbProgramTuru.SelectedValue.ToString() + " ";

                        if (kodcomboUcretDurumu.SelectedKodId != -1)
                            sql += " AND BursluUcretli = " + kodcomboUcretDurumu.SelectedKodId.ToString() + " ";

                        sql += " ORDER BY BasvuruProgramID DESC ";
                        CurrentBasvuruProgram = OgrenciMaster.Database.SelectSingle<BasvuruProgram>(sql);
                    }
                    else
                    {
                        if (cmbProgramTuru.SelectedValue == "-1")
                            CurrentBasvuruProgram = OgrenciMaster.Database.SelectSingle<BasvuruProgram>(@"SELECT *
                                                                                                          FROM ens.BasvuruProgram
                                                                                                          WHERE OrganizasyonID = ? AND OgretimYili = ? AND OgretimDonemi = ?
                                                                                                          ORDER BY BasvuruProgramID DESC", Convert.ToInt32(ov.UstBirimID),
                                                                                                        BasvuruYili, BasvuruDonem);
                        else
                            CurrentBasvuruProgram = OgrenciUygulama.getirBasvuruProgram(Convert.ToInt32(ov.UstBirimID), cmbProgramTuru.SelectedValue, BasvuruYili, BasvuruDonem, " ");
                    }
                }

                if (CurrentBasvuruProgram == null && !chkUstBirimBazinda.Checked)
                {

                    BasvuruProgram ustBirimProgram = OgrenciUygulama.getirBasvuruProgram(Convert.ToInt32(ov.UstBirimID), op.ProgramTuru.Kodid.ToString(), BasvuruYili, BasvuruDonem, " ");

                    //Simdi ustBirimProgram geldi ben onu currente eşlicem ama biraz modifiye edicem boylece kaydederken o organizasyonPrograma gore etmesi expected bro.
                    if (ustBirimProgram != null) // Kayit Geldi burda bana
                    {
                        doldurEkran(ustBirimProgram);
                        return;
                        //CurrentBasvuruProgram = ustBirimProgram;
                        //CurrentBasvuruProgram.Organizasyon = null;
                        //CurrentBasvuruProgram.OrganizasyonProgram = OgrenciMaster.Database.LoadEntityByID<OrganizasyonProgram>(organizasyon_.SelectedProgramId);
                        //CurrentBasvuruProgram.BasvuruProgramid = -1; // btn Kaydette yeni bir tane olusturcan basvuru program nesnesi ondan boyle yapıyoree . sadece ustbirimdeki goruntu olsn die
                        //Bunun yapmamın nedeni ise Bu alt Birim bazında kayıt . eGer kaydı yoksa ust birim kaydını cekicek ve onu kaydetm
                    }
                }
                if (!chkUstBirimBazinda.Checked)
                {
                    lblDisiplinTuruInfo.Visible = true;
                    lblDisiplinTuruInfo.Text = string.Format("Seçilen Program `{0}`dir", OgrenciUygulama.ProgramDisiplinBilgileriGetir(organizasyon_.SelectedProgramId));
                }
                else
                {
                    lblDisiplinTuruInfo.Visible = false;
                }


                //WorkItem 177813 Start
                if (CurrentBasvuruProgram != null)
                {

                    var belgeler = OgrenciUygulama.ProgramaGoreBelgeleriGetir(CurrentBasvuruProgram.BasvuruProgramid);
                    BasvuruProgramBelgeList = new List<BasvuruProgramBelge>();
                    BasvuruProgramBelgeList.AddRange(belgeler);



                    foreach (var row in gridBelgeler.Items)
                    {
                        var myRow = row as GridDataItem;
                        var kodId = myRow["KodID"].Text;
                        if (BasvuruProgramBelgeList.Count() > 0)
                        {
                            var currentBelgeData = BasvuruProgramBelgeList.Where(x => x.BelgeTuru == Convert.ToInt32(kodId)).FirstOrDefault();
                            if (currentBelgeData != null)
                            {
                                (myRow.FindControl("chkTCZorunlu") as CheckBox).Checked = currentBelgeData.ZorunluMu;
                                (myRow.FindControl("chkYUZorunlu") as CheckBox).Checked = currentBelgeData.YabancidaZorunlu;
                                myRow["Adim"].Text = currentBelgeData.Adim.ToString();
                            }

                        }

                    }

                    //if(BasvuruKontrolKriterList == null){
                    BasvuruKontrolKriterList = OgrenciUygulama.ProgramaGoreBasvuruKontrolListeleriGetir(CurrentBasvuruProgram.BasvuruProgramid);
                    //}
                    foreach (var item in BasvuruKontrolKriterList)
                    {
                        chkMinYsMezNot4.Checked = item.MinimumYuksekLisansMezuniyetNotu4sistem;
                        chkMinYsMezNot100.Checked = item.MinimumYuksekLisansMezuniyetNotu100sistem;
                        chkMinMezNot4.Checked = item.MinimumMezuniyetNotu4sistem;
                        chkMinMezNot100.Checked = item.MinimumMezuniyetNotu100sistem;
                    }

                    //if(ProgramDisiplinBasvuruList == null){
                    ProgramDisiplinBasvuruList = OgrenciUygulama.ProgramaGoreDisiplinBilgileriGetir(CurrentBasvuruProgram.BasvuruProgramid);
                    //}
                    //Info
                    //if (CurrentBasvuruProgram != null && CurrentBasvuruProgram.OrganizasyonProgram != null)
                    //    lblDisiplinTuruInfo.Text = string.Format("Seçilen Program `{0}`dir", OgrenciUygulama.ProgramDisiplinBilgileriGetir(CurrentBasvuruProgram.OrganizasyonProgram.OrganizasyonProgramid));
                    foreach (var item in ProgramDisiplinBasvuruList)
                    {
                        int index = cmbDisiplinTuru.FindItemIndexByValue(item.BasvurulabilecekDisiplinKodID.ToString());
                        cmbDisiplinTuru.SelectedIndex = index;
                        index = cmbBasvurulabilecekProgramTuru.FindItemIndexByValue(item.BasvurulabilecekProgramTuruID.ToString());
                        cmbBasvurulabilecekProgramTuru.SelectedIndex = index;
                        txtBasvurulabilecekProgramTuruSayisi.Text = item.MaxBasvuruProgramTuruSayisi.ToString();
                        txtEnFazlaBasvuruSayisi.Text = item.MaxBasvuruDisiplinSayisi.ToString();
                    }
                }
                //WorkItem 177813 End
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
                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                    {
                        txt_milli_sporcu_alan_ici_kontenjan.Text = CurrentBasvuruProgram.MilliSporcuAlanIciKontenjan.ToString();
                        txt_milli_sporcu_alan_disi_kontenjan.Text = CurrentBasvuruProgram.MilliSporcuAlanDisiKontenjan.ToString();
                    }
                    txt_min_basari_notu.Text =
                    txt_min_mezuniyet4.Text = CurrentBasvuruProgram.MinimumMezuniyetNotu4sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumMezuniyetNotu4sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_mezuniyet100.Text = CurrentBasvuruProgram.MinimumMezuniyetNotu100sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumMezuniyetNotu100sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_yl_mezuniyet4.Text = CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu4sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu4sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_yl_mezuniyet100.Text = CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu100sistem != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumYuksekLisansMezuniyetNotu100sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_basari_notu.Text = CurrentBasvuruProgram.MinimumBasariNotu != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumBasariNotu.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_mulakat_notu.Text = CurrentBasvuruProgram.MinimumMulakatNotu != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumMulakatNotu.GetValueOrDefault()).ToString().Replace(',', '.') : "0";
                    txt_min_bilimsel_notu.Text = CurrentBasvuruProgram.MinimumBilimNotu != null ? Convert.ToDouble(CurrentBasvuruProgram.MinimumBilimNotu.GetValueOrDefault()).ToString().Replace(',', '.') : "0";
                    txt_mulakat_yeri.Text = CurrentBasvuruProgram.MulakatYeri;
                    txt_ekbilgi.Text = CurrentBasvuruProgram.EkBilgi;
                    date_mulakat_tarihi.SelectedDate = CurrentBasvuruProgram.MulakatTarihi;
                    txt_bilim_sinavi.Text = CurrentBasvuruProgram.BilimSinaviYeri;
                    txtNumYabanciOgrenciDilSecimSayisi.Text = CurrentBasvuruProgram.YabanciUyrukluOgrenciDilSecimSayisi != null ? CurrentBasvuruProgram.YabanciUyrukluOgrenciDilSecimSayisi.GetValueOrDefault().ToString() : "0"; ;
                    date_bilim_sinavi_tarihi.SelectedDate = CurrentBasvuruProgram.BilimSinaviTarihi;
                    ara_hoca_mulakat.Value = "";
                    lbl_hoca_mulakat.Text = "";
                    cmb_mulakat_hoca_durum.SelectedKodId = -1;
                    JuriUyeleriMulakat = OgrenciUygulama.getirMulakatJuriUyeleri(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem);
                    grd_mulakat_hoca.Rebind();

                    if (UnipaMaster.Musteri.Id == (int)Universiteler.TobbETU || UnipaMaster.Musteri.Id == (int)Universiteler.TobbETUTest)
                        GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlariTobb(CurrentBasvuruProgram.BasvuruProgramid);
                    else
                        GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlari(CurrentBasvuruProgram.BasvuruProgramid);

                    //if (ilkmi)
                    grd_sinav_notlari.Rebind();
                    DegerlendirmeKriterleri = OgrenciUygulama.getirDegerlendirmeKrierleri(CurrentBasvuruProgram.BasvuruProgramid);
                    grd_degerlendirme_kriterleri.Rebind();
                    temizleAlsPuanTuru();
                    doldurAlsPuanTuru();
                    chkAlesGozukmesin.Checked = CurrentBasvuruProgram.GirisSinaviKontrolEtme;
                    //chkYabanciDilKontrolu.Checked = CurrentBasvuruProgram.YabanciDilSinaviKontrolEtme;
                    chkMulakatYok.Checked = CurrentBasvuruProgram.MulakatYok;
                    chkBasvurudaBarajNotlariGozuksun.Checked = (CurrentBasvuruProgram.BasvurudaBarajKontrolu != null) ? CurrentBasvuruProgram.BasvurudaBarajKontrolu : false;

                    if (CurrentBasvuruProgram.BursluUcretli != null && SistemParamUcretDurumu != null && SistemParamUcretDurumu.BooDeger1.HasValue &&
                        SistemParamUcretDurumu.BooDeger1 == (bool)true)
                        kodcomboUcretDurumu.SelectedKod = CurrentBasvuruProgram.BursluUcretli;

                    var cmd = OgrenciMaster.Database.GetSqlStringCommand(@"SELECT *
                                                                           FROM ens.BasvuruProgramBelge
                                                                           WHERE BasvuruProgramID = @basvuruProgramId");
                    OgrenciMaster.Database.AddInParameter(cmd, "@basvuruProgramId", DbType.Int32, CurrentBasvuruProgram.BasvuruProgramid);
                    var dt = OgrenciMaster.Database.ExecuteDatatable(cmd);
                    //chkBelgeDiploma.Checked = chkTranskript.Checked = chkAles.Checked = chkYabanciDilBelgesi.Checked = chkPasaport.Checked = chkAskerlik.Checked = false;

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    switch (dt.Rows[i]["BelgeTuru"].ToString())
                    //    {
                    //        case "163001": chkBelgeDiploma.Checked = true; break;
                    //        case "163002": chkTranskript.Checked = true; break;
                    //        case "163003": chkAles.Checked = true; break;
                    //        case "163004": chkYabanciDilBelgesi.Checked = true; break;
                    //        case "163005": chkPasaport.Checked = true; break;
                    //        case "163006": chkAskerlik.Checked = true; break;
                    //    }
                    //}


                }
                else
                {
                    RadTabStrip1.Enabled = false;
                    RadMultiPage1.Enabled = false;
                    //İstanbul Kemerburgaz Üniversitesi fotograf yukleme alanı, secili ve disable özelligi 
                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Kemerburgaz ||
                        OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.KemerburgazTest)
                    {
                        ChkResimZorunlu.Checked = true;
                        ChkResimZorunlu.Enabled = false;

                    }
                    else
                    {
                        ChkResimZorunlu.Checked = false;
                        ChkResimZorunlu.Enabled = true;
                    }
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
                    txt_min_bilimsel_notu.Value = 0;
                    txt_ekbilgi.Text = "";
                    txt_mulakat_yeri.Text = "";
                    date_mulakat_tarihi.SelectedDate = null;
                    txt_bilim_sinavi.Text = "";
                    date_bilim_sinavi_tarihi.SelectedDate = null;
                    ara_hoca_mulakat.Value = "";
                    lbl_hoca_mulakat.Text = "";
                    cmb_mulakat_hoca_durum.SelectedKodId = -1;
                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                    {
                        txt_milli_sporcu_alan_ici_kontenjan.Value = 0;
                        txt_milli_sporcu_alan_disi_kontenjan.Value = 0;
                    }
                    JuriUyeleriMulakat = OgrenciUygulama.getirJuriUyeleriNull();
                    grd_mulakat_hoca.Rebind();
                    GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlariNulll();
                    grd_sinav_notlari.Rebind();
                    DegerlendirmeKriterleri = OgrenciUygulama.getirPuanBarajlariNull();
                    grd_degerlendirme_kriterleri.Rebind();
                    temizleAlsPuanTuru();

                    if ((OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.Ege || OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.EgeTest) && ChkTurkYabanciOrtak.Checked)
                    {
                        txt_yabanci_kontenjan.Enabled = false;
                        txt_yabanci_kontenjan_alan_disi.Enabled = false;
                        txt_yedek_kontenjan_yabanci_alan_disi.Enabled = false;
                        txt_yedek_kontenjan_yabanci.Enabled = false;
                    }
                }
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.EkranHatasi, 1081045, SourceLevels.Error);
            }
        }

        private void doldurEkran(BasvuruProgram bp)
        {
            try
            {

                if (organizasyon_.SelectedProgramId == -1 && !chkUstBirimBazinda.Checked)
                    return;

                if (yildonem_basvuru.Donem != -1 && yildonem_basvuru.Yil != -1)
                {
                    pnl_genel.Enabled = true;
                }
                else
                {
                    pnl_genel.Enabled = false;
                }

                btn_kaydet.Enabled = true;


                if (bp != null && bp.BasvuruProgramid > 0)
                {
                    RadTabStrip1.Enabled = true;
                    RadMultiPage1.Enabled = true;
                    ChkMezunOlmayaniAl.Checked = bp.MezunOlmayaniAl;
                    ChkResimZorunlu.Checked = bp.ResimZorunlu;
                    date_baslama_tarihi.SelectedDate = bp.BaslamaTarihi;
                    date_obaslama_tarihi.SelectedDate = bp.OnayBaslamaTarihi;
                    date_bitis_tarihi.SelectedDate = bp.BitisTarihi;
                    date_obitis_tarihi.SelectedDate = bp.OnayBitisTarihi;
                    txt_kontenjan.Text = bp.Kontenjan.ToString();
                    txt_yabanci_kontenjan.Text = bp.YabanciKontenjan.ToString();
                    txt_turk_kontenjan.Text = bp.TurkKontenjan.ToString();
                    txt_yedek_kontenjan.Text = bp.YedekKontenjan.ToString();
                    txt_yedek_kontenjan_yabanci.Text = bp.YedekYabanciKontenjan.ToString();
                    txt_yedek_kontenjan_turk.Text = bp.TurkYedekKontenjan.ToString();
                    txt_turk_kontenjan_alan_disi.Text = bp.TurkKontenjanAlanDisi.ToString();
                    txt_yabanci_kontenjan_alan_disi.Text = bp.YabanciKontenjanAlanDisi.ToString();
                    txt_yedek_kontenjan_yabanci_alan_disi.Text = bp.YedekYabanciKontenjanAlanDisi.ToString();
                    txt_yedek_kontenjan_turk_alan_disi.Text = bp.TurkYedekKontenjanAlanDisi.ToString();
                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                    {
                        txt_milli_sporcu_alan_ici_kontenjan.Text = bp.MilliSporcuAlanIciKontenjan.ToString();
                        txt_milli_sporcu_alan_disi_kontenjan.Text = bp.MilliSporcuAlanDisiKontenjan.ToString();
                    }
                    txt_arastirma_gorevli_kontenjan.Text = bp.ArastirmaGorevlisiKontenjan.ToString();
                    txt_min_mezuniyet4.Text = bp.MinimumMezuniyetNotu4sistem != null ? Convert.ToDouble(bp.MinimumMezuniyetNotu4sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_mezuniyet100.Text = bp.MinimumMezuniyetNotu100sistem != null ? Convert.ToDouble(bp.MinimumMezuniyetNotu100sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_yl_mezuniyet4.Text = bp.MinimumYuksekLisansMezuniyetNotu4sistem != null ?
                                                 Convert.ToDouble(bp.MinimumYuksekLisansMezuniyetNotu4sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_yl_mezuniyet100.Text = bp.MinimumYuksekLisansMezuniyetNotu100sistem != null ?
                                                   Convert.ToDouble(bp.MinimumYuksekLisansMezuniyetNotu100sistem.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_basari_notu.Text = bp.MinimumBasariNotu != null ? Convert.ToDouble(bp.MinimumBasariNotu.ToString()).ToString().Replace(',', '.') : "0";
                    txt_min_mulakat_notu.Text = bp.MinimumMulakatNotu != null ? Convert.ToDouble(bp.MinimumMulakatNotu.GetValueOrDefault()).ToString().Replace(',', '.') : "0";
                    txt_mulakat_yeri.Text = bp.MulakatYeri;
                    txt_ekbilgi.Text = bp.EkBilgi;
                    date_mulakat_tarihi.SelectedDate = bp.MulakatTarihi;
                    txt_bilim_sinavi.Text = bp.BilimSinaviYeri;
                    txtNumYabanciOgrenciDilSecimSayisi.Text = bp.YabanciUyrukluOgrenciDilSecimSayisi != null ? bp.YabanciUyrukluOgrenciDilSecimSayisi.GetValueOrDefault().ToString() : "0"; ;
                    date_bilim_sinavi_tarihi.SelectedDate = bp.BilimSinaviTarihi;
                    ara_hoca_mulakat.Value = "";
                    lbl_hoca_mulakat.Text = "";
                    cmb_mulakat_hoca_durum.SelectedKodId = -1;
                    JuriUyeleriMulakat = OgrenciUygulama.getirMulakatJuriUyeleri(bp.BasvuruProgramid, BasvuruYili, BasvuruDonem);
                    grd_mulakat_hoca.Rebind();

                    if (UnipaMaster.Musteri.Id == (int)Universiteler.TobbETU || UnipaMaster.Musteri.Id == (int)Universiteler.TobbETUTest)
                        GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlariTobb(bp.BasvuruProgramid);
                    else
                        GirisSinavNotlari = OgrenciUygulama.getirPuanBarajlari(bp.BasvuruProgramid);

                    grd_sinav_notlari.Rebind();
                    DegerlendirmeKriterleri = OgrenciUygulama.getirDegerlendirmeKrierleri(bp.BasvuruProgramid);
                    grd_degerlendirme_kriterleri.Rebind();
                    temizleAlsPuanTuru();
                    doldurAlsPuanTuru();
                    chkAlesGozukmesin.Checked = bp.GirisSinaviKontrolEtme;
                    //chkYabanciDilKontrolu.Checked = bp.YabanciDilSinaviKontrolEtme;

                    if (BasvuruProgramBelgeList != null && BasvuruProgramBelgeList.Count == 0)
                    {

                    }
                }
                else
                {
                    RadTabStrip1.Enabled = false;
                    RadMultiPage1.Enabled = false;
                    //İstanbul Kemerburgaz Üniversitesi fotograf yukleme alanı, secili ve disable özelligi 
                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Kemerburgaz ||
                        OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.KemerburgazTest)
                    {
                        ChkResimZorunlu.Checked = true;
                        ChkResimZorunlu.Enabled = false;
                        //ChkResimZorunlu.Enabled;
                    }
                    else
                    {
                        ChkResimZorunlu.Checked = false;
                        ChkResimZorunlu.Enabled = true;
                    }
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
                    if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
                    OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
                    {
                        txt_milli_sporcu_alan_ici_kontenjan.Value = 0;
                        txt_milli_sporcu_alan_disi_kontenjan.Value = 0;
                    }
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
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.EkranHatasi, 1081045, SourceLevels.Error);
            }
        }

        private void JuriUyeleriDoldur()
        {
            try
            {
                ara_hoca_mulakat.Value = "";
                lbl_hoca_mulakat.Text = "";
                cmb_mulakat_hoca_durum.SelectedKodId = -1;

                if (CurrentBasvuruProgram != null && CurrentBasvuruProgram.BasvuruProgramid > 0)
                    JuriUyeleriMulakat = OgrenciUygulama.getirMulakatJuriUyeleri(CurrentBasvuruProgram.BasvuruProgramid, BasvuruYili, BasvuruDonem);
                else
                    JuriUyeleriMulakat = OgrenciUygulama.getirJuriUyeleriNull();

                grd_mulakat_hoca.Rebind();
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.JuriHata, 1081046, SourceLevels.Error);
            }
        }

        private void doldurAlsPuanTuru()
        {
            try
            {
                if (CurrentBasvuruProgram != null)
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


            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.AlesPuanHata, 1081047, SourceLevels.Error);
            }
        }

        private bool kaydetAlsPuanTuru(BasvuruProgram basvuruProgram)
        {
            try
            {
                bool sonuc = false;
                ProgramPuanTuru _temp;

                if (chk_sozel.Checked)//kaydet
                {
                    _temp = OgrenciUygulama.getirProgramPuanTuru(basvuruProgram.BasvuruProgramid, 942001);

                    if (_temp == null || _temp.ProgramPuanTuruid < 1)
                    {
                        _temp = new ProgramPuanTuru();
                        _temp.BasvuruProgram = basvuruProgram;
                        // _temp.ProgramPuanTuruid = 942001;
                        _temp.Kod = OgrenciUygulama.getirkod(942001);
                        sonuc = OgrenciUygulama.kaydetProgramPuanTuru(_temp);
                    }
                }
                else//sil
                {
                    _temp = OgrenciUygulama.getirProgramPuanTuru(basvuruProgram.BasvuruProgramid, 942001);

                    if (_temp != null && _temp.ProgramPuanTuruid > 0)
                        sonuc = OgrenciUygulama.silProgramPuanTuru(_temp);
                }
                if (chk_sayisal.Checked)//kaydet
                {
                    _temp = OgrenciUygulama.getirProgramPuanTuru(basvuruProgram.BasvuruProgramid, 942002);

                    if (_temp == null || _temp.ProgramPuanTuruid < 1)
                    {
                        _temp = new ProgramPuanTuru();
                        _temp.BasvuruProgram = basvuruProgram;
                        _temp.Kod = OgrenciUygulama.getirkod(942002);
                        sonuc = OgrenciUygulama.kaydetProgramPuanTuru(_temp);
                    }
                }
                else//sil
                {
                    _temp = OgrenciUygulama.getirProgramPuanTuru(basvuruProgram.BasvuruProgramid, 942002);

                    if (_temp != null && _temp.ProgramPuanTuruid > 0)
                        sonuc = OgrenciUygulama.silProgramPuanTuru(_temp);
                }
                if (chk_ea.Checked)//kaydet
                {
                    _temp = OgrenciUygulama.getirProgramPuanTuru(basvuruProgram.BasvuruProgramid, 942003);

                    if (_temp == null || _temp.ProgramPuanTuruid < 1)
                    {
                        _temp = new ProgramPuanTuru();
                        _temp.BasvuruProgram = basvuruProgram;
                        _temp.Kod = OgrenciUygulama.getirkod(942003);
                        sonuc = OgrenciUygulama.kaydetProgramPuanTuru(_temp);
                    }
                }
                else//sil
                {
                    _temp = OgrenciUygulama.getirProgramPuanTuru(basvuruProgram.BasvuruProgramid, 942003);

                    if (_temp != null && _temp.ProgramPuanTuruid > 0)
                        sonuc = OgrenciUygulama.silProgramPuanTuru(_temp);
                }

                return sonuc;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterHatasi, 1081051, SourceLevels.Critical);
            }
        }

        private bool kaydetAlsPuanTuru()
        {
            try
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
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterHatasi, 1081051, SourceLevels.Critical);
            }
        }

        private void temizleAlsPuanTuru()
        {
            chk_ea.Checked = false;
            chk_sayisal.Checked = false;
            chk_sozel.Checked = false;
        }

        public void BasvuruProgramSetEt(BasvuruProgram bProgram, bool ustBirimBazli, int mode)
        {
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

            int millisporcualanicikontenjan = 0;

            if (txt_milli_sporcu_alan_ici_kontenjan.Text != "")
                millisporcualanicikontenjan = Convert.ToInt32(txt_milli_sporcu_alan_ici_kontenjan.Text);

            int millisporcualandisikontenjan = 0;

            if (txt_milli_sporcu_alan_disi_kontenjan.Text != "")
                millisporcualandisikontenjan = Convert.ToInt32(txt_milli_sporcu_alan_disi_kontenjan.Text);

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

            decimal minbilimnotu = 0;

            if (txt_min_bilimsel_notu.Text != "")
                minbilimnotu = Convert.ToDecimal(txt_min_bilimsel_notu.Text.Replace('.', ','));

            if (txt_yedek_kontenjan.Text != "")
                bProgram.YedekKontenjan = Convert.ToInt32(txt_yedek_kontenjan.Value);

            if (txt_yedek_kontenjan_yabanci.Text != "")
                bProgram.YedekYabanciKontenjan = Convert.ToInt32(txt_yedek_kontenjan_yabanci.Value);

            kontenjan = yabancikontenjan + turkkontenjan + yabancikontenjanAlanDisi + turkkontenjanAlanDisi;
            yedekkontenjan = yedekyabancikontenjan + yedekturkkontenjan + yedekyabancikontenjanAlanDisi + yedekturkkontenjanAlanDisi;

            bool turkYabanciKontenjanOrtak = false;
            turkYabanciKontenjanOrtak = ChkTurkYabanciOrtak.Checked;



            //if (bProgram.DefaultDeger)
            //{
            //    bProgram = new BasvuruProgram();
            //}

            bProgram.ResimZorunlu = ChkResimZorunlu.Checked;
            bProgram.MezunOlmayaniAl = ChkMezunOlmayaniAl.Checked;
            bProgram.ArastirmaGorevlisiKontenjan = arastirmagorkon;
            bProgram.BaslamaTarihi = date_baslama_tarihi.SelectedDate;
            bProgram.OnayBaslamaTarihi = date_obaslama_tarihi.SelectedDate;
            bProgram.BitisTarihi = date_bitis_tarihi.SelectedDate;
            bProgram.OnayBitisTarihi = date_obitis_tarihi.SelectedDate;
            bProgram.Kontenjan = kontenjan;
            bProgram.YedekKontenjan = yedekkontenjan;
            bProgram.MilliSporcuAlanIciKontenjan = millisporcualanicikontenjan;
            bProgram.MilliSporcuAlanDisiKontenjan = millisporcualandisikontenjan;
            bProgram.YedekYabanciKontenjan = yedekyabancikontenjan;
            bProgram.TurkYedekKontenjan = yedekturkkontenjan;
            bProgram.MinimumMezuniyetNotu4sistem = minnot4;
            bProgram.MinimumMezuniyetNotu100sistem = minnot100;
            bProgram.MinimumYuksekLisansMezuniyetNotu4sistem = minylnot4;
            bProgram.MinimumYuksekLisansMezuniyetNotu100sistem = minylnot100;
            bProgram.MinimumBasariNotu = minbasarinotu;
            bProgram.MinimumMulakatNotu = minmulakatnotu;
            bProgram.MulakatTarihi = date_mulakat_tarihi.SelectedDate;
            bProgram.MulakatYeri = txt_mulakat_yeri.Text;
            bProgram.EkBilgi = txt_ekbilgi.Text;
            bProgram.MulakatYok = chkMulakatYok.Checked;
            bProgram.MinimumBilimNotu = minbilimnotu;
            bProgram.BasvurudaBarajKontrolu = chkBasvurudaBarajNotlariGozuksun.Checked;
            if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.Ege || OgrenciMaster.UnipaMaster.Musteri.Id == (int)Universiteler.EgeTest)
            {
                bProgram.TurkYabanciKontenjaniOrtak = turkYabanciKontenjanOrtak;
            }
            else
            {
                bProgram.TurkYabanciKontenjaniOrtak = false;
            }

            if (SistemParamUcretDurumu != null && SistemParamUcretDurumu.BooDeger1.HasValue && SistemParamUcretDurumu.BooDeger1 == (bool)true)
                bProgram.BursluUcretli = kodcomboUcretDurumu.SelectedKod;
            else bProgram.BursluUcretli = null;

            if (!ustBirimBazli)
            {
                OrganizasyonProgram op;

                if (mode == -1)
                    op = OgrenciUygulama.getorgprg(organizasyon_.SelectedOrganizasyonProgram.OrganizasyonProgramid);
                else
                    op = OgrenciUygulama.getorgprg(mode);

                bProgram.OrganizasyonProgram = op;
                bProgram.OgrenciTuru = op.ProgramTuru;
            }
            else
            {
                OrganizasyonView ov = OgrenciMaster.Database.LoadEntityByID<OrganizasyonView>(Organizasyon1.SelectedOrganizationId);
                bProgram.Organizasyon = OgrenciMaster.Database.LoadEntityByID<Organizasyon>(Convert.ToInt32(ov.UstBirimID));
                bProgram.OgrenciTuru = OgrenciMaster.Database.LoadEntityByID<Kod>(Convert.ToInt32(cmbProgramTuru.SelectedValue));
            }

            bProgram.BilimSinaviYeri = txt_bilim_sinavi.Text;
            bProgram.BilimSinaviTarihi = date_bilim_sinavi_tarihi.SelectedDate;
            bProgram.YabanciKontenjan = yabancikontenjan;
            bProgram.TurkKontenjan = turkkontenjan;
            // Alan dışı konenjanlar
            bProgram.TurkKontenjanAlanDisi = turkkontenjanAlanDisi;
            bProgram.YabanciKontenjanAlanDisi = yabancikontenjanAlanDisi;
            bProgram.TurkYedekKontenjanAlanDisi = yedekturkkontenjanAlanDisi;
            bProgram.YedekYabanciKontenjanAlanDisi = yedekyabancikontenjanAlanDisi;
            bProgram.YabanciDilSinaviYeri = "";
            bProgram.YabanciDilSinaviTarihi = null;
            bProgram.TurkceDilSinaviYeri = "";
            bProgram.TurkceDilSinaviTarihi = null;
            bProgram.OgretimYili = BasvuruYili;
            bProgram.YabanciUyrukluOgrenciDilSecimSayisi = int.Parse(txtNumYabanciOgrenciDilSecimSayisi.Text);
            bProgram.OgretimDonemi = OgrenciUygulama.getirkod(BasvuruDonem);

            if (chkAlesGozukmesin.Checked)
                bProgram.GirisSinaviKontrolEtme = true;
            else
                bProgram.GirisSinaviKontrolEtme = false;

            //kolon adı yanlış true ise kontrol etmektedir.
            //if (chkYabanciDilKontrolu.Checked)
            //    bProgram.YabanciDilSinaviKontrolEtme = true;
            //else
            //    bProgram.YabanciDilSinaviKontrolEtme = false;
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                bool sonuc = false;



                #region Kontroller
                if (!chk_sayisal.Checked && !chk_ea.Checked && !chk_sozel.Checked)
                {
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.AlesPuanTuruSeciniz);
                    return;
                }

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

                if (chkUstBirimBazinda.Checked && cmbProgramTuru.SelectedValue == "-1" && (OgrenciMaster.UnipaMaster.Musteri.Id == 1 || OgrenciMaster.UnipaMaster.Musteri.Id == 101))
                {
                    ltlInfo.Text = base.HataGoster("Program Türü Seçiniz");
                    return;
                }

                if (date_baslama_tarihi.SelectedDate == null || date_bitis_tarihi.SelectedDate == null)
                {
                    ltlInfo.Text = base.HataGoster("Başvuru Başlama veya Bitiş tarihi girmek zorundasınız.");
                    return;
                }

                if (SistemParamUcretDurumu != null && SistemParamUcretDurumu.BooDeger1.HasValue && SistemParamUcretDurumu.BooDeger1 == (bool)true && kodcomboUcretDurumu.SelectedKodId == -1)
                {
                    ltlInfo.Text = base.HataGoster("Lütfen Ücret Durumu Bilgisini seçiniz...");
                    return;
                }
                #endregion

                if (CurrentBasvuruProgram == null || CurrentBasvuruProgram.BasvuruProgramid < 1)
                    CurrentBasvuruProgram = new BasvuruProgram();

                BasvuruProgramSetEt(CurrentBasvuruProgram, chkUstBirimBazinda.Checked, -1);
                altBirimListesi = null;
                altBirimListesi = new List<OrganizasyonProgram>();
                currentNumber = 0;
                //Zorunlu belgelerin kayit edilmesi.



                //if (SistemParamBelgeKontrolu != null && SistemParamBelgeKontrolu.BooDeger1 != null && SistemParamBelgeKontrolu.BooDeger1.Value && (SistemParamBelgeKontrolu.StrDeger1 != null || SistemParamBelgeKontrolu.StrDeger1 != string.Empty) && KodGrupBelgeKontrolu != null)
                //{
                //    foreach (var item in gridBelgeler.Items)
                //    {
                //        var thisItem = item;
                //    }
                //}

                /*Gelen kaydın akademiktakvim ve akademiktakvimaktivite tablolarına kayıtları atılır...*/
                if (!chkUstBirimBazinda.Checked)
                    AkademikTakvimKaydiOlustur(CurrentBasvuruProgram.OrganizasyonProgram.OrganizasyonProgramid, yildonem_basvuru.Yil,
                                               yildonem_basvuru.Donem, (DateTime)date_baslama_tarihi.SelectedDate, (DateTime)date_bitis_tarihi.SelectedDate);

                if (CurrentBasvuruProgram.BasvuruProgramid > 0)//update
                    sonuc = OgrenciUygulama.guncelleBasvuruProgram(CurrentBasvuruProgram);
                else//kaydet
                    sonuc = OgrenciUygulama.kaydetBasvuruProgram(CurrentBasvuruProgram);

                StringBuilder hataMesajı = new StringBuilder();

                if (sonuc)
                {
                    //Gecmis verileri siliyoruz.
                    OgrenciUygulama.EskiBelgeSil(CurrentBasvuruProgram.BasvuruProgramid);
                    OgrenciUygulama.EskiBasvuruKontrolListeleriSil(CurrentBasvuruProgram.BasvuruProgramid);
                    OgrenciUygulama.EskiDisiplinBilgileriSil(CurrentBasvuruProgram.BasvuruProgramid);
                    //Yeni isaretlenen belgeler.
                    foreach (GridDataItem item in gridBelgeler.MasterTableView.Items)
                    {
                        if (CurrentBasvuruProgram.BasvuruProgramid > 0)
                        {
                            //Belgeler
                            var kodId = item.GetDataKeyValue("KodID").ToString();
                            var belgeEklemeModel = new BelgeKaydetModel();
                            belgeEklemeModel.BasvuruProgramID = CurrentBasvuruProgram.BasvuruProgramid;
                            belgeEklemeModel.BelgeTipi = kodId.ToInt().Value;
                            belgeEklemeModel.TcZorunlu = (item.FindControl("chkTCZorunlu") as CheckBox).Checked;
                            belgeEklemeModel.YuZorunlu = (item.FindControl("chkYUZorunlu") as CheckBox).Checked;
                            if (!String.IsNullOrEmpty(item["Adim"].Text) && item["Adim"].Text != "&nbsp;")
                            {
                                belgeEklemeModel.Adim = Convert.ToInt32((item["Adim"].Text));

                            }
                            OgrenciUygulama.BelgeleriKaydet(belgeEklemeModel);
                        }
                    }


                    //Disiplin Listesi
                    var disiplinListesiEklemeModel = new DisiplinKaydetModel()
                    {
                        MaxBasvuruProgramTuruSayisi = txtBasvurulabilecekProgramTuruSayisi.Text.ToInt(),
                        MaxBasvuruDisiplinSayisi = txtEnFazlaBasvuruSayisi.Text.ToInt(),
                        BasvuruProgramID = CurrentBasvuruProgram.BasvuruProgramid,
                        BasvurulabilecekProgramTuruID = cmbBasvurulabilecekProgramTuru.SelectedValue.ToInt(),
                        BasvurulabilecekDisiplinKodID = cmbDisiplinTuru.SelectedValue.ToInt(),
                    };

                    OgrenciUygulama.DisiplinBilgileriKaydet(disiplinListesiEklemeModel);


                    //Basvuruda Kontrol Listesi
                    var basvurudaKontrolEklemeModel = new BasvuruKontrolKaydetModel()
                    {
                        BasvuruProgramID = CurrentBasvuruProgram.BasvuruProgramid,
                        MinimumMezuniyetNotu100sistem = chkMinMezNot100.Checked,
                        MinimumMezuniyetNotu4sistem = chkMinMezNot4.Checked,
                        MinimumYuksekLisansMezuniyetNotu100sistem = chkMinYsMezNot100.Checked,
                        MinimumYuksekLisansMezuniyetNotu4sistem = chkMinYsMezNot4.Checked,
                    };

                    OgrenciUygulama.BasvuruKontrolListeleriKaydet(basvurudaKontrolEklemeModel);

                    sonuc = kaydetGirisSinavNotlari();
                    kaydetDegerlendirmeKriterleri();
                    kaydetAlsPuanTuru();

                    if (!chkUstBirimBazinda.Checked)
                    {
                        ltlInfo.Text = base.BilgiGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.kayittamam);
                        acEkran();
                    }

                    //Cok Vakit alıyor Hemşerim ondan gereksiz gorduk....
                    if (chkUstBirimBazinda.Checked)
                    {
                        //Burda Tum AltBirimler için save - update yapıcam
                        string altBirimSql = string.Format(@" SELECT *
                                                              FROM OrganizasyonProgram
                                                              WHERE OrganizasyonID IN (SELECT OrganizasyonID
                                                                                       FROM Organizasyon
                                                                                       WHERE UstBirim = {0} AND Birim <> 0) ", CurrentBasvuruProgram.Organizasyon.UstBirim);
                        string eklenti = "";

                        if (cmbProgramTuru.SelectedValue != "-1")
                            eklenti = string.Format(" and ProgramTuru={0} ", cmbProgramTuru.SelectedValue);

                        altBirimSql = altBirimSql + eklenti;
                        altBirimListesi = OgrenciMaster.Database.Select<OrganizasyonProgram>(altBirimSql).ToList();

                        //foreach (OrganizasyonProgram item in orgPrgList)
                        //{
                        //    Burda yapıcam işlemleri işte..Burasının Threadle Çalısması lasım ....
                        //    BasvuruProgramKaydıYap(item, hataMesajı);
                        //}
                    }
                }
                else
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.kayithatali);

                #region eski
                //if (sonuc)
                //{
                //    # region BasvuruAlim
                //    // Basvuru Alimi tamamen kaldiricaz artık ondan kayıt atmasına gerek yok.
                //    //BasvuruAlim BA = OgrenciUygulama.GetBasvuruAlim(OgrenciMaster.AktifYilDonem.Yil, OgrenciMaster.AktifYilDonem.Donem.Kodid);
                //    //if (BA == null)
                //    //{
                //    //    BA = new BasvuruAlim();
                //    //    BA.BasvuruBaslangicTarihi = date_baslama_tarihi.SelectedDate.Value;
                //    //    BA.BasvuruBitisTarihi = date_bitis_tarihi.SelectedDate.Value;
                //    //}
                //    //BA.BasvuruAlimTuru = OgrenciMaster.KodGetir(802001);//lisansüstü başvuru
                //    //if (BA.BasvuruBaslangicTarihi == null)
                //    //{
                //    //    BA.BasvuruBaslangicTarihi = date_baslama_tarihi.SelectedDate.Value;
                //    //}
                //    //else if (BA.BasvuruBaslangicTarihi != null && date_baslama_tarihi.SelectedDate.Value < BA.BasvuruBaslangicTarihi)
                //    //{
                //    //    BA.BasvuruBaslangicTarihi = date_baslama_tarihi.SelectedDate.Value;
                //    //}
                //    //if (BA.BasvuruBitisTarihi == null)
                //    //{
                //    //    BA.BasvuruBitisTarihi = date_bitis_tarihi.SelectedDate.Value;
                //    //}
                //    //else if (BA.BasvuruBitisTarihi != null && date_bitis_tarihi.SelectedDate.Value > BA.BasvuruBitisTarihi)
                //    //{
                //    //    BA.BasvuruBitisTarihi = date_bitis_tarihi.SelectedDate.Value;
                //    //}
                //    //BA.HedefDonem = OgrenciMaster.KodGetir(yildonem_basvuru.Donem);
                //    //BA.HedefYil = yildonem_basvuru.Yil;
                //    //BA.OgretimDonemi = OgrenciMaster.AktifYilDonem.Donem;
                //    //BA.OgretimYili = OgrenciMaster.AktifYilDonem.Yil;
                //    //OgrenciMaster.Database.SaveOrUpdate(BA);
                //    #endregion
                //}
                #endregion eski

                if (OgrenciMaster.UnipaMaster.Musteri.Id == 1 || OgrenciMaster.UnipaMaster.Musteri.Id == 101)
                {
                    if (hataMesajı.ToString() != "")
                        ltlInfo.Text += hataMesajı.ToString();
                }

                if (chkUstBirimBazinda.Checked && altBirimListesi.Count > 0 && SistemParamUstBirimKontrolu != null && SistemParamUstBirimKontrolu.BooDeger2.HasValue &&
                    SistemParamUstBirimKontrolu.BooDeger2 == (bool)true)
                {
                    lblProgress.Text = string.Format("Alt Birimler için işlem başlatılıyor...");
                    Timer1.Enabled = true;
                }
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {

                ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleriResources.KriterHatasi);
            }
        }

        private void AkademikTakvimKaydiOlustur(int organizasyonProgramID, int yil, int donem, DateTime BaslamaTarihi, DateTime BitisTarihi)
        {
            try
            {
                AkademikTakvim AkademikTakvim = OgrenciUygulama.getAkademikTakvim(organizasyonProgramID, yil, donem);

                if (AkademikTakvim == null)
                {
                    AkademikTakvim = new AkademikTakvim();
                    AkademikTakvim.Sinif = 99;
                    AkademikTakvim.OgretimYili = yil;
                    AkademikTakvim.OgretimDonemi = OgrenciUygulama.getDonem(donem);
                    AkademikTakvim.OrganizasyonProgramid = organizasyonProgramID;
                    OgrenciUygulama.guncelleKaydetAkademikTakvim(AkademikTakvim);
                }

                AkademikTakvimAktivite AkademikTakvimAktivite = OgrenciUygulama.getAkademikTakvimAktivite(AkademikTakvim.AkademikTakvimid);

                if (AkademikTakvimAktivite == null)
                    AkademikTakvimAktivite = new AkademikTakvimAktivite();

                AkademikTakvimAktivite.BaslamaTarihi = BaslamaTarihi;
                AkademikTakvimAktivite.BitisTarihi = BitisTarihi;
                AkademikTakvimAktivite.Aktivitekodu = OgrenciUygulama.getAktiviteKodu();
                AkademikTakvimAktivite.AkademikTakvim = AkademikTakvim;
                OgrenciUygulama.guncelleKaydetAkademikTakvimAktivite(AkademikTakvimAktivite);
            }
            catch (Exception ex) { }
        }

        private void BasvuruProgramKaydıYap(OrganizasyonProgram item)
        {

            try
            {
                string sqlbasvuruProgram = string.Format(@" SELECT TOP 1 *
                                                            FROM ens.BasvuruProgram
                                                            WHERE OrganizasyonProgramID = {0} AND OgrenciTuru = {1} AND OgretimYili = {2} AND OgretimDonemi = {3} ",
                                                         item.OrganizasyonProgramid, item.ProgramTuru.Kodid, yildonem_basvuru.Yil, yildonem_basvuru.Donem);

                if (kodcomboUcretDurumu.SelectedKodId != -1)
                    sqlbasvuruProgram += " AND BursluUcretli = " + kodcomboUcretDurumu.SelectedKodId.ToString() + " ";

                sqlbasvuruProgram += " ORDER BY BasvuruProgramID DESC ";

                BasvuruProgram bp = OgrenciMaster.Database.Select<BasvuruProgram>(sqlbasvuruProgram).ToList().FirstOrDefault();

                if (bp == null)
                    bp = new BasvuruProgram();

                BasvuruProgramSetEt(bp, false, item.OrganizasyonProgramid);
                OgrenciMaster.Database.SaveOrUpdate(bp);
                bool sonuc3 = kaydetGirisSinavNotlari(bp);
                kaydetDegerlendirmeKriterleri(bp);
                kaydetAlsPuanTuru(bp);
                AkademikTakvimKaydiOlustur(bp.OrganizasyonProgram.OrganizasyonProgramid, yildonem_basvuru.Yil, yildonem_basvuru.Donem, (DateTime)date_baslama_tarihi.SelectedDate,
                                           (DateTime)date_bitis_tarihi.SelectedDate);
            }
            catch (Exception) { }
        }

        protected void btn_hoca_ekle_mulakat_Click(object sender, EventArgs e)
        {
            try
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
                    ltlInfo.Text = "";

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

                if (_temp.BasvuruProgram.Organizasyon == null)
                {
                    OgrenciMaster.Database.Initialize(CurrentBasvuruProgram, CurrentBasvuruProgram.OrganizasyonProgram);
                    _temp.BasvuruProgram.Organizasyon = OgrenciUygulama.getOrganizasyon(CurrentBasvuruProgram.OrganizasyonProgram.OrganizasyonProgramid);
                }

                _temp.Donem = OgrenciUygulama.getirkod(BasvuruDonem);
                _temp.JuriTipi = OgrenciUygulama.getirkod(944001);
                _temp.OgretimUyesi = hocaMulakat;

                if (cmb_mulakat_hoca_durum.SelectedKodId > 0)
                    _temp.OgretimUyesiDurum = cmb_mulakat_hoca_durum.SelectedKod;

                _temp.OgretimUyesiUnvan = hocaMulakat.AkademikUnvan;
                _temp.Yil = BasvuruYili;

                if (_temp.JuriUyeleriid > 0)//update
                    sonuc = OgrenciUygulama.guncelleJuriUyesi(_temp);
                else//kaydet
                    sonuc = OgrenciUygulama.kaydetJuriUyesi(_temp);

                if (sonuc)
                {
                    JuriUyeleriDoldur();
                    //acEkran();
                    ltlInfo.Text = base.BilgiGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.hocaeklendi);
                }
                else
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleri.Resources.Lang.DerstenVazgecmeIslemleriResources.hocaeklenemedi);
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810015);
            }
        }

        protected void grd_sinav_notlari_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (GirisSinavNotlari != null)
                    grd_sinav_notlari.DataSource = GirisSinavNotlari.Liste;
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810016);
            }
        }

        private bool kaydetGirisSinavNotlari()
        {
            try
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
                    Telerik.Web.UI.RadDatePicker txt_gecerlilik_tarihi = (Telerik.Web.UI.RadDatePicker)grd_sinav_notlari.Items[i].FindControl("txt_gecerlilik_tarihi");

                    if (txt_puan1.Text != "")
                        _temp.Puan1 = Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));
                    else
                        _temp.Puan1 = 0;

                    if (txt_puan2.Text != "")
                        _temp.Puan2 = Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));
                    else
                        _temp.Puan2 = 0;


                    if (txt_gecerlilik_tarihi.SelectedDate != null)
                    {
                        if (txt_gecerlilik_tarihi.SelectedDate != txt_gecerlilik_tarihi.MinDate)
                            _temp.GecerlilikTarihi = txt_gecerlilik_tarihi.SelectedDate.Value;
                        else
                            _temp.GecerlilikTarihi = null;
                    }
                    else
                    {
                        _temp.GecerlilikTarihi = null;
                    }

                    if (txt_yabanci_uyruk.Text != "")
                        _temp.YabanciUyrukluPuanBaraji = Convert.ToDecimal(txt_yabanci_uyruk.Text.Replace('.', ','));
                    else
                        _temp.YabanciUyrukluPuanBaraji = 0;


                    if (_temp.GirisSinaviPuanBarajlariid > 0)//update
                        sonuc = OgrenciUygulama.guncelleGirisSinavBaraji(_temp) || sonuc;
                    else//kaydet
                        sonuc = OgrenciUygulama.kaydetGirisSinavBaraji(_temp) || sonuc;
                }

                return sonuc;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterHatasi, 1081051, SourceLevels.Critical);
            }
        }

        private bool kaydetGirisSinavNotlari(BasvuruProgram basvuruProgram)
        {
            try
            {
                bool sonuc = false;
                GirisSinaviPuanBarajlari _temp;

                if (grd_sinav_notlari.Items.Count < 1)
                    sonuc = true;

                for (int i = 0; i < grd_sinav_notlari.Items.Count; i++)
                {
                    _temp = OgrenciUygulama.getirGirisSinavBaraji(basvuruProgram.BasvuruProgramid, grd_sinav_notlari.Items[i].Cells[2].Text);

                    if (_temp == null || _temp.GirisSinaviPuanBarajlariid <= 0)
                        _temp = new GirisSinaviPuanBarajlari();

                    _temp.BasvuruProgram = basvuruProgram;
                    _temp.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_sinav_notlari.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_sinav_notlari.Items[i].FindControl("txt_puan1");
                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_sinav_notlari.Items[i].FindControl("txt_puan2");
                    Telerik.Web.UI.RadNumericTextBox txt_yabanci_uyruk = (Telerik.Web.UI.RadNumericTextBox)grd_sinav_notlari.Items[i].FindControl("txt_yabanci_uyruk");
                    Telerik.Web.UI.RadDateInput txt_gecerlilik_tarihi = (Telerik.Web.UI.RadDateInput)grd_sinav_notlari.Items[i].FindControl("txt_gecerlilik_tarihi");

                    if (txt_gecerlilik_tarihi.SelectedDate != null)
                    {
                        _temp.GecerlilikTarihi = txt_gecerlilik_tarihi.SelectedDate.Value;
                    }
                    else
                    {
                        _temp.GecerlilikTarihi = null;
                    }

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
                        sonuc = OgrenciUygulama.guncelleGirisSinavBaraji(_temp) || sonuc;
                    else//kaydet
                        sonuc = OgrenciUygulama.kaydetGirisSinavBaraji(_temp) || sonuc;
                }

                return sonuc;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterHatasi, 1081051, SourceLevels.Critical);
            }
        }

        private bool kontroldegerlendirmekriterleritoplamoran()
        {
            try
            {
                //bool sonuc = true;
                //decimal toplam2 = 0;
                //for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                //{
                //    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan1");
                //    if (txt_puan1.Text != "")
                //        toplam = toplam + Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));
                //}
                //for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                //{
                //    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan2");
                //    if (txt_puan2.Text != "")
                //        toplam2 = toplam2 + Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));
                //}

                for (int i = 2; i < grd_degerlendirme_kriterleri.Columns.Count; i++)
                {
                    // her cell icin tum rowlardaki textboxlarin yuzde toplamini alicam burayas
                    if (grd_degerlendirme_kriterleri.Columns[i].Visible == false)
                        continue;

                    string txtID = string.Format("txt_puan{0}", i - 1);
                    decimal toplam = 0;

                    for (int j = 0; j < grd_degerlendirme_kriterleri.Items.Count; j++)
                    {
                        Telerik.Web.UI.RadNumericTextBox txt_puan = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[j].FindControl(txtID);

                        if (txt_puan.Text != "")
                            toplam = toplam + Convert.ToDecimal(txt_puan.Text.Replace('.', ','));
                    }

                    if (toplam > 100 || toplam < 0)
                        return false;
                }

                return true;
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterHatasi, 1081050, SourceLevels.Critical);
            }
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
                        _istenenBelgeler = new IstenenBelgeler();

                    _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan1");

                    if (txt_puan1.Text != "")
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));
                    else
                        _istenenBelgeler.PuanAgirligi = 0;

                    _istenenBelgeler.HesaplamaTuru = 1;
                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 2);

                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                        _istenenBelgeler = new IstenenBelgeler();

                    _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan2");

                    if (txt_puan2.Text != "")
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));
                    else
                        _istenenBelgeler.PuanAgirligi = 0;

                    _istenenBelgeler.HesaplamaTuru = 2;
                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 3);
                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                        _istenenBelgeler = new IstenenBelgeler();

                    _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan3 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan3");

                    if (txt_puan3.Text != "")
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan3.Text.Replace('.', ','));
                    else
                        _istenenBelgeler.PuanAgirligi = 0;

                    _istenenBelgeler.HesaplamaTuru = 3;
                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                if (grd_degerlendirme_kriterleri.Columns[5].Visible == true)
                {
                    for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                    {
                        _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 4);
                        if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                            _istenenBelgeler = new IstenenBelgeler();

                        _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                        _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                        Telerik.Web.UI.RadNumericTextBox txt_puan4 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan4");

                        if (txt_puan4.Text != "")
                            _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan4.Text.Replace('.', ','));
                        else
                            _istenenBelgeler.PuanAgirligi = 0;

                        _istenenBelgeler.HesaplamaTuru = 4;
                        OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                    }
                }

                if (grd_degerlendirme_kriterleri.Columns[6].Visible == true)
                {
                    for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                    {
                        _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 5);
                        if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                            _istenenBelgeler = new IstenenBelgeler();

                        _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                        _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                        Telerik.Web.UI.RadNumericTextBox txt_puan5 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan5");

                        if (txt_puan5.Text != "")
                            _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan5.Text.Replace('.', ','));
                        else
                            _istenenBelgeler.PuanAgirligi = 0;

                        _istenenBelgeler.HesaplamaTuru = 5;
                        OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                    }
                }

                if (grd_degerlendirme_kriterleri.Columns[7].Visible == true)
                {
                    for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                    {
                        _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(CurrentBasvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 6);
                        if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                            _istenenBelgeler = new IstenenBelgeler();

                        _istenenBelgeler.BasvuruProgram = CurrentBasvuruProgram;
                        _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                        Telerik.Web.UI.RadNumericTextBox txt_puan6 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan6");

                        if (txt_puan6.Text != "")
                            _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan6.Text.Replace('.', ','));
                        else
                            _istenenBelgeler.PuanAgirligi = 0;

                        _istenenBelgeler.HesaplamaTuru = 6;
                        OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                    }
                }
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterGuncelleme, 1081001, SourceLevels.Error);
            }
        }

        private void kaydetDegerlendirmeKriterleri(BasvuruProgram basvuruProgram)
        {
            try
            {
                IstenenBelgeler _istenenBelgeler;

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(basvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 1);
                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                        _istenenBelgeler = new IstenenBelgeler();

                    _istenenBelgeler.BasvuruProgram = basvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan1");

                    if (txt_puan1.Text != "")
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan1.Text.Replace('.', ','));
                    else
                        _istenenBelgeler.PuanAgirligi = 0;

                    _istenenBelgeler.HesaplamaTuru = 1;
                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(basvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 2);
                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                        _istenenBelgeler = new IstenenBelgeler();

                    _istenenBelgeler.BasvuruProgram = basvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan2");

                    if (txt_puan2.Text != "")
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan2.Text.Replace('.', ','));
                    else
                        _istenenBelgeler.PuanAgirligi = 0;

                    _istenenBelgeler.HesaplamaTuru = 2;
                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                {
                    _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(basvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 3);
                    if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                        _istenenBelgeler = new IstenenBelgeler();

                    _istenenBelgeler.BasvuruProgram = basvuruProgram;
                    _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                    Telerik.Web.UI.RadNumericTextBox txt_puan3 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan3");

                    if (txt_puan3.Text != "")
                        _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan3.Text.Replace('.', ','));
                    else
                        _istenenBelgeler.PuanAgirligi = 0;

                    _istenenBelgeler.HesaplamaTuru = 3;
                    OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                }

                if (grd_degerlendirme_kriterleri.Columns[5].Visible == true)
                {
                    for (int i = 0; i < grd_degerlendirme_kriterleri.Items.Count; i++)
                    {
                        _istenenBelgeler = OgrenciUygulama.getirIstenenBelge(basvuruProgram.BasvuruProgramid, grd_degerlendirme_kriterleri.Items[i].Cells[2].Text, 4);
                        if (_istenenBelgeler == null || _istenenBelgeler.IstenenBelgelerid <= 0)
                        {
                            _istenenBelgeler = new IstenenBelgeler();
                        }
                        _istenenBelgeler.BasvuruProgram = basvuruProgram;
                        _istenenBelgeler.Kod = OgrenciUygulama.getirkod(Convert.ToDecimal(grd_degerlendirme_kriterleri.Items[i].Cells[2].Text));
                        Telerik.Web.UI.RadNumericTextBox txt_puan4 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[i].FindControl("txt_puan4");

                        if (txt_puan4.Text != "")
                            _istenenBelgeler.PuanAgirligi = Convert.ToDecimal(txt_puan4.Text.Replace('.', ','));
                        else
                            _istenenBelgeler.PuanAgirligi = 0;

                        _istenenBelgeler.HesaplamaTuru = 4;
                        OgrenciMaster.Database.SaveOrUpdate(_istenenBelgeler);
                    }
                }
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, DerstenVazgecmeIslemleriResources.KriterGuncelleme, 1081001, SourceLevels.Error);
            }
        }

        protected void grd_sinav_notlari_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is Telerik.Web.UI.GridDataItem)
                {
                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl("txt_puan1");
                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl("txt_puan2");
                    Telerik.Web.UI.RadNumericTextBox txt_yabanci_uyruk = (Telerik.Web.UI.RadNumericTextBox)e.Item.FindControl("txt_yabanci_uyruk");
                    Telerik.Web.UI.RadDatePicker txt_gecerlilik_tarihi = (Telerik.Web.UI.RadDatePicker)e.Item.FindControl("txt_gecerlilik_tarihi");

                    var dataItem = (e.Item.DataItem as GirisSinavPuanlarim);
                    if (dataItem.GecerlilikTarihi != null && txt_gecerlilik_tarihi.MinDate < dataItem.GecerlilikTarihi)
                    {
                        txt_gecerlilik_tarihi.SelectedDate = dataItem.GecerlilikTarihi;
                    }
                    else
                    {
                        txt_gecerlilik_tarihi.SelectedDate = txt_gecerlilik_tarihi.MinDate;
                        txt_gecerlilik_tarihi.SelectedDate = null;
                    }

                    if (e.Item.Cells[7].Text != "" && e.Item.Cells[7].Text != "&nbsp;")
                        txt_puan1.Text = Convert.ToDouble(e.Item.Cells[7].Text).ToString().Replace(',', '.');
                    //txt_puan1.Text = Convert.ToDouble(e.Item.Cells[6].Text).ToString();

                    if (e.Item.Cells[8].Text != "" && e.Item.Cells[8].Text != "&nbsp;")
                        txt_puan2.Text = Convert.ToDouble(e.Item.Cells[8].Text).ToString().Replace(',', '.');

                    if (e.Item.Cells[9].Text != "" && e.Item.Cells[9].Text != "&nbsp;")
                        txt_yabanci_uyruk.Text = Convert.ToDouble(e.Item.Cells[9].Text).ToString().Replace(',', '.'); ;



                }
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810019);
            }
        }

        protected void grd_degerlendirme_kriterleri_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (DegerlendirmeKriterleri != null)
                    grd_degerlendirme_kriterleri.DataSource = DegerlendirmeKriterleri.Liste;
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810020);
            }
        }

        protected void btn_hoca_sil_mulakat_Click(object sender, EventArgs e)
        {
            try
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
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810021);
            }
        }

        protected void yildonem_basvuru_DonemSelectedEventHandler(int yil, int donem)
        {
            try
            {
                //BelgeKontroluEkran();
                doldurEkran();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception)
            {
                ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleriResources.EkranDoldurma);
            }
        }

        protected void grd_mulakat_hoca_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
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
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810022);
            }
        }

        protected void txt_turk_kontenjan_TextChanged(object sender, EventArgs e)
        {
            try
            {

                txt_kontenjan.Text = (Convert.ToInt32(txt_turk_kontenjan.Text) + Convert.ToInt32(txt_yabanci_kontenjan.Text) + Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text)).ToString();

            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810023);
            }
        }

        protected void txt_yabanci_kontenjan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_kontenjan.Text = (Convert.ToInt32(txt_turk_kontenjan.Text) + Convert.ToInt32(txt_yabanci_kontenjan.Text) + Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text)).ToString();

            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810024);
            }
        }

        //protected void txt_milli_sporcu_kontenjan_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.Bayburt ||
        //           OgrenciMaster.UnipaMaster.Musteri.Id == (int)UniOgrenci.Master.Enums.Universiteler.BayburtTest)
        //        {
        //            txt_kontenjan.Text = (Convert.ToInt32(txt_turk_kontenjan.Text) + Convert.ToInt32(txt_yabanci_kontenjan.Text) + Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_milli_sporcu_alan_ici_kontenjan.Text) + Convert.ToInt32(txt_milli_sporcu_alan_disi_kontenjan.Text)).ToString();

        //        }
        //        else
        //        {
        //            txt_kontenjan.Text = (Convert.ToInt32(txt_turk_kontenjan.Text) + Convert.ToInt32(txt_yabanci_kontenjan.Text) + Convert.ToInt32(txt_turk_kontenjan_alan_disi.Text) + Convert.ToInt32(txt_yabanci_kontenjan_alan_disi.Text)).ToString();
        //        }
        //    }
        //    catch (UnipaException ex)
        //    {
        //        ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
        //    }
        //    catch (Exception ex)
        //    {
        //        ltlInfo.Text = base.HataGoster(ex.Message, 10810027);
        //    }
        //}

        protected void txt_yedek_kontenjan_turk_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_yedek_kontenjan.Text = (Convert.ToInt32(txt_yedek_kontenjan_turk.Text) + Convert.ToInt32(txt_yedek_kontenjan_turk_alan_disi.Text) +
                                            Convert.ToInt32(txt_yedek_kontenjan_yabanci_alan_disi.Text) + Convert.ToInt32(txt_yedek_kontenjan_yabanci.Text)).ToString();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810025);
            }
        }

        protected void txt_yedek_kontenjan_yabanci_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_yedek_kontenjan.Text = (Convert.ToInt32(txt_yedek_kontenjan_turk.Text) + Convert.ToInt32(txt_yedek_kontenjan_turk_alan_disi.Text) +
                                            Convert.ToInt32(txt_yedek_kontenjan_yabanci_alan_disi.Text) + Convert.ToInt32(txt_yedek_kontenjan_yabanci.Text)).ToString();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810026);
            }

        }

        #region exporttoexcel
        void ExportToExcel(IList<JuriUyeleri> reportList, string fileName, string reportName)
        {
            try
            {
                //The Clear method erases any buffered HTML output.
                HttpContext.Current.Response.Clear();
                //The AddHeader method adds a new HTML header and value to the response sent to the client.
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
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
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810027);
            }
        }
        #endregion

        protected void ExcelKayit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ExportToExcel(JuriUyeleriMulakat, "JüriÜyeleri.xls", "Jüri Üyeleri");
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810028);
            }
        }

        /// <summary>
        /// web servis testidir silmeyiniz. efecan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCaniasDeneme_Click(object sender, EventArgs e)
        {
            try
            {
                // GetHarcOnay(string ogrenciNo, int ogretimYili, Kod ogretimDonemi)
                Kod a = OgrenciMaster.Database.LoadEntityByID<Kod>(100001);
                //beykoz öğrenci no 0901030048 plato 20110105007, 20090101010
                object response = OgrenciUygulama.OgrenciMaster.HarcIslemleri.GetHarcOnay(ogrno.Text, 2012, a);
                string aciklama = ((UniOgrenci.Master.Entities.HarcOnay)(response)).Onay + ((UniOgrenci.Master.Entities.HarcOnay)(response)).OgretimYili.ToString() + ((UniOgrenci.Master.Entities.HarcOnay)(response)).OgretimDonemi.Aciklama.ToString();
                //string[] bol = aciklama.Split('.'); beykoz için 
                //string kayitYapabilirMi = bol[0];
                //string krediSayisi = bol[1];
                //string kredi = bol[2];
                lblTestResult.Text = aciklama;
            }
            catch
            { }

            //authentication auth = new authentication();
            //auth.Kullanici = "UNIPA";
            //auth.Sifre = "UNIPA";
            asd.authentication auth = new authentication();
            auth.Kullanici = "UNIPA";
            auth.Sifre = "UNIPA";
            asd.HarcSorguService hs = new HarcSorguService();
            object ab = hs.HarcOnay(auth, "0801060031", 2012, 100001);
        }

        protected void cmbBasvuruSecimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBasvuruSecimi.SelectedIndex == 1)
                pnlOrganizasyonSecimi.Enabled = false;
            else
                pnlOrganizasyonSecimi.Enabled = true;
        }

        protected void btnDigerBirimlerdenJuriAta_Click(object sender, EventArgs e)
        {
            cmbBirim.Enabled = true;
            cmbProgram.Enabled = true;
            cmbAltBirim.Enabled = true;
        }

        protected void cmbBirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblUyari.Text = "";
                int Birim, AltBirim;

                if (!int.TryParse(cmbBirim.SelectedValue, out Birim))
                    Birim = 0;

                DataTable dt = OgrenciUygulama.getAltBirim(Birim);
                cmbAltBirim.DataSource = dt;
                cmbAltBirim.DataBind();
                //cmbAltBirim.Items.Insert(0, new RadComboBoxItem("Seçiniz", "-1"));
                cmbAltBirim.DataBind();

                if (!int.TryParse(cmbAltBirim.SelectedValue, out AltBirim))
                    AltBirim = 0;

                if (AltBirim == -1 || AltBirim == 0)
                    AltBirim = Birim;

                cmbProgram.DataSource = OgrenciUygulama.getProgram(AltBirim);
                cmbProgram.DataBind();
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Birim getirilirken bir sorun oluştu", 10810025, SourceLevels.Error);
            }

        }

        protected void cmbAltBirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblUyari.Text = "";
                cmbProgram.DataSource = OgrenciUygulama.getProgram(int.Parse(cmbAltBirim.SelectedValue));
                //cmbProgram.Items.Insert(0,new RadComboBoxItem("Seçiniz", "-1"));
                cmbProgram.DataBind();
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Alt Birim getirilirken bir sorun oluştu", 10810025, SourceLevels.Error);
            }
        }

        protected void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUyari.Text = "";
        }

        protected void btnJurileriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                lblUyari.Text = "";
                if (cmbBirim.SelectedValue == "-1")
                {
                    lblUyari.Text = "Birim Seçmeniz gerekir.";
                    return;
                }

                if (yildonem_basvuru.Yil < 1900 || yildonem_basvuru.Donem < 100000)
                {
                    lblUyari.Text = "Yıl Dönem Seçmeden Bölüme jüri atayamazsınız.";
                    return;
                }

                IList<JuriUyeleri> JuriUyeler = OgrenciUygulama.getirJuriUyeleri(organizasyon_.SelectedOrganizasyonProgram.OrganizasyonProgramid, yildonem_basvuru.Yil, yildonem_basvuru.Donem);
                int ProgramID = -1;

                if (yildonem_basvuru.Yil.ToString() != "-1" && yildonem_basvuru.Donem.ToString() != "-1")
                {
                    for (int k = 0; k < cmbProgram.Items.Count; k++)
                    {
                        if (int.Parse(cmbProgram.SelectedValue) != -1)
                        {
                            ProgramID = int.Parse(cmbProgram.SelectedValue);

                            if (cmbProgram.Items[k].Value != cmbProgram.SelectedValue)
                                continue;
                        }
                        else
                            ProgramID = int.Parse(cmbProgram.Items[k].Value);

                        if (cmbProgram.Items[k].Value == "-1")
                            continue;

                        foreach (JuriUyeleri juriUye in JuriUyeler)
                        {
                            JuriUyeleri temp = new JuriUyeleri();
                            //temp = juriUye;
                            //if (cmbProgram.SelectedValue == "-1")
                            //{
                            //    continue;
                            //}

                            OrganizasyonProgram op = OgrenciMaster.Database.SelectSingle<OrganizasyonProgram>(@"SELECT *
                                                                                                                 FROM OrganizasyonProgram
                                                                                                                 WHERE OrganizasyonProgramID = ?", ProgramID);
                            BasvuruProgram bp = OgrenciMaster.Database.SelectSingle<BasvuruProgram>(@"SELECT *
                                                                                                       FROM ens.BasvuruProgram
                                                                                                       WHERE OrganizasyonProgramID = ? AND OgretimYili = ? AND OgretimDonemi = ?",
                                                                                                     op.OrganizasyonProgramid, BasvuruYili, BasvuruDonem);

                            if (bp != null)
                                temp.BasvuruProgram = bp;
                            else
                                //lblUyari.Text = "Lütfen Önce Başvurulacak Programı Kaydediniz!";
                                continue;

                            temp.Donem = juriUye.Donem;
                            temp.JuriTipi = juriUye.JuriTipi;
                            temp.OgretimUyesi = juriUye.OgretimUyesi;
                            temp.OgretimUyesiDurum = juriUye.OgretimUyesiDurum;
                            temp.Yil = juriUye.Yil;
                            temp.OgretimUyesiUnvan = juriUye.OgretimUyesiUnvan;
                            OgrenciMaster.Database.Save(temp);
                        }

                        lblUyari.Text = "Seçtiğiniz Birime jüriler atanmıştır.";
                    }
                }
            }
            catch (UnipaException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnipaException(ex, "Jüri Üyeleri eklenirken bir sorun oluştu.", 10810026, SourceLevels.Error);
            }
        }

        protected void ara_hoca_mulakat_OgretimOyesiSearchedEventHandler1(OgretimUyesi o)
        {
            try
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
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810003);
            }
        }

        protected void ara_hoca_mulakat_OgretimOyesiSelectedEventHandler1(OgretimUyesi value)
        {
            try
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
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810002);
            }
        }

        protected void chkAlesGozukmesin_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkAlesGozukmesin.Checked)
            {
                //Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan1");
                //Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan2");
                //Telerik.Web.UI.RadNumericTextBox txt_puan3 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan3");
                if (grd_degerlendirme_kriterleri.MasterTableView.Items.Count > 0)
                {
                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan1");
                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan2");
                    Telerik.Web.UI.RadNumericTextBox txt_puan3 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan3");
                    Telerik.Web.UI.RadNumericTextBox txt_puan4 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan4");
                    Telerik.Web.UI.RadNumericTextBox txt_puan5 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan5");
                    Telerik.Web.UI.RadNumericTextBox txt_puan6 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan6");

                    txt_puan1.Enabled = false;
                    txt_puan2.Enabled = false;
                    txt_puan3.Enabled = false;
                    txt_puan4.Enabled = false;
                    txt_puan5.Enabled = false;
                    txt_puan6.Enabled = false;
                }


            }
            else
            {
                if (grd_degerlendirme_kriterleri.MasterTableView.Items.Count > 0)
                {
                    Telerik.Web.UI.RadNumericTextBox txt_puan1 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan1");
                    Telerik.Web.UI.RadNumericTextBox txt_puan2 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan2");
                    Telerik.Web.UI.RadNumericTextBox txt_puan3 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan3");
                    Telerik.Web.UI.RadNumericTextBox txt_puan4 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan4");
                    Telerik.Web.UI.RadNumericTextBox txt_puan5 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan5");
                    Telerik.Web.UI.RadNumericTextBox txt_puan6 = (Telerik.Web.UI.RadNumericTextBox)grd_degerlendirme_kriterleri.Items[0].FindControl("txt_puan6");
                    txt_puan1.Enabled = true;
                    txt_puan2.Enabled = true;
                    txt_puan3.Enabled = true;
                    txt_puan4.Enabled = true;
                    txt_puan5.Enabled = true;
                    txt_puan6.Enabled = true;
                }
            }
        }

        protected void ChkTurkYabanciOrtak_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ChkTurkYabanciOrtak.Checked)
            {
                txt_yabanci_kontenjan.Enabled = false;
                txt_yabanci_kontenjan_alan_disi.Enabled = false;
                txt_yedek_kontenjan_yabanci_alan_disi.Enabled = false;
                txt_yedek_kontenjan_yabanci.Enabled = false;
                txt_yabanci_kontenjan.Value = 0;
                txt_yabanci_kontenjan_alan_disi.Value = 0;
                txt_yedek_kontenjan_yabanci_alan_disi.Value = 0;
                txt_yedek_kontenjan_yabanci.Value = 0;

            }
            else
            {
                txt_yabanci_kontenjan.Enabled = true;
                txt_yabanci_kontenjan_alan_disi.Enabled = true;
                txt_yedek_kontenjan_yabanci_alan_disi.Enabled = true;
                txt_yedek_kontenjan_yabanci.Enabled = true;
            }
        }

        protected void cmbProgramTuru_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            acEkran();
        }

        protected void chkUstBirimBazinda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUstBirimBazinda.Checked == false)
            {
                pnlOrganizasyonSecimi.Enabled = true;
                pnlOrganizasyonSecimi.Visible = true;
                panelUstBirimOrganizasyonUC.Enabled = false; panelUstBirimOrganizasyonUC.Visible = false;
                pnlUp.Visible = false;
            }
            else
            {
                pnlOrganizasyonSecimi.Enabled = false; pnlOrganizasyonSecimi.Visible = false;
                panelUstBirimOrganizasyonUC.Enabled = true; panelUstBirimOrganizasyonUC.Visible = true;
                pnlUp.Visible = true;
                lblProgress.Text = string.Empty;

            }

            if (yildonem_basvuru.Donem > 100000 && yildonem_basvuru.Yil > 1)
                try
                {
                    doldurEkran();
                }
                catch (UnipaException ex)
                {
                    ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
                }
                catch (Exception)
                {
                    ltlInfo.Text = base.HataGoster(DerstenVazgecmeIslemleriResources.EkranDoldurma);
                }
        }

        protected void Organizasyon1_OrganizasyonSelectedEventHandler(int selectedOrganizasyonID)
        {
            programYukle();
            doldurEkran();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (altBirimListesi.Count > 0 && currentNumber < altBirimListesi.Count)
            {
                List<OrganizasyonProgram> subList = altBirimListesi.Skip(currentNumber).Take(2).ToList();

                foreach (OrganizasyonProgram item in subList)
                    BasvuruProgramKaydıYap(item);

                currentNumber += subList.Count;
                lblProgress.Text = string.Format("İşlem Yapılan Program Sayısı : {0}, Toplam Sayı : {1}", currentNumber, altBirimListesi.Count);
            }
            else
            {
                lblProgress.Text = string.Format("İşlem Tamamlandı");
                Timer1.Enabled = false;
            }
        }

        protected void kodcomboUcretDurumu_SelectedIndexChanged(Kod k)
        {
            doldurEkran();
        }

        private void KodSistemParamDoldur()
        {
            if (SistemParamBelgeKontrolu == null)
            {
                SistemParamBelgeKontrolu = OgrenciMaster.Database.LoadEntityByID<SistemParam>(15016);

                if (SistemParamBelgeKontrolu != null && SistemParamBelgeKontrolu.BooDeger1 != null && SistemParamBelgeKontrolu.BooDeger1.Value && !(string.IsNullOrEmpty(SistemParamBelgeKontrolu.StrDeger1)))
                {
                    string _sql = string.Format(@"SELECT *
                                                  FROM Kod
                                                  WHERE KodID IN ({0})", SistemParamBelgeKontrolu.StrDeger1.Replace(';', ','));
                    KodGrupBelgeKontrolu = OgrenciMaster.Database.Select<Kod>(_sql).ToList();
                }
            }
        }

        //private void BelgeKontroluEkran()
        //{
        //    if (organizasyon_.SelectedProgramId != -1)
        //    {
        //        if (SistemParamBelgeKontrolu != null && SistemParamBelgeKontrolu.BooDeger1 != null && SistemParamBelgeKontrolu.BooDeger1.Value && (SistemParamBelgeKontrolu.StrDeger1 != null || SistemParamBelgeKontrolu.StrDeger1 != string.Empty) && KodGrupBelgeKontrolu != null)
        //        {
        //            foreach (var item in KodGrupBelgeKontrolu)
        //            {
        //                switch (item.Kodid)
        //                {
        //                    case 163001: chkBelgeDiploma.Visible = true; break;
        //                    case 163002: chkTranskript.Visible = true; break;
        //                    case 163003: chkAles.Visible = true; break;
        //                    case 163004: chkYabanciDilBelgesi.Visible = true; break;
        //                    case 163005: chkPasaport.Visible = true; break;
        //                    case 163006: chkAskerlik.Visible = true; break;
        //                }
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// Work Item 176542
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OncekiDonemdenAktar_Click(object sender, EventArgs e)
        {
            try
            {
                if (kaynakYilDonem.Yil == -1 || kaynakYilDonem.Donem == -1 || yildonem_basvuru.Yil == -1 || yildonem_basvuru.Donem == -1)
                {
                    throw new Exception("Aktarım yapmak istediğiniz hedef yıl-dönem ve kaynak yıl-dönem bilgilerini seçiniz.");
                }

                var kaynakYil = kaynakYilDonem.Yil;
                var kaynakDonem = kaynakYilDonem.Donem;
                var hedefYil = yildonem_basvuru.Yil;
                var hedefDonem = yildonem_basvuru.Donem;
                //Buyuk kucuk kontrolu yapiliyor.
                OgrenciUygulama.AtamaKaynakHedefKontrolu(hedefYil, hedefDonem, kaynakYil, kaynakDonem);

                if (!chkUstBirimBazinda.Checked)
                {
                    var basvuruProgram = GetBasvuruProgramKaynak(kaynakYil, kaynakDonem);

                    if (basvuruProgram == null)
                    {
                        throw new Exception("Aktarım için seçtiğiniz kaynak döneme ait kayıt bulunamadı.");
                    }

                    var cmd = OgrenciMaster.Database.GetSqlStringCommand(@"EXECUTE [dbo].[sp_0081_OncekiDonemdenKopyala] @KaynakYil, @KaynakDonem, @HedefYil, @HedefDonem, @BasvuruProgramIDKaynak");
                    OgrenciMaster.Database.AddInParameter(cmd, "@KaynakYil", DbType.Int32, kaynakYil);
                    OgrenciMaster.Database.AddInParameter(cmd, "@KaynakDonem", DbType.Int32, kaynakDonem);
                    OgrenciMaster.Database.AddInParameter(cmd, "@HedefYil", DbType.Int32, hedefYil);
                    OgrenciMaster.Database.AddInParameter(cmd, "@HedefDonem", DbType.Int32, hedefDonem);
                    OgrenciMaster.Database.AddInParameter(cmd, "@BasvuruProgramIDKaynak", DbType.Int32, basvuruProgram.BasvuruProgramid);
                    var dt = OgrenciMaster.Database.ExecuteScalar(cmd);

                    if (dt.ToString() == "1")
                    {
                        ltlInfo.Text = base.BilgiGoster("Aktarım işlemi tamamlandı.");
                        doldurEkran();
                    }
                    else
                    {
                        throw new Exception("Aktarım işlemi gerçekleştirilemedi.");
                    }
                }
                else
                {
                    string yapilamayanlar="";

                    List<BasvuruProgram> basvuruProgram = GetBasvuruProgramKaynakList(kaynakYil, kaynakDonem, int.Parse(cmbProgramTuru.SelectedValue), Organizasyon1.SelectedOrganizationId);

                    foreach (var basvuruProgram_ in basvuruProgram)
                    {
                        var cmd = OgrenciMaster.Database.GetSqlStringCommand(@"EXECUTE [dbo].[sp_0081_OncekiDonemdenKopyala] @KaynakYil, @KaynakDonem, @HedefYil, @HedefDonem, @BasvuruProgramIDKaynak");
                        OgrenciMaster.Database.AddInParameter(cmd, "@KaynakYil", DbType.Int32, kaynakYil);
                        OgrenciMaster.Database.AddInParameter(cmd, "@KaynakDonem", DbType.Int32, kaynakDonem);
                        OgrenciMaster.Database.AddInParameter(cmd, "@HedefYil", DbType.Int32, hedefYil);
                        OgrenciMaster.Database.AddInParameter(cmd, "@HedefDonem", DbType.Int32, hedefDonem);
                        OgrenciMaster.Database.AddInParameter(cmd, "@BasvuruProgramIDKaynak", DbType.Int32, basvuruProgram_.BasvuruProgramid);
                        var dt = OgrenciMaster.Database.ExecuteScalar(cmd);
                        if (dt.ToString() != "1")
                        {
                              string bpOrganizasyonAdi = string.Format(@"  select BirimAdi from vwOrganizasyon where organizasyonID={0}",basvuruProgram_.Organizasyon.Organizasyonid);
                              yapilamayanlar += OgrenciMaster.Database.Select<BasvuruProgram>(bpOrganizasyonAdi).ToList().FirstOrDefault() + ", ";
                        }
                    }
                    if (yapilamayanlar.Length > 0)
                    {
                        throw new Exception(yapilamayanlar.Substring(0,yapilamayanlar.Length-2)+" birimleri için aktarım işlemi gerçekleştirilemedi.");
                    }
                    else 
                    {
                        ltlInfo.Text = base.BilgiGoster("Aktarım işlemi tamamlandı.");
                        doldurEkran();
                    }
                }
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810028);
            }
        }

        public BasvuruProgram GetBasvuruProgramKaynak(int yil, int donem)
        {
            BasvuruProgram basvuruProgram;
            var basvuruProgramId = organizasyon_.SelectedProgramId;

            if (chkUstBirimBazinda.Checked)
            {
                //var ustBirimId = Convert.ToInt32(cmbBirim.SelectedValue);
                var ustBirimId = Convert.ToInt32(Organizasyon1.SelectedOrganizationId);
                if (cmbProgramTuru.SelectedValue == "-1")
                    basvuruProgram = OgrenciMaster.Database.SelectSingle<BasvuruProgram>(@"SELECT *
                                                                      FROM ens.BasvuruProgram
                                                                      WHERE OrganizasyonID = ? AND OgretimYili = ? AND OgretimDonemi = ?
                                                                      ORDER BY BasvuruProgramID DESC",
                        ustBirimId,
                        yil, donem);
                else
                    basvuruProgram = OgrenciUygulama.getirBasvuruProgram(ustBirimId, cmbProgramTuru.SelectedValue,
                        yil, donem, " ");
            }
            else
            {
                string s = string.Format(@"SELECT * FROM ens.BasvuruProgram 
                                       WHERE OrganizasyonProgramID = {0} AND OgretimYili = {1} AND OgretimDonemi = {2} ",
                                                 basvuruProgramId, yil, donem);
                basvuruProgram = OgrenciMaster.Database.SelectSingle<BasvuruProgram>(s);
            }

            return basvuruProgram;
        }
        public List<BasvuruProgram> GetBasvuruProgramKaynakList(int yil, int donem, int ogrenciTuru, int organizasyonId)
        {
            List<BasvuruProgram> basvuruProgram;
            string sBasvuruProgram;
            var basvuruProgramId = organizasyon_.SelectedProgramId;
            //var ustBirimId = Convert.ToInt32(cmbBirim.SelectedValue);
            var ustBirimId = Convert.ToInt32(Organizasyon1.SelectedOrganizationId);
            if (ogrenciTuru != -1)
            {
                sBasvuruProgram = string.Format(@"SELECT bp.* FROM ens.BasvuruProgram as bp inner join vwOrganizasyon as org on bp.OrganizasyonID=org.OrganizasyonID
                                                                       WHERE UstBirimID = {0} AND OgretimYili = {1} AND OgretimDonemi = {2}
                                                                      ORDER BY BasvuruProgramID DESC", organizasyonId, yil, donem);
            }
            else
            {
                sBasvuruProgram = string.Format(@"SELECT bp.* FROM ens.BasvuruProgram as bp inner join vwOrganizasyon as org on bp.OrganizasyonID=org.OrganizasyonID
                                                                       WHERE UstBirimID = {0} AND OgrenciTuru = {1} AND OgretimYili = {2} AND OgretimDonemi = {3}
                                                                       ORDER BY BasvuruProgramID DESC", organizasyonId, ogrenciTuru, yil, donem);
            }
            return OgrenciMaster.Database.Select<BasvuruProgram>(sBasvuruProgram).ToList();
        }
        protected void gridBelgeler_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                gridBelgeler.DataSource = OgrenciUygulama.BelgeleriGetir();
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810006);
            }
        }
        protected void gridBelgeler_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem row = e.Item as GridDataItem;

                    var rowModel = (row.DataItem as DataRowView);
                    var kodId = row["KodID"].Text.ToInt();
                    if (BasvuruProgramBelgeList != null && BasvuruProgramBelgeList.Count != 0)
                    {
                        BasvuruProgramBelgeList.ForEach(x =>
                        {
                            if (x.BelgeTuru == kodId)
                            {
                                (row.FindControl("chkTCZorunlu") as CheckBox).Checked = x.ZorunluMu;
                                (row.FindControl("chkYUZorunlu") as CheckBox).Checked = x.YabancidaZorunlu;
                            }
                        });
                    }
                }
            }
            catch (UnipaException ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, ex.Kod);
            }
            catch (Exception ex)
            {
                ltlInfo.Text = base.HataGoster(ex.Message, 10810019);
            }
        }
        /// <summary>
        /// Tablar tiklanilinca calistir.
        /// </summary>
        protected void gridBelgeler_OnClientTabSelected()
        {
            gridBelgeler.Rebind();
        }

    }
}
