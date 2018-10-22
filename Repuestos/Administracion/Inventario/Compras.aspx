<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Repuestos.Inventario.Compras" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--Definicion del panel principal--%>
    <div class="panel panel-primary">
        <div class="panel-heading"><%:Title %></div>
        <br />
        <div class="panel-body form-vertical">
            <%--Area para agregar botones--%>
            <div class="btn">
                <asp:LinkButton runat="server" ID="lkBtn_nuevo" CssClass="btn btn-primary" OnClick="lkBtn_nuevo_Click"><i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Ingresar Compra </asp:LinkButton>                
            </div>
            <br />
            <%--Area para desplegar informacion mediante una tabla -  gridview--%>
            <div>
                <asp:GridView runat="server" ID="gvCompras"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No Existen registros."
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    OnRowCommand="gvCompras_RowCommand"
                    OnPageIndexChanging="gvCompras_PageIndexChanging">

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
                        <asp:BoundField DataField="id_compra" SortExpression="id_compra" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="compra" HeaderText="Compra" />                        
                        <asp:BoundField DataField="nombre_proveedor" HeaderText="Proveedor" />
                        <asp:BoundField DataField="total" HeaderText="Total" />
                        <asp:BoundField DataField="fecha_compra" HeaderText="Fecha Compra" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        

                        <%--Boton de Modificar--%>
                        <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="Modificar" CommandName="modificar" ControlStyle-CssClass="btn btn-success" />

                        <%--Boton de Eliminar--%>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:Button Text="Eliminar" runat="server" ID="btnEliminar" CausesValidation="false" CommandName="eliminar" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger" OnClientClick="return confirm(&quot;¿Esta seguro de borrar opcion seleccionada?&quot;)" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


</asp:Content>
