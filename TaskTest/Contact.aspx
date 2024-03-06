<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Task_17_01.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <form>
            <div style="display:flex;flex-direction:column">
                <asp:Label ID="lblSearchStudentName" runat="server" Text="Name" ></asp:Label>
                <asp:TextBox ID="txtSearchStudentName" runat="server" required></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" style="margin:10px" type="submit"/>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" style="margin:10px" type="submit"/>
                <br />
            </div>
        </form>

        <form>

            <div style="display:flex;flex-direction:row">
                <asp:Label ID="lblIdData" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblNameData" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblGender" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDob" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblAddress1" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblAddress2" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblPhone1" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblPhone2" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblEmail" runat="server" style="margin-right:10px"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblPassword" runat="server" style="margin-right:10px"></asp:Label>
                <br />
            </div>

            <asp:TextBox ID="txtDataId" runat="server" hidden></asp:TextBox>
            <asp:TextBox ID="txtDataName" runat="server" hidden></asp:TextBox>

            <div style="display:flex;flex-direction:column">
                <asp:Label ID="lblUpdateStudentName" runat="server" Text="Name" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentName" runat="server" required></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentGender" runat="server" Text="Gender" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentGender" runat="server" required></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentDob" runat="server" Text="Date" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentDob" runat="server" type="date" required></asp:TextBox>
                <br />
                <asp:Label ID="lblUpdateStudentAddress1" runat="server" Text="Address1" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentAddress1" runat="server" required></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" style="margin:10px" type="submit" Width="200px"/>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentAddress2" runat="server" Text="Address2" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentAddress2" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentPhone1" runat="server" Text="Phone Number 1" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentPhone1" runat="server" required></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentPhone2" runat="server" Text="Phone Number 2" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentPhone2" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentEmail" runat="server" Text="Email" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentEmail" runat="server" type="email" required></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblUpdateStudentPassword" runat="server" Text="Password" Width="200px"></asp:Label>
                <asp:TextBox ID="txtUpdateStudentPassword" runat="server" type="password" required></asp:TextBox>
            </div>
        </form>
    </main>
</asp:Content>
