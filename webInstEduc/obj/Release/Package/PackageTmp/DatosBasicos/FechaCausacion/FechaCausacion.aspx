<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="FechaCausacion.aspx.cs" Inherits="AspIdentity.DatosBasicos.FechaCausacion.FechaCausacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/jqscripts/cssAngularPropios.css" rel="stylesheet" />
    <script src="/Angular/angular.min.js"></script>
    <script src="/Angular/angular-ui/ui-bootstrap-tpls.min.js"></script>
    <script src="/Angular/angular-route.js"></script>
    <script src="/Angular/angular-mocks.js"></script>
    <script src="/mAngular/angularModelo.js"></script>
    <script src="/mAngular/angularService.js"></script>
    <script src="/Angular/ng-table.js"></script>
    <script src="js/cFechaCausacion.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cFechaCausacion" ng-cloak>
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
                <div class="row">
                    <div class="col-xs-4">
                        <div class="panel panel-default">
                            <div class="panel-heading">Fecha de Causación</div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <label>Fecha de causación</label>
                                        <input type="date" ng-model="fecha_causacion" class="form-control" id="txtFechaActual"/>
                                    </div>
                                    <div class="col-xs-12 text-right">
                                        <button type="button" class="btn btn-info dropdown-toggle" ng-click="cambiarFechaCausacion()" style="margin-top:10px;">
                                            <span class="icon-save"></span> Guardar                    
                                        </button> 
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
