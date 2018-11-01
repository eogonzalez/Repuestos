<%@ Page Title="Ingreso de Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCompras.aspx.cs" Inherits="Repuestos.Administracion.Inventario.frmCompras" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">Formulario <%:Title %> </div>

        <div class="panel-body form-horizontal">

            <%--Encabezado--%>
            <div class="form-group input-sm">
                <asp:Label ID="lblFecha" AssociatedControlID="txtFechaCompra" CssClass="control-label col-xs-2" runat="server" Text="Fecha Compra:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtFechaCompra" CssClass="form-control" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtFechaCompra_CalendarExtender" runat="server" BehaviorID="txtFechaCompra_CalendarExtender" TargetControlID="txtFechaCompra" Format="dd/MM/yyyy" />
                </div>

                <asp:Label AssociatedControlID="ddlProveedor" Text="Proveedor: " runat="server" CssClass="control-label col-xs-2" />
                <div class="col-xs-4">
                    <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" />
                </div>
            </div>

            <div class="form-group input-sm">
                <asp:Label runat="server" ID="lblNumeroCompra" AssociatedControlID="txtNumeroCompra" CssClass="control-label col-xs-2" Text="Numero Compra:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox runat="server" ID="txtNumeroCompra" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>

                <asp:Label runat="server" ID="lblSerieCompra" AssociatedControlID="txtSerieCompra" CssClass="control-label col-xs-2" Text="Serie:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox runat="server" ID="txtSerieCompra" CssClass="form-control"></asp:TextBox>
                </div>

            </div>

            <div class="form-group input-sm">
                <asp:Label runat="server" ID="lblTotal" AssociatedControlID="txtTotal" CssClass="control-label col-xs-2" Text="Total:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
            </div>

            <%--Detalle de compras--%>
            <div>
                <div id="divAlertCorrecto" runat="server">
                    <p class="alert alert-success">
                        <asp:Literal runat="server" ID="MensajeCorrectoPrincipal" />
                    </p>
                </div>

                <div id="divAlertError" runat="server">
                    <p class="alert alert-danger" id="pAlertError" runat="server">
                        <asp:Literal runat="server" ID="ErrorMessagePrincipal" />
                    </p>
                </div>

                <div class="text-right">
                    <asp:LinkButton runat="server" ID="lkBtn_AgregarProducto" CssClass="btn btn-primary"><i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>Agregar Producto</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="lkBtn_viewPanel"></asp:LinkButton>

                    <cc1:ModalPopupExtender ID="lkBtn_AgregarProducto_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                        BehaviorID="lkBtn_AgregarProducto_ModalPopupExtender" PopupControlID="pnl_Producto" TargetControlID="lkBtn_AgregarProducto" CancelControlID="btnHide">
                    </cc1:ModalPopupExtender>

                    <cc1:ModalPopupExtender ID="lkBtn_viewPanel_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                        BehaviorID="lkBtn_viewPanel_ModalPopupExtender" PopupControlID="pnl_Producto" TargetControlID="lkBtn_viewPanel">
                    </cc1:ModalPopupExtender>
                </div>

                <div>
                    <asp:GridView runat="server" ID="gvProductos"
                        CssClass="table table-hover table-striped"
                        GridLines="None"
                        EmptyDataText="No se han agregado productos"
                        AutoGenerateColumns="false" 
                        OnRowCommand="gvProductos_RowCommand" 
                        OnPageIndexChanging="gvProductos_PageIndexChanging"
                        OnRowDataBound="gvProductos_RowDataBound">

                        <Columns>
                            <asp:BoundField DataField="correlativo" SortExpression="correlativo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                            <asp:BoundField DataField="repuesto" HeaderText="Repuesto" />
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" />
                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal" />
                            <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="Modificar" CommandName="modificarProducto" ControlStyle-CssClass="btn btn-success" />
                            <asp:ButtonField ButtonType="Button" Text="Eliminar" HeaderText="Eliminar" CommandName="eliminarProducto" ControlStyle-CssClass="btn btn-danger" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>

        <div class="panel-footer">

            <asp:LinkButton ID="lkbRegresar" runat="server" CssClass="btn btn-success" OnClick="btnRegresar_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-arrow-left"></span> Regresar
            </asp:LinkButton>

            <asp:LinkButton ID="lkbtnCerrarCompra" runat="server" CssClass="btn btn-danger" OnClick="lkbtnCerrarCompra_Click" OnClientClick="return confirm(&quot;¿Esta seguro de cerrar compra?. Esta opcion alimenta el inventario. &quot;)">
            <span aria-hidden="true" class="glyphicon glyphicon-floppy-save"></span> Cerrar Compra
            </asp:LinkButton>

        </div>

    </div>

    <%--panel--%>
    <div>
        <%--Panel de productos--%>
        <asp:Panel runat="server" ID="pnl_Producto" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Style="overflow: auto; max-height: 445px; width: 40%;">

            <div class="panel-heading">Adjunto Producto</div>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>


            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ddlProducto" CssClass="control-label col-xs-2" Text="Repuesto:"></asp:Label>
                    <div class="col-xs-10">
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtCantidad" CssClass="control-label col-xs-2" Text="Cantidad:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtCantidad" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <asp:Label runat="server" AssociatedControlID="txtPrecio" CssClass="control-label col-xs-2" Text="Precio Q."></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtPrecio" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>

            </div>

            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardarProducto" CssClass="btn btn-primary" Text="Agregar" CommandName="GuardarProducto" OnClick="btnGuardarProducto_Click" />
                <asp:Button runat="server" ID="btnSalir" CssClass="btn btn-default" Text="Salir" CausesValidation="false" OnClick="btnSalir_Click" />
            </div>

        </asp:Panel>
    </div>

</asp:Content>
