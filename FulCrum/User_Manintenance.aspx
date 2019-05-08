<%@ Page Title="" Language="C#" MasterPageFile="~/FulcrumMaster.Master" AutoEventWireup="true" CodeBehind="User_Manintenance.aspx.cs" Inherits="Fulcrum.User_Manintenance" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <Telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Outlook"></Telerik:RadAjaxLoadingPanel>
    <Telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

        <div class="wrapper">
            <div class="container-fluid">
                <div class="contentArea">
                    <p class="pageHeading">User Maintenance</p>
                    <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
                        <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px" ForeColor="Red"></asp:Label>
                        <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px" ForeColor="Green"></asp:Label>
                    </div>

                    <div class="row m-b-30" style="background-color: #fff; padding: 10px 5px;">
                        <div class="w3-row-padding">
                            <div class="w3-half">
                                <div class="w3-row-padding padding-none">
                                    <div class="w3-quarter">
                                        <label>
                                            First Name
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvFirstName" runat="server" Text="*" ErrorMessage="Please enter first name" ForeColor="Red" ControlToValidate="txtFirstName" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>
                                            Last Name
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvLastName" runat="server" Text="*" ErrorMessage="Please enter last name" ForeColor="Red" ControlToValidate="txtLastName" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>
                                            Email
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvEmail" runat="server" Text="*" ErrorMessage="Please enter email" ForeColor="Red" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regEmail" runat="server" SetFocusOnError="true" Text="*" ErrorMessage="Invalid email." Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="^\w+([-+.']\w+)*@pike.com" /></label><br />
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" Width="294px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="w3-row-padding padding-none">
                                      <div class="w3-quarter"></div> <div class="w3-quarter"></div>
                                    
                                 </div>
                            </div>
                            <div class="w3-half">
                                <div class="w3-row-padding padding-none">
                                    <div class="w3-quarter">
                                        <label>User Role &nbsp;<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvRole" runat="server" InitialValue="0" Text="*" ErrorMessage="Please select role" ForeColor="Red" ControlToValidate="ddlRole" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <asp:DropDownList ID="ddlRole" CssClass="w3-input w3-border" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="w3-quarter">
                                        <label>Application Type &nbsp<asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvAppType" runat="server" InitialValue="0" Text="*" ErrorMessage="Please select application type" ForeColor="Red" ControlToValidate="ddlAppType" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                        <Telerik:RadComboBox  RenderMode="Lightweight" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" EnableAjaxSkinRendering="true" MaxHeight="150" ID="ddlAppType" MarkFirstMatch="True" Filter="Contains" AllowCustomText="true" runat="server" EmptyMessage="Application Type"></Telerik:RadComboBox>
                                        <%--<asp:DropDownList ID="ddlAppType" CssClass="w3-input w3-border" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged"></asp:DropDownList>--%>
                                    </div>
                                     <div class="float-right"> <div class="w3-quarter"> <label>&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                         <asp:Button ID="BtnCancel" CausesValidation="false"  runat="server"  CssClass="btn btn-active btn-lg"  Text="Cancel" OnClick="BtnCancel_Click" />
                                     </div></div> <div></div>  
                                   <div class="float-right"> &nbsp;&nbsp;&nbsp;
                                     <div class="w3-quarter">
                                         <label>&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                        <asp:Button ID="btn_Submit" runat="server" Text="Insert" Width="120px" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                    </div> </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="w3-row-padding padding-none">
                        <Telerik:RadGrid ID="rgUsers" runat="server" AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false" OnNeedDataSource="rgUsers_NeedDataSource" PageSize="20" TableLayout="Fixed" Width="100%" Skin="Bootstrap" RenderMode="Lightweight" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" OnItemCommand="rgUsers_ItemCommand" OnItemDataBound="rgUsers_ItemDataBound">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView EditMode="InPlace">
                                <Columns>
                                    <Telerik:GridBoundColumn DataField="USER_ROLE_ID" FilterControlWidth="50px" HeaderStyle-Wrap="false" HeaderText="USER_ROLE_ID" ItemStyle-HorizontalAlign="Left" UniqueName="USER_ROLE_ID" Display="false"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="USER_ID" FilterControlWidth="50px" HeaderStyle-Wrap="false" HeaderText="USER_ID" ItemStyle-HorizontalAlign="Left" UniqueName="USER_ID" Display="false"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="User_ROLE" FilterControlWidth="50px" HeaderStyle-Wrap="false" HeaderText="User_ROLE" ItemStyle-HorizontalAlign="Left" UniqueName="User_ROLE" Display="false"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="USER_NAME" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="User Name" ItemStyle-HorizontalAlign="Left" UniqueName="USER_NAME" Visible="true"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="ROLE_NAME" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="Role Name" ItemStyle-HorizontalAlign="Left" UniqueName="ROLE_NAME" Visible="true"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="USER_EMAIL" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="User Email" ItemStyle-HorizontalAlign="Left" UniqueName="USER_EMAIL" Visible="true"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="FULCRUM_COMPANY_ID" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="Company Id" ItemStyle-HorizontalAlign="Left" UniqueName="FULCRUM_COMPANY_ID" Display="false"></Telerik:GridBoundColumn>
                                    <Telerik:GridBoundColumn DataField="APPLICATION_NAME" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="Application Name" ItemStyle-HorizontalAlign="Left" UniqueName="APPLICATION_NAME" Visible="true"></Telerik:GridBoundColumn>

                                    <Telerik:GridButtonColumn ItemStyle-HorizontalAlign="Center" ButtonType="ImageButton" HeaderText="Edit" ImageUrl="./Images/icon-pencil.gif" CommandName="cmdEdit" UniqueName="cmdEdit"></Telerik:GridButtonColumn>
                                    <Telerik:GridButtonColumn ItemStyle-HorizontalAlign="Center" ButtonType="ImageButton" HeaderText="Delete" ImageUrl="./images/icon-delete.png" CommandName="cmdDelete" UniqueName="cmdDelete"></Telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </Telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
        
            <asp:ValidationSummary ShowMessageBox="true" ShowSummary="false" ID="ValidationSummary1" HeaderText="You must enter a value in the following fields:" DisplayMode="BulletList" EnableClientScript="true" runat="server" />
    </Telerik:RadAjaxPanel>
</asp:Content>
