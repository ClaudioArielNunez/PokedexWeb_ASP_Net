<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="PokemonLista.aspx.cs" Inherits="Ejemplo_1.PokemonLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="my-3">Listado de Pokemons</h2>
    <div class="row">
        <div class="mb-4 col-4">
            <label for="txtFiltro" class="form-label">Filtro por Nombre:</label>
            <asp:TextBox OnTextChanged="txtFiltro_TextChanged" AutoPostBack="true" ID="txtFiltro" CssClass="form-control" runat="server" />
        </div>
        <div class="mb-4  col-4 ">
            <label for="txtFiltro" class="form-label">Filtro avanzado:</label>
            <br />
            <asp:CheckBox Text="" ID="chkAvanzado" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />
        </div>
    </div>

    <%if (chkAvanzado.Checked) //usamos el chequed para abrirlo
        {%>
    <div class="mb-4 row">
        <div class="col-3">
            <asp:Label Text="Campo" runat="server" />
            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" ID="ddlCampo" CssClass="form-control">
                <asp:ListItem Text="Nombre" />
                <asp:ListItem Text="Tipo" />
                <asp:ListItem Text="Número" />
            </asp:DropDownList>
        </div>
        <div class="col-3">
            <asp:Label Text="Criterio" runat="server" />
            <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="col-3">
            <asp:Label Text="Filtro" runat="server" />
            <asp:TextBox ID="txtFiltroAvanzado" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-3">
            <asp:Label Text="Estado" runat="server" />
            <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control">
                <asp:ListItem Text="Todos" />
                <asp:ListItem Text="Activo" />
                <asp:ListItem Text="Inactivo" />
            </asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" Text="Buscar" runat="server" />
                <asp:Button ID="btnLimpiar" OnClick="btnLimpiar_Click"  CssClass="btn btn-success" Text="Limpiar resultados" runat="server" />
            </div>
        </div>
    </div>
    <%} %>

    <asp:GridView ID="dgvPokemon" DataKeyNames="Id" OnSelectedIndexChanged="dgvPokemon_SelectedIndexChanged" AutoGenerateColumns="false" CssClass="table" runat="server"
        OnPageIndexChanging="dgvPokemon_PageIndexChanging" AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Numero" DataField="Numero" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Tipo" DataField="Tipo.Descripcion" />
            <asp:BoundField HeaderText="Debilidad" DataField="Debilidad.Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️" />

        </Columns>
    </asp:GridView>
    <a href="FormularioPokemon.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
