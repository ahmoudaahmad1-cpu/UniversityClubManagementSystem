<%@ Page Title="Clubs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clubs.aspx.cs" Inherits="UniversityClubManagementSystem.Clubs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="fw-bold mb-4">Browse Clubs</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

    <div class="card shadow-sm mb-4">
        <div class="card-body">
            <div class="row g-3 align-items-end">
                <div class="col-md-5">
                    <label for="txtSearch" class="form-label">Search</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by name or description..." />
                </div>
                <div class="col-md-4">
                    <label for="ddlCategory" class="form-label">Category</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
    </div>

    <asp:GridView ID="gvClubs" runat="server"
        CssClass="table table-striped table-hover"
        AutoGenerateColumns="False"
        EmptyDataText="No clubs found."
        HeaderStyle-CssClass="table-dark">
        <Columns>
            <asp:BoundField DataField="ClubName" HeaderText="Club Name" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="PresidentName" HeaderText="President" />
            <asp:BoundField DataField="MemberCount" HeaderText="Members" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <a href='<%# "ClubDetails.aspx?id=" + Eval("ClubId") %>' class="btn btn-sm btn-outline-primary">View Details</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
