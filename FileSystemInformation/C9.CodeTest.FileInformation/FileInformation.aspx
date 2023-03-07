<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileInformation.aspx.cs" Inherits="C9.CodeTest.FileInformation.FileInformation" MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentArea" runat="server">
    <!-- Main component for a primary marketing message or call to action -->
    <div class="jumbotron">
        <h2>Load Files</h2>
        <table class="table">
            <tr>
                <td>
                    <asp:Label ID="lblBrowseFolder" runat="server" Text="Select Folder" class="form-label" /></td>
                <td>
                    <div class="row">
                        <asp:TextBox ID="txtBrowseFolder" runat="server" Text="" Width="465px"></asp:TextBox>
                        <asp:Button ID="btnBrowseFolder" runat="server" CssClass="btn btn-primary" Text="..." OnClick="btnBrowseFolder_Click" />
                    </div>
                </td>
            </tr>

            <tr>
                <td scope="col">
                    <asp:Label ID="lblFileCount" runat="server" Text="Total Files in selected folder: " Font-Size="Larger"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFileCountValue" runat="server" Text="" Font-Bold="true" Font-Size="Larger"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFileCountByType" runat="server" Text="Number of Files by Type" Font-Size="Larger"></asp:Label></td>
                <td>
                    <asp:GridView ID="gvFilesByType" runat="server" Height="50px" CssClass="align-content-center" PagerStyle-CssClass="align-content-sm-end"></asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFilter" runat="server" Text="Filter by File Type" Font-Size="Larger"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFileList" class="form-control" runat="server" OnSelectedIndexChanged="ddlFileList_SelectedIndexChanged" AutoPostBack="true" />
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvFileList" runat="server" AutoGenerateColumns="false" Height="50px" AllowSorting="true"
                        AllowPaging="true" PageSize="5" PagerStyle-CssClass="pagination" OnPageIndexChanging="gvFileList_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="File Name" DataField="FileName" />
                            <asp:BoundField HeaderText="File Type" DataField="FileType" />
                            <asp:BoundField HeaderText="Created" DataField="CreationDate" />
                            <asp:BoundField HeaderText="Last Modified" DataField="LastModified" />
                            <asp:BoundField HeaderText="Read Only" DataField="IsReadOnly" />
                            <asp:BoundField HeaderText="File Size" DataField="Lenght" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <div class="row">
            <h3>Export Data</h3>
            <asp:Button ID="btnExport" Text="Export" runat="server" OnClick="btnExport_Click" CssClass="btn btn-primary" />
        </div>
        <h5>
            <asp:Label ID="lblMessage" runat="server" />
        </h5>
    </div>
</asp:Content>
