<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Ejemplo_1.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="my-3">Mi Perfil</h2>
    <div class="my-3 row">
        <div class="col-6 ">
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtApellido" cssclass="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtFecha" class="form-label">Fecha de Nacimiento</label>
                <asp:TextBox ID="txtFecha" TextMode="Date" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="col-6 ">
            <div class="mb-3">
                <label for="txtImagen" class="form-label">Imagen Perfil</label>
                <input type="file" id="txtImagen" class="form-control" runat="server" /><%--etiqueta de HTML con runat server--%>
            </div>
            <asp:Image ImageUrl="https://www.pngitem.com/pimgs/m/30-307416_profile-icon-png-image-free-download-searchpng-employee.png" 
                        ID="imgNuevoPerfil" CssClass="img-fluid mb-3" Width="300px" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-6">
            <asp:Button Text="Guardar" OnClick="btnGuardar_Click" ID="btnGuardar" CssClass="btn btn-primary" runat="server" />
            <a href="#">Regresar</a>
        </div>
    </div>
</asp:Content>
