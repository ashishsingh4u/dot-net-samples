<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_Default.aspx.cs" Inherits="WebAjaxDemo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function test() {
            PageMethods.GetDate(OnSucceeded, OnFailed);
        }

        function OnFailed(error) {
            // Alert user to the error.
            alert(error.get_message());
        }
        function OnSucceeded(resultString) {
            document.getElementById("_data").value = resultString;
        }

</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" 
            ScriptMode="Release">
        </asp:ScriptManager>
    
        <asp:TextBox ID="_data" runat="server" Width="284px"></asp:TextBox>
    
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click"/>
    
        <input id="Button2" type="button" value="button" onclick="test()" /></div>
    </form>
</body>
</html>
