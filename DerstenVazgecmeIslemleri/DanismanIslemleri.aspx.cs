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


        protected override int UygulamaID
        {
            get { return 10008101; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Danisman = "Danışman Adı";
            #region Danışman Onaylama işlemi açıksa Genel paneli, kapalıysa Uyarı paneli açılacak.
            string sql = "select * from DersVazgecmeAktivite where GetDate() between DanismanOnayBaslangicTarihi and DanismanOnayBitisTarihi";
            DbCommand cmd = OgrenciMaster.Database.GetSqlStringCommand(sql);
            DataTable dt = OgrenciMaster.Database.ExecuteDatatable(cmd);
            bool danismanOnaylamaAcik = dt != null && dt.Rows.Count > 0;
            pnlGenel.Visible = danismanOnaylamaAcik;
            pnlUyari.Visible = !danismanOnaylamaAcik;
            #endregion
        }

        protected void grdDanisman_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                grdDanisman.DataSource = OgrenciUygulama.DerstenVazgecenOgrencileriListele();
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
                        dto.GuncelleyenKisi = Danisman;
                        OgrenciUygulama.DersVazgecmeDanismaninOnayGuncellemesi(dto);
                        grdDanisman.Rebind();
                        ltlInfo.Text = BilgiGoster("Vazgeçme onaylandı.");
                    }
                    else
                        ltlInfo.Text = HataGoster("Onay işlemi yapılamadı.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
