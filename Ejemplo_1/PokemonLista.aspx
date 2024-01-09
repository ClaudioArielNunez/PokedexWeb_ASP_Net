<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="PokemonLista.aspx.cs" Inherits="Ejemplo_1.PokemonLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="my-3">Listado de Pokemons</h2>
    <div class="row">
        <div class="mb-4 col-4">
            <label for="txtFiltro" class="form-label">Filtro por Nombre:</label>
            <asp:TextBox  OnTextChanged="txtFiltro_TextChanged" AutoPostBack="true" ID="txtFiltro" cssClass="form-control" runat="server" />            
        </div>
    </div>
    <asp:GridView ID="dgvPokemon" DataKeyNames="Id" OnSelectedIndexChanged="dgvPokemon_SelectedIndexChanged" AutoGenerateColumns="false" cssClass="table" runat="server"
                   OnPageIndexChanging="dgvPokemon_PageIndexChanging" AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Numero" DataField="Numero" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre"/>   
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion"/>
            <asp:CheckBoxField  HeaderText="Activo" DataField="Activo" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />            

        </Columns>
    </asp:GridView>
    <a href="FormularioPokemon.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
