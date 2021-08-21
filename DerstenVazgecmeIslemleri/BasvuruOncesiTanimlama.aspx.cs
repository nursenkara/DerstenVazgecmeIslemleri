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

        public void btnKaydet_Click(object sender, EventArgs e)
        {
            decimal gano;
            gano = Convert.ToDecimal(radTxtGano.Text);
            //???bu fonksiyonu ganoya değer girildikten sonra yazabilirsin kaydet butonuna basıldıktan sonra değil yani
            if (Convert.ToInt32(cmbBuyukturKucukturEsittir.SelectedItem.Value) == 1)//>
            {
                lblGanoSartiAciklamasi.Text = gano + " ortalamadan fazla olanlar başvurabilir.";
            }
            else if (Convert.ToInt32(cmbBuyukturKucukturEsittir.SelectedItem.Value) == 2)// <
            {
                lblGanoSartiAciklamasi.Text = gano + " ortalamadan az olanlar başvuramaz.";
            }
            else if (Convert.ToInt32(cmbBuyukturKucukturEsittir.SelectedItem.Value) == 3)// >=
            {
                lblGanoSartiAciklamasi.Text = gano + " ortalamadan büyük veya eşit olanlar başvurabilir.";
            }
            else if (Convert.ToInt32(cmbBuyukturKucukturEsittir.SelectedItem.Value) == 4)// <=
            {
                lblGanoSartiAciklamasi.Text = gano + " ortalamadan küçük veya eşit olanlar başvuramaz.";
            }


            DateTime ogrenciBasvuruBaslangicTarihi = radDateOgrenciBasvuruBaslangicTarihi.SelectedDate ?? new DateTime();
            DateTime ogrenciBasvuruBitisTarihi = radDateOgrenciBasvuruBitisTarihi.SelectedDate ?? new DateTime();
            DateTime danismanOnayBaslangicTarihi = radDateDanismanOnayBaslangicTarihi.SelectedDate ?? new DateTime();
            DateTime danismanOnayBitisTarihi = radDateDanismanOnayBitisTarihi.SelectedDate ?? new DateTime();
            
            int yil = yildonem_basvuru.Yil;
            int donem = yildonem_basvuru.Donem;
            int ayniAndaVazgecebilecegiDersSayisi = Convert.ToInt32(radAyniAndaVazgecebilecegiDersSayisi.Text);
            bool ayniDerstenFarkliDonemdeVazgecebilirMi = chkAyniDerstenFarkliDonemdeVazgecmeDurumu.Checked;

            OgrencininDersVazgecmeDTO ogrencininDersVazgecmeDTOsu = new OgrencininDersVazgecmeDTO();
            ogrencininDersVazgecmeDTOsu.Gano = gano;
            ogrencininDersVazgecmeDTOsu.OgrenciBasvuruBaslangicTarihi = ogrenciBasvuruBaslangicTarihi;
            ogrencininDersVazgecmeDTOsu.OgrenciBasvuruBitisTarihi = ogrenciBasvuruBitisTarihi;
            ogrencininDersVazgecmeDTOsu.DanismanOnayBaslangicTarihi = danismanOnayBaslangicTarihi;
            ogrencininDersVazgecmeDTOsu.DanismanOnayBitisTarihi = danismanOnayBitisTarihi;
            ogrencininDersVazgecmeDTOsu.Yil = yil;
            ogrencininDersVazgecmeDTOsu.Donem = donem;
            ogrencininDersVazgecmeDTOsu.AyniAndaVazgecebilecegiDersSayisi = ayniAndaVazgecebilecegiDersSayisi;
            ogrencininDersVazgecmeDTOsu.AyniDerstenBaskaDonemdeVazgecebilirMi = ayniDerstenFarkliDonemdeVazgecebilirMi;


            OgrenciUygulama.OgrenciIsleriSaveorUpdate(ogrencininDersVazgecmeDTOsu);

        }
    }
}
