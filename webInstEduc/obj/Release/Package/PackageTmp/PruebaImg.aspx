<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="PruebaImg.aspx.cs" Inherits="AspIdentity.PruebaImg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div>
    
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="BtnGuardarImagen" runat="server" OnClick="BtnGuardarImagen_Click" Text="Guardar" />
        <asp:Button ID="BtnImprimirImagen" runat="server" OnClick="BtnImprimirImagen_Click" Text="ImprimirImagen" />
        <img src="/wsFoto.ashx" />
    </div>
    </form>
</asp:Content>
