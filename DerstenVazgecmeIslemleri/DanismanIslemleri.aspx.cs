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
        public List<OgrenciDersGoruntulemeDTO> DerstenVazgecenOgrencilerinListesi
        {
            get
            {
                return FromPageSession<List<OgrenciDersGoruntulemeDTO>>("DerstenVazgecenOgrencilerinListesi", null);
            }
            set
            {
                PageSession["DerstenVazgecenOgrencilerinListesi"] = getDerstenVazgecenlerinListesi();
            }
        }


        protected override int UygulamaID
        {
            get { return 10008101; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Örnek kullanım
            // OgrenciUygulama.DerstenVazgecmeKayitInsert(null, null, null);
        }

        protected void grdDanisman_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //List<OgrenciDTO> ogrList = new List<OgrenciDTO>();
            //ogrList.Add(new OgrenciDTO { Id = 1, No = 180601034, Adi = "Nur", Soyadi = "Şenkara", BasvurduguTarih = DateTime.Today });
            //ogrList.Add(new OgrenciDTO { Id = 2, No = 180601035, Adi = "Kamuran", Soyadi = "Karagöz", BasvurduguTarih = DateTime.Today });
            //ogrList.Add(new OgrenciDTO { Id = 3, No = 180601036, Adi = "Nilüfer", Soyadi = "Poyraz", BasvurduguTarih = DateTime.Today });
            //ogrList.Add(new OgrenciDTO { Id = 4, No = 180601037, Adi = "Gökhan", Soyadi = "Şenkara", BasvurduguTarih = DateTime.Today });
            //ogrList.Add(new OgrenciDTO { Id = 5, No = 180601038, Adi = "Mehmet Ali", Soyadi = "Poyraz", BasvurduguTarih = DateTime.Today });
            //ogrList.Add(new OgrenciDTO { Id = 6, No = 180601039, Adi = "Sevim", Soyadi = "Şenkara", BasvurduguTarih = DateTime.Today });
            //ogrList.Add(new OgrenciDTO { Id = 7, No = 180601040, Adi = "Bedrettin", Soyadi = "Şenkara", BasvurduguTarih = DateTime.Today });
            //grdDanisman.DataSource = ogrList;
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
        private List<OgrenciDersGoruntulemeDTO> getDerstenVazgecenlerinListesi()
        {
            return OgrenciUygulama.DerstenVazgecenOgrencileriListele(4159, 42082108);//webconfig ten hangi kullanıcının girdiğini anlaman gerekir
        }



        protected void grdDanisman_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem gdi = (GridDataItem)e.Item;
                int OgrenciDersId = int.Parse(gdi.GetDataKeyValue("OgrenciDersId").ToString());
                if (e.CommandName == "cnOnay")
                {
                    if (OgrenciDersId != -1)
                    {
                        
                    }
                    else
                    {
                        ltlInfo.Text = HataGoster("Onay işlemi yapılamadı.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
