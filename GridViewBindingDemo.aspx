<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewBindingDemo.aspx.cs" Inherits="GridviewManipulationsDemo.GridViewBindingDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            color: #FF6600;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvEmpDetails" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gvEmpDetails_RowCancelingEdit" OnRowEditing="gvEmpDetails_RowEditing" OnSelectedIndexChanged="gvEmpDetails_SelectedIndexChanged" OnRowDataBound="gvEmpDetails_RowDataBound" OnRowDeleting="gvEmpDetails_RowDeleting" OnRowUpdating="gvEmpDetails_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpNo">
                    <EditItemTemplate>
                        <asp:Label ID="lbl_ER_EmpNo" runat="server" Text='<%# Eval("EmpNo") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_IR_EmpNo" runat="server" Text='<%# Eval("EmpNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ER_Name" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_IR_Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Salary">
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ER_Salary" runat="server" Text='<%# Eval("Salary") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_IR_Salary" runat="server" Text='<%# Eval("Salary") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_ER_Job" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="lbl_ER_Job" runat="server" Text='<%# Eval("Designation") %>' Visible="False"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_IR_Job" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandName="Select" Text="Show"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkModify" runat="server" CausesValidation="True" CommandName="Update" Text="Modify"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm(&quot;Are you sure?&quot;);"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
        <br />
        <br />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;Do you want to delete all the selected records?&quot;);" Text="Delete" />
&nbsp;<span class="auto-style1"><strong>Note: This delete button is to delete the selected records</strong></span></form>
</body>
</html>
