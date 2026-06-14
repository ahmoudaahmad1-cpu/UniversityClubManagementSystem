<%@ Page Title="Club Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClubDetails.aspx.cs" Inherits="UniversityClubManagementSystem.ClubDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />
    <asp:Label ID="lblSuccess" runat="server" CssClass="alert alert-success d-block" Visible="false" />

    <asp:Panel ID="pnlClubInfo" runat="server" Visible="false">
        <div class="d-flex justify-content-between align-items-start flex-wrap gap-2 mb-3">
            <div>
                <h2 class="fw-bold mb-1"><asp:Label ID="lblClubName" runat="server" /></h2>
                <span class="badge bg-secondary"><asp:Label ID="lblCategory" runat="server" /></span>
            </div>
            <asp:Label ID="lblMembershipStatus" runat="server" CssClass="badge fs-6" Visible="false" />
        </div>

        <div class="card shadow-sm mb-4">
            <div class="card-body">
                <p><strong>President:</strong> <asp:Label ID="lblPresident" runat="server" /></p>
                <p><strong>Members:</strong> <asp:Label ID="lblMemberCount" runat="server" /></p>
                <p class="mb-0"><strong>Description:</strong></p>
                <p><asp:Label ID="lblDescription" runat="server" /></p>

                <asp:Button ID="btnJoin" runat="server" Text="Request to Join" CssClass="btn btn-success" OnClick="btnJoin_Click" Visible="false" />
            </div>
        </div>

        <h4 class="mb-3">Club Activities</h4>
        <asp:GridView ID="gvActivities" runat="server"
            CssClass="table table-striped table-hover"
            AutoGenerateColumns="False"
            EmptyDataText="No activities scheduled for this club."
            HeaderStyle-CssClass="table-dark">
            <Columns>
                <asp:BoundField DataField="ActivityTitle" HeaderText="Title" />
                <asp:BoundField DataField="ActivityDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy hh:mm tt}" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlNotFound" runat="server" Visible="false" CssClass="alert alert-warning">
        Club not found. <a href="Clubs.aspx">Return to clubs list</a>
    </asp:Panel>
</asp:Content>
