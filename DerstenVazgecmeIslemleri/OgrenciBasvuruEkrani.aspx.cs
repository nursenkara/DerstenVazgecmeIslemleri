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

        ///

        private List<OgrenciDersGoruntulemeDTO> DerstenVazgecenOgrenciler
        {
            get
            {
                return FromPageSession<List<OgrenciDersGoruntulemeDTO>>("DerstenVazgecenOgrenciListesi", null);
            }
            set
            {
                PageSession["DerstenVazgecenOgrenciListesi"] = getDerstenVazgecenOgrencilerListesi();
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
            OgrenciId = "2264661"; // UnipaMaster.AuthenticatedUser.KullaniciProfil.EkBilgi1;
            AyniAndaVazgecebilecegiDersSayisi = OgrenciUygulama.GetAyniAndaVazgecebilecegiDersSayisi();

            OgrencininKesinKayitOlduguDerslerinListesi = getderslist();

            if (VazgecilenDersler == null)
            {
                VazgecilenDersler = new List<int>();
            }

            string sqlOgrenciDersIdyiBulma = string.Format(@"select od.OgrenciDersId from Ogrenci o inner join OgrenciDers od on o.OgrenciId=od.OgrenciId where o.OgrenciId = {0}", OgrenciId);
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sqlOgrenciDersIdyiBulma);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);

            #region Başvurular açıksa Genel paneli, kapalıysa Uyarı paneli açılacak.
            string sqlBasvuruTarihiArasindaMi = "select * from DersVazgecmeAktivite where GetDate() between OgrenciBasvuruBaslangicTarihi and OgrenciBasvuruBitisTarihi";
            DbCommand cmdSelect = OgrenciMaster.Database.GetSqlStringCommand(sqlBasvuruTarihiArasindaMi);
            DataTable dtSelect = OgrenciMaster.Database.ExecuteDatatable(cmdSelect);
            bool basvurularAcik = dtSelect != null && dtSelect.Rows.Count > 0;
            pnlGenel.Visible = basvurularAcik;
            pnlUyari.Visible = !basvurularAcik;
            #endregion

            //OgrenciBasvuruEkrani.aspx: Vazgeç butonuna basılmışsa; geri al butonu aktif olacak vazgeç butonu pasif olacak.
            //OgrenciBasvuruEkrani.aspx: Geri Al butonuna basılmışsa; vazgeç butonu aktif olacak geri al butonu pasif olacak.

            // grdOgrenci.Columns[3].Visible = false; // visible false yapılmayacak. Buton disable yapılacak.
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

        protected void grdOgrenci_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {



            //if (e.Item is Telerik.Web.UI.GridTemplateColumn)
            //    {
            //        e.Item.Cells[3].Enabled = false;
            //    }



        }

        private List<OgrenciDersGoruntulemeDTO> getDerstenVazgecenOgrencilerListesi()
        {
            return OgrenciUygulama.DerstenVazgecenOgrencileriListele(OgrenciId);
        }

        private List<OgrencininDersVazgecmeDTO> getderslist()
        {
            return OgrenciUygulama.OgrencininAldigiDersleriListele(OgrenciId);//webconfig ten hangi kullanıcının girdiğini anlaman gerekir
        }

        protected void grdOgrenci_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem gdi = (GridDataItem)e.Item;
                int ogrenciDersId = 0;
                int.TryParse(gdi.GetDataKeyValue("OgrenciDersId").ToString(), out ogrenciDersId);

                if (e.CommandName == "cnVazgec")
                {
                    if (ogrenciDersId != -1)
                    {
                        if (AyniAndaVazgecebilecegiDersSayisi > VazgecilenDersler.Count)
                        {
                            if (!VazgecilenDersler.Contains(ogrenciDersId)) // listede yoksa ekle
                            {
                                VazgecilenDersler.Add(ogrenciDersId);
                                lblVazgecilecekDerslerGuncelle();
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
            if (VazgecilenDersler.Count > 0)
            {
                OgrenciDersGoruntulemeDTO oas = OgrenciUygulama.OgrenciAdiveSoyadiniGetir(OgrenciId);
                foreach (int ogrenciDersID in VazgecilenDersler)
                {
                    string ekleyenGuncelleyenKisi = oas.OgrenciAd + " " + oas.OgrenciSoyad;
                    OgrencininDersVazgecmeDTO dto = new OgrencininDersVazgecmeDTO();
                    dto.IlkEkleyenKisi = ekleyenGuncelleyenKisi;
                    dto.GuncelleyenKisi = ekleyenGuncelleyenKisi;
                    dto.OgrenciDersId = ogrenciDersID;
                    OgrenciUygulama.DerstenVazgecmeDurumSaveOrUpdate(dto);
                }
            }
        }

        protected void btnOnay_Click(object sender, EventArgs e)
        {
            //grdOgrenci.
            //OgrenciUygulama.DersVazgecmeyiYapanOgrencininIlkKaydi(ogrenciDersId);

        }
        //protected void btnVazgec_Click(object sender, EventArgs e)
        //{
        //    //grdOgrenci.Columns[3].Visible = false;//geri al false olacak


        //}
        //protected void btnGeriAl_Click(object sender, EventArgs e)
        //{

        //    //grdOgrenci.Columns[2].Visible = false;//vazgec false olacak

        //}

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
                ///
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
