<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Fulcrum.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="errorDivForms">
        <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
            <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px" ForeColor="Red"></asp:Label>
            <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px" ForeColor="Green"></asp:Label>
        </div>
    </div>
</body>
</html>
