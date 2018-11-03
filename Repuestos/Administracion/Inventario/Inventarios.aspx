<%@ Page Title="Inventarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventarios.aspx.cs" Inherits="Repuestos.Inventario.Inventarios" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
    <%--Definicion del panel principal--%>
    <div class="panel panel-primary">
        <div class="panel-heading"><%:Title %></div>
        <br />
        <div class="panel-body form-vertical">
 
            <%--Area para desplegar informacion mediante una tabla -  gridview--%>
            <div>
                <asp:GridView runat="server" ID="gvInventarios"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No Existen registros."
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    PageSize="20"
                    OnRowCommand="gvInventarios_RowCommand"
                    OnPageIndexChanging="gvInventarios_PageIndexChanging"
                    OnRowDataBound="gvInventarios_RowDataBound">
 
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
                        <asp:BoundField DataField="id_producto" SortExpression="id_producto" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="disponible" HeaderText="Disponible" />
                        <asp:BoundField DataField="precio_maximo" HeaderText="Precio Maximo" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
 
 
</asp:Content>