<%@ Page Title="Solicitudes de registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SolicitudRegistro.aspx.cs" Inherits="Repuestos.Account.SolicitudRegistro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="panel panel-primary">
        <div class="panel-heading"><%: Title %></div>
        <br />
        <p class="text-danger">
            <asp:Literal ID="ErrorMessage" runat="server" />
        </p>
        <asp:LinkButton runat="server" ID="lkBtn_Hidden_Autorizar" Style="display: hidden"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="lkBtn_Autorizar_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="lkBtn_Autorizar_ModalPopupExtender" PopupControlID="pnl_nuevo" TargetControlID="lkBtn_Hidden_Autorizar">
        </cc1:ModalPopupExtender>

        <asp:LinkButton runat="server" ID="lkBtn_Hidden_Motivo" Style="display: hidden"></asp:LinkButton>

        <cc1:ModalPopupExtender ID="lkBtn_Motivo_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="lkBtn_Motivo_ModalPopupExtender" PopupControlID="pnl_Motivo" TargetControlID="lkBtn_Hidden_Motivo">
        </cc1:ModalPopupExtender>

        <div class="panel-body form-vertical">

            <div>
                <asp:GridView runat="server" ID="gvSolicitudRegistro"
                    CssClass="table table-hover table-striped"
                    GridLines="None"
                    EmptyDataText="No existen solicitudes de registro."
                    AllowPaging="true"
                    OnPageIndexChanging="gvSolicitudRegistro_PageIndexChanging"
                    OnRowCommand="gvSolicitudRegistro_RowCommand"
                    AutoGenerateColumns="false">

                    <PagerSettings Mode="Numeric"
                        Position="Bottom"
                        PageButtonCount="10" />

                    <PagerStyle BackColor="LightBlue"
                        Height="30px"
                        VerticalAlign="Bottom"
                        HorizontalAlign="Center" />

                    <Columns>
                        <asp:BoundField DataField="id_usuario" SortExpression="id_usuario">
                            <HeaderStyle CssClass="display:none" />
                            <ItemStyle CssClass="display:none" />
                        </asp:BoundField>

                        <asp:BoundField DataField="nombres" HeaderText="Nombre" />
                        <asp:BoundField DataField="apellidos" HeaderText="Apellido" />
                        <asp:BoundField DataField="cui" HeaderText="CUI" />
                        <asp:BoundField DataField="correo" HeaderText="Correo" />
                        <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Registro" />
                        <asp:ButtonField ButtonType="Button" Text="Autorizar" CommandName="autorizar" ControlStyle-CssClass="btn btn-success" />
                        <asp:ButtonField ButtonType="Button" Text="Rechazar" CommandName="rechazar" ControlStyle-CssClass="btn btn-danger" />

                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>

    <div>
        <asp:Panel runat="server" ID="pnl_nuevo" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Mantenimiento de <%:Title%>.</div>
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessagePanel" />
            </p>
            <div class="panel-body form-horizontal">

                <p class="alert alert-info">
                    ¡Atento! Usted esta realizando una autorizacion para que el usuario:
                    <strong>
                        <asp:Literal runat="server" ID="UsuarioMesage" />
                    </strong>
                    Seleccione el tipo de rol que desea asignarle, en el siguiente listado.
                </p>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ddlTipoPermiso" CssClass="control-label col-xs-2" Text="Permiso: "></asp:Label>
                    <div class="col-xs-10">
                        <asp:DropDownList runat="server" ID="ddlTipoPermiso" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>


                <div class="panel-footer">
                    <asp:Button runat="server" ID="btnAutorizar" CssClass="btn btn-success" Text="Autorizar" OnClientClick="return confirm(&quot;¿Esta seguro de aprobar el usuario seleccionado?&quot;)" OnClick="btnAutorizar_Click" />
                    <asp:Button runat="server" ID="btnSalir" CssClass="btn btn-default" Text="Salir" CausesValidation="false" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnl_motivo" CssClass="panel panel-primary" BorderColor="Black" BackColor="White"
            BorderStyle="Inset" BorderWidth="1px" heigth="600" Width="35%">
            <div class="panel-heading">Motivo Rechazo o Aclaracion de <%:Title%>.</div>
            <p class="text-danger">
                <asp:Literal runat="server" ID="Literal1" />
            </p>
            <div class="panel-body form-horizontal">

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtMotivo" CssClass="control-label col-xs-2" Text="Motivo: "></asp:Label>
                    <div class="col-xs-10">
                        <asp:TextBox ID="txtMotivo" runat="server" TextMode="MultiLine" CssClass="form-control" />
                    </div>
                </div>

                <div class="panel-footer">
                    <asp:Button runat="server" ID="btnEnviar" CssClass="btn btn-success" Text="Enviar" OnClick="btnEnviar_Click" />
                    <asp:Button runat="server" ID="btnSalirMotivo" CssClass="btn btn-default" Text="Salir" CausesValidation="false" />
                </div>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
