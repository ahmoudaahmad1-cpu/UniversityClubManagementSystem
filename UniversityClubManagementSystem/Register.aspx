<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UniversityClubManagementSystem.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6">
            <div class="card shadow-sm">
                <div class="card-body p-4">
                    <h2 class="card-title text-center mb-4">Student Registration</h2>

                    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />
                    <asp:Label ID="lblSuccess" runat="server" CssClass="alert alert-success d-block" Visible="false" />

                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-warning" DisplayMode="BulletList" />

                    <div class="mb-3">
                        <label for="txtFullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server"
                            ControlToValidate="txtFullName"
                            ErrorMessage="Full name is required."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="Email is required."
                            CssClass="text-danger"
                            Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ErrorMessage="Please enter a valid email address."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

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

                    <div class="mb-3">
                        <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server"
                            ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Please confirm your password."
                            CssClass="text-danger"
                            Display="Dynamic" />
                        <asp:CompareValidator ID="cvPassword" runat="server"
                            ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtPassword"
                            Operator="Equal"
                            ErrorMessage="Passwords do not match."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="txtPhone" class="form-label">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                        <asp:RegularExpressionValidator ID="revPhone" runat="server"
                            ControlToValidate="txtPhone"
                            ValidationExpression="^[\d\s\-\(\)\+]{7,20}$"
                            ErrorMessage="Please enter a valid phone number."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

                    <div class="mb-3">
                        <label for="txtMajor" class="form-label">Major</label>
                        <asp:TextBox ID="txtMajor" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvMajor" runat="server"
                            ControlToValidate="txtMajor"
                            ErrorMessage="Major is required."
                            CssClass="text-danger"
                            Display="Dynamic" />
                    </div>

                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success w-100 mb-3" OnClick="btnRegister_Click" />

                    <p class="text-center mb-0">
                        Already have an account? <a href="Login.aspx">Login here</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
