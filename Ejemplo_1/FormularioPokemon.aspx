<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioPokemon.aspx.cs" Inherits="Ejemplo_1.FormularioPokemon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="my-3">Formulario</h2>
    <div class="row ">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id:</label>
                <asp:TextBox ID="txtId" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNumero" class="form-label">Numero:</label>
                <asp:TextBox ID="txtNumero" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo:</label>
                <asp:DropDownList ID="ddlTipo" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlDebilidad" class="form-label">Debilidad:</label>
                <asp:DropDownList ID="ddlDebilidad" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" OnClick="btnAceptar_Click" CssClass=" btn btn-primary" runat="server" />
                <a href="Default.aspx" class="ms-3 text-decoration-none">Cancelar</a>
            </div>
        </div>
        <%----- 2 columna---%>
        <div class="col-6">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción:</label>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" runat="server" />
            </div>
            <%--script manager--%>
            <asp:ScriptManager ID="scrManager" runat="server" />
            <%--update panel--%>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <%--contenido q usa update panel--%>
                    <div class="mb-3">
                        <label for="txtUrlImagen" class="form-label">UrlImagen:</label>
                        <asp:TextBox ID="txtUrlImagen" OnTextChanged="txtUrlImagen_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                    </div>
                    <asp:Image ImageUrl="https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg" runat="server"
                        ID="imgPokemon" Width="60%" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class=" row">
        <div class="col-6">
            <asp:UpdatePanel ID="updatePanel" runat="server">
                <ContentTemplate>
                    <div class="mb-2">
                        <asp:Button ID="bbtnEliminar" OnClick="bbtnEliminar_Click" CssClass="btn btn-danger" Text="Eliminar" runat="server" />
                    </div>
                    <%if (confirmaEliminacion){%>
                    <div class="ms-2">                    
                        <asp:CheckBox ID="chkConfirmarEliminacion" Text="Confirmar eliminación" runat="server" />
                        <asp:Button ID="btnConfirmaEliminacion" OnClick="btnConfirmaEliminacion_Click" CssClass="btn btn-outline-danger" Text="Eliminar" runat="server" />
                    </div>                    
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
