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
    <cc1:AppHeader ID="AppHeader1" runat="server" meta:resourcekey="AppHeader1Resource1"
        Text="" UygulamaIDGoster="True" />
    <cc2:JsLocalizer ID="JsLocalizer1" runat="server" AssemblyName="DerstenVazgecmeIslemleri" CacheResult="True"
        ResourcePath="Resources.Lang.DerstenVazgecmeIslemleriResources" meta:resourcekey="JsLocalizer1Resource1" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Literal ID="ltlInfo" runat="server" EnableViewState="False" meta:resourcekey="ltlInfoResource1"></asp:Literal>
    <table border="1" width="100%">
        <tr>
            <td width="40%">
                <div>
                    <asp:Label ID="LabelGano" runat="server" Text="Gano:" meta:resourcekey="lblGanoResource1"></asp:Label>
                    <telerik:RadComboBox ID="cmbBuyukturKucukturEsittir" Width="75px" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seçiniz..." Value="0" />
                            <telerik:RadComboBoxItem Text=">" Value="1" />
                            <telerik:RadComboBoxItem Text="<" Value="2" />
                            <telerik:RadComboBoxItem Text="=" Value="3" />
                            <telerik:RadComboBoxItem Text="&gt;=" Value="4" />
                            <telerik:RadComboBoxItem Text="&lt;=" Value="5" />
                        </Items>
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </telerik:RadComboBox>
                    <telerik:RadTextBox ID="RadTextBox1" Width="50px" runat="server" MaxLength="40" onkeypress="return onlyDotsAndNumbers(this,event);"
                        LabelCssClass="" meta:resourcekey="txt_adResource1">
                    </telerik:RadTextBox>
                </div>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Başvuru Tarihi:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="RadDateTimePicker1" runat="server" Width="170px" TimeView-Enabled="true"
                                TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Başvuru Bitiş Tarihi:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="RadDateTimePicker2" runat="server" Width="170px" TimeView-Enabled="true"
                                TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Danışman Başlama Tarihi:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="RadDateTimePicker3" runat="server" Width="170px" TimeView-Enabled="true"
                                TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="Danışman Bitiş Tarihi:"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDateTimePicker ID="RadDateTimePicker4" runat="server" Width="170px" TimeView-Enabled="true"
                                TimePopupButton-Visible="true" DateInput-DisplayDateFormat="dd.MM.yyyy | HH:mm"
                                DateInput-DateFormat="d-MM-yyyy | HH:mm" Culture="Turkish (Turkey)">
                            </telerik:RadDateTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cc3:YilDonemCombo Width="350px" ID="yildonem_basvuru" runat="server" Direction="Horizontal"
                                LabelDonem="Başvuru Dönemi" LabelYil="Başvuru Yılı" ShowDonem="True" OnDonemSelectedEventHandler="yildonem_basvuru_DonemSelectedEventHandler"
                                AutoSelectYilDonem="False" Donem="-1" FixPopupIndex="False" meta:resourcekey="yildonem_basvuruResource1"
                                Yil="-1" />
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
                    <asp:Label ID="Label20" runat="server" Text="Aynı anda kaç dersten vazgeçebilir? :"></asp:Label>
                    <telerik:RadTextBox ID="RadTextBox2" Width="50px" runat="server" MaxLength="40" onkeypress="return isNumberKey(event)"
                        LabelCssClass="" meta:resourcekey="txt_adResource1">
                    </telerik:RadTextBox>
                    <!------label textbox-->
                </div>
                <br />
                <div>
                    <asp:Label ID="Label21" runat="server" Text="Aynı dersten başka dönemde tekrar vazgeçme başvurusu yapabilir mi? :"></asp:Label>
                    <asp:CheckBox runat="server" ID="CheckBox1" AutoPostBack="true" />
                    <!------label checkbox-->
                </div>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
