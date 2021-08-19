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
