<%@ Page Title="Manage Activities" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageActivities.aspx.cs" Inherits="UniversityClubManagementSystem.ManageActivities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="page-title mb-0">Manage Activities</h2>
        <a href="AddActivity.aspx" class="btn btn-success">Add New Activity</a>
    </div>
    <asp:Label ID="lblMessage" runat="server" Visible="false" />

    <div class="card mb-3">
        <div class="card-body">
            <div class="row g-2 align-items-end">
                <div class="col-md-8">
                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" CssClass="form-label">Search</asp:Label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by title, club, or location" />
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
            <asp:GridView ID="gvActivities" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False" DataKeyNames="ActivityId"
                OnRowCommand="gvActivities_RowCommand" EmptyDataText="No activities found.">
                <Columns>
                    <asp:BoundField DataField="ActivityTitle" HeaderText="Title" />
                    <asp:BoundField DataField="ClubName" HeaderText="Club" />
                    <asp:BoundField DataField="ActivityDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy hh:mm tt}" />
                    <asp:BoundField DataField="Location" HeaderText="Location" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <a href='<%# "EditActivity.aspx?id=" + Eval("ActivityId") %>' class="btn btn-sm btn-primary me-1">Edit</a>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteActivity" CommandArgument='<%# Eval("ActivityId") %>'
                                CssClass="btn btn-sm btn-danger" Text="Delete"
                                OnClientClick="return confirm('Are you sure you want to delete this activity?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
