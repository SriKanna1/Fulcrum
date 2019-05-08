<%@ Page Title="" Language="C#" MasterPageFile="~/FulcrumMaster.Master" AutoEventWireup="true" CodeBehind="FieldDataEntry.aspx.cs" Inherits="Fulcrum.FieldDataEntry" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Outlook" ></telerik:RadAjaxLoadingPanel>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <Telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function ShowCheckViolations(TrackingId, PoleId) {
                    var ddlTrackingId = document.getElementById("<%=ddlTrackingNo.ClientID%>");
                    var TrackingId = ddlTrackingId.options[ddlTrackingId.selectedIndex].text;
                    var ddlPole = document.getElementById("<%=ddl_GoToPole.ClientID%>");
                    var PoleId = ddlPole.options[ddlPole.selectedIndex].text;
                    window.radopen("CheckViolations.aspx?TrackingId=" + TrackingId + "&PoleId=" + PoleId, "RadWindow1")
                    return false;
                }
                function CloseModal() {
                    window.close();
                }
                function allowOnlyNumber(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode
                    if (charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;
                    return true;
                }

                //function isDecimal(evt) {
                //    var charCode = (evt.which) ? evt.which : event.keyCode
                //    var parts = evt.srcElement.value.split('.');
                //    if (parts.length > 1 && charCode == 46)
                //        return false;
                //    else {
                //        if (charCode == 46 || (charCode >= 48 && charCode <= 57))
                //            return true;
                //        return false;
                //    }
                //}

                //function makeMoney(source) {
                //    //Grab the value from the element
                //    var money = parseFloat(source.value, 10).toFixed(2);

                //    //Format your value
                // source.value = '$' + money.toString();
                //}

            </script>
        </Telerik:RadCodeBlock> 
        <Telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <Telerik:RadWindow ID="RadWindow1" Width="1055px" ShowContentDuringLoad="false" Modal="true" Animation="Fade" ReloadOnShow="true" VisibleStatusbar="false" Height="600px" runat="server" Behaviors="Close" OnClientClose="CloseModal"></Telerik:RadWindow>
            </Windows>
        </Telerik:RadWindowManager>
        <div class="wrapper">
            <div class="container-fluid">
                <div class="contentArea">
                    <p class="pageHeading">Field Data Entry Page</p>
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
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label for="example-text-input" class="col-sm-2 col-form-label">
                                                    Pole:<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_GoToPole" ValidationGroup="A" runat="server" InitialValue="0" Text="*" ErrorMessage="Please select pole no" ForeColor="Red" ControlToValidate="ddl_GoToPole" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddl_GoToPole" CssClass="custom-select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_GoToPole_SelectedIndexChanged" Width="95"></asp:DropDownList>
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
                            <div class="w3-half">
                                <div class="w3-row-padding padding-none">
                                    <div class="w3-quarter">
                                        <label>Component ID</label><br />
                                        <asp:TextBox ID="txt_Component_Id" ReadOnly="true" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Station &nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                        <asp:TextBox ID="txt_Station" class="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>CommMap# &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvCommMap" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter CommMap#" ForeColor="Red" ControlToValidate="txtCommMap" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:TextBox ID="txtCommMap" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Latitude</label><br />
                                        <asp:TextBox ID="txtLatitude" ReadOnly="true" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="w3-row-padding padding-none">
                                    <div class="w3-quarter">
                                        <label>Owner<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_Owner" ValidationGroup="A" InitialValue="0" runat="server" Text="*" ErrorMessage="Please Enter Owner" ForeColor="Red" ControlToValidate="ddl_Owner" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:DropDownList ID="ddl_Owner" CssClass="custom-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Power Map# &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvPowerMap" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter PowerMap#" ForeColor="Red" ControlToValidate="txtPowerMap" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:TextBox ID="txtPowerMap" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>NJUNS# &nbsp;</label><br />
                                        <asp:TextBox ID="txtNJUNS" onkeypress="return allowOnlyNumber(event);" runat="server" class="w3-input w3-border"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Longitude</label><br />
                                        <asp:TextBox ID="txtLongitude" ReadOnly="true" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="w3-half">
                                <div class="w3-row-padding padding-none">
                                    <div class="w3-quarter">
                                        <label>Ht-Class Existing &nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                        <asp:TextBox ID="txt_HtClass_ExistingFT" onkeypress="return allowOnlyNumber(event);" CssClass="w3-input w3-border w3-float" runat="server" Width="40px"></asp:TextBox>
                                        <asp:TextBox ID="txt_HtClass_ExistingInch" onkeypress="return allowOnlyNumber(event);" runat="server" CssClass="w3-input w3-border w3-float" Width="40px"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Ht-Class New &nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvHtClass_NewFeet" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter HtClass New feet" ForeColor="Red" ControlToValidate="txt_HtClass_NewFT" Display="Dynamic"></asp:RequiredFieldValidator>&nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_HtClass_NewInch" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter HtClass New Inch" ForeColor="Red" ControlToValidate="txt_HtClass_NewInch" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txt_HtClass_NewFT" onkeypress="return allowOnlyNumber(event);" CssClass="w3-input w3-border w3-float" runat="server" Width="40px"></asp:TextBox>
                                        <asp:TextBox ID="txt_HtClass_NewInch" onkeypress="return allowOnlyNumber(event);" runat="server" CssClass="w3-input w3-border w3-float" Width="40px"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Temperature &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_Temp" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter Temperature" ForeColor="Red" ControlToValidate="txt_Temp" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:TextBox ID="txt_Temp" onkeypress="return allowOnlyNumber(event);" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Date &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_Date" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Select Date" ForeColor="Red" ControlToValidate="RadDate" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <Telerik:RadDatePicker ID="RadDate" runat="server" CssClass="w3-input w3-border"></Telerik:RadDatePicker>
                                    </div>
                                </div>
                                <div class="w3-row-padding padding-none">
                                    <div class="w3-half">
                                        <label>GPC Construction Cost ($) &nbsp;</label><%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvConstructionCost" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter GPC Construction Cost" ForeColor="Red" ControlToValidate="txtConstructionCost" Display="Dynamic"></asp:RequiredFieldValidator>--%><br />
                                        <%--<asp:RegularExpressionValidator ID="regGPCCost" runat="server" Text="*" ErrorMessage="Invalid number" ValidationGroup="A" ControlToValidate="txtConstructionCost" ValidationExpression="^\d+([,\.]\d{1,2})?"></asp:RegularExpressionValidator>--%>
                                        <%--<Telerik:RadNumericTextBox ID="txtConstructionCost"  DataFormatString ="C"  Type="Currency"   Culture="en-US"   MaxLength="10" Height="30px" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" runat="server"></Telerik:RadNumericTextBox>--%>
                                        <%--<asp:RegularExpressionValidator ID="regGPCCost" runat="server" Text="*" ErrorMessage="Invalid number" ValidationGroup="A" ControlToValidate="txtConstructionCost" ValidationExpression="^\d+([,\.]\d{1,2})?"></asp:RegularExpressionValidator>--%>
                                        <Telerik:RadNumericTextBox ID="txtConstructionCost" Type="Currency" Culture="en-US" CssClass="w3-input w3-border" MaxLength="10" IncrementSettings-InterceptMouseWheel="false" IncrementSettings-InterceptArrowKeys="false" NumberFormat-DecimalDigits="2" NumberFormat-GroupSeparator="" runat="server"></Telerik:RadNumericTextBox>

                                        <%--<asp:TextBox ID="txtConstructionCost"  onblur='makeMoney(this);' onkeypress="return isDecimal(event)" CssClass="w3-input w3-border" runat="server"></asp:TextBox>--%>
                                    </div>
                                    <div class="w3-half">
                                        <label>Location &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_Location" ValidationGroup="A" runat="server" Text="*" ErrorMessage="Please Enter Location" ForeColor="Red" ControlToValidate="txt_Location" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:TextBox ID="txt_Location" CssClass="w3-input w3-border" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-b-30">
                        <div class="w3-row-padding padding-none">
                            <div class="w3-half padding-grid">
                                <asp:Label ID="lblPoaInfo" runat="server"></asp:Label>
                                <div style="width: 600px; height: 435px; overflow-y: auto;">
                                    <Telerik:RadGrid ID="rg_POA" runat="server"  RenderMode="Lightweight" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" AllowSorting="false" AutoGenerateColumns="False" TabIndex="99" TableLayout="Fixed"
                                        OnNeedDataSource="rg_POA_NeedDataSource" OnItemCommand="rg_POA_ItemCommand" OnInsertCommand="rg_POA_InsertCommand" OnUpdateCommand="rg_POA_UpdateCommand" OnItemDataBound="rg_POA_ItemDataBound" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                        <MasterTableView EditMode="InPlace" EnableHeaderContextMenu="true" CommandItemDisplay="Top" TableLayout="Fixed" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage">
                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                            <CommandItemTemplate>
                                                <div style="padding: 5px 5px;">
                                                    <asp:LinkButton ID="LinkButton1" ValidationGroup="A" runat="server" ForeColor="Blue" Font-Bold="true" CommandName="InitInsert" Visible='<%# !rg_POA.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle; width:20px; height:18px;" alt="" src="Images/Add-new1.png"/> &nbsp;Add New POM</asp:LinkButton>
                                                </div>
                                            </CommandItemTemplate>
                                            <ColumnGroups>
                                                <Telerik:GridColumnGroup HeaderText="POM" Name="POM"></Telerik:GridColumnGroup>
                                                <Telerik:GridColumnGroup HeaderText="New POM" Name="NewPOM"></Telerik:GridColumnGroup>
                                            </ColumnGroups>
                                            <Columns>
                                                <Telerik:GridButtonColumn ItemStyle-HorizontalAlign="Center" ButtonType="ImageButton" HeaderText="Delete" ImageUrl="./images/icon-delete.png" CommandName="cmdDelete" UniqueName="cmdDelete"></Telerik:GridButtonColumn>
                                                <Telerik:GridEditCommandColumn EditImageUrl="Images/icon-pencil.gif" HeaderText="Edit" ButtonType="ImageButton" UniqueName="EditCommandColumn"></Telerik:GridEditCommandColumn>
                                                <Telerik:GridBoundColumn HeaderStyle-Font-Bold="true" DataField="_child_record_id" Display="false" HeaderText="child_record_id" UniqueName="child_record_id"></Telerik:GridBoundColumn>

                                                <Telerik:GridTemplateColumn HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCompanyy" DataTextField="Company" DataValueField="Company" runat="server" UniqueName="Company"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="135px" />
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlType" DataTextField="Type" DataValueField="Type" runat="server" UniqueName="Type"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="145px" />
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="child_record_id" Display="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtChildRecordId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"_child_record_id")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="_record_Id" Display="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRecordId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"_record_Id")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>

                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" ColumnGroupName="POM" HeaderStyle-Font-Bold="true" DataField="PomFt" Visible="true" HeaderText="Ft '" UniqueName="PomFt"></Telerik:GridBoundColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" ColumnGroupName="POM" HeaderStyle-Font-Bold="true" DataField="PomIn" Visible="true" HeaderText="In ''" UniqueName="PomIn"></Telerik:GridBoundColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" ColumnGroupName="NewPOM" HeaderStyle-Font-Bold="true" DataField="NewFt" Visible="true" HeaderText="Ft '" UniqueName="NewFt"></Telerik:GridBoundColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" ColumnGroupName="NewPOM" HeaderStyle-Font-Bold="true" DataField="NewIn" Visible="true" HeaderText="In ''" UniqueName="NewIn"></Telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </Telerik:RadGrid>
                                </div>
                            </div>
                            <div class="w3-half padding-grid">
                                <asp:Label ID="lblMidspanInfo" Visible="false" runat="server"></asp:Label>
                                <div style="width: 680px; height: 435px; overflow-y: auto;">
                                    <Telerik:RadGrid ID="rg_Midspans"   runat="server" RenderMode="Lightweight" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" AllowSorting="false" AutoGenerateColumns="False" TabIndex="99" TableLayout="Fixed"
                                        OnNeedDataSource="rg_Midspans_NeedDataSource" OnItemCommand="rg_Midspans_ItemCommand" OnInsertCommand="rg_Midspans_InsertCommand" OnUpdateCommand="rg_Midspans_UpdateCommand" OnItemDataBound="rg_Midspans_ItemDataBound" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                        <MasterTableView EditMode="InPlace" TableLayout="Fixed" CommandItemDisplay="Top" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                            <CommandItemTemplate>
                                                <div style="padding: 5px 5px;">
                                                    <asp:LinkButton ID="LinkButton2" ValidationGroup="A" runat="server" ForeColor="Blue" Font-Bold="true" CommandName="InitInsert" Visible='<%# !rg_Midspans.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle; width:20px; height:18px;" alt="" src="Images/Add-new1.png"/> &nbsp;Add New Midspan</asp:LinkButton>
                                                </div>
                                            </CommandItemTemplate>
                                            <ColumnGroups>
                                                <Telerik:GridColumnGroup HeaderText="Midspan" Name="Midspan"></Telerik:GridColumnGroup>
                                            </ColumnGroups>
                                            <Columns>
                                                <Telerik:GridButtonColumn ItemStyle-HorizontalAlign="Center" ButtonType="ImageButton" HeaderText="Delete" ImageUrl="./images/icon-delete.png" CommandName="cmdDelete" UniqueName="cmdDelete"></Telerik:GridButtonColumn>
                                                <Telerik:GridEditCommandColumn EditImageUrl="Images/icon-pencil.gif" HeaderText="Edit" ButtonType="ImageButton" UniqueName="EditCommandColumn"></Telerik:GridEditCommandColumn>
                                                <Telerik:GridBoundColumn ItemStyle-Width="22px" ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="_child_record_id" Display="false" HeaderText="child_record_id" UniqueName="child_record_id"></Telerik:GridBoundColumn>
                                                <Telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="child_record_id" Display="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtChildRecordId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"_child_record_id")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="_record_Id" Display="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRecordId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"_record_Id")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="[To]" Visible="true" HeaderText="To" UniqueName="[To]"></Telerik:GridBoundColumn>
                                                <Telerik:GridTemplateColumn HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCompanyy" DataTextField="Company" DataValueField="Company" runat="server" UniqueName="Company"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="135px" />
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlType" DataTextField="Type" DataValueField="Type" runat="server" UniqueName="Type"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="145px" />
                                                </Telerik:GridTemplateColumn>
                                                <%--<Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="Company" Visible="true" HeaderText="Company" UniqueName="Company"></Telerik:GridBoundColumn>--%>
                                                <%--<Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="Type" Visible="true" HeaderText="Type" UniqueName="Type"></Telerik:GridBoundColumn>--%>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" ColumnGroupName="Midspan" HeaderStyle-Font-Bold="true" DataField="mid_ft" Visible="true" HeaderText="Ft '" UniqueName="mid_ft"></Telerik:GridBoundColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" ColumnGroupName="Midspan" HeaderStyle-Font-Bold="true" DataField="mid_in" Visible="true" HeaderText="In ''" UniqueName="mid_in"></Telerik:GridBoundColumn>
                                                <Telerik:GridTemplateColumn HeaderText="Over">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlOver" DataTextField="Over" DataValueField="Over" runat="server" UniqueName="Over"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="135px" />
                                                </Telerik:GridTemplateColumn>
                                                <%--<Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="[Over]" Visible="true" HeaderText="Over" UniqueName="[Over]"></Telerik:GridBoundColumn>--%>
                                            </Columns>
                                        </MasterTableView>
                                    </Telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                        <div class="w3-row-padding padding-none">
                            <div>
                                <asp:Label ID="lblNotesInfo" Visible="false" runat="server"></asp:Label>
                                <div style="width: 1280px; height: 435px; overflow-y: auto;">
                                    <Telerik:RadGrid ID="rg_Notes"  runat="server" RenderMode="Lightweight" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" AllowSorting="false" AutoGenerateColumns="False" TabIndex="99" TableLayout="Fixed"
                                        OnNeedDataSource="rg_Notes_NeedDataSource" OnItemCommand="rg_Notes_ItemCommand" OnItemDataBound="rg_Notes_ItemDataBound" OnUpdateCommand="rg_Notes_UpdateCommand" OnInsertCommand="rg_Notes_InsertCommand" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                        <MasterTableView EditMode="InPlace" CommandItemDisplay="Top" InsertItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                                            <CommandItemTemplate>
                                                <div style="padding: 5px 5px;">
                                                    <asp:LinkButton ID="LinkButton2" ValidationGroup="A" runat="server" ForeColor="Blue" Font-Bold="true" CommandName="InitInsert" Visible='<%# !rg_Notes.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle; width:20px; height:18px;" alt="" src="Images/Add-new1.png"/> &nbsp;Add New Notes</asp:LinkButton>
                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                                <Telerik:GridButtonColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px" ButtonType="ImageButton" HeaderText="Delete" ImageUrl="./images/icon-delete.png" CommandName="cmdDelete" UniqueName="cmdDelete"></Telerik:GridButtonColumn>
                                                <Telerik:GridEditCommandColumn ItemStyle-Width="45px" EditImageUrl="Images/icon-pencil.gif" HeaderText="Edit" ButtonType="ImageButton" UniqueName="EditCommandColumn"></Telerik:GridEditCommandColumn>
                                                <Telerik:GridTemplateColumn HeaderText="Company">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCompanyy" DataTextField="Company" DataValueField="Company" runat="server" UniqueName="Company"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderText="Notes">
                                                    <ItemTemplate>
                                                        <%--<asp:DropDownList ID="ddlNotes" Width="950px" DataTextField="Notes" DataValueField="Notes" runat="server" UniqueName="Notes"></asp:DropDownList>--%>
                                                        <Telerik:RadComboBox RenderMode="Lightweight" EnableAjaxSkinRendering="true" MaxHeight="150" ID="ddlNotes" Width="850px" MarkFirstMatch="True" Filter="Contains" AllowCustomText="true" runat="server" EmptyMessage="Notes Desc"></Telerik:RadComboBox>

                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="Id" Display="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Id")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" Display="false" DataField="Id" HeaderText="Id" UniqueName="Id"></Telerik:GridBoundColumn>
                                                <Telerik:GridBoundColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" DataField="Step#" HeaderText="Step#" UniqueName="Step#"></Telerik:GridBoundColumn>
                                                <Telerik:GridTemplateColumn ItemStyle-CssClass="GridTextCss" HeaderStyle-Font-Bold="true" UniqueName="Share" DataField="Share" Visible="true" HeaderText="$Share">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkShare" Enabled="false" Checked='<%# Eval("Share").ToString() == "1" ? true:false %>' runat="server" />
                                                    </ItemTemplate>
                                                </Telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </Telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="w3-row-padding padding-none">
                        <div class="w3-row-padding padding-none">
                            <div class="w3-half padding-grid">
                                <label>Field Notes &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfv_FieldNotes" runat="server" Text="*" ErrorMessage="Please Enter Field Notes" ForeColor="Red" ValidationGroup="A" ControlToValidate="txt_FieldNotes" Display="Dynamic"></asp:RequiredFieldValidator></label>
                                <asp:TextBox ID="txt_FieldNotes" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
                            </div>
                            <div class="w3-half padding-grid">
                                <label>Engineer Notes &nbsp;</label><%--<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvEngNotes" runat="server" Text="*" ErrorMessage="Please Enter Engineer Notes" ForeColor="Red" ValidationGroup="A" ControlToValidate="txtEngNotes" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                <asp:TextBox ID="txtEngNotes" runat="server" CssClass="w3-input w3-border"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="w3-row-padding padding-none">
                        <div class="w3-half padding-grid">
                            <div class="w3-third">
                                <asp:Button ID="btn_CheckViolations" CssClass="btn btn-primary" runat="server" ValidationGroup="A" Text="Check Violations" OnClick="btn_CheckViolations_Click" />
                            </div>
                        </div>
                        <div class="w3-half padding-grid">
                            <asp:CheckBox ID="chkPoleComplete" runat="server" Text="Pole Complete" />
                            <asp:Button ID="btn_Submit" runat="server" Text="insert" ValidationGroup="A" CssClass="btn btn-active btn-lg" OnClick="btn_Submit_Click" />
                            <asp:Label ID="lblInsertInfo" Visible="false" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="w3-row-padding padding-none">
                    <div class="w3-row-padding padding-none">
                        <div id="DivViolation" runat="server">
                            <table>
                                <tr>
                                    <td>Pole#:&nbsp;<asp:TextBox ID="txt_Pole" ReadOnly="true" runat="server" Width="50"></asp:TextBox></td>
                                    <td><27' Ground Clearance(Railroad) 
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <Telerik:RadGrid ID="Rad27GC" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </Telerik:RadGrid>
                                    </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>40 inch violations
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <Telerik:RadGrid ID="Rad40InchVio" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </Telerik:RadGrid>
                                    </div>
                                    </td>
                                    <td><18' Ground Clearance(Highway) 
                                    <div style="width: 480px; overflow-y: scroll;">
                                        <Telerik:RadGrid ID="Rad18GCHigh" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                            <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                        </Telerik:RadGrid>
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
                                            <Telerik:RadGrid ID="Rad30InchVio" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="width: 480px; overflow-y: scroll;">
                                            <Telerik:RadGrid ID="Rad156GCTrav" Height="80px" ShowHeader="false" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
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
                                            <Telerik:RadGrid ID="Rad12InchVio" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="width: 480px; overflow-y: scroll;">
                                            <Telerik:RadGrid ID="Rad13GCRural" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
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
                                            <Telerik:RadGrid ID="Rad04InchVio" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="width: 480px; overflow-y: scroll;">
                                            <Telerik:RadGrid ID="Rad96GCPedOnly" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
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
                                            <Telerik:RadGrid ID="RadMSSeparation30" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
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
                                            <Telerik:RadGrid ID="RadMSSeparation12" ShowHeader="false" Height="80px" runat="server" ItemStyle-CssClass="griditems" AlternatingItemStyle-CssClass="griditemsalternate" HeaderStyle-CssClass="gridheading" FilterItemStyle-CssClass="gridfilter" BorderColor="#CDCDCD" HeaderStyle-BorderColor="#CDCDCD">
                                                <MasterTableView NoMasterRecordsText=""></MasterTableView>
                                            </Telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:ValidationSummary ShowMessageBox="true" ShowSummary="false" ID="ValidationSummary1" HeaderText="You must enter a value in the following fields:" DisplayMode="BulletList" EnableClientScript="true" runat="server" />--%>
    </Telerik:RadAjaxPanel>
</asp:Content>
