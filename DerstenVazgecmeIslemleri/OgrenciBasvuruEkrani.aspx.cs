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
        protected override int UygulamaID
        {
            get { return 10008101; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdOgrenci_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<DersDTO> dersList = new List<DersDTO>();
            dersList.Add(new DersDTO { DersAdi = "Yazılım Mimari ve Tasarımı", DersKodu = 1 });
            dersList.Add(new DersDTO { DersAdi = "Nümerik Analiz", DersKodu = 2 });
            dersList.Add(new DersDTO { DersAdi = "Diferansiyel Denklemler", DersKodu = 3 });
            dersList.Add(new DersDTO { DersAdi = "Matematik 2", DersKodu = 4 });
            grdOgrenci.DataSource = dersList;
        }


        protected void grdOgrenci_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem gdi = (GridDataItem)e.Item;
                int DersKodu = int.Parse(gdi.GetDataKeyValue("DersKodu").ToString());
                if (e.CommandName == "cnVazgec")
                {

                }
                else if (e.CommandName == "cnGeriAl")
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
