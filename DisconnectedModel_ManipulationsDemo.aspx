<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisconnectedModel_ManipulationsDemo.aspx.cs" Inherits="GridviewManipulationsDemo.DisconnectedModel_ManipulationsDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADO.NET CRUD operations using Disconnected Model</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%">
    <tr>
    <td style="text-align:right">
        <asp:Label ID="lblEmpID" runat="server" Text="Employee ID"></asp:Label></td>
    <td>
        <asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
            Text="Search" />
        </td>
    </tr>
     <tr>
     <td style="text-align:right">
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></td>
    <td>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
     <td style="text-align:right">
        <asp:Label ID="lblSal" runat="server" Text="Salary"></asp:Label></td>
    <td>
        <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
     <td style="text-align:right">
         <asp:Label ID="lblJob" runat="server" Text="Designation"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlJob" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJob_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="txtJob" runat="server" Visible="False"></asp:TextBox>
         </td>
    </tr>
     <tr>
    <td style="text-align:center" colspan="2">
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" 
            onclick="btnAddNew_Click" />
        &nbsp;
        <asp:Button ID="btnModify" runat="server" Text="Modify" 
            onclick="btnModify_Click" />
        &nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" 
            onclick="btnRemove_Click" OnClientClick="return confirm(&quot;Do you want to remove the employee&quot;);" style="height: 26px" />
        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Reset" onclick="btnCancel_Click" 
             />
         </td>
    
    </tr>
    <tr>
    <td style="text-align:center" colspan="2">
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </td>
    
    </tr>
    </table>
    </form>
</body>
</html>
