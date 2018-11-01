<%@ Page Title="Servicios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="Repuestos.Administracion.Servicios.Servicios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--Definicion del panel principal--%>
    <div class="panel panel-primary">
        <div class="panel-heading"><%:Title %></div>
        <br />
        <div class="panel-body form-vertical">
            <%--Area para agregar botones--%>
            <div class="btn">
                <asp:LinkButton runat="server" ID="lkBtn_nuevo" CssClass="btn btn-primary" OnClick="lkBtn_nuevo_Click"><i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Ingresar Servicio</asp:LinkButton>
            </div>
            <br />
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorPrincipal" />
            </p>
            <%--Area para desplegar informacion mediante una tabla -  gridview--%>
            <div>
                <asp:GridView runat="server" ID="gvServicios"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No Existen registros."
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    OnRowCommand="gvServicios_RowCommand"
                    OnPageIndexChanging="gvServicios_PageIndexChanging"
                    OnRowDataBound="gvServicios_RowDataBound">

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
                        <asp:BoundField DataField="id_servicio" SortExpression="id_servicio" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="nombres" HeaderText="Cliente" />
                        <asp:BoundField DataField="vehiculo" HeaderText="Vehiculo" />
                        <asp:BoundField DataField="tipo_servicio" HeaderText="Tipo Servicio" />
                        <asp:BoundField DataField="costo_total" HeaderText="Total" />
                        <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha Servicio" />
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
