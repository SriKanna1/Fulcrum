<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckViolations.aspx.cs" Inherits="Fulcrum.CheckViolations" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Check Violation</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/icons.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/circle.css" rel="stylesheet" type="text/css" />
    <link href="css/styleW3.css" rel="stylesheet" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWin() {
                GetRadWindow().BrowserWindow.location.href = 'FieldDataEntry.aspx';
                GetRadWindow().close();
            }
            function GetRadWindow() {
                var oWindow = null; if (window.radWindow)
                    oWindow = window.radWindow; else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; return oWindow;
            }
        </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
          
                <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
                    <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px" ForeColor="Red"></asp:Label>
                    <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px" ForeColor="Green"></asp:Label>
                </div> 
                    <div id="cm-table">
                        <table>
                            <tr>
                                <td>Pole#:&nbsp;<asp:TextBox ID="txt_Pole" runat="server" AutoPostBack="true" OnTextChanged="txt_Pole_TextChanged" Width="50"></asp:TextBox></td>
                                <td><27' Ground Clearance(Railroad) 
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad27GC" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>40 inch violations
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad40InchVio" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                                <td><18' Ground Clearance(Highway) 
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad18GCHigh" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>30 inch violations</td>
                                <td><15'6" Ground Clearance (Trav. Veh)</td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad30InchVio" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad156GCTrav" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>12 inch violations</td>
                                <td><13" Ground Clearance (Rural Rds)</td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad12InchVio" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad13GCRural" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>4 inch violations</td>
                                <td><9'6" Ground Clearance (Ped Only)</td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad04InchVio" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="Rad96GCPedOnly" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Midspan Separation Violations (30")</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="RadMSSeparation30" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Midspan Separation Violations (12")</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <telerik:RadGrid ID="RadMSSeparation12" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div> 
            <asp:ValidationSummary ShowMessageBox="true" ShowSummary="false" ID="ValidationSummary1" HeaderText="You must enter a value in the following fields:" DisplayMode="BulletList" EnableClientScript="true" runat="server" />
        </telerik:RadAjaxPanel>
    </form>
</body>
</html>
