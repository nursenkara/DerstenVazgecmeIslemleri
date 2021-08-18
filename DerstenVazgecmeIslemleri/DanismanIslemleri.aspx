<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanismanIslemleri.aspx.cs" Inherits="DerstenVazgecmeIslemleri.DanismanIslemleri" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="UniOgrenci.Master.Web.UI" Namespace="UniOgrenci.Master.Web.UI.UserControls"
    TagPrefix="cc3" %>
<%@ Register Assembly="Unipa.Framework.Web.UI" Namespace="Unipa.Framework.Web.UI.UserControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="Unipa.Framework.Web.UI" Namespace="Unipa.Framework.Web.UI"
    TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
        <cc1:AppHeader ID="AppHeader1" runat="server" meta:resourcekey="AppHeader1Resource1"
        Text="" UygulamaIDGoster="True" />
    <cc2:JsLocalizer ID="JsLocalizer1" runat="server" AssemblyName="DerstenVazgecmeIslemleri" CacheResult="True"
        ResourcePath="Resources.Lang.DerstenVazgecmeIslemleriResources" meta:resourcekey="JsLocalizer1Resource1" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="ltlInfo" runat="server" EnableViewState="False" meta:resourcekey="ltlInfoResource1"></asp:Literal>
    <fieldset>
        <legend>
            <asp:Label ID="lblDanismanVazgecmeIslemleri" runat="server" Text="Danışman Öğretmen Dersten Vazgeçme İşlemleri"></asp:Label>
        </legend>
        <br />
        <br />
    
        <telerik:RadGrid ID="grdDanisman" runat="server" AllowPaging="True" Width="920" AutoGenerateColumns="False"
            meta:resourcekey="RadGridResource" PageSize="100" GridLines="None" CellSpacing="0"
            ShowGroupPanel="false" ShowStatusBar="false" OnNeedDataSource="grdDanisman_NeedDataSource"
            OnItemCommand="grdDanisman_ItemCommand">
            <MasterTableView AllowSorting="true" TableLayout="Fixed" CommandItemDisplay="Top"
                AllowMultiColumnSorting="false" DataKeyNames="OgrenciDersId" ShowFooter="false" Font-Names="Tahoma,Geneva,FreeSans,Helvetica,sans-serif"
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
                 <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkOnay" AutoPostBack="true" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="OgrenciAd" HeaderText="Öğrenci Ad" UniqueName="OgrenciAd" ItemStyle-Width="200px"
                        HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                    <telerik:GridBoundColumn DataField="OgrenciSoyad" HeaderText="Öğrenci Soyad" UniqueName="OgrenciSoyad"
                        ItemStyle-Width="150px" HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                    <telerik:GridBoundColumn DataField="DersKodu" HeaderText="DersKodu" UniqueName="DersKodu"
                        ItemStyle-Width="150px" HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
                        <telerik:GridBoundColumn DataField="DersAdi" HeaderText="Ders Adı" UniqueName="DersAdi"
                        ItemStyle-Width="150px" HeaderStyle-Width="150px" FilterControlWidth="100" ItemStyle-Wrap="true" />
        
                   
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:Button ID="btnOnay" CommandName="cnOnay" runat="server" Text="Onay"></asp:Button>
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
    </form>
    </fieldset>
</body>
</html>
