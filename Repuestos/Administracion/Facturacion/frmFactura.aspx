<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmFactura.aspx.cs" Inherits="Repuestos.Administracion.Facturacion.frmFactura" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">Formulario <%:Title %> </div>

        <div class="panel-body form-horizontal">

            <%--Encabezado--%>
            <div class="form-group input-sm">
                <asp:Label ID="lblFecha" AssociatedControlID="txtFechaFactura" CssClass="control-label col-xs-2" runat="server" Text="Fecha Factura:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtFechaFactura" CssClass="form-control" runat="server"></asp:TextBox>
                    <cc1:calendarextender id="txtFechaFactura_CalendarExtender" runat="server" behaviorid="txtFechaFactura_CalendarExtender" targetcontrolid="txtFechaFactura" format="dd/MM/yyyy" />
                </div>

                <asp:Label AssociatedControlID="ddlCliente" Text="Cliente: " runat="server" CssClass="control-label col-xs-2" />
                <div class="col-xs-4">
                    <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group input-sm">
                <asp:Label runat="server" ID="lblNumeroFactura" AssociatedControlID="txtNumeroFactura" CssClass="control-label col-xs-2" Text="Numero Factura:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox runat="server" ID="txtNumeroFactura" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Label runat="server" ID="lblSerieFactura" AssociatedControlID="txtSerieFactura" CssClass="control-label col-xs-2" Text="Serie:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox runat="server" ID="txtSerieFactura" CssClass="form-control"></asp:TextBox>
                </div>

            </div>

            <div class="form-group input-sm">
                <asp:Label runat="server" ID="lblTotal" AssociatedControlID="txtTotal" CssClass="control-label col-xs-2" Text="Total:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
            </div>

            <%--Detalle de Factura--%>
            <div>

                <div>
                    <asp:GridView runat="server" ID="gvDetalleFactura"
                        CssClass="table table-hover table-striped"
                        GridLines="None"
                        EmptyDataText="No se han agregado productos"
                        AutoGenerateColumns="false"
                        OnPageIndexChanging="gvDetalleFactura_PageIndexChanging">

                        <Columns>
                            <asp:BoundField DataField="correlativo" SortExpression="correlativo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />                            
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal" />                            
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>

        <div class="panel-footer">

            <asp:LinkButton ID="lkbRegresar" runat="server" CssClass="btn btn-success" OnClick="lkbRegresar_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-arrow-left"></span> Regresar
            </asp:LinkButton>
        </div>

    </div>

</asp:Content>
