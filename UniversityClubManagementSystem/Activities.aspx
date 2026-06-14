<%@ Page Title="Activities" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Activities.aspx.cs" Inherits="UniversityClubManagementSystem.Activities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="fw-bold mb-4">Upcoming Activities</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

    <asp:GridView ID="gvActivities" runat="server"
        CssClass="table table-striped table-hover"
        AutoGenerateColumns="False"
        EmptyDataText="No upcoming activities found."
        HeaderStyle-CssClass="table-dark">
        <Columns>
            <asp:BoundField DataField="ActivityTitle" HeaderText="Title" />
            <asp:BoundField DataField="ClubName" HeaderText="Club" />
            <asp:BoundField DataField="ActivityDate" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy hh:mm tt}" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
        </Columns>
    </asp:GridView>
</asp:Content>
