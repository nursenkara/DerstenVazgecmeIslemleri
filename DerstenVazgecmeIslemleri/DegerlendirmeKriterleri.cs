using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

namespace DerstenVazgecmeIslemleri
{
    [Serializable]
    public class DegerlendirmeKriterlerim
    {
        public DegerlendirmeKriterlerim()
        {

        }
        public int KodID
        {
            set;// { KodID = value; }
            get;// { return KodID; }
        }
        public string Aciklama
        {
            set;// { Aciklama = value; }
            get;// { return Aciklama; }
        }
        public double? Puan1
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? Puan2
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? Puan3
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? Puan4
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? Puan5
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? Puan6
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
    }
    [Serializable]
    public class DegerlendirmeKriterleriListem
    {
        public DegerlendirmeKriterleriListem()
        {

        }
        public DegerlendirmeKriterleriListem(DataTable _veriler)
        {
            DegerlendirmeKriterlerim _temp; ;

            if (this.Liste != null)
                this.Liste.Clear();
            else
                this.Liste = new List<DegerlendirmeKriterlerim>();

            for (int i = 0; i < _veriler.Rows.Count; ++i)
            {
                _temp = new DegerlendirmeKriterlerim();
                _temp.Aciklama = _veriler.Rows[i][1].ToString();
                _temp.KodID = Convert.ToInt32(_veriler.Rows[i][0].ToString());
                _temp.Puan1 = Convert.ToDouble(_veriler.Rows[i][2].ToString());
                _temp.Puan2 = Convert.ToDouble(_veriler.Rows[i][3].ToString());
                _temp.Puan3 = Convert.ToDouble(_veriler.Rows[i][4].ToString());
                _temp.Puan4 = Convert.ToDouble(_veriler.Rows[i][5].ToString());
                _temp.Puan5 = Convert.ToDouble(_veriler.Rows[i][6].ToString());
                _temp.Puan6 = Convert.ToDouble(_veriler.Rows[i][7].ToString());
                this.Liste.Add(_temp);
            }
        }
        public List<DegerlendirmeKriterlerim> Liste
        {
            set;
            get;
        }
    }

    [Serializable]
    public class GirisSinavPuanlarim
    {
        public GirisSinavPuanlarim()
        {

        }
        public int KodID
        {
            set;// { KodID = value; }
            get;// { return KodID; }
        }
        public string Aciklama
        {
            set;// { Aciklama = value; }
            get;// { return Aciklama; }
        }
        public double? Puan1
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? Puan2
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        public double? YabanciUyrukluPuanBaraji
        {
            set;// { PuanAgirligi = value; }
            get;// { return PuanAgirligi; }
        }
        /// <summary>
        /// Basvurunun alinmasi icin sinava ait gecerlilik tarihi
        /// </summary>
        public DateTime? GecerlilikTarihi { get; set; }
    }

    [Serializable]
    public class GirisSinavPuanlariListem
    {
        public GirisSinavPuanlariListem()
        {

        }
        public GirisSinavPuanlariListem(DataTable _veriler)
        {
            try
            {
                if (_veriler == null)
                {
                    return;
                }

                GirisSinavPuanlarim _temp;

                if (this.Liste != null)
                    this.Liste.Clear();
                else
                    this.Liste = new List<GirisSinavPuanlarim>();

                for (int i = 0; i < _veriler.Rows.Count; ++i)
                {
                    _temp = new GirisSinavPuanlarim();
                    _temp.Aciklama = _veriler.Rows[i][1].ToString();
                    _temp.KodID = Convert.ToInt32(_veriler.Rows[i][0].ToString());
                    _temp.Puan1 = Convert.ToDouble(_veriler.Rows[i][2].ToString());
                    _temp.Puan2 = Convert.ToDouble(_veriler.Rows[i][3].ToString());
                    _temp.YabanciUyrukluPuanBaraji = Convert.ToDouble(_veriler.Rows[i][4].ToString());
                    string TarihKontrol = _veriler.Rows[i][5].ToString();
                    if (!string.IsNullOrEmpty(TarihKontrol))
                        _temp.GecerlilikTarihi = Convert.ToDateTime(_veriler.Rows[i][5].ToString());
                    this.Liste.Add(_temp);
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
           
        }
        public List<GirisSinavPuanlarim> Liste
        {
            set;
            get;
        }
    }
}
