<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Ejemplo_1.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class=" my-2">Crea tu perfil Trainee</h2>
    <div class="row">
        <div class="my-3 col-4">
            <div class="mb-3">
                 <label for="txtId" class="form-label">Email:</label>
                <asp:TextBox ID="txtEmail" cssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                 <label for="txtId" class="form-label">Password:</label>
                <asp:TextBox ID="txtPass" TextMode="Password" cssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3 ">
                <asp:Button Text="Registrarse" OnClick="btnRegistrarse_Click"  ID="btnRegistrarse" CssClass="btn btn-primary me-3" runat="server" />
                <a href="Default.aspx"  >Cancelar</a>
                
            </div>

        </div>
    </div>
</asp:Content>
