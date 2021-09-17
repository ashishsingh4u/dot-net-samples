<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HttpPushFromMsSql._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="font-family: Tahoma, Verdana, Sans-Serif; font-size: 70%;"> <!--http://css-discuss.incutio.com/?page=InternetExplorerWinBugs-->
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" >
            <Scripts>
                <asp:ScriptReference Path="Script/jquery-1.2.6.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
                <asp:Label ID="DebugLabel" runat="server" Text="..."></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
