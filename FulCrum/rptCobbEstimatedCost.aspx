<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/FulcrumMaster.Master" CodeBehind="rptCobbEstimatedCost.aspx.cs" Inherits="FulCrum.rptCobbEstimatedCost" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=5.1.11.713, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
  <div class="wrapper">
        <div class="errorDivForms">
           <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
                    <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px"></asp:Label>
                    <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px"></asp:Label>
                </div>
        </div>
        <div class="container-fluid">
              <p class="pageHeading">Estimated Cost Report</p>
                <div class="w3-row-padding padding-none">
                <telerik:ReportViewer ID="ReportViewer1" runat="server" Width="1050px" ShowPrintButton="false"
                    Height="600px" CssClass="reportStyle1"
                    Report="FulCrumReport_Lib.CobbEstimatedCostReport, FulCrumReport_Lib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" Resources-ExportSelectFormatText="Export to the selected format">
                </telerik:ReportViewer>
            </div>
        </div>
    </div>
 </asp:Content>
