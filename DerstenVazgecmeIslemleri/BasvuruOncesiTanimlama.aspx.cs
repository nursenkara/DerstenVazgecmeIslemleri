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
    public partial class BasvuruOncesiTanimlama : OgrenciBasePage<DerstenVazgecmeIslemleriUygulama>
    {
        /// <summary>
        /// XX-XXXX-XX formatında uygulama ID değeri.
        /// ProjeId-UygulamaNo-Ekran No(.aspx sayısı) olarak düşünülmeli.
        /// </summary>
        protected override int UygulamaID
        {
            get { return 10008101; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            yildonem_basvuru.DonemSelectedEventHandler += new UniOgrenci.Master.Web.UI.UserControls.YilDonemCombo.DonemSelected(yildonem_basvuru_DonemSelectedEventHandler);

        }

        protected void yildonem_basvuru_DonemSelectedEventHandler(int yil, int donem)
        {
        }
    }
}
