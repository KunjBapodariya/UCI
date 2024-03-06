<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Task_17_01.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div style="display:flex;flex-direction:column">
            <asp:Label ID="lblStudentName" runat="server" Text="Name" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentName" runat="server" required></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblStudentGender" runat="server" Text="Gender" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentGender" runat="server" required></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblStudentDob" runat="server" Text="Date" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentDob" runat="server" type="date" required></asp:TextBox>

            <br />

            <br />

            <div style="display:flex;flex-direction:row;margin:10px">
                <asp:DropDownList ID="ddlCountries" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlStates" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="ddlDistricts" runat="server"></asp:DropDownList>
                <br />
            </div>

            <br />

            <asp:Label ID="lblStudentAddress1" runat="server" Text="Address 1" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentAddress1" runat="server" ></asp:TextBox>
            <br />
            <asp:Label ID="lblStudentAddress2" runat="server" Text="Address 2" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentAddress2" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" type="submit" OnClick="btnSubmit_Click" style="margin:10px" Width="200px"/>
            <br />
            <br />
            <asp:Label ID="lblStudentPhone1" runat="server" Text="Phone Number 1" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentPhone1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblStudentPhone2" runat="server" Text="Phone Number 2" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentPhone2" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblStudentEmail" runat="server" Text="Email" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentEmail" runat="server" type="email" required></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblStudentPassword" runat="server" Text="Password" Width="200px"></asp:Label>
            <asp:TextBox ID="txtStudentPassword" runat="server" type="password" required></asp:TextBox>
        </div>
        
    </main>
</asp:Content>