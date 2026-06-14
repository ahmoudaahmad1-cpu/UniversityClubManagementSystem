<%@ Page Title="Manage Students" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="UniversityClubManagementSystem.ManageStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Manage Students</h2>
    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card mb-3">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-8">
                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" CssClass="form-label">Search</asp:Label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by name, email, or username" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary me-2" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:GridView ID="gvStudents" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False" DataKeyNames="StudentId"
                OnRowCommand="gvStudents_RowCommand" EmptyDataText="No students found.">
                <Columns>
                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Username" HeaderText="Username" />
                    <asp:BoundField DataField="Major" HeaderText="Major" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='<%# Convert.ToBoolean(Eval("IsBlocked")) ? "badge bg-danger" : "badge bg-success" %>'>
                                <%# Convert.ToBoolean(Eval("IsBlocked")) ? "Blocked" : "Active" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnToggle" runat="server" CommandName="ToggleBlock" CommandArgument='<%# Eval("StudentId") %>'
                                CssClass="btn btn-sm btn-warning"
                                Text='<%# Convert.ToBoolean(Eval("IsBlocked")) ? "Unblock" : "Block" %>'
                                OnClientClick="return confirm('Are you sure you want to change this student status?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
