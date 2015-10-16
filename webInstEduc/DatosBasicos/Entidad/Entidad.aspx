<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Entidad.aspx.cs" Inherits="AspIdentity.DatosBasicos.Entidad.Entidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/jqscripts/cssAngularPropios.css" rel="stylesheet" />
    <script src="/jqscripts/qrcode.js"></script>
    <script src="/Angular/angular.min.js"></script>
    <script src="/Angular/angular-ui/ui-bootstrap-tpls.min.js"></script>
    <script src="/Angular/angular-route.js"></script>
    <script src="/Angular/angular-mocks.js"></script>
    <script src="/mAngular/angularModelo.js"></script>
    <script src="/mAngular/angularService.js"></script>
    <script src="/Angular/ng-table.js"></script>
    <script src="../Entidad/js/cEntidad.js"></script>
    <div class="container" ng-app="Model">
    <form id="datosEntidad" runat="server">
        <div ng-controller="cEntidad" ng-cloak>
            <div class="loading-spiner-holder" data-loading >
                <div class="loading-spiner">
                    <div id="floatingCirclesG">
                        <div class="f_circleG" id="frotateG_01">
                        </div>
                        <div class="f_circleG" id="frotateG_02"> 
                        </div>
                        <div class="f_circleG" id="frotateG_03">
                        </div>
                        <div class="f_circleG" id="frotateG_04">
                        </div>
                        <div class="f_circleG" id="frotateG_05">
                        </div>
                        <div class="f_circleG" id="frotateG_06">
                        </div>
                        <div class="f_circleG" id="frotateG_07">
                        </div>
                        <div class="f_circleG" id="frotateG_08">
                        </div>
                    </div>       
                </div>
            </div>
                <div class="container">
                    
                    <div class="form-horizontal" style="margin:10px">
                        <div class="col-xs-10">                        
                            <h1>ENTIDAD</h1>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">ID</label>
                                    <div class="col-sm-10">
                                        <input type="text" id="id" class="form-control" name="id" runat="server" ng-model="entidad.id" required="required" readonly="true"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">Nombre</label>
                                    <div class="col-sm-10">
                                        <input type="text" id="nombre"  class="form-control" name="nombre" runat="server" ng-model="entidad.nombre" required="required"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">Dirección</label>
                                    <div class="col-sm-10">
                                        <input type="text" id="direccion" class="form-control" name="direccion" runat="server" ng-model="entidad.direccion" required="required"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">Telefono</label>
                                    <div class="col-sm-10">
                                        <input type="text" id="telefono" class="form-control" name="telefono" runat="server" ng-model="entidad.telefono" required/>
                                    </div>
                                </div>
                            <div class="form-group">
                                <div class="col-sm-offset-9 col-sm-9">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-edit" OnClick="Button1_Click" Text="Actualizar" />
                                </div>
                                
                            </div>
                            <div "form-group">
                            
                                    <asp:FileUpload ID="FileUpload1" runat="server" />

                            </div>
                            <div "form-group">
                                <div id="i1">
                                    <img id="imagen" src="/api/Entidad/Logo" class="img-rounded"/>
                                </div>

                                <asp:Button ID="BtnGuardarImagen" runat="server" OnClick="BtnGuardarImagen_Click" Text="Cambiar" CssClass="btn btn-success btn-sm" />
                                <input type="button" id="cancelar" value="Cancelar" class="btn btn-default btn-sm"/>
                            </div>

                        </div>                   
                    </div>
                </div>
        </div>
    </form>
    </div>
</asp:Content>
