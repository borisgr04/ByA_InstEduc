<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="gLiquidaciones.aspx.cs" Inherits="AspIdentity.Acudientes.Liquidaciones.gLiquidaciones.gLiquidaciones" %>
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
    <script src="js/cLiquidaciones.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cLiquidaciones" ng-cloak>
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
                        <div class="panel-heading">Liquidaciones</div>
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
                                <div class="col-xs-2">
                                    <label>Grupo Pago: </label>
                                    <select class="form-control" ng-model="grupo_pago" ng-change="_traerLiquidacionesEstudiante()" ng-options="grupo_pago as grupo_pago.nombre for grupo_pago in grupos_pagos"></select> 
                                </div>
                                <div class="col-xs-3">
                                    <button style="margin-top:23px" class="btn btn-info btn-xs" ng-click="_traerLiquidacionesEstudiante()"><span class="glyphicon glyphicon-search"></span> Buscar Liquidaciones</button>
                                </div>
                            </div>
                            <div class="row" ng-show="verLiquidaciones">
                                <hr style="margin: 10px" />
                                <div class="row">
                                    <div class="col-xs-12" style="margin-left:10px">
                                        <a ng-click="_irLiquidar()" class="btn btn-default btn-xs"><span class="glyphicon glyphicon-plus"></span> Liquidar </a>
                                        <%--<a ng-click="_irPagar()" class="btn btn-xs btn-default btn-xs" style="margin-right:15px"><span class="glyphicon glyphicon-usd"></span> Pago  </a>--%>

                                        <a ng-disabled="noSeleccionadoLiq" class="btn btn-default btn-xs" ng-click="_irDetalleLiquidacion()"><span class="glyphicon glyphicon-search"></span> Detalles  </a>
                                        <a ng-disabled="noSeleccionadoLiq || data.selectLiquidacion.estado == 'PA'" ng-click="_irPagarLiquidacion()" class="btn btn-default btn-xs"><span class="glyphicon glyphicon-usd"></span> Pagar liquidación  </a>
                                        <button ng-disabled="noSeleccionadoLiq || data.selectLiquidacion.estado == 'PA'" class="btn btn-xs btn-default" ng-click="_irAnularLiquidacion()"><span class="glyphicon glyphicon-remove"></span> Anular liquidación</button>
                                        <button ng-disabled="noSeleccionadoLiq || data.selectLiquidacion.estado == 'LI'" class="btn btn-xs btn-default" ng-click="_irAnularPago()"><span class="glyphicon glyphicon-remove"></span> Anular pago</button>                                        
                                    </div>                                    
                                </div>
                                <div class="row" style="margin:10px">
                                    <table class="table table-bordered table-hover table-striped tablesorter" id="Table1">
                                        <thead>
                                            <tr>
                                                <th style="width:40px"> <i class="fa fa-sort"></i></th>
                                                <th>No. Liquidación <i class="fa fa-sort"></i></th>
                                                <th>Grupo <i class="fa fa-sort"></i></th>
                                                <th>Liquidada <i class="fa fa-sort"></i></th>
                                                <th>Pagada <i class="fa fa-sort"></i></th>
                                                <th class="text-right">Total <i class="fa fa-sort"></i></th>
                                                <th>Estado <i class="fa fa-sort"></i></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-show="liquidaciones.length > 0" ng-repeat="liquidacion in liquidaciones">
                                                <td><label><input type="radio" ng-change="_selectLiquidacion()" ng-model="data.selectLiquidacion" name="name" ng-value="{{liquidacion}}" /></label></td>                                                
                                                <td><strong>{{liquidacion.id}}</strong></td>
                                                <td><strong>{{liquidacion.nombre_grupo}}</strong></td>
                                                <td><strong>{{liquidacion.fecha | date:'MMM d, y'}}</strong></td> 
                                                <td><strong>{{liquidacion.fecha_pago | date:'MMM d, y'}}</strong></td> 
                                                <td class="text-right">{{liquidacion.ValorTotal | currency}}</td>  
                                                <td>{{_getNombreEstadoLiquidacion(liquidacion)}}</td>  
                                            </tr>    
                                            <tr ng-show="liquidaciones.length==0">
                                                <th colspan="7" class="text-left">No se han registrado liquidaciones</th>
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
