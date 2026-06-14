<%@ Page Title="Edit Activity" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditActivity.aspx.cs" Inherits="UniversityClubManagementSystem.EditActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Edit Activity</h2>
    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card">
        <div class="card-body">
            <asp:HiddenField ID="hfActivityId" runat="server" />
            <div class="mb-3">
                <asp:Label ID="lblClub" runat="server" AssociatedControlID="ddlClub" CssClass="form-label">Club</asp:Label>
                <asp:DropDownList ID="ddlClub" runat="server" CssClass="form-select" />
                <asp:RequiredFieldValidator ID="rfvClub" runat="server" ControlToValidate="ddlClub"
                    InitialValue="" ErrorMessage="Please select a club." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle" CssClass="form-label">Activity Title</asp:Label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                    ErrorMessage="Activity title is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" CssClass="form-label">Description</asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblActivityDate" runat="server" AssociatedControlID="txtActivityDate" CssClass="form-label">Activity Date &amp; Time</asp:Label>
                <asp:TextBox ID="txtActivityDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                <asp:RequiredFieldValidator ID="rfvActivityDate" runat="server" ControlToValidate="txtActivityDate"
                    ErrorMessage="Activity date is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="mb-3">
                <asp:Label ID="lblLocation" runat="server" AssociatedControlID="txtLocation" CssClass="form-label">Location</asp:Label>
                <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtLocation"
                    ErrorMessage="Location is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="Update Activity" CssClass="btn btn-primary me-2" OnClick="btnUpdate_Click" />
            <a href="ManageActivities.aspx" class="btn btn-secondary">Cancel</a>
        </div>
    </div>
</asp:Content>
