<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UniversityClubManagementSystem.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Admin Dashboard</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

    <div class="row g-3 mb-4">
        <div class="col-md-3">
            <div class="card stat-card h-100">
                <div class="card-body">
                    <h6 class="text-muted">Total Students</h6>
                    <h3 class="mb-0"><asp:Label ID="lblStudentCount" runat="server" Text="0" /></h3>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card stat-card h-100">
                <div class="card-body">
                    <h6 class="text-muted">Total Clubs</h6>
                    <h3 class="mb-0"><asp:Label ID="lblClubCount" runat="server" Text="0" /></h3>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card stat-card h-100">
                <div class="card-body">
                    <h6 class="text-muted">Total Activities</h6>
                    <h3 class="mb-0"><asp:Label ID="lblActivityCount" runat="server" Text="0" /></h3>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card stat-card h-100">
                <div class="card-body">
                    <h6 class="text-muted">Pending Memberships</h6>
                    <h3 class="mb-0"><asp:Label ID="lblPendingCount" runat="server" Text="0" /></h3>
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <h5>Quick Actions</h5>
            <div class="d-flex flex-wrap gap-2">
                <a href="ManageStudents.aspx" class="btn btn-outline-primary">Manage Students</a>
                <a href="ManageClubs.aspx" class="btn btn-outline-primary">Manage Clubs</a>
                <a href="AddClub.aspx" class="btn btn-outline-success">Add Club</a>
                <a href="ManageActivities.aspx" class="btn btn-outline-primary">Manage Activities</a>
                <a href="AddActivity.aspx" class="btn btn-outline-success">Add Activity</a>
                <a href="ManageMemberships.aspx" class="btn btn-outline-warning">Manage Memberships</a>
            </div>
        </div>
    </div>
</asp:Content>
