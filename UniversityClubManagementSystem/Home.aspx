<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="UniversityClubManagementSystem.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

    <div class="mb-4">
        <h2 class="fw-bold">Welcome, <asp:Label ID="lblStudentName" runat="server" /></h2>
        <p class="text-muted">Here is a quick overview of your club activity.</p>
    </div>

    <div class="row g-3 mb-4">
        <div class="col-md-4">
            <div class="card text-white bg-primary shadow-sm h-100">
                <div class="card-body">
                    <h5 class="card-title">My Memberships</h5>
                    <p class="display-6 mb-0"><asp:Label ID="lblMembershipCount" runat="server" Text="0" /></p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card text-white bg-success shadow-sm h-100">
                <div class="card-body">
                    <h5 class="card-title">Approved Clubs</h5>
                    <p class="display-6 mb-0"><asp:Label ID="lblApprovedCount" runat="server" Text="0" /></p>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="card text-white bg-info shadow-sm h-100">
                <div class="card-body">
                    <h5 class="card-title">Upcoming Activities</h5>
                    <p class="display-6 mb-0"><asp:Label ID="lblActivityCount" runat="server" Text="0" /></p>
                </div>
            </div>
        </div>
    </div>

    <div class="d-flex flex-wrap gap-2">
        <a href="Clubs.aspx" class="btn btn-primary">Browse Clubs</a>
        <a href="Activities.aspx" class="btn btn-outline-primary">View Activities</a>
        <a href="MyMemberships.aspx" class="btn btn-outline-secondary">My Memberships</a>
    </div>
</asp:Content>
