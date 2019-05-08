<%@ Page Title="" Language="C#" MasterPageFile="~/FulcrumMaster.Master" AutoEventWireup="true" CodeBehind="CobbJobData.aspx.cs" Inherits="Fulcrum.CobbJobData" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Outlook"></Telerik:RadAjaxLoadingPanel>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="wrapper">
            <div class="container-fluid">
                <div class="contentArea">
                    <p class="pageHeading">Job Information</p>
                    <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
                        <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px" ForeColor="Red"></asp:Label>
                        <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px" ForeColor="Green"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title-box">
                                <div class="float-right">
                                    <div class="row" style="width: 500px;"> 
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label for="example-text-input" class="col-sm-4 col-form-label">
                                                    Tracking&nbsp;No:<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvTrackingNo" ValidationGroup="A" runat="server" InitialValue="0" Text="*" ErrorMessage="Please select tracking no" ForeColor="Red" ControlToValidate="ddlTrackingNo" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList CssClass="custom-select" ID="ddlTrackingNo" runat="server" Width="110" AutoPostBack="true" OnSelectedIndexChanged="ddlTrackingNo_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div> 
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="row m-b-30" style="background-color: #fff; padding: 10px 5px;">
                        <div class="w3-row-padding">
                            <div class="w3-row-padding">
                                <div class="w3-half">
                                    <div class="w3-row-padding padding-none">
                                        <%-- <div class="w3-third">
                                        <label>Reference # &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvReference" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please enter reference" ForeColor="Red" ControlToValidate="txtReference" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtReference" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                        <div class="w3-third">
                                            <label>City(nearest) &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvCity" runat="server" Text="*" ErrorMessage="Please enter city" ValidationGroup="A" ForeColor="Red" ControlToValidate="txtCity" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtCity" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w3-third">
                                            <label>Engineer &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvEngineer" runat="server" Text="*" ValidationGroup="A" ErrorMessage="Please enter engineer" ForeColor="Red" ControlToValidate="txtEngineer" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtEngineer" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w3-third">
                                            <label>Engineer Start Date &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_startDate" runat="server" Text="*" ErrorMessage="Please select start date" ForeColor="Red" ValidationGroup="A" ControlToValidate="RadstartDate" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <Telerik:RadDatePicker ID="RadstartDate" runat="server" CssClass="form-control"></Telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="w3-half">
                                    <div class="w3-row-padding padding-none">

                                        <%--  <div class="w3-third">
                                        <label>Work Order # &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvworkorder" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please enter workorder" ForeColor="Red" ControlToValidate="txtworkorder" Display="Dynamic"></asp:RequiredFieldValidator></label>                                        
                                        <asp:TextBox ID="txtworkorder" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                        <div class="w3-third">
                                            <label>County &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvCounty" runat="server" Text="*" ErrorMessage="Please enter county" ValidationGroup="A" ForeColor="Red" ControlToValidate="txtCounty" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtCounty" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w3-third">
                                            <label>Pike Job# &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvPikeJob" runat="server" ValidationGroup="A" Text="*" ErrorMessage="Please enter pike job" ForeColor="Red" ControlToValidate="txtPikeJob" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtPikeJob" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="w3-row-padding">
                                <div class="w3-half">
                                    <div class="w3-row-padding padding-none">
                                        <div class="w3-third">
                                            <label>Field Engineer &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvFieldEng" runat="server" Text="*" ValidationGroup="A" ErrorMessage="Please enter field engineer" ForeColor="Red" ControlToValidate="txtFieldEng" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtFieldEng" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w3-third">
                                            <label>Field Engineer Start Date&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRadDateFileEngStartDate" runat="server" Text="*" ErrorMessage="Please select field engineer start date" ForeColor="Red" ValidationGroup="A" ControlToValidate="RadDateEngStartDate" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <Telerik:RadDatePicker ID="RadDateEngStartDate" runat="server" CssClass="form-control"></Telerik:RadDatePicker>
                                        </div>
                                        <div class="w3-third">
                                            <label>NJUNS Proj Num</label>
                                            <asp:TextBox ID="txtNJUNSProjNum" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="w3-half">
                                    <div class="w3-row-padding padding-none">
                                        <%-- <div class="w3-third">
                                        <label>Region &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRegion" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please enter region" ForeColor="Red" ControlToValidate="txtRegion" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtRegion" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                        <div class="w3-third">
                                            <label>QC Engineer &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvQCEngineer" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please enter QC engineer" ForeColor="Red" ControlToValidate="txtQCEngineer" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtQCEngineer" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="w3-third">
                                            <label>QC Start Date &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvQCDate" runat="server" Text="*" ErrorMessage="Please select QC date" ForeColor="Red" ValidationGroup="A" ControlToValidate="RadQCDate" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <Telerik:RadDatePicker ID="RadQCDate" runat="server" CssClass="form-control"></Telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="w3-row-padding">
                                <div class="w3-half">
                                    <div class="w3-row-padding padding-none">
                                        <div class="w3-third">
                                            <label>Job Type &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvjobtype" InitialValue="0" runat="server" Text="*" ErrorMessage="Please select job type" ValidationGroup="A" ForeColor="Red" ControlToValidate="ddljobtype" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:DropDownList ID="ddljobtype" CssClass="custom-select my-1 mr-sm-2" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="w3-third">
                                            <label>NJUNS Code</label>
                                            <asp:DropDownList ID="ddlNJUNSCode" CssClass="custom-select my-1 mr-sm-2" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="w3-third">
                                            <label>Job Name &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_JobName" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please enter job name" ForeColor="Red" ControlToValidate="txt_JobName" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txt_JobName" CssClass="form-control" Width="310" runat="server"></asp:TextBox>
                                        </div>
                                        <%--     <div class="w3-third">
                                        <label>Head Quarters &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvHeadquaters" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please enter headquarters" ForeColor="Red" ControlToValidate="txtHeadquaters" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtHeadquaters" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>--%>
                                    </div>
                                </div>
                                <div class="w3-half">
                                    <div class="w3-row-padding padding-none">

                                        <div class="w3-third">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w3-row-padding">
                            <div class="w3-row-padding padding-none">
                                <div class="w3-half padding-grid">
                                    <asp:Label ID="lblPermiteeInfo" Visible="false" runat="server"></asp:Label>
                                    <Telerik:RadGrid ID="rg_Permitee" runat="server" AllowMultiRowSelection="false" RenderMode="Lightweight" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False" TabIndex="99" TableLayout="Fixed"
                                        OnNeedDataSource="rg_Permitee_NeedDataSource" OnItemDataBound="rg_Permitee_ItemDataBound"
                                        OnInsertCommand="rg_Permitee_InsertCommand" OnUpdateCommand="rg_Permitee_UpdateCommand" GridLines="None" OnItemCommand="rg_Permitee_ItemCommand" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                        <MasterTableView EditMode="InPlace" CommandItemDisplay="Top" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <CommandItemTemplate>
                                                <div style="padding: 5px 5px;">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Blue" Font-Bold="true" CommandName="InitInsert" Visible='<%# !rg_Permitee.MasterTableView.IsItemInserted %>'> &nbsp;Add New Company</asp:LinkButton>
                                                    </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                                <%--<telerik:GridButtonColumn ItemStyle-HorizontalAlign="Center"  ButtonType="ImageButton" HeaderText="Delete"  ImageUrl="./images/icon-delete.png" CommandName="cmdDelete" UniqueName="cmdDelete"></telerik:GridButtonColumn>--%>
                                                <Telerik:GridEditCommandColumn EditImageUrl="Images/icon-pencil.gif" HeaderText="Edit" ButtonType="ImageButton" UniqueName="EditCommandColumn"></Telerik:GridEditCommandColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="CompanyName" Visible="true" HeaderText="Company Name" UniqueName="CompanyName"></Telerik:GridBoundColumn>

                                                <Telerik:GridTemplateColumn HeaderText="Permitee">
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="RadioButton1" Checked='<%# Eval("Permitee").ToString() == "1" ? true:false %>' Enabled="false" runat="server" />
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </Telerik:RadGrid>
                                </div>
                                <div class="w3-half padding-grid">
                                    <label style="color: red;">NOTE: Add or Remove Company Names from this list as needed ---------> Check the box beside the company who submitted this permit(one company only)</label>
                                </div>
                            </div>
                        </div>
                        <div class="w3-row-padding">
                            <div class="w3-row-padding padding-none">
                                <asp:Button ID="btn_JobInsert" CssClass="btn btn-primary btn-lg btn-block" OnClick="btn_JobInsert_Click" runat="server" ValidationGroup="A" CausesValidation="true" Text="Update" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Telerik:RadAjaxPanel>
</asp:Content>
