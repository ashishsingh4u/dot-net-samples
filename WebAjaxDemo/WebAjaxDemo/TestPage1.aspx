<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage1.aspx.cs" Inherits="WebAjaxDemo.TestPage1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" Width="386px"></asp:TextBox>
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="386px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1Click" Text="Button" 
            Width="149px" />
    
    </div>
    </form>
</body>
</html>
