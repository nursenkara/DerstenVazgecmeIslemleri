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
                PageSession["OgrencininKesinKayitOlduguDerslerinListesi"] = getderslist();
            }
        }



        protected override int UygulamaID
        {
            get { return 10008101; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string ogrenciId = UnipaMaster.AuthenticatedUser.KullaniciProfil.EkBilgi1;
            string sqlOgrenciDersIdyiBulma = string.Format(@"select od.OgrenciDersId from Ogrenci o inner join OgrenciDers od on o.OgrenciId=od.OgrenciId where o.OgrenciId = {0}",ogrenciId);
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
            grdOgrenci.Columns[3].Visible = false;
            
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
        private List<OgrenciDersGoruntulemeDTO> getDerstenVazgecenOgrencilerListesi()
        {
            string ogrenciId = UnipaMaster.AuthenticatedUser.KullaniciProfil.EkBilgi1;
      
            return OgrenciUygulama.DerstenVazgecenOgrencileriListele(ogrenciId);

        }
        private List<OgrencininDersVazgecmeDTO> getderslist()
        {
            string ogrenciId = UnipaMaster.AuthenticatedUser.KullaniciProfil.EkBilgi1;
 
            
            return OgrenciUygulama.OgrencininAldigiDersleriListele(ogrenciId);//webconfig ten hangi kullanıcının girdiğini anlaman gerekir
        }


        protected void grdOgrenci_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem gdi = (GridDataItem)e.Item;
                int ogrenciDersId = int.Parse(gdi.GetDataKeyValue("OgrenciDersId").ToString());

                if (e.CommandName == "cnVazgec")
                {
                    
                   
                    //UnipaSecurity.EncryptValue()


                    if (ogrenciDersId != -1)
                    {
                        grdOgrenci.Columns[3].Visible = false;//geri al false olacak
                        List<OgrenciDersGoruntulemeDTO> ogrenciDersGoruntulemeDTO = new List<OgrenciDersGoruntulemeDTO>();
                        ogrenciDersGoruntulemeDTO.Add(new OgrenciDersGoruntulemeDTO
                        { 
                        DersKodu = grdOgrenci.Columns.FindByDataField("DersKodu").ToString(),
                        DersAdi = grdOgrenci.Columns.FindByDataField("DersAdi").ToString()
                        });
                        //Öğrenci dersten vazgeçecek.
                        //
                    }
                    else
                    {
                        //ltlInfo.Text = HataGoster("Vazgeçme işlemi yapılamadı.");
                    }
                }
                else if (e.CommandName == "cnGeriAl")
                {
                    //int ogrenciDersId = int.Parse(gdi.GetDataKeyValue("OgrenciDersId").ToString());
                    if (ogrenciDersId != -1)
                    {
                        grdOgrenci.Columns[2].Visible = false;//vazgec false olacak
                        List<OgrenciDersGoruntulemeDTO> ogrenciDersGoruntulemeDTO = new List<OgrenciDersGoruntulemeDTO>();
                        ogrenciDersGoruntulemeDTO.Remove(new OgrenciDersGoruntulemeDTO
                        {
                            DersKodu = grdOgrenci.Columns.FindByDataField("DersKodu").ToString(),
                            DersAdi = grdOgrenci.Columns.FindByDataField("DersAdi").ToString()
                        });
                        
                    }
                    else
                    {
                        //ltlInfo.Text = HataGoster("Geri alma işlemi yapılamadı.");
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
    }
}
