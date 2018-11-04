<%@ Page Title="Facturas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Facturas.aspx.cs" Inherits="Repuestos.Administracion.Facturacion.Facturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <%--Definicion del panel principal--%>
    <div class="panel panel-primary">
        <div class="panel-heading"><%:Title %></div>
        <br />
        <div class="panel-body form-vertical">
            <%--Area para agregar botones--%>
            
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorPrincipal" />
            </p>
            <%--Area para desplegar informacion mediante una tabla -  gridview--%>
            <div>
                <asp:GridView runat="server" ID="gvFacturas"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No Existen registros."
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    OnRowCommand="gvFacturas_RowCommand"
                    OnPageIndexChanging="gvFacturas_PageIndexChanging">

                    <%--Propiedades para establecer el paginador--%>
                    <PagerSettings Mode="Numeric"
                        Position="Bottom"
                        PageButtonCount="10" />

                    <PagerStyle BackColor="LightBlue"
                        Height="30px"
                        VerticalAlign="Bottom"
                        HorizontalAlign="Center" />

                    <%--Area para definir las columnas de mi tabla--%>
                    <Columns>
                        <%--Columnas de la tabla que deseamos mostrar, es necesario consultar la llave primaria de la tabla--%>
                        <asp:BoundField DataField="id_factura" SortExpression="id_factura" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="numero_factura" HeaderText="Numero Factura" />
                        <asp:BoundField DataField="fecha_factura" HeaderText="Fecha" />
                        <asp:BoundField DataField="total" HeaderText="Total" />                        
                        <asp:BoundField DataField="estado" HeaderText="Estado" />

                        <%--Boton de Modificar--%>
                        <asp:ButtonField ButtonType="Button" Text="Mostrar Detalles" HeaderText="Mostrar" CommandName="mostrar" ControlStyle-CssClass="btn btn-success" />
                         <asp:ButtonField ButtonType="Button" Text="Imprimir" HeaderText="Formulario" CommandName="imprimir" ControlStyle-CssClass="btn btn-info" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>



</asp:Content>
