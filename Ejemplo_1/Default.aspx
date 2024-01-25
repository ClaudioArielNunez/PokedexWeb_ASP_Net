<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ejemplo_1.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="my-3">Bienvenido a la web de Pokemons</h2>


    <div class="row row-cols-1 row-cols-md-4 g-4">
        <%
            foreach (var poke in ListaPokemon)
            {%>
        <div class="col">
            <div class="card">
                <img src="<%:poke.UrlImagen %>" class="card-img-top" alt="<%:poke.Nombre %>">
                <div class="card-body">
                    <h5 class="card-title"><%:poke.Nombre %></h5>
                    <p class="card-text"><%:poke.Descripcion %></p>
                    <a href="DetallePokemon.aspx">Ver detalle</a>
                    <asp:Button Text="aceptar" data-toggle="modal" data-target="#exampleModal" ID="btnAceptar" OnClick="btnAceptar_Click" CssClass="ms-2 btn btn-primary" runat="server" />
                    <%--boton q llama a modal--%>
                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                        Demo
                    </button>
                </div>
            </div>
            <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <img src="<%:poke.UrlImagen %>" class="card-img-top" alt="<%:poke.Nombre %>">
                            <h1 class="modal-title fs-5" id="exampleModalLabel"><%:poke.Nombre %></h1>
                            <p class="card-text"><%:poke.Descripcion %></p>
                            <p class="card-text"><%:poke.Tipo %></p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%}%>
    </div>

    <!-- Modal -->
    <%if (verModal)
        {  %>

    <% }%>
</asp:Content>
