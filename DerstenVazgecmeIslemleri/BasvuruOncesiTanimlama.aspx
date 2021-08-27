<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasvuruOncesiTanimlama.aspx.cs"
    Inherits="DerstenVazgecmeIslemleri.BasvuruOncesiTanimlama" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="UniOgrenci.Master.Web.UI" Namespace="UniOgrenci.Master.Web.UI.UserControls"
    TagPrefix="cc3" %>
<%@ Register Assembly="Unipa.Framework.Web.UI" Namespace="Unipa.Framework.Web.UI.UserControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="Unipa.Framework.Web.UI" Namespace="Unipa.Framework.Web.UI"
    TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="Resources/Style/DerstenVazgecmeIslemleri.css" />
</head>
<body>

    <script language="Javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <script>
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

    <form id="form1" runat="server">
    <cc1:AppHeader ID="AppHeaderBasvuruOncesiTanimlama" runat="server" meta:resourcekey="AppHeaderBasvuruOncesiTanimlama"
        Text="" UygulamaIDGoster="True" />
    <cc2:JsLocalizer ID="JsLocalizerBasvuruOncesiTanimlama" runat="server" AssemblyName="DerstenVazgecmeIslemleri"
        CacheResult="True" ResourcePath="Resources.Lang.DerstenVazgecmeIslemleriResources"
        meta:resourcekey="JsLocalizerBasvuruOncesiTanimlamaResource1" />
    <asp:ScriptManager ID="ScriptManagerBasvuruOncesiTanimlama" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="ltlInfo" runat="server" EnableViewState="False" meta:resourcekey="ltlInfoResource1"></asp:Literal>
    <table border="1" width="100%">
        <tr>
            <td width="40%">
                <div>
                    <asp:Label ID="lblGano" runat="server" Text="Gano:" meta:resourcekey="lblGanoResource1"></asp:Label>
                    <telerik:RadComboBox ID="cmbBuyukturKucukturEsittir" Width="75px" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seçiniz..." Value="0" />
                            <telerik:RadComboBoxItem Text=">" Value="1" />
                            <telerik:RadComboBoxItem Text="<" Value="2" />
                            <telerik:RadComboBoxItem Text="&gt;=" Value="3" />
                            <telerik:RadComboBoxItem Text="&lt;=" Value="4" />
                        </Items>
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                    <telerik:RadTextBox ID="radTxtGano" Width="50px" runat="server" MaxLength="40" onkeypress="return onlyDotsAndNumbers(this,event);"
                        LabelCssClass="" meta:resourcekey="txt_Gano">
                    </telerik:RadTextBox>
                    <br />
                    <asp:Label ID="lblBilgi" runat="server" Text="Öğrencinin genel ağırlıklı not ortalamasına göre öğrenci başvuru ekranı açılacak. Belirlenen ganodan küçükse açılmayacak.Çünkü belirlenen ganodan düşük olanlar derslerden vazgeçemeyecekler."
                        meta:resourcekey="lblBilgiResource1"></asp:Label>
                    <br />
                    <asp:Label ID="lblGanoSartiAciklamasi" runat="server" Text="" meta:resourcekey="lblGanoSartiAciklamaResource1"></asp:Label>
                    <br />
                </div>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblOgrenciBasvuruBaslangicTarihi" runat="server" Text="Öğrenci Başvuru Başlangıç Tarihi: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="radDateOgrenciBasvuruBaslangicTarihi" runat="server"
                                Width="170px" TimeView-Enabled="true" TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOgrenciBasvuruBitisTarihi" runat="server" Text="Öğrenci Başvuru Bitiş Tarihi: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="radDateOgrenciBasvuruBitisTarihi" runat="server" Width="170px"
                                TimeView-Enabled="true" TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDanismanOnayBaslangicTarihi" runat="server" Text="Danışman Onay Başlangıç Tarihi: "></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="radDateDanismanOnayBaslangicTarihi" runat="server"
                                Width="170px" TimeView-Enabled="true" TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDanismanOnayBitisTarihi" runat="server" Text="Danışman Onay Bitiş Tarihi:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="radDateDanismanOnayBitisTarihi" runat="server" Width="170px"
                                TimeView-Enabled="true" TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cc3:YilDonemCombo Width="350px" ID="yildonem_basvuru" runat="server" Direction="Horizontal"
                                LabelDonem="Başvuru Dönemi" LabelYil="Başvuru Yılı" ShowDonem="True" OnDonemSelectedEventHandler="yildonem_basvuru_DonemSelectedEventHandler"
                                AutoSelectYilDonem="False" Donem="-1" FixPopupIndex="False" meta:resourcekey="yildonem_basvuruResource1"
                                Yil="-1" Height="71px" />
                        </td>
                    </tr>
                </table>
                <div>
                </div>
                <br />
                <div>
                </div>
                <br />
                <div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div>
                    <asp:Label ID="lblAyniAndaVazgecebilecegiDersSayisi" runat="server" Text="Aynı anda kaç dersten vazgeçebilir? :"></asp:Label>
                    <telerik:RadTextBox ID="radAyniAndaVazgecebilecegiDersSayisi" Width="50px" runat="server"
                        MaxLength="40" onkeypress="return isNumberKey(event)" LabelCssClass="" meta:resourcekey="txt_AyniAndaVazgecebilecegiDersSayisi">
                    </telerik:RadTextBox>
                    <!------label textbox-->
                </div>
                <br />
                <div>
                    <asp:Label ID="lblAyniDerstenFarkliDonemdeVazgecmeDurumu" runat="server" Text="Aynı dersten başka dönemde tekrar vazgeçme başvurusu yapabilir mi? :"></asp:Label>
                    <asp:CheckBox runat="server" ID="chkAyniDerstenFarkliDonemdeVazgecmeDurumu" AutoPostBack="true" />
                    <!------label checkbox-->
                </div>
                <br />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div>
        <asp:Button ID="btnKaydet" CommandName="cnKaydet" OnClick="btnKaydet_Click" runat="server"
            Text="KAYDET" Visible="true"></asp:Button>
             <asp:Button ID="btnGuncelle" CommandName="cnGuncelle" OnClick="btnGuncelle_Click" runat="server"
            Text="GÜNCELLE" Visible="true"></asp:Button>
    </div>
    </br>
    <asp:Panel ID="pnlKayitlariGoster" runat="server" meta:resourcekey="pnl_kayitResource1"
        Style="margin: 0 auto; width: 960px;">
        <table width="100%">
            <tr>
                <td>
                    <fieldset runat="server">
                        <legend>
                            <asp:Label ID="lblKayitlar" runat="server" Text="Geçmiş Yıl-Dönemdeki Öğrenci İşleri Kayıtları"
                                meta:resourcekey="lbl_ogenciIsleri_field"></asp:Label>
                        </legend>
                        <asp:Panel runat="server" Visible="true" ID="pnlBilgiGoster">
                            <table width="100%">
                    <tr>
                    </br>
                     <asp:Button ID="btnListele" CommandName="cnListele" runat="server" Text="Listele"
            Visible="true" OnClick="btnListele_Click"></asp:Button>
                    </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="grdOgrenciIsleri" runat="server" AllowPaging="True" Width="920"
                                            AutoGenerateColumns="False" meta:resourcekey="RadGridOgrenciIsleriResource" PageSize="100"
                                            GridLines="None" CellSpacing="0" ShowGroupPanel="false" ShowStatusBar="false"
                                            OnNeedDataSource="grdOgrenciIsleri_NeedDataSource">
                                            <MasterTableView AllowSorting="true" TableLayout="Fixed" CommandItemDisplay="Top"
                                                AllowMultiColumnSorting="false" ShowFooter="false" Font-Names="Tahoma,Geneva,FreeSans,Helvetica,sans-serif" DataKeyNames="DersVazgecmeAktiviteId" 
                                                Font-Size="8">
                                                <CommandItemSettings ExportToPdfText="PDF olarak Yazdır" ExportToExcelText="Excel olarak Yazdır"
                                                    ShowExportToPdfButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false"
                                                    ShowRefreshButton="false" ShowExportToCsvButton="false"></CommandItemSettings>
                                                <NoRecordsTemplate>
                                                    <div style="padding: 8px">
                                                        <asp:Label ID="lblKayitBulunamadi" runat="server" Text="Kayıt Bulunamadı"></asp:Label>
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Yil" HeaderText="Yıl" UniqueName="Yil" ItemStyle-Width="150px"
                                                        HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                                                    <telerik:GridBoundColumn DataField="Donem" HeaderText="Dönem" UniqueName="Donem"
                                                        ItemStyle-Width="150px" HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                                                    <telerik:GridBoundColumn DataField="OgrenciBasvuruBaslangicTarihi" HeaderText="Öğrenci Başvuru Başlangıç Tarihi" DataFormatString="{0:dd.MM.yyyy}"
                                                        UniqueName="OgrenciBasvuruBaslangicTarihi" ItemStyle-Width="150px" HeaderStyle-Width="150px"
                                                        FilterControlWidth="100" ItemStyle-Wrap="true" />
                                                    <telerik:GridBoundColumn DataField="OgrenciBasvuruBitisTarihi" HeaderText="Öğrenci Başvuru Bitiş Tarihi" DataFormatString="{0:dd.MM.yyyy}"
                                                        UniqueName="OgrenciBasvuruBitisTarihi" ItemStyle-Width="150px" HeaderStyle-Width="150px"
                                                        FilterControlWidth="100" ItemStyle-Wrap="true" />
                                                    <telerik:GridBoundColumn DataField="DanismanOnayBaslangicTarihi" HeaderText="Danışman Onay Başlangıç Tarihi" DataFormatString="{0:dd.MM.yyyy}"
                                                        UniqueName="DanismanOnayBaslangicTarihi" ItemStyle-Width="150px" HeaderStyle-Width="150px"
                                                        FilterControlWidth="100" ItemStyle-Wrap="true" />
                                                    <telerik:GridBoundColumn DataField="DanismanOnayBitisTarihi" HeaderText="Danışman Onay Bitiş Tarihi" DataFormatString="{0:dd.MM.yyyy}"
                                                        UniqueName="DanismanOnayBitisTarihi" ItemStyle-Width="150px" HeaderStyle-Width="150px"
                                                        FilterControlWidth="100" ItemStyle-Wrap="true" />
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="false" ReorderColumnsOnClient="false"
                                                AllowDragToGroup="false" AllowColumnsReorder="false">
                                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
                                                <Scrolling AllowScroll="true" ScrollHeight="" UseStaticHeaders="true" />
                                                <Resizing AllowColumnResize="true" />
                                                <ClientEvents OnGridCreated="onGridCreated" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>

    <script>
    window.addEventListener("load", _ => {document.title = "Başvuru Öncesi Tanımlama"});
    </script>

</body>
</html>
