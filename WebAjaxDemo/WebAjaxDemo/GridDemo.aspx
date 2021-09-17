<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridDemo.aspx.cs" Inherits="WebAjaxDemo.GridDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 192px">
    
        <asp:UpdatePanel ID="_ajaxPanel" runat="server">
            <ContentTemplate>
                <asp:GridView ID="_gridView" runat="server" BackColor="#8C3E95" Width="626px">
                    <EditRowStyle BackColor="#9D5AA5" />
                    <EmptyDataRowStyle BackColor="#9D5AA5" />
                    <HeaderStyle BackColor="#391E8A" />
                    <RowStyle BackColor="#E882AD" BorderStyle="Solid" />
                </asp:GridView>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="_addButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="_addButton" runat="server" onclick="AddButtonClick" 
            Text="Button" />
    
    </div>
    </form>
</body>
</html>
