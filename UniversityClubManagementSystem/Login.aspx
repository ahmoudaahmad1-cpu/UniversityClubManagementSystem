<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UniversityClubManagementSystem.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-6 col-lg-5">
            <div class="card shadow-sm">
                <div class="card-body p-4">
                    <h2 class="card-title text-center mb-4">Student Login</h2>

                    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-warning" DisplayMode="BulletList" />

                    <div class="mb-3">
                        <label for="txtUsername" class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                            ControlToValidate="txtUsername"
                            ErrorMessage="Username is required."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                            ControlToValidate="txtPassword"
                            ErrorMessage="Password is required."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100 mb-3" OnClick="btnLogin_Click" />

                    <p class="text-center mb-0">
                        Don't have an account? <a href="Register.aspx">Register here</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
