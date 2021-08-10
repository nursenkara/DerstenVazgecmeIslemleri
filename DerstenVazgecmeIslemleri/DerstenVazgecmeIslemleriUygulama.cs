using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using UniOgrenci.Master;
using Unipa.Framework.UnipaMaster;
using UniOgrenci.Master.Entities;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using UniOgrenci.Master.Exceptions;
using System.Diagnostics;
using DerstenVazgecmeIslemleri.Resources.Lang;

namespace DerstenVazgecmeIslemleri
{
    /// <summary>
    /// Uygulamanın business logic'ini gerçekleştirecek olan sınıf.
    /// </summary>
    [Serializable]
    public partial class DerstenVazgecmeIslemleriUygulama : OgrenciUygulama
    {
        public DerstenVazgecmeIslemleriUygulama(UnipaMaster unipaMaster, int uygulamaId, int projeId) : base(unipaMaster, projeId, uygulamaId) { }
    }
}
