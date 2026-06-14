<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UniversityClubManagementSystem.DefaultPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center py-5">
        <h1 class="display-5 fw-bold text-primary mb-3">University Club Management</h1>
        <p class="lead text-muted mb-4">Discover clubs, join activities, and connect with your campus community.</p>
        <div class="d-flex justify-content-center gap-3 flex-wrap">
            <a href="Clubs.aspx" class="btn btn-primary btn-lg">Browse Clubs</a>
            <a href="Login.aspx" class="btn btn-outline-primary btn-lg">Student Login</a>
            <a href="Register.aspx" class="btn btn-success btn-lg">Register</a>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-md-4 mb-3">
            <div class="card h-100 shadow-sm">
                <div class="card-body text-center">
                    <h5 class="card-title">Explore Clubs</h5>
                    <p class="card-text text-muted">Browse clubs by category and find groups that match your interests.</p>
                </div>
            </div>
        </div>
        <div class="col-md-4 mb-3">
            <div class="card h-100 shadow-sm">
                <div class="card-body text-center">
                    <h5 class="card-title">Join Activities</h5>
                    <p class="card-text text-muted">View upcoming workshops, events, and competitions.</p>
                </div>
            </div>
        </div>
        <div class="col-md-4 mb-3">
            <div class="card h-100 shadow-sm">
                <div class="card-body text-center">
                    <h5 class="card-title">Manage Memberships</h5>
                    <p class="card-text text-muted">Request to join clubs and track your membership status.</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
