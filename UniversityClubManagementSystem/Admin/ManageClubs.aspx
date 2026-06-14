<%@ Page Title="Manage Clubs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageClubs.aspx.cs" Inherits="UniversityClubManagementSystem.ManageClubs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="page-title mb-0">Manage Clubs</h2>
        <a href="AddClub.aspx" class="btn btn-success">Add New Club</a>
    </div>
    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card mb-3">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-8">
                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" CssClass="form-label">Search</asp:Label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by club name, category, or president" />
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
            <asp:GridView ID="gvClubs" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False" DataKeyNames="ClubId"
                OnRowCommand="gvClubs_RowCommand" EmptyDataText="No clubs found.">
                <Columns>
                    <asp:BoundField DataField="ClubName" HeaderText="Club Name" />
                    <asp:BoundField DataField="Category" HeaderText="Category" />
                    <asp:BoundField DataField="PresidentName" HeaderText="President" />
                    <asp:BoundField DataField="MemberCount" HeaderText="Members" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "badge bg-success" : "badge bg-secondary" %>'>
                                <%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <a href='<%# "EditClub.aspx?id=" + Eval("ClubId") %>' class="btn btn-sm btn-primary me-1">Edit</a>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteClub" CommandArgument='<%# Eval("ClubId") %>'
                                CssClass="btn btn-sm btn-danger" Text="Delete"
                                OnClientClick="return confirm('Are you sure you want to delete this club?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
