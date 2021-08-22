<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OgrenciBasvuruEkrani.aspx.cs"
    Inherits="DerstenVazgecmeIslemleri.OgrenciBasvuruEkrani" %>

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
</head>
<body>

    <script type="text/javascript">
        function onGridCreated(sender, args) {
            if (sender.get_element().offsetHeight > 300) {
                sender.GridDataDiv.style.height = "300px";
                sender.repaint();
            }
        }
    </script>

    <form id="form1" runat="server">
    <cc1:AppHeader ID="AppHeaderOgrenciBasvuruEkrani" runat="server" meta:resourcekey="AppHeaderOgrenciBasvuruEkrani"
        Text="" UygulamaIDGoster="True" />
    <cc2:JsLocalizer ID="JsLocalizerOgrenciBasvuruEkrani" runat="server" AssemblyName="DerstenVazgecmeIslemleri"
        CacheResult="True" ResourcePath="Resources.Lang.DerstenVazgecmeIslemleriResources"
        meta:resourcekey="JsLocalizerOgrenciBasvuruEkrani" />
    <asp:ScriptManager ID="ScriptManagerOgrenciBasvuruEkrani" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="ltlInfo" runat="server" EnableViewState="False" meta:resourcekey="ltlInfoResource1"></asp:Literal>
    <asp:Panel ID="pnlGenel" runat="server">
        <fieldset>
            <legend>
                <asp:Label ID="lblOgrenciBasvuruEkrani" runat="server" Text="Öğrenci Başvuru Ekranı"></asp:Label>
            </legend>
            <br />
            <div>
                <asp:Label ID="lblSecilenveKayitlananDersler" runat="server" Text="Seçilen ve Kayıtlanan Dersler"></asp:Label>
            </div>
            <br />
            <!---->
            <div>
                <asp:Button ID="btnDanismanaGonder" CommandName="cnDanismanaGonder" runat="server"
                    Text="Danışmana Gönder" Visible="true" OnClick="btnDanismanaGonder_Click"></asp:Button>
                <asp:Button ID="btnOnay" runat="server" Text="Onay" Visible="true" OnClick="btnOnay_Click">
                </asp:Button>
            </div>
            <br />
            <div>
                <asp:Label ID="lblAciklamaOgrenciBasvuruEkrani" runat="server" Text="Vazgeçmek istediğiniz derslere tıklayıp emin olduktan sonra 'Onay' butonuna, son olarak da 'Danışmana Gönder' butonuna basmalısınız.<br/>  Onay butonuna basmadan önce vazgeçme ve/veya geri alma işlemlerinizi yapabilirsiniz."></asp:Label>
            </div>
            <br />
            <telerik:RadGrid ID="grdOgrenci" runat="server" AllowPaging="True" Width="920" AutoGenerateColumns="False"
                meta:resourcekey="RadGridOgrenciBasvuruResource" PageSize="100" GridLines="None"
                CellSpacing="0" ShowGroupPanel="false" ShowStatusBar="false" OnItemDataBound="grdOgrenci_ItemDataBound"
                OnNeedDataSource="grdOgrenci_NeedDataSource" OnItemCommand="grdOgrenci_ItemCommand">
                <MasterTableView AllowSorting="true" TableLayout="Fixed" CommandItemDisplay="Top"
                    AllowMultiColumnSorting="false" DataKeyNames="OgrenciDersId" ShowFooter="false"
                    Font-Names="Tahoma,Geneva,FreeSans,Helvetica,sans-serif" Font-Size="8">
                    <CommandItemSettings ExportToPdfText="PDF olarak Yazdır" ExportToExcelText="Excel olarak Yazdır"
                        ShowExportToPdfButton="false" ShowExportToExcelButton="false" ShowAddNewRecordButton="false"
                        ShowRefreshButton="false" ShowExportToCsvButton="false"></CommandItemSettings>
                    <NoRecordsTemplate>
                        <div style="padding: 8px">
                            <asp:Label ID="lblKayitBulunamadi" runat="server" Text="Kayıt Bulunamadı"></asp:Label>
                        </div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="DersKodu" HeaderText="Ders Kodu" UniqueName="DersKodu"
                            ItemStyle-Width="150px" HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                        <telerik:GridBoundColumn DataField="DersAdi" HeaderText="Ders Adı" UniqueName="DersAdi"
                            ItemStyle-Width="150px" HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <telerik:RadButton ID="btnVazgec" CommandName="cnVazgec" runat="server" Text="Vazgeç">
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <telerik:RadButton ID="btnGeriAl" CommandName="cnGeriAl" runat="server" Text="Geri Al">
                                </telerik:RadButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
            <div>
                <h4>
                    Vazgeçilecek Dersler
                </h4>
                <asp:Label ID="lblVazgecilecekDersler" runat="server" Text=""></asp:Label>
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnlUyari" runat="server" Visible="false">
        <center>
            <h1>
                Başvurular şuan kapalıdır.</h1>
        </center>
    </asp:Panel>
    </form>

    <script>
    window.addEventListener("load", _ => {document.title = "Öğrenci Başvuru Ekranı"});
    </script>

</body>
</html>
