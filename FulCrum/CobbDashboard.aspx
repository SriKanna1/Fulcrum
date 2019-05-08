<%@ Page Title="" Language="C#" MasterPageFile="~/FulcrumMaster.Master" AutoEventWireup="true" CodeBehind="CobbDashboard.aspx.cs" Inherits="Fulcrum.CobbDashboard" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper">
        <div class="errorDivForms">
            <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
                <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px" ForeColor="Red"></asp:Label>
                <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px" ForeColor="Green"></asp:Label>
            </div>
        </div>

        <div class="container-fluid">
             <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="float-right">
                            <div class="row" style="width: 500px;">
                                <div class="col-sm-6">
                                    <div class="form-group row"> 
                                         <label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                        <div class="col-sm-6">
                                            <label for="example-text-input" class="col-sm-4 col-form-label">Tracking&nbsp;No:<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvTrackingNo" runat="server" InitialValue="0" Text="*" ErrorMessage="Please select tracking no" ForeColor="Red" ControlToValidate="ddlTrackingNo" ValidationGroup="A" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group row">
                                        <asp:DropDownList CssClass="custom-select" ID="ddlTrackingNo" runat="server" Width="110"></asp:DropDownList>
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" ValidationGroup="A" Width="80px" CssClass="btn btn-primary" OnClick="btnDelete_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-xl-12">
                    <div>
                        <div class="card-body">
                            <h4 class="mt-0 header-title" style="margin-left: 20px;">Forms</h4>
                            <div>
                                <div class="w3-row-padding padding-none">
                                    <div>
                                        <div class="dashboardDiv1 dashboardOuterDiv">
                                            <div class="dashboardOuterDiv01">
                                                <asp:ImageButton ID="ImageButton3" ImageUrl="Images/field-data-entry.png" runat="server" PostBackUrl="CobbFieldDataEntry.aspx" AlternateText="Field Data Entry" CssClass="vAlignBottom" /></div>
                                            <div class="dashboardOuterDiv02"><a href="CobbFieldDataEntry.aspx">FIELD DATA ENTRY</a></div>
                                        </div>
                                        <div class="dashboardDiv1 dashboardOuterDiv">
                                            <div class="dashboardOuterDiv01">
                                                <asp:ImageButton ID="ImageButton4" ImageUrl="Images/jobadata.png" runat="server" PostBackUrl="CobbJobData.aspx" AlternateText="Job Data" CssClass="vAlignBottom" /></div>
                                            <div class="dashboardOuterDiv02"><a href="CobbJobData.aspx">JOB DATA</a></div>
                                        </div>
                                        <div class="w3-clear">&nbsp;</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" runat="server">
                            <h4 class="mt-0 header-title" style="margin-left: 20px;">Reports</h4>
                            <div>
                                <div class="w3-row-padding padding-none">
                                    <div>
                                        <div class="dashboardDiv1 dashboardOuterDiv">
                                            <div class="dashboardOuterDiv01">
                                                <asp:ImageButton ID="ImageButton6" ImageUrl="Images/work-location-company.png" runat="server" PostBackUrl="rptCobbWorkLocationByCompany.aspx" AlternateText="Work Location Report By Company" CssClass="vAlignBottom" /></div>
                                            <div class="dashboardOuterDiv02"><a href="rptCobbWorkLocationByCompany.aspx">WORK LOCATION BY COMPANY</a></div>
                                        </div>
                                        <div class="dashboardDiv1 dashboardOuterDiv">
                                            <div class="dashboardOuterDiv01">
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="Images/work-location.png" runat="server" PostBackUrl="rptCobbWorkLocation.aspx" AlternateText="Work Location Report" CssClass="vAlignBottom" /></div>
                                            <div class="dashboardOuterDiv02"><a href="rptCobbWorkLocation.aspx">WORK LOCATION</a></div>
                                        </div>
                                        <div class="dashboardDiv1 dashboardOuterDiv">
                                            <div class="dashboardOuterDiv01">
                                                <asp:ImageButton ID="ImageButton2" ImageUrl="Images/estimated-cost.png" runat="server" PostBackUrl="rptCobbEstimatedCost.aspx" AlternateText="Estimated Cost Report" CssClass="vAlignBottom" /></div>
                                            <div class="dashboardOuterDiv02"><a href="rptCobbEstimatedCost.aspx">ESTIMATED COST</a></div>
                                        </div>
                                        <div class="dashboardDiv1 dashboardOuterDiv">
                                            <div class="dashboardOuterDiv01">
                                                <asp:ImageButton ID="ImageButton5" ImageUrl="Images/work-locations.png" runat="server" PostBackUrl="rptCobbWorkLocationImage.aspx" AlternateText="Make-Ready Work Locations" CssClass="vAlignBottom" /></div>
                                            <div class="dashboardOuterDiv02"><a href="rptCobbWorkLocationImage.aspx">WORK LOCATIONS IMAGE</a></div>
                                        </div>
                                        <div class="w3-clear">&nbsp;</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>