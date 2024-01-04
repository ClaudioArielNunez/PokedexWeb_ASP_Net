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
                        <asp:Button Text="aceptar" ID="btnAceptar" cssClass="ms-2 btn btn-primary" runat="server" />
                    </div>
                </div>
            </div>

            <%}%>        
     </div>
</asp:Content>
