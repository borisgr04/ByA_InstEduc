<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="gNotasCredito.aspx.cs" Inherits="AspIdentity.NotaCredito.gNotasCredito.gNotasCredito" %>
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
    <script src="js/gcNotaCredito.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="gcNotaCredito" ng-cloak>
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
                <div class="row" id="secInfo"></div>
                <div class="row">
                    <div class="panel panel-default">
                        <div class="panel-heading">Notas Credito</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-xs-2">
                                        <label>Id. Estudiante:</label>   
                                        <div class="input-group">
                                            <input type="text" class="form-control" ng-blur="_traerestudiante()" ng-model="estudiante.identificacion" required format="number"/>
                                            <div class="input-group-btn ">
                                                <button ng-click="_traerestudiante()" type="button" class="btn btn-info no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-search"></span></button>
                                            </div>
                                        </div> 
                                    </div>
                                    <div class="col-xs-3">
                                        <label>Nombre:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" ng-model="estudiante.nombre_completo" disabled="disabled"/>
                                            <div class="input-group-btn ">
                                                <button ng-click="_limpiarEstudiante()" type="button" class="btn btn-danger no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-remove"></span></button>                  
                                            </div>
                                        </div>                                     
                                    </div>
                            </div>
                            <div class="row">
                                <hr style="margin: 10px" />
                                <div class="row">
                                    <div class="col-xs-12" style="margin-left:10px">
                                        <a ng-click="_irNuevaNotaCredito()" class="btn btn-default btn-xs"><span class="glyphicon glyphicon-plus"></span> Nueva Nota Credito </a>                                        
                                    </div>                                    
                                </div>
                                <div class="row" style="margin:10px">
                                    <table class="table table-bordered table-hover table-striped tablesorter" id="Table1">
                                        <thead>
                                            <tr>
                                                <th>No. Nota <i class="fa fa-sort"></i></th>
                                                <th>Grupo <i class="fa fa-sort"></i></th>
                                                <th>Fecha <i class="fa fa-sort"></i></th>
                                                <th class="text-right">Total <i class="fa fa-sort"></i></th>
                                                <th style="width:40px"> <i class="fa fa-sort"></i></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-show="NotasCredito.length > 0" ng-repeat="liquidacion in NotasCredito">
                                                <td><strong>{{liquidacion.id}}</strong></td>
                                                <td><strong>{{liquidacion.nombre_grupo}}</strong></td>
                                                <td><strong>{{liquidacion.fecha | date:'MMM d, y'}}</strong></td> 
                                                <td class="text-right">{{liquidacion.ValorTotal | currency}}</td>  
                                                <td><a href="javascript:void(0)" ng-click="_irDetalleNota(liquidacion)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>
                                            </tr>    
                                            <tr ng-show="liquidaciones.length==0">
                                                <th colspan="4" class="text-left">No se han registrado liquidaciones</th>
                                            </tr>                                                   
                                        </tbody>                                                    
                                    </table>                                                
                                </div>
                            </div>                              
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
