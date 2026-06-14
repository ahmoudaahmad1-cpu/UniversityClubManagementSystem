<%@ Page Title="My Memberships" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyMemberships.aspx.cs" Inherits="UniversityClubManagementSystem.MyMemberships" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="fw-bold mb-4">My Memberships</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />
    <asp:Label ID="lblSuccess" runat="server" CssClass="alert alert-success d-block" Visible="false" />

    <asp:GridView ID="gvMemberships" runat="server"
        CssClass="table table-striped table-hover"
        AutoGenerateColumns="False"
        EmptyDataText="You have no membership requests yet."
        HeaderStyle-CssClass="table-dark"
        OnRowCommand="gvMemberships_RowCommand"
        DataKeyNames="MembershipId">
        <Columns>
            <asp:BoundField DataField="ClubName" HeaderText="Club" />
            <asp:BoundField DataField="JoinDate" HeaderText="Request Date" DataFormatString="{0:MMM dd, yyyy}" />
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <span class='<%# GetStatusBadgeClass(Eval("Status").ToString()) %>'><%# Eval("Status") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="btnCancel" runat="server"
                        Text="Cancel Request"
                        CssClass="btn btn-sm btn-outline-danger"
                        CommandName="CancelRequest"
                        CommandArgument='<%# Eval("MembershipId") %>'
                        Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
