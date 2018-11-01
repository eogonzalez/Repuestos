<%@ Page Title="Hoja de Servicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmServicio.aspx.cs" Inherits="Repuestos.Administracion.Servicios.frmServicio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">Formulario <%:Title %> </div>

        <div class="panel-body form-horizontal">

            <%--Encabezado--%>
            <h3><span class="label label-primary">Encabezado Hoja de Servicio</span></h3>
            <div class="thumbnail">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="form-group input-sm">
                            <asp:Label ID="lblFecha" AssociatedControlID="txtFechaServicio" CssClass="control-label col-xs-2" runat="server" Text="Fecha Servicio:"></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox ID="txtFechaServicio" CssClass="form-control" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaServicio_CalendarExtender" runat="server" BehaviorID="txtFechaServicio_CalendarExtender" TargetControlID="txtFechaServicio" Format="dd/MM/yyyy" />
                            </div>

                            <asp:Label AssociatedControlID="ddlCliente" Text="Cliente: " runat="server" CssClass="control-label col-xs-2" />
                            <div class="col-xs-4">
                                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" AutoPostBack="true"/>
                            </div>
                        </div>

                        <div class="form-group input-sm">

                            <asp:Label runat="server" AssociatedControlID="ddlVehiculo" CssClass="control-label col-xs-2" Text="Vehiculo:"></asp:Label>
                            <div class="col-xs-4">
                                <asp:DropDownList runat="server" ID="ddlVehiculo" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <asp:Label runat="server" AssociatedControlID="txtKilometraje" CssClass="control-label col-xs-2" Text="Kilometraje:"></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox runat="server" ID="txtKilometraje" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>

                        <div class="form-group input-sm">
                            <asp:Label runat="server" AssociatedControlID="ddlTipoServicio" CssClass="control-label col-xs-2" Text="Tipo Servicio:"></asp:Label>
                            <div class="col-xs-4">
                                <asp:DropDownList runat="server" ID="ddlTipoServicio" CssClass="form-control" OnSelectedIndexChanged="ddlTipoServicio_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <asp:Label runat="server" AssociatedControlID="txtCostoServicio" CssClass="control-label col-xs-2" Text="Costo Servicio:"></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox runat="server" ID="txtCostoServicio" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                        </div>

                        <div class="form-group input-sm">
                            <asp:Label runat="server" ID="lblTotal" AssociatedControlID="txtTotal" CssClass="control-label col-xs-2" Text="Total:"></asp:Label>
                            <div class="col-xs-4">
                                <asp:TextBox runat="server" ID="txtTotal" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-xs-4 col-md-offset-2">
                                <asp:LinkButton ID="lkbtn_GuardarEncabezado" runat="server" CssClass="btn btn-danger" OnClick="lkbtn_GuardarEncabezado_Click">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-save"></span> Guardar Encabezado
                                </asp:LinkButton>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />

            <%--Area de Mensajes--%>
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
            </div>

            <%--Detalle de repuestos--%>
            <h3><span class="label label-primary">Detalle de Repuestos</span></h3>
            <div class="thumbnail">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <%--Boton para agregar Repuestos--%>
                        <div class="text-right">
                            <asp:LinkButton runat="server" ID="lkBtn_AgregarProducto" CssClass="btn btn-primary" OnClick="lkBtn_AgregarProducto_Click"><i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>Agregar Repuesto</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lkBtn_viewPanel"></asp:LinkButton>

                            <cc1:ModalPopupExtender ID="lkBtn_viewPanel_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                BehaviorID="lkBtn_viewPanel_ModalPopupExtender" PopupControlID="pnl_Producto" TargetControlID="lkBtn_viewPanel">
                            </cc1:ModalPopupExtender>
                        </div>
                        <%--Grid detalle--%>
                        <div>
                            <asp:GridView runat="server" ID="gvProductos"
                                CssClass="table table-hover table-striped"
                                GridLines="None"
                                EmptyDataText="No se han agregado detalle"
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <h3><span class="label label-primary">Otros Servicios</span></h3>
            <div class="thumbnail">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <%--Boton para agregar servicio externo--%>
                        <div class="text-right">
                            <asp:LinkButton runat="server" ID="lkBtn_AgregarServicioExterno" CssClass="btn btn-primary" OnClick="lkBtn_AgregarServicioExterno_Click"><i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>Agregar Servicio Externo</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lkBtn_viewPanelServicioExterno"></asp:LinkButton>

                            <cc1:ModalPopupExtender ID="lkBtn_viewPanelServicioExterno_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                BehaviorID="lkBtn_viewPanelServicioExterno_ModalPopupExtender" PopupControlID="pnl_ServicioExterno" TargetControlID="lkBtn_viewPanelServicioExterno">
                            </cc1:ModalPopupExtender>
                        </div>
                        <%--Grid Detalle Servicios--%>
                        <div>
                            <asp:GridView runat="server" ID="gvServiciosExternos"
                                CssClass="table table-hover table-striped"
                                GridLines="None"
                                EmptyDataText="No se han agregado detalle"
                                AutoGenerateColumns="false"
                                OnRowCommand="gvServiciosExternos_RowCommand"
                                OnPageIndexChanging="gvServiciosExternos_PageIndexChanging"
                                OnRowDataBound="gvServiciosExternos_RowDataBound">

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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>

        <div class="panel-footer">

            <asp:LinkButton ID="lkbRegresar" runat="server" CssClass="btn btn-success" OnClick="lkbRegresar_Click">
            <span aria-hidden="true" class="glyphicon glyphicon-arrow-left"></span> Regresar
            </asp:LinkButton>

            <asp:LinkButton ID="lkbtnCerrarCompra" runat="server" CssClass="btn btn-danger" OnClick="lkbtnCerrarCompra_Click" OnClientClick="return confirm(&quot;¿Esta seguro de cerrar Servicio?. &quot;)">
            <span aria-hidden="true" class="glyphicon glyphicon-floppy-save"></span> Cerrar Servicio
            </asp:LinkButton>

        </div>

    </div>

    <%--panel para agregar repuesto--%>
    <div>
        <%--Panel de productos--%>
        <asp:Panel runat="server" ID="pnl_Producto" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Style="overflow: auto; max-height: 445px; width: 40%;">

            <div class="panel-heading">Agregar Repuesto</div>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessageRepuesto" />
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
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <asp:Label runat="server" AssociatedControlID="txtPrecio" CssClass="control-label col-xs-2" Text="Precio Q."></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>

            </div>

            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardarProducto" CssClass="btn btn-primary" Text="Agregar" CommandName="GuardarProducto" OnClick="btnGuardarProducto_Click" />
                <asp:Button runat="server" ID="btnSalir" CssClass="btn btn-default" Text="Salir" CausesValidation="false" OnClick="btnSalir_Click" />
            </div>

        </asp:Panel>
    </div>

    <%--panel para agregar otros servicios--%>
    <div>
        <asp:Panel runat="server" ID="pnl_ServicioExterno" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Style="overflow: auto; max-height: 445px; width: 40%;">
            <div class="panel-heading">Agregar Servicio Externo</div>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessageServicioExterno" />
            </p>
            <div class="panel-body form-horizontal">
            </div>

            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardarServicioExterno" CssClass="btn btn-primary" Text="Agregar" CommandName="GuardarServicio" OnClick="btnGuardarServicioExterno_Click" />
                <asp:Button runat="server" ID="btnSalirServicioExterno" CssClass="btn btn-default" Text="Salir" CausesValidation="false" OnClick="btnSalirServicioExterno_Click" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
