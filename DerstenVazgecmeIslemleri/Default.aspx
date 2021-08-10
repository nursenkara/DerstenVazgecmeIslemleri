<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DerstenVazgecmeIslemleri._Default"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="UniOgrenci.Master.Web.UI" Namespace="UniOgrenci.Master.Web.UI.UserControls"
    TagPrefix="cc3" %>
<%@ Register Assembly="Unipa.Framework.Web.UI" Namespace="Unipa.Framework.Web.UI.UserControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="Unipa.Framework.Web.UI" Namespace="Unipa.Framework.Web.UI"
    TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 92px;
        }
        .d0
        {
            background-color: #d5dbdb;
            color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:AppHeader ID="AppHeader1" runat="server" meta:resourcekey="AppHeader1Resource1"
        Text="" UygulamaIDGoster="True" />
    <cc2:JsLocalizer ID="JsLocalizer1" runat="server" AssemblyName="DerstenVazgecmeIslemleri" CacheResult="True"
        ResourcePath="Resources.Lang.DerstenVazgecmeIslemleriResources" meta:resourcekey="JsLocalizer1Resource1" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="ltlInfo" runat="server" EnableViewState="False" meta:resourcekey="ltlInfoResource1"></asp:Literal>
    <div style="margin-left: 50px; margin-top: 25px; width: 500px;">
        <asp:Panel ID="pnlBasvuruSecimi" runat="server" Visible="False" meta:resourcekey="pnlBasvuruSecimiResource1">
            <asp:Label ID="lblBasvuruSecimi" Text="Başvuru Seçimi Yapınız :" runat="server" meta:resourcekey="lblBasvuruSecimiResource1"></asp:Label>
            <asp:DropDownList ID="cmbBasvuruSecimi" Width="350px" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="cmbBasvuruSecimi_SelectedIndexChanged" meta:resourcekey="cmbBasvuruSecimiResource1">
            </asp:DropDownList>
        </asp:Panel>
    </div>
    <div>
        <table width="100%">
            <tr>
                <td valign="top" style="width: 250px">
                    <table>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="pnlOrganizasyonSecimi" meta:resourcekey="pnlOrganizasyonSecimiResource1">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lbl_org" runat="server" Text="Program Seçimi" meta:resourcekey="lbl_orgResource1"></asp:Label>
                                        </legend>
                                        <cc3:Organization ID="organizasyon_" GetActives="true" OnProgramSelectedEventHandler="organizasyon_ProgramSelectedEventHandler"
                                            OnOrganizasyonSelectedEventHandler="organizasyon_orgSelectedEventHandler" runat="server"
                                            Level="4" AltBirimLabel="Alt Birim: " BirimLabel="Birim: " CmbAltBirimEnabled="True"
                                            LabelWidths="220" ComboboxWidths="200" CmbBirimEnabled="True" CmbUstBirimEnabled="True"
                                            Direction="Vertical" EnableAltBirim="True" EnableBirim="True" EnableProgram="True"
                                            EnableUstBirim="True" EventLevel="0" OkulTuru="50004" FixPopupIndex="False" IsYetkiEnabled="True"
                                            meta:resourcekey="organizasyon_Resource1" ProgramLabel="Program: " SelectedOrganizationId="-1"
                                            SelectedProgramId="-1" UstBirimLabel="Üst Birim: " Width="100%" />
                                    </fieldset>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" ID="panelUstBirimOrganizasyonUC" Enabled="false" GroupingText="Program Seçimi"
                                    Visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <cc3:Organization ID="Organizasyon1" runat="server" Level="1" EventLevel="4" UstBirimLabel="Enstitü : "
                                                    AltBirimLabel="Alt Birim: " BirimLabel="Birim: " CmbAltBirimEnabled="True" CmbBirimEnabled="True"
                                                    CmbUstBirimEnabled="True" Direction="Vertical" EnableAltBirim="True" EnableBirim="True"
                                                    EnableProgram="True" EnableUstBirim="True" FixPopupIndex="False" GetActives="True"
                                                    ComboboxWidths="160" LabelWidths="200" IsYetkiEnabled="True" OkulTuru="50004"
                                                    ProgramLabel="Program: " SelectedOrganizationId="-1" SelectedProgramId="-1" Width="100%"
                                                    OnOrganizasyonSelectedEventHandler="Organizasyon1_OrganizasyonSelectedEventHandler" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblProgramTuru" runat="server" Text="Program Türü :" meta:resourcekey="lblProgramTuruResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbProgramTuru" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbProgramTuru_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlUcretDurumu" runat="server" Visible="false" GroupingText="Ücret Durumu Seçimi"
                                    meta:resourcekey="lblUcretDurumuSecimi">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblUcretliBurslu" runat="server" Text="Ücret Durumu :" Widthte="55px"
                                                    meta:resourcekey="lblUcretDurumu"></asp:Label>
                                            </td>
                                            <td>
                                                <cc3:KodCombo ID="kodcomboUcretDurumu" runat="server" KodGrupId="162" SelectedKodId="-1"
                                                    AutoPostBack="true" Sort="False" Text="" OnSelectedIndexChanged="kodcomboUcretDurumu_SelectedIndexChanged" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Panel runat="server" ID="pnlChk" GroupingText="Üst Birim Bazında Seçim" meta:resourcekey="ustBirimBazindaSecimPanelResource1"
                                    Visible="false">
                                    <asp:Label Text="Üst Birim Bazında :" ID="dd" runat="server" meta:resourcekey="UstBirimbazindaTextResource1"></asp:Label>
                                    <asp:CheckBox runat="server" ID="chkUstBirimBazinda" AutoPostBack="true" OnCheckedChanged="chkUstBirimBazinda_CheckedChanged" />
                                    <asp:Panel ID="pnlUp" runat="server">
                                        <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="2000" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                        <asp:Label ID="lblProgress" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label2" runat="server" Text="Genel Bilgiler" meta:resourcekey="Label2Resource1"></asp:Label>
                                    </legend>
                                    <asp:Panel ID="pnl_YilDonem" runat="server" Width="100%" meta:resourcekey="pnl_YilDonemResource1">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <cc3:YilDonemCombo Width="300px" ID="yildonem_basvuru" runat="server" Direction="Horizontal"
                                                        LabelDonem="Başvuru Dönemi" LabelYil="Başvuru Yılı" ShowDonem="True" OnDonemSelectedEventHandler="yildonem_basvuru_DonemSelectedEventHandler"
                                                        AutoSelectYilDonem="False" Donem="-1" FixPopupIndex="False" meta:resourcekey="yildonem_basvuruResource1"
                                                        Yil="-1" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="ChkTurkYabanciOrtak" AutoPostBack="true"  Visible="false" Checked="true" 
                                                    OnCheckedChanged="ChkTurkYabanciOrtak_OnCheckedChanged" />
                                                    <asp:Label Text="Yabancı ve Türk Uyruklu Öğrenciler için Ortak Kontenjan Kullanılsın" ID="lblTurkYabanciOrtak" runat="server"  
                                                    Visible="false" meta:resourcekey="lblTurkYabanciOrtakResource1"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Panel ID="pnlKaynaktanAktar" runat="server">
                                                        <fieldset>
                                                            <legend>
                                                                <asp:Label ID="Label15" runat="server" Text="Kaynak Dönem Seçimi" meta:resourcekey="Label15Resource1"></asp:Label>
                                                            </legend>
                                                            <cc3:YilDonemCombo Width="300px" ID="kaynakYilDonem" runat="server" Direction="Horizontal"
                                                                LabelDonem="Kaynak Dönemi" LabelYil="Kaynak Yılı" ShowDonem="True" AutoSelectYilDonem="False"
                                                                Donem="-1" FixPopupIndex="False" meta:resourcekey="yildonem_kaynakResource1"
                                                                Yil="-1" />
                                                            <telerik:RadButton ID="btnOncekiDonemdenAktar" runat="server" Text="Kaynak Dönemden Aktar"
                                                                OnClick="OncekiDonemdenAktar_Click">
                                                            </telerik:RadButton>
                                                        </fieldset>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_genel" runat="server" Enabled="False" meta:resourcekey="pnl_genelResource1">
                                        <table>
                                            <tr class="d0">
                                                <td>
                                                    <asp:Label ID="lbl_baslama_tarihi" runat="server" Text="Başvuru Başlama Tarihi:"
                                                        meta:resourcekey="lbl_baslama_tarihiResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="date_baslama_tarihi" runat="server" Width="170px"
                                                        TimeView-Enabled="true" TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                                        DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)" meta:resourcekey="date_baslama_tarihiResource1">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_bitis_tarihi" runat="server" Text="Başvuru Bitiş Tarihi :" meta:resourcekey="lbl_bitis_tarihiResource1"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <telerik:RadDateTimePicker ID="date_bitis_tarihi" runat="server" Width="170px" TimeView-Enabled="true"
                                                        TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                                        DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)" meta:resourcekey="date_bitis_tarihiResource1">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_obaslama_tarihi" runat="server" Text="Onay Başlama Tarihi :" meta:resourcekey="lbl_obaslama_tarihiResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="date_obaslama_tarihi" runat="server" Width="170px"
                                                        TimeView-Enabled="true" TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                                        DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)" meta:resourcekey="date_obaslama_tarihiResource1">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_obitis_tarihi" runat="server" Text="Onay Bitiş Tarihi :" meta:resourcekey="lbl_obitis_tarihiResource1"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadDateTimePicker ID="date_obitis_tarihi" runat="server" Width="170px" TimeView-Enabled="true"
                                                        TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                                        DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)" meta:resourcekey="date_obitis_tarihiResource1">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                            <tr class="d0">
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" Text="Türk Uyr. Öğrenciler Alan İçi :" meta:resourcekey="LabelTUResource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_turk_kontenjan" Width="50px" Value="0" runat="server"
                                                        MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" OnTextChanged="txt_turk_kontenjan_TextChanged"
                                                        Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_turk_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_yabanci_kontenjan" runat="server" Text="Yabancı Uyr. Öğrenciler Alan İçi :"
                                                        Width="260px" meta:resourcekey="lbl_yabanci_kontenjanAResource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_yabanci_kontenjan" Width="50px" Value="0" runat="server"
                                                        MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" OnTextChanged="txt_yabanci_kontenjan_TextChanged"
                                                        Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_yabanci_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_kontenjan" runat="server" Text="Toplam kontenjan :" meta:resourcekey="lbl_kontenjanResource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_kontenjan" Width="50px" runat="server" Value="0"
                                                        Enabled="False" MaxValue="999" MinValue="0" MaxLength="4" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="txt_turk_kontenjan_alan_disi_LBL" runat="server" Text="Türk Uyr. Öğrenciler Alan Dışı :"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_turk_kontenjan_alan_disi" Width="50px" Value="0"
                                                        runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                        OnTextChanged="txt_turk_kontenjan_TextChanged" Culture="Turkish (Turkey)" LabelCssClass=""
                                                        meta:resourcekey="txt_turk_kontenjan_alan_disiResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="txt_yabanci_kontenjan_alan_disi_LBL" runat="server" Text="Yabancı Uyr. Öğrenciler Alan Dışı :"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_yabanci_kontenjan_alan_disi" Width="50px" Value="0"
                                                        runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                        OnTextChanged="txt_yabanci_kontenjan_TextChanged" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_yabanci_kontenjan_alan_disiResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_arastirma_gorevlisi_kontenjan" runat="server" Text="Araştırma Görevlisi Kontenjanı :"
                                                        Visible="False" meta:resourcekey="lbl_arastirma_gorevlisi_kontenjanResource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_arastirma_gorevli_kontenjan" Width="50px" Value="0"
                                                        runat="server" MaxValue="999" MinValue="0" MaxLength="4" Visible="False" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_arastirma_gorevli_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr class="d0" id="trMilliSporcu" runat="server" visible="false">
                                                <td>
                                                    <asp:Label ID="lbl_milli_sporcu_alan_ici_kontenjan" runat="server" Text="Türk Uyruklu Milli Sporcu Alan İçi :" 
                                                        meta:resourcekey="LabelMilliSporcuAlanIciResource1" Visible ="false" ></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_milli_sporcu_alan_ici_kontenjan" Width="50px" Value="0" runat="server"
                                                      MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" Visible="false"
                                                        Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_milli_sporcu_alan_ici_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_milli_sporcu_alan_disi_kontenjan" runat="server" Text="Türk Uyruklu Milli Sporcu Alan Dışı :"
                                                        Width="260px" meta:resourcekey="LabelMilliSporcuAlanDisiResource1" Visible="false" ></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_milli_sporcu_alan_disi_kontenjan" Width="50px" Value="0" runat="server"
                                                        MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" Visible="false"
                                                        Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_milli_sporcu_alan_disi_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            
                                            <tr class="d0">
                                                <td align="left" style="text-align: left">
                                                    <asp:Label ID="txt_yedek_kontenjan_turk_alan_disi_LBL" runat="server" Text="Yedek Turk Uyr. Öğrenci Kontenjanı Alan Dışı:"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_turk_alan_disi" Width="50px" Value="0"
                                                        runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                        OnTextChanged="txt_yedek_kontenjan_turk_TextChanged" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_yedek_kontenjan_turk_alan_disiResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="txt_yedek_kontenjan_yabanci_alan_disi_LBL" runat="server" Text="Yedek Yabancı Uyr Öğrenci Kontenjanı Alan Dışı :"></asp:Label>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_yabanci_alan_disi" Width="50px"
                                                        Value="0" runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                        OnTextChanged="txt_yedek_kontenjan_yabanci_TextChanged" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_yedek_kontenjan_yabanci_alan_disiResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Yedek Turk Uyr Öğrenci Kontenjanı Alan İçi:"
                                                        meta:resourcekey="Label8Resource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_turk" Width="50px" Value="0" runat="server"
                                                        MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" OnTextChanged="txt_yedek_kontenjan_turk_TextChanged"
                                                        Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_yedek_kontenjan_turkResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label6" runat="server" Text="Yedek Yabancı Uyr Öğrenci Kontenjanı Alan İçi :"
                                                        meta:resourcekey="Label6Resource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_yabanci" Width="50px" Value="0"
                                                        runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                        OnTextChanged="txt_yedek_kontenjan_yabanci_TextChanged" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_yedek_kontenjan_yabanciResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="Label5" runat="server" Text="Toplam Yedek kontenjan :" meta:resourcekey="Label5Resource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_yedek_kontenjan" Width="50px" runat="server" Value="0"
                                                        Enabled="False" MaxValue="999" MinValue="0" MaxLength="4" Culture="Turkish (Turkey)"
                                                        LabelCssClass="" meta:resourcekey="txt_yedek_kontenjanResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr class="d0">
                                                <td>
                                                    <asp:Label ID="lbl_min_mezuniyet_notu4" runat="server" Text="Min. Mezuniyet Notu(4'lük sistem) :"
                                                        meta:resourcekey="lbl_min_mezuniyet_notu4Resource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_mezuniyet4" Width="50px" runat="server" Value="0"
                                                        MaxValue="4" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_min_mezuniyet4Resource1">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_min_mezuniyet_notu100" runat="server" Text="Min. Mezuniyet Notu(100'lük sistem) :"
                                                        meta:resourcekey="lbl_min_mezuniyet_notu100Resource1"></asp:Label>
                                                </td>
                                                <td colspan="3" align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_mezuniyet100" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass=""
                                                        meta:resourcekey="txt_min_mezuniyet100Resource1">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_min_yl_mezuniyet_notu4" runat="server" Text="Min. Y.L. Mezuniyet Notu(4'lük sistem) :"
                                                        meta:resourcekey="lbl_min_yl_mezuniyet_notu4Resource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_yl_mezuniyet4" Width="50px" runat="server"
                                                        Value="0" MaxValue="4" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass=""
                                                        meta:resourcekey="txt_min_yl_mezuniyet4Resource1">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lbl_min_yl_mezuniyet_notu100" runat="server" Text="Min. Y.L. Mezuniyet Notu(100'lük sistem) :"
                                                        meta:resourcekey="lbl_min_yl_mezuniyet_notu100Resource1"></asp:Label>
                                                </td>
                                                <td colspan="2" align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_yl_mezuniyet100" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass=""
                                                        meta:resourcekey="txt_min_yl_mezuniyet100Resource1">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr class="d0">
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Min. Başarı Notu :" meta:resourcekey="Label4Resource1"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_basari_notu" Width="50px" runat="server" Value="0"
                                                        MaxValue="100" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass="" meta:resourcekey="txt_min_basari_notuResource1">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblMinMulakatNotu" runat="server" Text="Min. Mülakat Notu :" meta:resourcekey="lblMinMulakatNotuResource1"></asp:Label>
                                                </td>
                                                <td colspan="3" align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_mulakat_notu" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass=""
                                                        meta:resourcekey="txt_min_mulakat_notuResource1">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%-- MinBilimselDegerlendirmeNotu--%>
                                                <td style="text-align: left">
                                                    <asp:Label ID="Label14" runat="server" Text="Min. Bilimsel Değerlendirme Notu :"
                                                        meta:resourcekey="lblMinBilimselDegerlendirmeNotuResource1"></asp:Label>
                                                </td>
                                                <td style="text-align: left" align="left">
                                                    <telerik:RadNumericTextBox ID="txt_min_bilimsel_notu" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass="">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr class="d0">
                                                <td colspan="2">
                                                    <asp:Label ID="Label13" runat="server" Text="Yabancı Uyruklu Öğrenciler İçin Seçilebilecek Minimum Dil Sayısı :"
                                                        meta:resourcekey="Label13Resource1"></asp:Label>
                                                </td>
                                                <td colspan="4">
                                                    <telerik:RadNumericTextBox ID="txtNumYabanciOgrenciDilSecimSayisi" Width="50px" runat="server"
                                                        Value="1" MaxValue="3" MinValue="0" Culture="Turkish (Turkey)" LabelCssClass=""
                                                        meta:resourcekey="txt_min_mulakat_notuResource1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="20px" valign="middle" colspan="1">
                                                    <asp:CheckBox ID="ChkResimZorunlu" runat="server" Text="Resim Zorunlu : " TextAlign="Left"
                                                        meta:resourcekey="ChkResimZorunluResource1" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkMezunOlmayaniAl" runat="server" Text="Mezun Olmayanı Al : "
                                                        TextAlign="Left" meta:resourcekey="ChkMezunOlmayaniAlResource1" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="chkBasvurudaBarajNotlariGozuksun" runat="server" Text="Başvuruda Baraj Notları Kontrol Edilsin :"
                                                        TextAlign="Left" meta:resourcekey="ChkBasvurudaBarajNotlariKontroluResource1" />
                                                </td>
                                                <td height="20px" valign="middle">
                                                    <asp:CheckBox ID="chkMulakatYok" runat="server" Text="Mülakat Yok : " TextAlign="Left"
                                                        meta:resourcekey="ChkMulakatYokResource1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <%--<table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkBelgeDiploma" runat="server" Text="Diploma/Mezuniyet Belgesi"
                                                                    TextAlign="Left" Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkTranskript" runat="server" Text="Transkript" TextAlign="Left"
                                                                    Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkAles" runat="server" Text="ALES Sonuç Belgesi" TextAlign="Left"
                                                                    Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkYabanciDilBelgesi" runat="server" Text="Yabancı Dil Belgesi"
                                                                    TextAlign="Left" Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkPasaport" runat="server" Text="Pasaport" TextAlign="Left" Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkAskerlik" runat="server" Text="Askerlik Begesi" TextAlign="Left"
                                                                    Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="d0" colspan="6" valign="top">
                                                    <table>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:CheckBox ID="chkAlesGozukmesin" runat="server" Text="Ales şartı aranmasın" AutoPostBack="true"
                                                                    OnCheckedChanged="chkAlesGozukmesin_OnCheckedChanged" meta:resourcekey="AlesSartiAranmasinResource1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="ALES Puan Türü :" meta:resourcekey="Label1Resource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chk_sayisal" runat="server" Text="Sayısal" meta:resourcekey="chk_sayisalResource1" />
                                                                <asp:CheckBox ID="chk_ea" runat="server" Text="Eşit Ağırlık" meta:resourcekey="chk_eaResource1" />
                                                                <asp:CheckBox ID="chk_sozel" runat="server" Text="Sözel" meta:resourcekey="chk_sozelResource1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6" valign="top">
                                                    <asp:Label ID="lblEkBilgi" runat="server" Text="Ek Bilgi :" meta:resourcekey="lblEkBilgiResource1"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <telerik:RadTextBox ID="txt_ekbilgi" runat="server" TextMode="MultiLine" Width="600px"
                                                        LabelCssClass="" meta:resourcekey="txt_ekbilgiResource1">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="d0">
                                        </table>
                                    </asp:Panel>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btn_kaydet" Width="100px" runat="server" Text="Kaydet" OnClick="btn_kaydet_Click"
                                    meta:resourcekey="btn_kaydetResource1" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlCaniasDeneme" Visible="false" runat="server">
                        <asp:TextBox ID="ogrno" runat="server"></asp:TextBox>
                        <asp:Button ID="btnCaniasDeneme" runat="server" Text="deneme" OnClick="btnCaniasDeneme_Click" />
                        <br />
                        <asp:Label ID="lblTestResult" runat="server" Text="dene"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" meta:resourcekey="RadTabStrip1Resource1" OnClientTabSelected="gridBelgeler_OnClientTabSelected">
                        <Tabs>
                            <telerik:RadTab Text="Değerlendirme Araçları" Selected="True" meta:resourcekey="RadTabResource1">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Mülakat/Bilim Sınavı Bilgileri" meta:resourcekey="RadTabResource2">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Sınav Puan Barajları" meta:resourcekey="RadTabResource3">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Belgeler" meta:resourcekey="RadTabResource4" Visible="true">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Disiplin Bilgileri" meta:resourcekey="RadTabResource5" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Başvuru Kontrol Listeleri" meta:resourcekey="RadTabResource6"
                                Visible="false">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%"
                        meta:resourcekey="RadMultiPage1Resource1">
                        <telerik:RadPageView ID="RadPageView1" runat="server" meta:resourcekey="RadPageView1Resource1"
                            Selected="True">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="grd_degerlendirme_kriterleri" runat="server" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" GridLines="None"
                                            OnNeedDataSource="grd_degerlendirme_kriterleri_NeedDataSource" CellSpacing="0"
                                            meta:resourcekey="grd_degerlendirme_kriterleriResource1">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True"></Selecting>
                                            </ClientSettings>
                                            <MasterTableView ClientDataKeyNames="KodID" DataKeyNames="KodID">
                                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="KodID" FilterControlAltText="Filter KodID column"
                                                        HeaderText="KodID" meta:resourcekey="GridBoundColumnResource1" UniqueName="KodID"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Aciklama" FilterControlAltText="Filter Aciklama column"
                                                        HeaderText="Araç" meta:resourcekey="GridBoundColumnResource2" UniqueName="Aciklama">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Yüzde katkı oranı"
                                                        meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan1" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_puan1Resource1" Value='<%# DataBinder.Eval(Container.DataItem, "Puan1") %>'>
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn2 column"
                                                        HeaderText="Yüzde katkı oranı(Yabancı Uyruklu muaf olmayan Öğrenciler İçin)"
                                                        UniqueName="TemplateColumn2">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan3" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_puan3Resource1" Value='<%# DataBinder.Eval(Container.DataItem, "Puan3") %>'>
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column"
                                                        HeaderText="Yüzde katkı oranı(Güzel Sanatlar Enstitüsü İçin)" meta:resourcekey="GridTemplateColumnResource2"
                                                        UniqueName="TemplateColumn1">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan2" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_puan2Resource1" Value='<%# DataBinder.Eval(Container.DataItem, "Puan2") %>'>
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn4 column"
                                                        HeaderText="Yüzde katkı oranı(Değerlendirme Farklı)" meta:resourcekey="GridTemplateColumnResource7"
                                                        UniqueName="TemplateColumn4">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan4" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_puan4Resource1" Value='<%# DataBinder.Eval(Container.DataItem, "Puan4") %>'>
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn5 column"
                                                        HeaderText="Başvuruda Kontrol Edilecek Baraj Puan Oranları" UniqueName="TemplateColumn5">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan5" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" Value='<%# DataBinder.Eval(Container.DataItem, "Puan5") %>'>
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="İkinci Değerlendirme Oranları" UniqueName="TemplateColumn6">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan6" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" Value='<%# DataBinder.Eval(Container.DataItem, "Puan6") %>'>
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                <PagerTemplate>
                                                    <cc3:GridReportTools ID="GridReportTools1" runat="server" ExportImagesSize="0, 0"
                                                        ExportToCSV="False" ExportToExcelML="True" ExportToWord="False" meta:resourcekey="GridReportTools1Resource1"
                                                        RadGridName="grd_degerlendirme_kriterleri"></cc3:GridReportTools>
                                                </PagerTemplate>
                                            </MasterTableView>
                                            <PagerStyle AlwaysVisible="True" />
                                            <FilterMenu EnableTheming="True" EnableImageSprites="False">
                                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                            </FilterMenu>
                                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                            </HeaderContextMenu>
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server" meta:resourcekey="RadPageView2Resource1">
                            <table width="100%">
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label3" runat="server" Text="Yazılı Bilim Sınavı Yeri :" meta:resourcekey="Label3Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_bilim_sinavi" Width="100%" TextMode="MultiLine" MaxLength="200"
                                            runat="server" LabelCssClass="" meta:resourcekey="txt_bilim_sinaviResource1">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label7" runat="server" Text="Yazılı Bilim Sınavı Tarihi :" meta:resourcekey="Label7Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="date_bilim_sinavi_tarihi" runat="server" Culture="Turkish (Turkey)"
                                            meta:resourcekey="date_bilim_sinavi_tarihiResource1">
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="lbl_mulakat_yeri" runat="server" Text="Mülakat Yeri :" meta:resourcekey="lbl_mulakat_yeriResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_mulakat_yeri" Width="100%" TextMode="MultiLine" MaxLength="200"
                                            runat="server" LabelCssClass="" meta:resourcekey="txt_mulakat_yeriResource1">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="lbl_mulakat_tarihi" runat="server" Text="Mülakat Tarihi :" meta:resourcekey="lbl_mulakat_tarihiResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="date_mulakat_tarihi" runat="server" Culture="Turkish (Turkey)"
                                            meta:resourcekey="date_mulakat_tarihiResource1">
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_juri_uye" runat="server" Text="Jüri Üyeleri" meta:resourcekey="lbl_juri_uyeResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc3:SearchOgretimUyesi ID="ara_hoca_mulakat" runat="server" ActivateImage="True"
                                                        FieldOrder="" MaxLength="0" meta:resourcekey="ara_hoca_mulakatResource1" OgretimUyesiID="-1"
                                                        PanelWidth="" ShowAraImage="True" ShowLblTCKimlik="True" Value="" OnOgretimOyesiSearchedEventHandler="ara_hoca_mulakat_OgretimOyesiSearchedEventHandler1"
                                                        OnOgretimOyesiSelectedEventHandler="ara_hoca_mulakat_OgretimOyesiSelectedEventHandler1" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_hoca_mulakat" runat="server" meta:resourcekey="lbl_hoca_mulakatResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc3:KodCombo ID="cmb_mulakat_hoca_durum" runat="server" KodGrupId="943" meta:resourcekey="cmb_mulakat_hoca_durumResource1"
                                                        SelectedKodId="-1" Sort="False" Text="" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_hoca_ekle_mulakat" Width="100px" runat="server" Text="Ekle" OnClick="btn_hoca_ekle_mulakat_Click"
                                                        meta:resourcekey="btn_hoca_ekle_mulakatResource1" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_hoca_sil_mulakat" Width="100px" runat="server" Text="Sil" OnClick="btn_hoca_sil_mulakat_Click"
                                                        meta:resourcekey="btn_hoca_sil_mulakatResource1" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Panel runat="server" ID="pnlDigerBirimlerdenJuriAtama">
                                            <fieldset>
                                                <legend>
                                                    <asp:Label runat="server" ID="lblJuriAciklama" Text="Diğer Birimlere Aşağıdaki listedeki jurileri eklemek için seçim yapınız">
                                                    </asp:Label></legend>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnDigerBirimlerdenJuriAta" runat="server" OnClick="btnDigerBirimlerdenJuriAta_Click"
                                                                Text="Diğer Birimlerden Jüri Ataması" Visible="false" Width="150px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Atama Yapılacak Birim :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbBirim" runat="server" AutoPostBack="true" DataTextField="BirimAdi"
                                                                Width="200px" DataValueField="OrganizasyonID" Enabled="true" OnSelectedIndexChanged="cmbBirim_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="Atama Yapılacak Alt Birim :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbAltBirim" runat="server" AutoPostBack="true" DataTextField="AltBirimAdi"
                                                                Width="200px" DataValueField="OrganizasyonID" Enabled="true" OnSelectedIndexChanged="cmbAltBirim_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text="Atama Yapılacak Program :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbProgram" runat="server" DataTextField="Aciklama" DataValueField="OrganizasyonProgramID"
                                                                Width="200px" Enabled="true" OnSelectedIndexChanged="cmbProgram_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnJurileriEkle" runat="server" OnClick="btnJurileriEkle_Click" Text="Jürileri Ekle" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUyari" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="grd_mulakat_hoca" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Height="180px" meta:resourcekey="grd_mulakat_hocaResource1"
                                            OnItemCommand="grd_mulakat_hoca_ItemCommand" OnNeedDataSource="grd_mulakat_hoca_NeedDataSource">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <ClientSettings EnablePostBackOnRowClick="True">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <MasterTableView DataKeyNames="JuriUyeleriID">
                                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridClientSelectColumn CommandName="secme" FilterControlAltText="Filter column column"
                                                        meta:resourcekey="GridClientSelectColumnResource1" UniqueName="column">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridClientSelectColumn>
                                                    <telerik:GridBoundColumn DataField="JuriUyeleriID" FilterControlAltText="Filter JuriUyeleriID column"
                                                        HeaderText="JuriUyeleriID" meta:resourcekey="GridBoundColumnResource3" UniqueName="JuriUyeleriID"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesiUnvan.Aciklama" FilterControlAltText="Filter OgretimUyesiUnvan.Aciklama column"
                                                        HeaderText="Ünvan" meta:resourcekey="GridBoundColumnResource4" UniqueName="OgretimUyesiUnvan.Aciklama">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.Ad" FilterControlAltText="Filter OgretimUyesi.Ad column"
                                                        HeaderText="Ad" meta:resourcekey="GridBoundColumnResource5" UniqueName="OgretimUyesi.Ad">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.Soyad" FilterControlAltText="Filter OgretimUyesi.Soyad column"
                                                        HeaderText="Soyad" meta:resourcekey="GridBoundColumnResource6" UniqueName="OgretimUyesi.Soyad">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesiDurum.Aciklama" FilterControlAltText="Filter OgretimUyesiDurum.Aciklama column"
                                                        HeaderText="Durum" meta:resourcekey="GridBoundColumnResource7" UniqueName="OgretimUyesiDurum.Aciklama">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.OgretimUyesiID" FilterControlAltText="Filter OgretimUyesiID column"
                                                        meta:resourcekey="GridBoundColumnResource8" UniqueName="OgretimUyesiID" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle AlwaysVisible="True" />
                                            </MasterTableView>
                                            <PagerStyle AlwaysVisible="True" />
                                            <FilterMenu EnableImageSprites="False" EnableTheming="True">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                            </FilterMenu>
                                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" EnableTheming="True">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                            </HeaderContextMenu>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:ImageButton ID="ExcelKayit" runat="server" Height="33px" ImageUrl="~/excel.jpg"
                                            meta:resourcekey="ExcelKayitResource1" OnClick="ExcelKayit_Click" Width="29px" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView5" runat="server" meta:resourcekey="RadPageView5Resource1">
                            <%--<asp:CheckBox ID="chkYabanciDilKontrolu" runat="server" Text="Yabancı Dil Kriterlerini Başvuru Esnasında Kontrol Et " />--%>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="grd_sinav_notlari" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" PageSize="1000" GridLines="None" OnNeedDataSource="grd_sinav_notlari_NeedDataSource"
                                            OnItemDataBound="grd_sinav_notlari_ItemDataBound" CellSpacing="0" meta:resourcekey="grd_sinav_notlariResource1">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <ClientSettings>
                                                <Selecting AllowRowSelect="True"></Selecting>
                                            </ClientSettings>
                                            <MasterTableView ClientDataKeyNames="KodID" DataKeyNames="KodID">
                                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="KodID" FilterControlAltText="Filter KodID column"
                                                        HeaderText="KodID" meta:resourcekey="GridBoundColumnResource9" UniqueName="KodID"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Aciklama" FilterControlAltText="Filter Aciklama column"
                                                        HeaderText="Sınav Türü" meta:resourcekey="GridBoundColumnResource10" UniqueName="Aciklama">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Minimum Puan Barajı (02.06.2007 Sonrası)"
                                                        meta:resourcekey="GridTemplateColumnResource4" UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan1" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_puan1Resource2">
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn1 column"
                                                        HeaderText="Minimum Puan Barajı (02.06.2007 Öncesi)" meta:resourcekey="GridTemplateColumnResource5"
                                                        UniqueName="TemplateColumn1">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan2" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_puan2Resource2">
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn2 column"
                                                        HeaderText="Yabancı Uyruklu " meta:resourcekey="GridTemplateColumnResource6"
                                                        UniqueName="TemplateColumn2">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_yabanci_uyruk" runat="server" Culture="Turkish (Turkey)"
                                                                LabelCssClass="" meta:resourcekey="txt_yabanci_uyrukResource1">
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Puan1" FilterControlAltText="Filter Puan1 column"
                                                        HeaderText="Puan1" meta:resourcekey="GridBoundColumnResource11" UniqueName="Puan1"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Puan2" FilterControlAltText="Filter Puan2 column"
                                                        HeaderText="Puan2" meta:resourcekey="GridBoundColumnResource12" UniqueName="Puan2"
                                                        Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="YabanciUyrukluPuanBaraji" FilterControlAltText="Filter YabanciUyrukluPuanBaraji column"
                                                        HeaderText="YabanciUyrukluPuanBaraji" meta:resourcekey="GridBoundColumnResource13"
                                                        UniqueName="YabanciUyrukluPuanBaraji" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn DataField="GecerlilikTarihi" FilterControlAltText="Filter GecerlilikTarihi column"
                                                        HeaderText="GecerlilikTarihi" meta:resourcekey="GridBoundColumnResource14" UniqueName="GecerlilikTarihi"
                                                        Visible="True">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <telerik:RadDatePicker ID="txt_gecerlilik_tarihi" runat="server"  Width="170px" TimeView-Enabled="false"
                                                                TimePopupButton-Visible="false"  DateInput-DisplayDateFormat="dd.MM.yyyy" DateInput-DateFormat="d.MM.yyyy"
                                                                Culture="Turkish (Turkey)" meta:resourcekey="txt_gecerlilik_tarihiResource1">
                                                            </telerik:RadDatePicker>
                                                            <%--<telerik:RadDateInput MinDate="1.01.1900" ID="txt_gecerlilik_tarihi" meta:resourcekey="txt_gecerlilik_tarihiResource1"
                                                                runat="server" EmptyMessage="02.01.2019">
                                                            </telerik:RadDateInput>--%>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                <PagerTemplate>
                                                    <cc3:GridReportTools ID="GridReportTools1" runat="server" ExportImagesSize="0, 0"
                                                        ExportToCSV="False" ExportToExcelML="True" ExportToWord="False" meta:resourcekey="GridReportTools1Resource2"
                                                        RadGridName="grd_sinav_notlari"></cc3:GridReportTools>
                                                </PagerTemplate>
                                            </MasterTableView>
                                            <PagerStyle AlwaysVisible="True" />
                                            <FilterMenu EnableTheming="True" EnableImageSprites="False">
                                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                            </FilterMenu>
                                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                            </HeaderContextMenu>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSinavUyari" runat="server" Text="* Cambridge Sınavı için 1 ile 3 arası değerler girilmelidir."
                                            ForeColor="Red" meta:resourcekey="lblSinavUyariResource1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pageBelgeler" runat="server" meta:resourcekey="RadPageView3Resource1">
                            <telerik:RadGrid ID="gridBelgeler" runat="server" AllowPaging="True" AllowSorting="True"
                                OnNeedDataSource="gridBelgeler_NeedDataSource" OnItemDataBound="gridBelgeler_ItemDataBound"
                                Width="50%" GridLines="None" AutoGenerateColumns="False" CellSpacing="0" PageSize="50">
                                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                </HeaderContextMenu>
                                <MasterTableView DataKeyNames="KodID">
                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="KodID" FilterControlAltText="Filter Belge column"
                                            UniqueName="KodID" Visible="false">
                                            <HeaderStyle Font-Bold="False" HorizontalAlign="Right" VerticalAlign="Middle" />
                                            <ItemStyle Font-Bold="False" HorizontalAlign="Right" VerticalAlign="Middle" Width="60%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Aciklama" FilterControlAltText="Filter Belge column"
                                            HeaderText="Belge Adı" meta:resourcekey="gridBelgelerAciklamaResource" UniqueName="Aciklama"
                                            Visible="True">
                                            <HeaderStyle Font-Bold="False" HorizontalAlign="left" VerticalAlign="Middle" />
                                            <ItemStyle Font-Bold="false" HorizontalAlign="left" VerticalAlign="Middle" Width="60%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="T.C Zorunlu" UniqueName="TcZorunluGridTemplate">
                                            <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkTCZorunlu" runat="server" Visible="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Yabancı Uyruklu Zorunlu" UniqueName="YabanciUyrukluZorunluGridTemplate">
                                            <HeaderStyle Font-Bold="False" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkYUZorunlu" runat="server" Visible="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Adim" FilterControlAltText="Filter column column"
                                            HeaderText="Adım" UniqueName="Adim" Visible="false">
                                            <ItemStyle Font-Bold="false" HorizontalAlign="left" VerticalAlign="Middle" Width="60%" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pageDisiplin" runat="server" meta:resourcekey="RadPageView3Resource1">
                            <asp:Panel runat="server" ID="pnlDisiplinBilgileri">
                                <fieldset>
                                    <legend>
                                        <asp:Label runat="server" ID="lblDisiplinBilgileri" Text="Disiplin Bilgileri">
                                        </asp:Label>
                                    </legend>
                                    <table>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="lblDisiplinTuruInfo" Text="Seçilen Program Tek Disiplinlidir." runat="server"
                                                    Font-Size="Large" ForeColor="#0066FF" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Disiplin Türü :
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbDisiplinTuru" runat="server">
                                                </telerik:RadComboBox>
                                            </td>
                                            <td style="width: 15%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Başvurulabilecek Program Türü :
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cmbBasvurulabilecekProgramTuru" runat="server">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                En Fazla Başvuru Sayısı:
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtEnFazlaBasvuruSayisi" runat="server">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td style="width: 15%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Başvurulabilecek Program Türü Sayısı :
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtBasvurulabilecekProgramTuruSayisi" runat="server">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pageBasvuruKontrol" runat="server" meta:resourcekey="RadPageView3Resource1">
                            <asp:Panel runat="server" ID="pnlBasvuruKontrol">
                                <fieldset>
                                    <legend>
                                        <asp:Label runat="server" ID="lblBasvuruKontrol" Text="Başvuru Baraj Kriterleri Kontrolü">
                                        </asp:Label>
                                    </legend>
                                    <table>
                                        <tr>
                                            <td>
                                                Mezuniyet Notu 4'lük sistem :
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkMinMezNot4" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Mezuniyet Notu 100'lük sistem :
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkMinMezNot100" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Yüksek Lisans Mezuniyet Notu 4'lük sistem :
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkMinYsMezNot4" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Yüksek Lisans Mezuniyet Notu 100'lük sistem :
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkMinYsMezNot100" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Height="75px"
        Width="75px">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Height="75px"
        Width="75px">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" meta:resourcekey="RadAjaxManager1Resource1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txt_turk_kontenjan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_yabanci_kontenjan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_yedek_kontenjan_turk">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_yedek_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_yedek_kontenjan_yabanci">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_yedek_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_turk_kontenjan_alan_disi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_yabanci_kontenjan_alan_disi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_yedek_kontenjan_turk_alan_disi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_yedek_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txt_yedek_kontenjan_yabanci_alan_disi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txt_yedek_kontenjan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnDigerBirimlerdenJuriAta">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbBirim" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbAltBirim" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbProgram" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnJurileriEkle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbBirim">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbAltBirim" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbProgram" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbAltBirim">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbProgram" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnJurileriEkle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblUyari" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkAlesGozukmesin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grd_degerlendirme_kriterleri" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    </form>
</body>
</html>

<script>
    function gridBelgeler_OnClientTabSelected(sender, eventArgs) {
        var tab = eventArgs.get_tab();
        //alert(tab.get_text());
    }
</script>

