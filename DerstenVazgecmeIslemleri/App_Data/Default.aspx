<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DerstenVazgecmeIslemleri._Default" %>

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
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 92px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:AppHeader ID="AppHeader1" runat="server" />
    <cc2:JsLocalizer ID="JsLocalizer1" runat="server" AssemblyName="DerstenVazgecmeIslemleri" CacheResult="True"
        ResourcePath="Resources.Lang.DerstenVazgecmeIslemleriResources" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="ltlInfo" runat="server" EnableViewState="False"></asp:Literal>
    <div>
        <table width="100%">
            <tr>
                <td valign="top" style="width: 250px">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lbl_org" runat="server" Text="Program Seçimi"></asp:Label>
                        </legend>
                        <cc3:Organization ID="organizasyon_" GetActives="false" OnProgramSelectedEventHandler="organizasyon_ProgramSelectedEventHandler"
                            OnOrganizasyonSelectedEventHandler="organizasyon_orgSelectedEventHandler" runat="server"
                            Level="4" />
                    </fieldset>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="Label2" runat="server" Text="Genel Bilgiler"></asp:Label>
                                    </legend>
                                    <asp:Panel ID="pnl_YilDonem" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <cc3:YilDonemCombo Width="300px" ID="yildonem_basvuru" runat="server" Direction="Horizontal"
                                                        LabelDonem="Başvuru Dönemi" LabelYil="Başvuru Yılı" ShowDonem="True" OnDonemSelectedEventHandler="yildonem_basvuru_DonemSelectedEventHandler" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnl_genel" runat="server" Enabled="False">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_baslama_tarihi" runat="server" Text="Başvuru Başlama Tarihi :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="date_baslama_tarihi" runat="server">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lbl_bitis_tarihi" runat="server" Text="Başvuru Bitiş Tarihi :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="date_bitis_tarihi" runat="server">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_obaslama_tarihi" runat="server" Text="Onay Başlama Tarihi :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="date_obaslama_tarihi" runat="server">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lbl_obitis_tarihi" runat="server" Text="Onay Bitiş Tarihi :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="date_obitis_tarihi" runat="server">
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="Türk Uyruklu Öğrenci Kontenjanı :"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txt_turk_kontenjan" Width="50px" Value="0" runat="server"
                                                                    MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" OnTextChanged="txt_turk_kontenjan_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_turk_kontenjan_alan_disi_LBL" runat="server" Text="Alan Dışı :"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txt_turk_kontenjan_alan_disi" Width="50px" Value="0"
                                                                    runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                                    OnTextChanged="txt_turk_kontenjan_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_yabanci_kontenjan" runat="server" Text="Yabancı Uyruklu Öğrenci Kontenjanı :"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txt_yabanci_kontenjan" Width="50px" Value="0" runat="server"
                                                                    MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" OnTextChanged="txt_yabanci_kontenjan_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_yabanci_kontenjan_alan_disi_LBL" runat="server" Text="Alan Dışı :"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox ID="txt_yabanci_kontenjan_alan_disi" Width="50px" Value="0"
                                                                    runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                                    OnTextChanged="txt_yabanci_kontenjan_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lbl_kontenjan" runat="server" Text="Toplam kontenjan :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_kontenjan" Width="50px" runat="server" Value="0"
                                                        Enabled="false" MaxValue="999" MinValue="0" MaxLength="4">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lbl_arastirma_gorevlisi_kontenjan" runat="server" Text="Araştırma Görevlisi Kontenjanı :"
                                                        Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_arastirma_gorevli_kontenjan" Width="50px" Value="0"
                                                        runat="server" MaxValue="999" MinValue="0" MaxLength="4" Visible="False">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Yedek Turk Uyruklu Öğrenci Kontenjanı :"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_turk" Width="50px" Value="0" runat="server"
                                                                    MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True" OnTextChanged="txt_yedek_kontenjan_turk_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_yedek_kontenjan_turk_alan_disi_LBL" runat="server" Text="Alan Dışı :"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_turk_alan_disi" Width="50px" Value="0"
                                                                    runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                                    OnTextChanged="txt_yedek_kontenjan_turk_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Yedek Yabancı Uyruklu Öğrenci Kontenjanı :"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_yabanci" Width="50px" Value="0"
                                                                    runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                                    OnTextChanged="txt_yedek_kontenjan_yabanci_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_yedek_kontenjan_yabanci_alan_disi_LBL" runat="server" Text="Alan Dışı :"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <telerik:RadNumericTextBox ID="txt_yedek_kontenjan_yabanci_alan_disi" Width="50px"
                                                                    Value="0" runat="server" MaxValue="999" MinValue="0" MaxLength="4" AutoPostBack="True"
                                                                    OnTextChanged="txt_yedek_kontenjan_yabanci_TextChanged">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Toplam Yedek kontenjan :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_yedek_kontenjan" Width="50px" runat="server" Value="0"
                                                        Enabled="false" MaxValue="999" MinValue="0" MaxLength="4">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_min_mezuniyet_notu4" runat="server" Text="Min. Mezuniyet Notu(4'lük sistem) :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_min_mezuniyet4" Width="50px" runat="server" Value="0"
                                                        MaxValue="4" MinValue="0">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lbl_min_mezuniyet_notu100" runat="server" Text="Min. Mezuniyet Notu(100'lük sistem) :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_min_mezuniyet100" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_min_yl_mezuniyet_notu4" runat="server" Text="Min. Y.L. Mezuniyet Notu(4'lük sistem) :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_min_yl_mezuniyet4" Width="50px" runat="server"
                                                        Value="0" MaxValue="4" MinValue="0">
                                                        <NumberFormat DecimalDigits="2" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lbl_min_yl_mezuniyet_notu100" runat="server" Text="Min. Y.L. Mezuniyet Notu(100'lük sistem) :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_min_yl_mezuniyet100" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Min. Başarı Notu :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_min_basari_notu" Width="50px" runat="server" Value="0"
                                                        MaxValue="100" MinValue="0">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblMinMulakatNotu" runat="server" Text="Min. Mülakat Notu :"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_min_mulakat_notu" Width="50px" runat="server"
                                                        Value="0" MaxValue="100" MinValue="0">
                                                        <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="20px" valign="middle">
                                                    <asp:CheckBox ID="ChkResimZorunlu" runat="server" Text="Resim Zorunlu : " TextAlign="Left" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkMezunOlmayaniAl" runat="server" Text="Mezun Olmayanı Al : "
                                                        TextAlign="Left" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblEkBilgi" runat="server" Text="Ek Bilgi :"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <telerik:RadTextBox ID="txt_ekbilgi" runat="server" TextMode="MultiLine" Width="600px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btn_kaydet" Width="100px" runat="server" Text="Kaydet" OnClick="btn_kaydet_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <asp:TextBox ID="ogrno" runat="server"></asp:TextBox>
                        <asp:Button ID="webdeneme" runat="server" Text="deneme" OnClick="webdeneme_Click" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Black" MultiPageID="RadMultiPage1"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab Text="Değerlendirme Araçları" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Mülakat/Bilim Sınavı Bilgileri">
                            </telerik:RadTab>
                            <%--<telerik:RadTab Text="Yabancı Dil Bilgileri" Visible="false">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Türkçe Sınavı Bilgileri" Visible="false">
                            </telerik:RadTab>--%>
                            <telerik:RadTab Text="Sınav Puan Barajları">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="grd_degerlendirme_kriterleri" runat="server" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" GridLines="None"
                                            AllowMultiRowSelection="false" OnNeedDataSource="grd_degerlendirme_kriterleri_NeedDataSource">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <PagerStyle AlwaysVisible="True" />
                                            <MasterTableView DataKeyNames="KodID" ClientDataKeyNames="KodID">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="KodID" Visible="false" HeaderText="KodID">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Aciklama" HeaderText="Araç">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Yüzde katkı oranı">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan1" EnableViewState="true" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Puan1") %>'>
                                                                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Yüzde katkı oranı(Güzel Sanatlar Enstitüsü İçin)">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan2" EnableViewState="true" runat="server"  Value='<%# DataBinder.Eval(Container.DataItem, "Puan2") %>'>
                                                                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Yüzde katkı oranı(Yabancı Uyruklu Öğrenciler İçin)">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan3" EnableViewState="true" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Puan3") %>'>
                                                                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="true" />
                                                <PagerTemplate>
                                                    <cc3:GridReportTools ID="GridReportTools1" runat="server" RadGridName="grd_degerlendirme_kriterleri"
                                                        ExportToExcel="true" ExportToPDF="true" ExportToExcelML="True" />
                                                </PagerTemplate>
                                            </MasterTableView>
                                            <ClientSettings EnablePostBackOnRowClick="false">
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <FilterMenu EnableTheming="True">
                                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="ALES Puan Türü :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_sayisal" runat="server" Text="Sayısal" />
                                        <asp:CheckBox ID="chk_ea" runat="server" Text="Eşit Ağırlık" />
                                        <asp:CheckBox ID="chk_sozel" runat="server" Text="Sözel" />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <table width="100%">
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label3" runat="server" Text="Bilim Sınavı Yeri :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_bilim_sinavi" Width="100%" TextMode="MultiLine" MaxLength="200"
                                            Rows="2" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="Label7" runat="server" Text="Bilim Sınavı Tarihi :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="date_bilim_sinavi_tarihi" runat="server">
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="lbl_mulakat_yeri" runat="server" Text="Mülakat Yeri :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_mulakat_yeri" Width="100%" TextMode="MultiLine" MaxLength="200"
                                            Rows="2" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="lbl_mulakat_tarihi" runat="server" Text="Mülakat Tarihi :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="date_mulakat_tarihi" runat="server">
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_juri_uye" runat="server" Text="Jüri Üyeleri"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc3:SearchOgretimUyesi ID="ara_hoca_mulakat" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_hoca_mulakat" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc3:KodCombo ID="cmb_mulakat_hoca_durum" runat="server" KodGrupId="943" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_hoca_ekle_mulakat" Width="100px" runat="server" Text="Ekle" OnClick="btn_hoca_ekle_mulakat_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_hoca_sil_mulakat" Width="100px" runat="server" Text="Sil" OnClick="btn_hoca_sil_mulakat_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="grd_mulakat_hoca" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" GridLines="None" OnNeedDataSource="grd_mulakat_hoca_NeedDataSource"
                                            OnItemCommand="grd_mulakat_hoca_ItemCommand" Height="180px">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <ClientSettings EnablePostBackOnRowClick="true">
                                                <Selecting AllowRowSelect="True" />
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <PagerStyle AlwaysVisible="True" />
                                            <MasterTableView DataKeyNames="JuriUyeleriID">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridClientSelectColumn UniqueName="column" CommandName="secme">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridClientSelectColumn>
                                                    <telerik:GridBoundColumn DataField="JuriUyeleriID" Visible="false" HeaderText="JuriUyeleriID">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesiUnvan.Aciklama" HeaderText="Ünvan">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.Ad" HeaderText="Ad">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.Soyad" HeaderText="Soyad">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesiDurum.Aciklama" HeaderText="Durum">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.OgretimUyesiID" UniqueName="OgretimUyesiID"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="true" />
                                                <PagerTemplate>
                                                    <%-- <cc3:GridReportTools ID="GridReportTools1" runat="server" RadGridName="grd_mulakat_hoca"
                                                       ExportToExcel="true" ExportToPDF="true" ExportToExcelML="True"  />--%>
                                                </PagerTemplate>
                                            </MasterTableView>
                                            <FilterMenu EnableTheming="True">
                                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                            </FilterMenu>
                                            <HeaderContextMenu EnableTheming="True">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                            </HeaderContextMenu>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:ImageButton ID="ExcelKayit" runat="server" OnClick="ExcelKayit_Click" Width="29px"
                                            Height="33px" ImageUrl="~/excel.jpg" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <%--  <telerik:RadPageView ID="RadPageView3" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_yabanci_dil_yer" runat="server" Text="Yabancı Dil Sınav Yeri :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_yabanci_dil_yer" Width="100%" TextMode="MultiLine" MaxLength="200"
                                            Rows="2" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_yabanci_dil_tarih" runat="server" Text="Yabancı Dil Sınav Tarihi :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="date_yabanci_dil" runat="server">
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_turkce_sinav" runat="server" Text="Türkçe Sınav Yeri :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_turkce_sinav" Width="100%" TextMode="MultiLine" MaxLength="200"
                                            Rows="2" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_turkce_sinat_tarihi" runat="server" Text="Türkçe Sınav Tarihi :"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDateTimePicker ID="date_turkce_sinav" runat="server">
                                        </telerik:RadDateTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Jüri Üyeleri"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc3:SearchOgretimUyesi ID="hoca_turkcesinav" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_turkce_sinav_hoca" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc3:KodCombo ID="cmb_turkce_sinav_hoca_durum" runat="server" KodGrupId="943" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_turkce_sinav_hoca_ekle" Width="100px" runat="server" Text="Ekle"
                                                        OnClick="btn_turkce_sinav_hoca_ekle_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_turkce_sinav_hoca_sil" Width="100px" runat="server" Text="Sil"
                                                        OnClick="btn_turkce_sinav_hoca_sil_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="grid_turkce_sinav" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" GridLines="None" AllowMultiRowSelection="false" OnNeedDataSource="grid_turkce_sinav_NeedDataSource">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <PagerStyle AlwaysVisible="True" />
                                            <MasterTableView DataKeyNames="JuriUyeleriID" ClientDataKeyNames="JuriUyeleriID">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridClientSelectColumn>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridClientSelectColumn>
                                                    <telerik:GridBoundColumn DataField="JuriUyeleriID" Visible="false" HeaderText="JuriUyeleriID">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesiUnvan.Aciklama" HeaderText="Ünvan">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.Ad" HeaderText="Ad">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesi.Soyad" HeaderText="Soyad">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="OgretimUyesiDurum.Aciklama" HeaderText="Durum">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="true" />
                                                <PagerTemplate>
                                                    <cc3:GridReportTools ID="GridReportTools1" runat="server" RadGridName="grid_turkce_sinav"
                                                        ExportToExcel="true" ExportToPDF="true" ExportToExcelML="True" />
                                                </PagerTemplate>
                                            </MasterTableView>
                                            <FilterMenu EnableTheming="True">
                                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                            </FilterMenu>
                                            <ClientSettings EnablePostBackOnRowClick="false">
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>--%>
                        <telerik:RadPageView ID="RadPageView5" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="grd_sinav_notlari" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" PageSize="1000" GridLines="None" AllowMultiRowSelection="false"
                                            OnNeedDataSource="grd_sinav_notlari_NeedDataSource" OnItemDataBound="grd_sinav_notlari_ItemDataBound">
                                            <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                                <Excel Format="ExcelML" />
                                            </ExportSettings>
                                            <PagerStyle AlwaysVisible="True" />
                                            <MasterTableView DataKeyNames="KodID" ClientDataKeyNames="KodID">
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="KodID" Visible="false" HeaderText="KodID">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Aciklama" HeaderText="Sınav Türü">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <%--<telerik:GridTemplateColumn HeaderText="Puan 1">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_puan1" EnableViewState="true" runat="server" Text='<%# Eval("Puan1") %>'>
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                    <telerik:GridTemplateColumn HeaderText="Minimum Puan Barajı (02.06.2007 Sonrası)">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan1" EnableViewState="true" runat="server">
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Minimum Puan Barajı (02.06.2007 Öncesi)">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_puan2" EnableViewState="true" runat="server">
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Yabancı Uyruklu ">
                                                        <ItemTemplate>
                                                            <telerik:RadNumericTextBox ID="txt_yabanci_uyruk" EnableViewState="true" runat="server">
                                                                <NumberFormat DecimalDigits="3" GroupSeparator=""></NumberFormat>
                                                            </telerik:RadNumericTextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Puan1" Visible="false" HeaderText="Puan1">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Puan2" Visible="false" HeaderText="Puan2">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="YabanciUyrukluPuanBaraji" Visible="false" HeaderText="YabanciUyrukluPuanBaraji">
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <PagerStyle AlwaysVisible="true" />
                                                <PagerTemplate>
                                                    <cc3:GridReportTools ID="GridReportTools1" runat="server" RadGridName="grd_sinav_notlari"
                                                        ExportToExcel="true" ExportToPDF="true" ExportToExcelML="True" />
                                                </PagerTemplate>
                                            </MasterTableView>
                                            <FilterMenu EnableTheming="True">
                                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                            </FilterMenu>
                                            <ClientSettings EnablePostBackOnRowClick="false">
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSinavUyari" runat="server" Text="* Cambridge Sınavı için 1 ile 3 arası değerler girilmelidir."
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    </form>
</body>
</html>
