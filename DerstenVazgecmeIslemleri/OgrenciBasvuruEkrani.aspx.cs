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
    public partial class OgrenciBasvuruEkrani : OgrenciBasePage<DerstenVazgecmeIslemleriUygulama>
    {
        #region Session'lar
        public List<int> VazgecilenDersler
        {
            get
            {
                return FromPageSession<List<int>>("VazgecilenDersler", null);
            }
            set
            {
                PageSession["VazgecilenDersler"] = value;
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

        public byte AyniAndaVazgecebilecegiDersSayisi
        {
            get
            {
                return FromPageSession<byte>("AyniAndaVazgecebilecegiDersSayisi", 0);
            }
            set
            {
                PageSession["AyniAndaVazgecebilecegiDersSayisi"] = value;
            }
        }


        public List<OgrencininDersVazgecmeDTO> OgrencininKesinKayitOlduguDerslerinListesi
        {
            get
            {
                return FromPageSession<List<OgrencininDersVazgecmeDTO>>("OgrencininKesinKayitOlduguDerslerinListesi", null);
            }
            set
            {
                PageSession["OgrencininKesinKayitOlduguDerslerinListesi"] = value;
            }
        }
        #endregion

        protected override int UygulamaID
        {
            get { return 10008101; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // geçici olarak bir aktif öğrenci alındı.
            // select * from ogrenci where Durum = 905002
            OgrenciId = "2239801"; // "2240155"; // "2264661"; // UnipaMaster.AuthenticatedUser.KullaniciProfil.EkBilgi1;
            AyniAndaVazgecebilecegiDersSayisi = OgrenciUygulama.GetAyniAndaVazgecebilecegiDersSayisi();
 
            OgrencininKesinKayitOlduguDerslerinListesi = getderslist();

            OgrencininDersVazgecmeDTO dto = new OgrencininDersVazgecmeDTO();
            

             //dto.OgrenciIslerininBelirledigiGano = OgrenciUygulama.OgrenciIslerininBelirledigiGano(ogrencininDersVazgecmeDtosu).ToDecimal() ?? new Decimal();


            if (VazgecilenDersler == null)
            {
                VazgecilenDersler = OgrenciUygulama.GetOgrencininDahaOncedenVazgectigiOgrenciDersIdler(OgrenciId);
                lblVazgecilecekDerslerGuncelle();
            }
            #region Öğrencinin ganosuna uygunsa genel paneli açılacak,uygun değilse ikinci uyari paneli açılacak
            //if (OgrenciUygulama.OgrenciGanosuEsitveBuyukse(dto,OgrenciId) && 
            //    OgrenciUygulama.OgrenciGanosuEsitveKucukse(dto,OgrenciId) &&
            //    OgrenciUygulama.OgrenciGanosuKucukse(dto, OgrenciId) &&
            //    OgrenciUygulama.OgrenciGanosuBuyukse(dto, OgrenciId))
            //{
            //    pnlGenel.Visible = true;
            //    pnlBasvuruKapaliUyarisi.Visible = false;
            //}
            //else
            //    pnlGanoUygunDegilUyarisi.Visible = true;

            #endregion

            #region Başvurular açıksa Genel paneli, kapalıysa Uyarı paneli açılacak.
            if (OgrenciUygulama.OgrenciBasvuruTarihleriArasindaBasvurmusMu())
            {
                pnlGenel.Visible = true;
                pnlBasvuruKapaliUyarisi.Visible = false;
            }
            else
            {
                pnlBasvuruKapaliUyarisi.Visible = true;
                pnlGenel.Visible = false;
            }
            #endregion

        }

        protected void grdOgrenci_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (OgrencininKesinKayitOlduguDerslerinListesi == null)
                    OgrencininKesinKayitOlduguDerslerinListesi = new List<OgrencininDersVazgecmeDTO>();
                grdOgrenci.DataSource = OgrencininKesinKayitOlduguDerslerinListesi;
            }
            catch (Exception ex)
            {
                ltlInfo.Text = HataGoster(ex.Message);
            }
        }

 

        private List<OgrencininDersVazgecmeDTO> getderslist()
        {
            return OgrenciUygulama.OgrencininAldigiDersleriListele(OgrenciId);
        }

        protected void grdOgrenci_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem gdi = (GridDataItem)e.Item;
                int ogrenciDersId = 0;
                int.TryParse(gdi.GetDataKeyValue("OgrenciDersId").ToString(), out ogrenciDersId);
                OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDtosu = new OgrencininDersVazgecmeDTO();
                ogrencininDersVazgecmeDtosu.OgrenciDersId = ogrenciDersId;
                 
                
                if (e.CommandName == "cnVazgec")
                {
                    if (ogrenciDersId != -1)
                    {
                        if (AyniAndaVazgecebilecegiDersSayisi > VazgecilenDersler.Count)
                        {
                            if (!VazgecilenDersler.Contains(ogrenciDersId) || !OgrenciUygulama.GetOgrencininDahaOncedenVazgectigiVeOnaylananOgrenciDersIdler(OgrenciId).Contains(ogrenciDersId) || !OgrenciUygulama.GetOgrencininDahaOncedenVazgectigiVeDanismanaGonderdigiOgrenciDersIdler(OgrenciId).Contains(ogrenciDersId)) // listede yoksa durumu 5 olanlarda, ONAYLANMADIYSA ve danışmana gönderilmedi
                            {
                                 
                                 VazgecilenDersler.Add(ogrenciDersId);
                                 lblVazgecilecekDerslerGuncelle();
                                 OgrenciUygulama.OgrenciVazgectiInsert(ogrencininDersVazgecmeDtosu);
                                 //OgrenciUygulama.OgrenciVazgectiLog(ogrencininDersVazgecmeDtosu.OgrenciDersId);

                                 if (OgrenciUygulama.GetOgrencininDahaOncedenVazgectigiVeDanismanaGonderdigiOgrenciDersIdler(OgrenciId).Contains(ogrenciDersId))
                                 {
                                     OgrenciUygulama.OgrenciVazgectiUpdate(ogrencininDersVazgecmeDtosu);
                                 }

                            }
                            else
                                ltlInfo.Text = UyariGoster("Ders vazgeçilmek üzere listeye eklenmiş.");
                            
                        }
                        else
                            ltlInfo.Text = UyariGoster(string.Format("Aynı anda vazgeçilebilecek ders sayısı en fazla {0} olmalıdır.", AyniAndaVazgecebilecegiDersSayisi));
                    }
                }
                else if (e.CommandName == "cnGeriAl")
                {
                    if (ogrenciDersId != -1)
                    {
                        int index = VazgecilenDersler.IndexOf(ogrenciDersId);
                        if (index > -1 && VazgecilenDersler.Count > 0)
                        {
                            VazgecilenDersler.RemoveAt(index);
                            lblVazgecilecekDerslerGuncelle();
                            OgrenciUygulama.OgrenciGeriAlInsert(ogrenciDersId);
                            //ogrencininDersVazgecmeDtosu.Durum = 4;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDanismanaGonder_Click(object sender, EventArgs e)
        {
            OgrencininDersVazgecmeDTO dto = new OgrencininDersVazgecmeDTO();
            
            if (VazgecilenDersler.Count > 0 && VazgecilenDersler.Count <= OgrenciUygulama.GetAyniAndaVazgecebilecegiDersSayisi().ToInt())
            {

                OgrenciDersGoruntulemeDTO oas = OgrenciUygulama.OgrenciAdiveSoyadiniGetir(OgrenciId);
                foreach (int ogrenciDersID in VazgecilenDersler)
                {
                   

                    string ekleyenGuncelleyenKisi = oas.OgrenciAd + " " + oas.OgrenciSoyad;

                    dto.IlkEkleyenKisi = ekleyenGuncelleyenKisi;
                    dto.GuncelleyenKisi = ekleyenGuncelleyenKisi;
                    dto.OgrenciDersId = ogrenciDersID;
                    

                    OgrenciUygulama.DanismanaGonderildiSaveOrUpdate(dto);



                }

                ltlInfo.Text = BilgiGoster("Dersler vazgeçilmek üzere danışmana gönderildi.");
            }
            else
            {
                ltlInfo.Text = UyariGoster("Vazgeçilecek ders sayısını aştınız.");
            }
        }

   

        private void lblVazgecilecekDerslerGuncelle()
        {
            lblVazgecilecekDersler.Text = "";
            if (VazgecilenDersler.Count > 0)
            {
                string odIds = "";
                foreach (int id in VazgecilenDersler)
                {
                    odIds += id.ToString() + ",";
                }
                odIds = odIds.Substring(0, odIds.Length - 1);
                //sondaki virgülü kaldırmak için
                string sql = @"
                        select
                            od.OgrenciDersId,
                            d.DersKodu,
                            d.DersAdi
                        from ogrenci o
                            inner join OgrenciDers od on od.OgrenciID = o.OgrenciID
                            inner join ders d on d.DersID = od.DersID
                        where
                            od.ogrenciDersId in ({0})
                ";
                DataTable dt = OgrenciMaster.Database.ExecuteDatatable(OgrenciMaster.Database.GetSqlStringCommand(string.Format(sql, odIds)));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow rw in dt.Rows)
                    {
                        lblVazgecilecekDersler.Text += rw["DersKodu"].ToString() + ": " + rw["DersAdi"].ToString() + "<br>";
                    }
                }
            }
        }
    }
}
