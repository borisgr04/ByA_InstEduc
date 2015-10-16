<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="AnularPago.aspx.cs" Inherits="AspIdentity.Liquidaciones.PagarLiquidacion.PagarLiquidacion" %>
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
    <script src="js/cPagarLiquidacion.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cPagarLiquidacion" ng-cloak>
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
                    <%--<div class="panel panel-default">
                        <div class="panel-heading">Detalle liquidación</div>
                        <div class="panel-body">--%>
                            <div class="row">                                
                                <div class="row">
                                    <div class="col-xs-2">                                        
                                    </div>
                                    <div class="col-xs-8">
                                        <div class="text-left" style="margin:10px;">
                                            <button ng-disabled="!verLiquidacion" class="btn btn-info" ng-click="_print()"><span class="glyphicon glyphicon-print"></span> Imprimir</button>   
                                            <button ng-show="verLiquidacion" ng-disabled="liquidacion.estado == 'LI' || liquidacion.estado == 'AN' || !habGuardar" ng-click="_anularPago()" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span> Anular Pago</button>
                                            <button ng-show="verLiquidacion" ng-click="_limpiar()" class="btn btn-info"><span class="glyphicon glyphicon-search"></span> Buscar otra liquidación</button>
                                            <button ng-click="_Back()" class="btn btn-success"><span class="glyphicon glyphicon-chevron-left"></span> Atrás</button>
                                        </div>
                                        <div class="panel panel-default" id="print">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-xs-4 text-center" style="margin-top:15px">
                                                        <img src="/wsFoto.ashx" style="width:60%;height:100px" />
                                                    </div>
                                                    <div class="col-xs-4 text-center">
                                                        <h3><strong>{{entidad.nombre}}</strong></h3>
                                                        <h5><strong>Dirección: </strong> {{entidad.direccion}}</h5>
                                                        <h5><strong>Teléfono: </strong> {{entidad.telefono}}</h5>
                                                    </div>
                                                    <div class="col-xs-4 text-center" style="margin-top:15px" id="qrNoLiq">
                                                    </div>
                                                </div> 
                                                <div class="row text-center" style="margin:10px">
                                                    <h3>LIQUIDACIÓN</h3>
                                                    <h5 ng-show="verLiquidacion">No. {{liquidacion.id}}</h5>
                                                    <div class="row"  ng-hide="verLiquidacion">
                                                        <div class="col-xs-4"></div>
                                                        <div class="col-xs-4">
                                                            <div class="row">
                                                                <div class="col-xs-3">
                                                                    <h5>No. </h5>
                                                                </div>
                                                                 <div class="col-xs-7">
                                                                     <input type="text" class="form-control text-right" ng-model="nro_liquidacion" ng-blur="_traerLiquidacion()" />
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="col-xs-4"></div>
                                                    </div>                                                    
                                                    <hr style="margin: 15px 4px 4px 4px" />
                                                </div>
                                                <div class="row" style="margin-left:5px" ng-show="verLiquidacion">
                                                    <div class="col-xs-3 text-left">
                                                        <strong>Id Estudiante: </strong> {{estudiante.identificacion}} 
                                                    </div>
                                                    <div class="col-xs-5 text-left">
                                                        <strong>Nombre: </strong> {{estudiante.nombre_completo}} 
                                                    </div>
                                                    <div class="col-xs-4 text-left">
                                                        <strong>Liquidada: </strong>{{liquidacion.fecha | date:'MMM d, y'}}
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-left:5px" ng-show="verLiquidacion">
                                                    <div class="col-xs-3 text-left">
                                                        <strong>Grupo Pago: </strong> {{grupo_pago.nombre}} 
                                                    </div>
                                                    <div class="col-xs-5 text-left">
                                                        <strong>Estado: </strong> {{liquidacion.nombre_estado}}
                                                    </div>
                                                    <div class="col-xs-4 text-left">
                                                        <strong>Vence: </strong> {{liquidacion.fecha_max_pago | date:'MMM d, y'}}
                                                    </div> 
                                                </div>
                                                <div class="row" style="margin-left:5px">
                                                    <div class="col-xs-8 text-left">
                                                        <strong>Observación: </strong> {{liquidacion.observacion}} 
                                                    </div>
                                                    <div class="col-xs-4 text-left" ng-show="liquidacion.estado=='PA'">
                                                        <strong>Pagada: </strong> {{liquidacion.fecha_pago | date:'MMM d, y'}}
                                                    </div>
                                                </div>
                                                <div class="row" style="margin:10px">
                                                    <table class="table table-bordered table-hover table-striped tablesorter" id="Table1">
                                                        <thead>
                                                            <tr>
                                                                <th>Concepto <i class="fa fa-sort"></i></th>
                                                                <th style="text-align:right">Periodo <i class="fa fa-sort"></i></th>
                                                                <th style="text-align:right">Vigencia <i class="fa fa-sort"></i></th>
                                                                <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr ng-show="liquidacion.detalles_pago != 0" ng-repeat="detalle in liquidacion.detalles_pago">
                                                                <td><strong>{{detalle.nombre_concepto}}</strong></td>
                                                                <td class="text-right">{{detalle.periodo}}</td>
                                                                <td class="text-right">{{detalle.vigencia}}</td>
                                                                <td class="text-right">{{detalle.valor | currency:"$":0}}</td>
                                                            </tr>
                                                            <tr ng-show="liquidacion.detalles_pago != 0">
                                                                <td class="text-right" colspan="3"><strong> Total = </strong></td>
                                                                <td class="text-right">{{_getTotalLiquidacion() | currency:"$":0}}</td>
                                                            </tr>
                                                            <tr ng-show="liquidacion.detalles_pago == 0">
                                                                <td class="text-left" colspan="4"><strong>No ha digitado un numero de liquidación</strong></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div> 
                                    <div class="col-xs-2"></div>
                                </div>  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <%--</div>
    </div>--%>
</asp:Content>
