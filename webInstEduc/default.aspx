<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AspIdentity._default" %>
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
    <script src="/cDefault.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cDefault" ng-cloak>
            <div class="container">



    <div class="row text-center" style="margin-top:30px">
        <div class="row text-center" style="margin:10px">
            <div class="col-xs-3"></div>
            <div class="col-xs-6">
                 <h1>Colegio Calleja Real</h1>
            </div> 
            <div class="col-xs-3">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-12 text-center">
                                <h6><strong>Fecha Causación:</strong>  {{fecha_causacion | date:'yyyy-MM-dd'}}</h6>
                                <a href="javascript:;" ng-click="SiEditarFechaCausacion()">                                
                                    <h6 ng-hide="editFecha" ></h6>  
                                </a>
                                <div class="input-group" ng-show="editFecha">
                                    <input type="date" ng-model="fecha_causacion" class="form-control" id="txtFechaActual"/>
                                    <div class="input-group-btn ">
                                        <button type="button" class="btn btn-info dropdown-toggle" ng-click="GuardarFechaCausacion()">D
                                            <span class="icon-save"></span>                        
                                        </button>                  
                                    </div>
                                </div>                                 
                            </div>
                        </div>            
                    </div>   
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-3"></div>
            <div class="col-xs-6">
                <div class="row">
                    <div class="input-group">
                        <input type="text" class="form-control" ng-model="filtro"/>
                        <div class="input-group-btn ">
                            <button type="button" class="btn btn-info dropdown-toggle" data-toggle="dropdown" ng-click="_traerestudiantes()">
                                <span class="icon-search"></span>                        
                            </button> 
                            <button type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" ng-click="_limpiarEstudiante()">
                                <span class="icon-remove"></span>                        
                            </button>                   
                        </div>
                    </div> 
                </div>
                <div class="row">
                    <table class="table table-bordered table-hover table-striped tablesorter" id="Table1">
                        <tbody>
                            <tr>
                                <td><strong>Código</strong></td>
                                <td><strong>Identificación</strong></td>
                                <td><strong>Nombre</strong></td>
                            </tr>
                            <tr ng-repeat="estudiante in estudiantes" ng-click="_seleccionarEstudiante(estudiante)">
                                <td class="text-right" style="width:100px"><strong>{{estudiante.codigo}}</strong></td>
                                <td class="text-right" style="width:100px"><strong>{{estudiante.identificacion | currency:"":0}}</strong></td>
                                <td class="text-left"><strong>{{estudiante.terceros.apellido}}  {{estudiante.terceros.nombre}}</strong></td>
                            </tr>                                                     
                        </tbody>                                                    
                    </table> 
                </div>
            </div>
            <div class="col-xs-3">               
            </div>
        </div>
    </div>
    <div class="row" style="margin:30px">
        <div class="col-xs-3">
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
                            <h4>Matricula</h4>
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
        <div class="col-xs-3">
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
            <div class="panel panel-default" ng-click="_irTransacciones()">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                             <i class="icon-table icon-5x"></i>
                        </div>
                        <div class="col-xs-9 text-center">
                            <h4>Transacciones</h4>
                        </div>
                    </div>            
                </div>   
            </div>
            </a>
        </div>
        <div class="col-xs-3">
            <a href="javascript:;">
            <div class="panel panel-default" ng-click="_irRealizarPago()">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-3">
                            <i class="icon-dollar icon-5x"></i>
                        </div>
                        <div class="col-xs-9 text-center">
                            <h4>Realizar pago</h4>
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
    </div>  
                
                
       </div>
           </div>
       </div>  
</asp:Content>
