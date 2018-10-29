<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Repuestos.Catalogos.Clientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--Definicion del panel principal--%>
    <div class="panel panel-primary">
        <div class="panel-heading"><%:Title %></div>
        <br />
        <div class="panel-body form-vertical">
            <%--Area para agregar botones--%>
            <div class="btn">
                <asp:LinkButton runat="server" ID="lkBtn_nuevo" CssClass="btn btn-primary"><i aria-hidden="true" class="glyphicon glyphicon-pencil"></i> Nuevo </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lkBtn_viewPanel"></asp:LinkButton>

                <%--Funcionamiento de la accion de ocultar el panel y hacerlo visible y flotante mediante ajax--%>
                <cc1:ModalPopupExtender ID="lkBtn_nuevo_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="lkBtn_nuevo_ModalPopupExtender" PopupControlID="pnl_nuevo" TargetControlID="lkBtn_nuevo" CancelControlID="btnHide">
                </cc1:ModalPopupExtender>

                <cc1:ModalPopupExtender ID="lkBtn_viewPanel_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                    BehaviorID="lkBtn_viewPanel_ModalPopupExtender" PopupControlID="pnl_nuevo" TargetControlID="lkBtn_viewPanel">
                </cc1:ModalPopupExtender>

            </div>
            <br />
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorPrincipal" />
            </p>
            <%--Area para desplegar informacion mediante una tabla -  gridview--%>
            <div>
                <asp:GridView runat="server" ID="gvClientes"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No Existen registros."
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    OnRowCommand="gvClientes_RowCommand"
                    OnPageIndexChanging="gvClientes_PageIndexChanging">

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
                        <asp:BoundField DataField="id_cliente" SortExpression="id_cliente" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="correo" HeaderText="Correo" />

                        <%--Boton de Modificar--%>
                        <asp:ButtonField ButtonType="Button" Text="Modificar" HeaderText="Modificar" CommandName="modificar" ControlStyle-CssClass="btn btn-success" />

                        <%--Boton de Eliminar--%>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:Button Text="Eliminar" runat="server" ID="btnEliminar" CausesValidation="false" CommandName="eliminar" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-danger" OnClientClick="return confirm(&quot;¿Esta seguro de borrar opcion seleccionada?&quot;)" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vehiculos">
                            <ItemTemplate>
                                <asp:Button runat="server" Text="Vehiculos" ID="btnVehiculos" CausesValidation="false" CommandName="vehiculos" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-warning"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <%--Formulario para ingreso y modificacion de registros--%>
    <div>
        <%--Definicion del panel para ingreso y modificacion de registros--%>
        <asp:Panel runat="server" ID="pnl_nuevo" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" Style="overflow: auto; max-height: 545px; width: 35%;">
            <%--Encabezado del panel--%>
            <div class="panel-heading">Mantenimiento de <%:Title %></div>
            <%--Texto estatico para mostrar errores, estilo bootstrap error--%>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>

            <%--Cuerpo del Formulario--%>
            <div class="panel-body form-horizontal">


                <%--Campo Nit--%>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtNit" CssClass="control-label col-xs-4" runat="server" Text="NIT: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtNit" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNit"
                            CssClass="text-danger" ErrorMessage="El campo no puede quedar vacio." />
                    </div>
                </div>

                <%--Campo Nombre--%>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtNombres" CssClass="control-label col-xs-4" runat="server" Text="Nombres: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtNombres" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombres"
                            CssClass="text-danger" ErrorMessage="El campo no puede quedar vacio." />
                    </div>
                </div>


                <%--Campo Direccion--%>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtDireccion" CssClass="control-label col-xs-4" runat="server" Text="Direccion: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtDireccion" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDireccion"
                            CssClass="text-danger" ErrorMessage="El campo no puede quedar vacio." />
                    </div>
                </div>

                <%--Campo Telefono--%>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtTelefono" CssClass="control-label col-xs-4" runat="server" Text="Telefono: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtTelefono" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtTelefono"
                            CssClass="text-danger" ErrorMessage="El campo no puede quedar vacio." />--%>
                    </div>
                </div>

                <%--Campo Correo--%>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtCorreo" CssClass="control-label col-xs-4" runat="server" Text="Correo: "></asp:Label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txtCorreo" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtCorreo"
                            CssClass="text-danger" ErrorMessage="El campo no puede quedar vacio." />--%>
                    </div>
                </div>

            </div>

            <%--Pie del formulario, donde estan los botones de guardar y salir--%>
            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar" CommandName="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button runat="server" ID="btnSalir" CssClass="btn btn-default" Text="Salir" CausesValidation="false" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
