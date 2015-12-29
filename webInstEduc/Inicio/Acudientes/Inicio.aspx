<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AspIdentity.Inicio.Acudientes.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/jqscripts/cssAngularPropios.css" rel="stylesheet" />
    <script src="/Angular/angular.min.js"></script>
    <script src="/Angular/angular-ui/ui-bootstrap-tpls.min.js"></script>
    <script src="/Angular/angular-route.js"></script>
    <script src="/Angular/angular-mocks.js"></script>
    <script src="/Angular/ng-table.js"></script>
    <script src="/mAngular/angularModelo.js"></script>
    <script src="/mAngular/angularService.js"></script>
    <script src="js/cInicioAcudientes.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cInicioAcudientes" ng-cloak>
            <div class="container">

    <div class="row text-center" style="margin-top:15px">
        <div class="row text-center" style="margin:10px">
            <div class="col-xs-7 col-md-offset-2">
                 <h1>Colegio Calleja Real</h1>
                <hr />
                <table class="table table-bordered table-hover table-striped tablesorter" id="Table1">
                    <tbody>
                        <tr>
                            <td><strong></strong></td>
                            <td><strong>Código</strong></td>
                            <td><strong>Identificación</strong></td>
                            <td><strong>Nombre</strong></td>
                            <td><strong>Grado</strong></td>
                        </tr>
                        <tr ng-repeat="estudiante in estudiantes">
                            <td><label><input type="checkbox" ng-model="estudiante.activo" ng-change="SelecEstudiante(estudiante)"/></label></td>
                            <td class="text-right" style="width:100px"><strong>{{estudiante.codigo}}</strong></td>
                            <td class="text-right" style="width:100px"><strong>{{estudiante.identificacion | currency:"":0}}</strong></td>
                            <td class="text-left"><strong>{{estudiante.terceros.apellido}}  {{estudiante.terceros.nombre}}</strong></td>
                            <td class="text-left"><strong>{{estudiante.nombre_grado}} - {{estudiante.nombre_curso}}</strong></td>
                        </tr>                                                     
                    </tbody>                                               
                </table>
            </div> 
        </div>

        <div class="row">
            <div class="col-xs-3 col-md-offset-1">
                <a href="javascript:;">
                    <div class="panel panel-default" ng-click="_irInformacion()">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="icon-user icon-5x"></i>
                                </div>
                                <div class="col-xs-9 text-center">
                                    <h4>Información</h4>
                                </div>
                            </div>            
                        </div>   
                    </div>
                </a>
            </div>
            <div class="col-xs-3">
                <a href="javascript:;">
                    <div class="panel panel-default" ng-click="_irMatricula()">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="icon-file-text-alt icon-5x"></i>
                                </div>
                                <div class="col-xs-9 text-center">
                                    <h4 style="margin-top:23px">Matricula</h4>
                                </div>
                            </div>            
                        </div>   
                    </div>
                </a>
            </div>
            <div class="col-xs-3">
                <a href="javascript:;">
                    <div class="panel panel-default" ng-click="_irLiquidaciones()">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="icon-list-alt icon-5x"></i>
                                </div>
                                <div class="col-xs-9 text-center">
                                    <h4 style="margin-top:23px">Liquidaciones</h4>
                                </div>
                            </div>            
                        </div>   
                    </div>
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-3 col-md-offset-1">
                <a href="javascript:;">
                    <div class="panel panel-default" ng-click="_irLiquidar()">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="icon-file-alt icon-5x"></i>
                                </div>
                                <div class="col-xs-9 text-center">
                                    <h4>Liquidar</h4>
                                </div>
                            </div>            
                        </div>   
                    </div>
                </a>
            </div>
            <div class="col-xs-3">
                <a href="javascript:;">
                    <div class="panel panel-default" ng-click="_irEstadoCuentaEstudiante()">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="icon-table icon-5x"></i>
                                </div>
                                <div class="col-xs-9 text-center">
                                    <h4>Estado cuenta</h4>
                                </div>
                            </div>            
                        </div>   
                    </div>
                </a>
            </div>
            <div class="col-xs-3">
                <a href="javascript:;">
                    <div class="panel panel-default" ng-click="_irPagosEstudiante()">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="icon-sort-by-attributes icon-5x"></i>
                                </div>
                                <div class="col-xs-9 text-center">
                                    <h4>Pagos Estudiante</h4>
                                </div>
                            </div>            
                        </div>   
                    </div>
                </a>
            </div>
        </div>
    </div>
    </div>
        </div></div> 
</asp:Content>
