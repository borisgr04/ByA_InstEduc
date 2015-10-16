<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="PagarSolo.aspx.cs" Inherits="AspIdentity.Liquidaciones.PagarSolo.PagarSolo" %>
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
    <script src="js/cPagar.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cLiquidacion" ng-cloak>
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
                    <div class="panel panel-default">
                        <div class="panel-heading">Pagar</div>
                        <div class="panel-body">
                            <div class="row">
                                <form name="datosLiquidacion">                                
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
                                    <div class="col-xs-3">
                                        <label>Fecha de pago (DD/MM/AAAA):</label>
                                        <input type="datetime-local" ng-model="fecha_pago" id="txtFechaPago" ng-change="_GetCarteraCausada()" class="form-control" />
                                    </div>
                                    <div class="col-xs-2">
                                        <label>Valor:</label>
                                        <div class="input-group">
                                            <input type="text" ng-model="valor_a_liquidar" format="number" ng_blur="_GetCarteraCausadaValor()" class="form-control text-right"/>
                                            <div class="input-group-btn ">
                                                <button type="button" ng-click="_GetCarteraCausadaValor()" class="btn btn-info dropdown-toggle" data-toggle="dropdown">
                                                    <span class="icon-search"></span>                        
                                                </button>                   
                                            </div>
                                        </div>                                        
                                    </div>
                                    <div class="col-xs-1">
                                        <button style="margin-top:21px" ng-disabled="valor_a_liquidar==0" ng-hide="!habGuardar" class="btn btn-success btn-xs" ng-click="_pagar()"><span class="glyphicon glyphicon-usd"></span> Pagar</button>
                                    </div>
                                </form>
                            </div>
                            <hr />
                            <div class="form-group row">
                                <div class="col-xs-2">
                                    <label>Grupo Pago:</label>
                                    <select class="form-control" ng-change="_GetCarteraCausada()" ng-model="grupo_pago" ng-options="grupo_pago as grupo_pago.nombre for grupo_pago in grupos_pagos"></select> 
                                </div>
                                <div class="col-xs-4">
                                    <label>Observación:</label>
                                    <input type="text" class="form-control" ng-model="observacion"/> 
                                </div>
                                <div class="col-xs-2">
                                    <div style="margin-top:27px;">
                                        <strong>Grado:  </strong>  {{estudiante.nombre_grado}}
                                    </div>                                    
                                </div>
                                <div class="col-xs-2">
                                    <div style="margin-top:27px;">
                                        <strong>Curso:  </strong>  {{estudiante.nombre_curso}}
                                    </div>                                    
                                </div>
                                <div class="col-xs-2 text-right">
                                    <button type="button" class="btn btn-info dropdown-toggle" ng-click="_verTodaCarteta()" data-toggle="dropdown" style="margin-top:27px;">
                                        <span class="icon-search"></span>  Ver todo                    
                                    </button>   
                                </div>
                            </div>
                            <div class="row" style="margin:0px">
                                 <table class="table table-bordered table-hover table-striped tablesorter" id="Table2">
                                    <thead>
                                        <tr>
                                            <th>Concepto <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Periodo <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Vigencia <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                                            <th style="text-align:center; width:10px;"><i class="fa fa-sort"></i></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-show="carteras.length > 0" ng-repeat="cartera in carteras">
                                            <td><strong>{{cartera.nombre_concepto}}</strong></td>
                                            <td class="text-right">{{cartera.periodo}}</td>
                                            <td class="text-right">{{cartera.vigencia}}</td>
                                            <td class="text-right">
                                                <label ng-show="(cartera.tipo=='CA' && !EditarValorCartera)">{{cartera.valor | currency:"":0}}</label>
                                                <input ng-show="(cartera.tipo=='IN' || EditarValorCartera)" style="height:20px;" type="text" ng-blur="_SumarValorPagar()" ng-model="cartera.valor" class="form-control text-right transparente" format="number" />
                                            </td>
                                            <td class="text-center"><a href="javascript:;" ng-click="_removeItemCartera(cartera)"><span class="glyphicon glyphicon-remove"></span></a></td>
                                        </tr>
                                        <tr ng-show="carteras.length > 0">
                                            <td class="text-right" colspan="3"><strong> Total = </strong></td>
                                            <td class="text-right">{{carteras |sumByKey:'valor' | currency:"$":0}}</td>
                                            <td class="text-right"></td>
                                        </tr>
                                        <tr ng-show="carteras.length == 0">
                                            <td colspan="5">No se han encontrado registros</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="row" style="margin:15px" ng-show="verLiquidacion">
                                <div class="row">
                                    <div class="col-xs-2"></div>
                                    <div class="col-xs-8">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <div class="row text-center" style="margin:10px">
                                                    <h3>LIQUIDACIÓN</h3>
                                                    <h5>No. {{liquidacion.id}}</h5>
                                                    <hr style="margin: 15px 4px 4px 4px" />
                                                </div>
                                                <div class="row" style="margin-left:5px">
                                                    <div class="col-xs-3">
                                                        <strong>Fecha: </strong> {{liquidacion.fecha | date}}
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <strong>Id Estudiante: </strong> {{estudiante.identificacion}} 
                                                    </div>
                                                    <div class="col-xs-5">
                                                        <strong>Nombre: </strong> {{estudiante.nombre}} 
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-left:5px">
                                                    <div class="col-xs-4">
                                                        <strong>Grupo Pago: </strong> {{grupo_pago.nombre}} 
                                                    </div>
                                                    <div class="col-xs-5">
                                                        <strong>Estado: </strong> {{liquidacion.nombre_estado}}
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
                                                            <tr ng-repeat="cartera in carteras">
                                                                <td><strong>{{cartera.nombre_concepto}}</strong></td>
                                                                <td class="text-right">{{cartera.periodo}}</td>
                                                                <td class="text-right">{{cartera.vigencia}}</td>
                                                                <td class="text-right">{{cartera.valor | currency:"$":0}}</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-right" colspan="3"><strong> Total = </strong></td>
                                                                <td class="text-right">{{_getTotalLiquidacion() | currency:"$":0}}</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="text-center" style="margin:10px;">
                                                    <button ng-disabled="editCartera" class="btn btn-info"><span class="glyphicon glyphicon-print"></span> Imprimir</button>
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
        </div>
    </div>
</asp:Content>
