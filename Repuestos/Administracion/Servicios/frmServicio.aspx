﻿<%@ Page Title="Hoja de Servicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmServicio.aspx.cs" Inherits="Repuestos.Administracion.Servicios.frmServicio" %>

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
                                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged" AutoPostBack="true" />
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
                                <asp:LinkButton ID="lkbtn_GuardarEncabezado" runat="server" CssClass="btn btn-danger" OnClick="lkbtn_GuardarEncabezado_Click" CommandName="Guardar">
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
                                AllowPaging="true"
                                PageSize="5"
                                OnRowCommand="gvProductos_RowCommand"
                                OnPageIndexChanging="gvProductos_PageIndexChanging"
                                OnRowDataBound="gvProductos_RowDataBound">

                                <%--Propiedades para establecer el paginador--%>
                                <PagerSettings Mode="Numeric"
                                    Position="Bottom"
                                    PageButtonCount="10" />

                                <PagerStyle BackColor="LightBlue"
                                    Height="30px"
                                    VerticalAlign="Bottom"
                                    HorizontalAlign="Center" />

                                <Columns>
                                    <asp:BoundField DataField="corr_servicio_repuesto" SortExpression="corr_servicio_repuesto" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                    <asp:BoundField DataField="producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="precio_venta" HeaderText="Precio" />
                                    <asp:BoundField DataField="sub_total" HeaderText="Subtotal" />
                                    <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="Modificar" CommandName="modificar" ControlStyle-CssClass="btn btn-success" />
                                    <asp:ButtonField ButtonType="Button" Text="Eliminar" HeaderText="Eliminar" CommandName="eliminar" ControlStyle-CssClass="btn btn-danger" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--Detalle de servicios--%>
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
                                AllowPaging="true"
                                PageSize="5"
                                OnRowCommand="gvServiciosExternos_RowCommand"
                                OnPageIndexChanging="gvServiciosExternos_PageIndexChanging"
                                OnRowDataBound="gvServiciosExternos_RowDataBound">

                                <%--Propiedades para establecer el paginador--%>
                                <PagerSettings Mode="Numeric"
                                    Position="Bottom"
                                    PageButtonCount="5" />

                                <PagerStyle BackColor="LightBlue"
                                    Height="30px"
                                    VerticalAlign="Bottom"
                                    HorizontalAlign="Center" />

                                <Columns>
                                    <asp:BoundField DataField="corr_servicio_externo" SortExpression="corr_servicio_externo" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Servicio" />
                                    <asp:BoundField DataField="precio" HeaderText="Precio" />
                                    <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="Modificar" CommandName="modificar" ControlStyle-CssClass="btn btn-success" />
                                    <asp:ButtonField ButtonType="Button" Text="Eliminar" HeaderText="Eliminar" CommandName="eliminar" ControlStyle-CssClass="btn btn-danger" />
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

            <asp:LinkButton ID="lkbtnCerrarServicio" runat="server" CssClass="btn btn-danger" OnClick="lkbtnCerrarServicio_Click" OnClientClick="return confirm(&quot;¿Esta seguro de cerrar Servicio?. &quot;)">
            <span aria-hidden="true" class="glyphicon glyphicon-floppy-save"></span> Cerrar Servicio
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="lkbtn_viewPanelCerrarServicio"></asp:LinkButton>

            <cc1:ModalPopupExtender ID="lkbtn_viewPanelCerrarServicio_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                BehaviorID="lkbtn_viewPanelCerrarServicio_ModalPopupExtender" PopupControlID="pnl_CerrarServicio" TargetControlID="lkbtn_viewPanelCerrarServicio">
            </cc1:ModalPopupExtender>

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
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>

                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtCantidadDisponible" CssClass="control-label col-xs-2" Text="Cantidad Diponible:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtCantidadDisponible" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>


                    <asp:Label runat="server" AssociatedControlID="txtPrecio" CssClass="control-label col-xs-2" Text="Precio Q."></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtCantidad" CssClass="control-label col-xs-2" Text="Cantidad:"></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>

            </div>

            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardarProducto" CssClass="btn btn-primary" Text="Agregar" CommandName="Guardar" OnClick="btnGuardarProducto_Click" />
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

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtDescripcion" CssClass="control-label col-xs-2" Text="Descripcion:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <asp:Label runat="server" AssociatedControlID="txtPrecioServicio" CssClass="control-label col-xs-2" Text="Precio Q."></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtPrecioServicio" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardarServicioExterno" CssClass="btn btn-primary" Text="Agregar" CommandName="Guardar" OnClick="btnGuardarServicioExterno_Click" />
                <asp:Button runat="server" ID="btnSalirServicioExterno" CssClass="btn btn-default" Text="Salir" CausesValidation="false" OnClick="btnSalirServicioExterno_Click" />
            </div>
        </asp:Panel>
    </div>

    <%--panel para cerrar servicio--%>
    <div>
        <asp:Panel runat="server" ID="pnl_CerrarServicio" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Style="overflow: auto; max-height: 445px; width: 40%;">
            <div class="panel-heading">Agregar Servicio Externo</div>
            <p class="text-danger">
                <asp:Literal runat="server" ID="Literal1" />
            </p>
            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtNumeroFactura" CssClass="control-label col-xs-2" Text="Numero Factura:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <asp:Label runat="server" AssociatedControlID="txtSerieFactura" CssClass="control-label col-xs-2" Text="Serie Factura:"></asp:Label>
                    <div class="col-xs-4">
                        <asp:TextBox ID="txtSerieFactura" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="panel-footer">
                <asp:Button runat="server" ID="btnCerrarServicio" CssClass="btn btn-primary" Text="Cerrar" CommandName="Cerrar Servicio" OnClick="btnCerrarServicio_Click" />
                <asp:Button runat="server" ID="btnSalirCerrarFactura" CssClass="btn btn-default" Text="Salir" CausesValidation="false" OnClick="btnSalirCerrarFactura_Click" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
