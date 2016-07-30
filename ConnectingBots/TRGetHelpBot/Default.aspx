<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TRGetHelpBot.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BOT</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblVersion" runat="server"></asp:Label>

     <p>Describe your bot here and your terms of use etc.</p>
    <p>Visit <a href="https://www.botframework.com/">Bot Framework</a> to register your bot. When you register it, remember to set your bot's endpoint to <pre>https://<i>your_bots_hostname</i>/api/messages</pre></p>
        
    </div>
    </form>
</body>
</html>
