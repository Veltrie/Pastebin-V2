<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewPaste.aspx.cs" Inherits="VelBin.ViewPaste" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Paste</title>
</head>
<body>
    <div>
        <h1>View Paste</h1>
        <div>
            <label for="title">Title:<br />
            </label>
            <pre>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </pre>
            <br />
        </div>
        <div>
            <label for="content">Content:<br />
            </label>
            <pre>
                <asp:Label ID="lblContent" runat="server"></asp:Label>
            </pre>
        </div>
    </div>
    <div>
    <asp:Label ID="lblBurnAfterReading" runat="server"></asp:Label>
        <div>
    <asp:Label ID="lblBurnStatus" runat="server"></asp:Label>
</div>
</div>
</body>
</html>
