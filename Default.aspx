<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList runat="server" ID="ddlA">
            <asp:ListItem Text="C#" Value="1" Group="Microsoft"></asp:ListItem>
            <asp:ListItem Text="VB.net" Value="2" Group="Microsoft"></asp:ListItem>
            <asp:ListItem Text="PHP" Value="3" Group="Open Source"></asp:ListItem>
            <asp:ListItem Text="Java" Value="4" Group="Open Source" Enabled="false"></asp:ListItem>
            <asp:ListItem Text="Perl" Value="5" Group="Open Source"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button runat="server" ID="btnSubmit" Text="Submit" />
    </div>
    </form>
</body>
</html>
