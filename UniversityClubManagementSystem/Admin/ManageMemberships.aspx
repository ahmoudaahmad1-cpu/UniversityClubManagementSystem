<%@ Page Title="Manage Memberships" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageMemberships.aspx.cs" Inherits="UniversityClubManagementSystem.ManageMemberships" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="page-title">Manage Memberships</h2>
    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card mb-3">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-8">
                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" CssClass="form-label">Search</asp:Label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by student name, club name, or status" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary me-2" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btnClear_Click" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <asp:GridView ID="gvMemberships" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False" DataKeyNames="MembershipId"
                OnRowCommand="gvMemberships_RowCommand" EmptyDataText="No memberships found.">
                <Columns>
                    <asp:BoundField DataField="StudentName" HeaderText="Student" />
                    <asp:BoundField DataField="ClubName" HeaderText="Club" />
                    <asp:BoundField DataField="JoinDate" HeaderText="Join Date" DataFormatString="{0:MMM dd, yyyy}" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='<%# GetStatusBadgeClass(Eval("Status").ToString()) %>'>
                                <%# Eval("Status") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Panel ID="pnlPendingActions" runat="server" Visible='<%# Eval("Status").ToString() == "Pending" %>'>
                                <asp:LinkButton ID="btnApprove" runat="server" CommandName="Approve" CommandArgument='<%# Eval("MembershipId") %>'
                                    CssClass="btn btn-sm btn-success me-1" Text="Approve"
                                    OnClientClick="return confirm('Approve this membership request?');" />
                                <asp:LinkButton ID="btnReject" runat="server" CommandName="Reject" CommandArgument='<%# Eval("MembershipId") %>'
                                    CssClass="btn btn-sm btn-danger" Text="Reject"
                                    OnClientClick="return confirm('Reject this membership request?');" />
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
