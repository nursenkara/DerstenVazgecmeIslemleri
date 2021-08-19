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
          // OgrenciBasvuruEkrani.aspx: Sayfa ilk yüklendiğinde tüm geri al butonları disabled olacak.
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

        private List<OgrencininDersVazgecmeDTO> getderslist()
        {
            return OgrenciUygulama.OgrencininAldigiDersleriListele(1826127);//webconfig ten hangi kullanıcının girdiğini anlaman gerekir
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
                        //Öğrenci dersten vazgeçecek.
                        OgrenciUygulama.DersVazgecmeyiYapanOgrencininIlkKaydi(ogrenciDersId);
                    }
                    else
                    {
                        ltlInfo.Text = HataGoster("Vazgeçme işlemi yapılamadı.");
                    }
                }
                else if (e.CommandName == "cnGeriAl")
                {
                    //int ogrenciDersId = int.Parse(gdi.GetDataKeyValue("OgrenciDersId").ToString());
                    if (ogrenciDersId != -1)
                    {
                        
                    }
                    else
                    {
                        ltlInfo.Text = HataGoster("Geri alma işlemi yapılamadı.");
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
            
       
          
        }
    }
}
