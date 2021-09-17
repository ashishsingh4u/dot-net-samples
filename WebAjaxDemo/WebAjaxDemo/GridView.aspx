<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridView.aspx.cs" Inherits="WebAjaxDemo.GridView" %>
      
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
      
     <html xmlns="http://www.w3.org/1999/xhtml">
     <head id="Head1" runat="server">
         <title></title>
         <style type="text/css">
         .none {display:none;}
        </style>
        <script src="jquery-1.5.1.min.mic.js" type="text/javascript"></script>
        <script type="text/javascript">
            function CallPageMethod(methodName, onSuccess, onFail,onComplete) {
                var args = '';
                var l = arguments.length;
                if (l > 4) {
                    for (var i = 4; i < l - 1; i += 2) {
                        if (args.length != 0) args += ',';
                        args += '"' + arguments[i] + '":"' + arguments[i + 1] + '"';
                    }
                }
                var loc = window.location.href;
                loc = (loc.substr(loc.length - 1, 1) == "/") ? loc + "default.aspx" : loc;
                $.ajax({
                    type: "POST",
                    url: loc + "/" + methodName,
                    data: "{" + args + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: onSuccess,
                    fail: onFail,
                    complete: onComplete
                });
            }

            function select(index) {
                var id = $("#id" + index).html();
                var title = $("#ttl" + index).html();
                var author = $("#auth" + index).html();

                CallPageMethod("SelectBook", success, fail,complete,
                "id", id,
                "title", title,
                "author", author);
            }

            function success(response) {
                var textBox = document.getElementById("_messageText");
                textBox.value = response.d;
            }

            function fail(response) {
                
            }
            function complete(xhr, status) 
            {
                
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
        <asp:GridView 
            ID="gv" 
            runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Select"><ItemTemplate><a href="javascript:void(0);" onclick="select(<%= index.ToString() %>);">Select</a></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Title"><ItemTemplate><span id="id<%= this.index.ToString() %>" class="none"><asp:Literal ID="id" Text='<%# Bind("ID") %>' runat="server" /></span><span id="ttl<%= this.index.ToString() %>"><asp:Literal ID="ttl" Text='<%# Bind("Title") %>' runat="server" /></span> </ItemTemplate> </asp:TemplateField> <asp:TemplateField HeaderText="Author" SortExpression="Author"> <ItemTemplate> <span id="auth<%= this.index.ToString() %>"><asp:Literal ID="author" Text='<%# Bind("Author") %>' runat="server" /></span> </ItemTemplate> </asp:TemplateField> <asp:TemplateField HeaderText="Publish Date" SortExpression="PublishDateShort"> <ItemTemplate> <span id="pubDate<%= this.index.ToString() %>"><asp:Literal ID="dt" Text='<%# Bind("PublishDateShort") %>' runat="server" /></span><% this.index++; %> </ItemTemplate> </asp:TemplateField>
            </Columns>
       </asp:GridView>
        <asp:TextBox ID="_messageText" runat="server" Width="358px"></asp:TextBox>
        </form>
    </body>
    </html>
