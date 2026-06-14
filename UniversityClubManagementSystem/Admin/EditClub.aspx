<%@ Page Title="Edit Club" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditClub.aspx.cs" Inherits="UniversityClubManagementSystem.EditClub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Edit Club</h2>
    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card">
        <div class="card-body">
            <asp:HiddenField ID="hfClubId" runat="server" />
            <div class="mb-3">
                <asp:Label ID="lblClubName" runat="server" AssociatedControlID="txtClubName" CssClass="form-label">Club Name</asp:Label>
                <asp:TextBox ID="txtClubName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvClubName" runat="server" ControlToValidate="txtClubName"
                    ErrorMessage="Club name is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" CssClass="form-label">Description</asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCategory" runat="server" AssociatedControlID="txtCategory" CssClass="form-label">Category</asp:Label>
                <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="txtCategory"
                    ErrorMessage="Category is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblPresidentName" runat="server" AssociatedControlID="txtPresidentName" CssClass="form-label">President Name</asp:Label>
                <asp:TextBox ID="txtPresidentName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPresidentName" runat="server" ControlToValidate="txtPresidentName"
                    ErrorMessage="President name is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3 form-check">
                <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-check-input" />
                <asp:Label ID="lblIsActive" runat="server" AssociatedControlID="chkIsActive" CssClass="form-check-label">Is Active</asp:Label>
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Club" CssClass="btn btn-primary me-2" OnClick="btnUpdate_Click" />
            <a href="ManageClubs.aspx" class="btn btn-secondary">Cancel</a>
        </div>
    </div>
</asp:Content>
