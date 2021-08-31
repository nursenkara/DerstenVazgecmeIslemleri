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
using DerstenVazgecmeIslemleri.DTOs;

namespace DerstenVazgecmeIslemleri
{
    public partial class DanismanIslemleri : OgrenciBasePage<DerstenVazgecmeIslemleriUygulama>
    {


        #region Session'lar
        public List<int> DanismanaGonderilenDersler
        {
            get
            {
                return FromPageSession<List<int>>("DanismanaGonderilenDersler", null);
            }
            set
            {
                PageSession["DanismanaGonderilenDersler"] = value;
            }
        }
        public string Danisman
        {
            get
            {
                return FromPageSession<string>("Danisman", null);
            }
            set
            {
                PageSession["Danisman"] = value;
            }
        }

        public int DanismanId
        {
            get
            {
                return FromPageSession<int>("DanismanId", 0);
            }
            set
            {
                PageSession["DanismanId"] = value;
            }
        }

        public List<OgrenciDersGoruntulemeDTO> DerstenVazgecenOgrencilerinListesi
        {
            get
            {
                return FromPageSession<List<OgrenciDersGoruntulemeDTO>>("DerstenVazgecenOgrencilerinListesi", null);
            }
            set
            {
                PageSession["DerstenVazgecenOgrencilerinListesi"] = value;
            }
        }
        public string OgrenciId
        {
            get
            {
                return FromPageSession<string>("OgrenciId", null);
            }
            set
            {
                PageSession["OgrenciId"] = value;
            }
        }
        public OgrencininDersVazgecmeDTO AktifYilDonem
        {
            get
            {
                return FromPageSession<OgrencininDersVazgecmeDTO>("AktifYilDonem", null);
            }
            set
            {
                PageSession["AktifYilDonem"] = value;
            }
        }

        #endregion
        protected override int UygulamaID
        {
            get { return 10008101; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DanismanId = 24888;
            AktifYilDonem = OgrenciUygulama.AktifYilDonem();
            OgrenciDersGoruntulemeDTO das = new OgrenciDersGoruntulemeDTO();
            OgrencininDersVazgecmeDTO dto = new OgrencininDersVazgecmeDTO();
            dto.Yil = AktifYilDonem.Yil;
            dto.Donem = AktifYilDonem.Donem;
            das.DanismanAdi = OgrenciUygulama.DanismanAdiniSoyadiniGetir(DanismanId).DanismanAdi;
            das.DanismanSoyadi = OgrenciUygulama.DanismanAdiniSoyadiniGetir(DanismanId).DanismanSoyadi;
            Danisman = das.DanismanAdi + " " +das.DanismanSoyadi;
            DerstenVazgecenOgrencilerinListesi = OgrenciUygulama.DerstenVazgecenOgrencileriListele();
            OgrenciId = "2239801";


            if (DanismanaGonderilenDersler == null)
            {
                DanismanaGonderilenDersler = OgrenciUygulama.GetDanismanaGonderilenOgrenciDersIdler(OgrenciId);
     
            }
            #region Danışman Onaylama işlemi açıksa Genel paneli, kapalıysa Uyarı paneli açılacak.
            if (OgrenciUygulama.DanismanOnayTarihleriArasindaMi())
            {
                pnlGenel.Visible = true;

            }
            else
            {
                pnlUyari.Visible = true;
                pnlGenel.Visible = false;
            }
            #endregion
        }

        protected void grdDanisman_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (DerstenVazgecenOgrencilerinListesi == null)
                    DerstenVazgecenOgrencilerinListesi = new List<OgrenciDersGoruntulemeDTO>();
                grdDanisman.DataSource = DerstenVazgecenOgrencilerinListesi;
            }
            catch (Exception ex)
            {
                ltlInfo.Text = HataGoster(ex.Message);
            }
        }

        protected void grdDanisman_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem gdi = (GridDataItem)e.Item;
                int ogrenciDersId = 0;
                int.TryParse(gdi.GetDataKeyValue("OgrenciDersId").ToString(), out ogrenciDersId);
                if (e.CommandName == "cnOnay")
                {
                    if (ogrenciDersId != -1)
                    {
                        OgrencininDersVazgecmeDTO dto = new OgrencininDersVazgecmeDTO();
                        dto.OgrenciDersId = ogrenciDersId;
                        dto.IslemiYapanKullanici = Danisman;
                        OgrenciUygulama.DanismanOnayInsert(dto);
                        grdDanisman.Rebind();
                        ltlInfo.Text = BilgiGoster("Vazgeçme onaylandı.");
                    }
                    else
                        ltlInfo.Text = HataGoster("Onay işlemi yapılamadı.");
                }
                else if (e.CommandName == "cnRed")
                {
                    if (ogrenciDersId != -1)
                    {
                        OgrencininDersVazgecmeDTO dto = new OgrencininDersVazgecmeDTO();
                        dto.OgrenciDersId = ogrenciDersId;
                        dto.IslemiYapanKullanici = Danisman;
                        OgrenciUygulama.DanismanRedInsert(dto);

                        int index = DanismanaGonderilenDersler.IndexOf(ogrenciDersId);
                        if (index > -1 && DanismanaGonderilenDersler.Count > 0)
                             {
                              DanismanaGonderilenDersler.RemoveAt(index);
                               //lblVazgecilecekDerslerGuncelle(); gridin onay ve red i silinmeli ve reddedildi yazılmalı
                              grdDanisman.Rebind();

                            }
                       
                        ltlInfo.Text = BilgiGoster("Reddetme onaylandı.");
                    }
                    else
                        ltlInfo.Text = HataGoster("Reddetme işlemi yapılamadı.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
