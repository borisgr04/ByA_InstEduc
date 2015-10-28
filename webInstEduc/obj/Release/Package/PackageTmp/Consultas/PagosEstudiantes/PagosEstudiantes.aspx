<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="PagosEstudiantes.aspx.cs" Inherits="AspIdentity.Consultas.PagosEstudiantes.PagosEstudiantes" %>
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
    <script src="js/PagosEstudiantes.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cConsultaPagosEstudiante" ng-cloak>
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
            <div class="panel panel-default">
            <div class="panel-heading">Listado de Pagos</div>
            <div class="panel-body">
            <div class="row" style="margin:7px">
                <div class="row">
                    <form name="datosConsulta">
                        <div class="col-xs-2">
                            <label>Desde</label>
                            <input type="date" class="form-control" ng-model="obj_consulta.FechaInicial" id="fch_inicial" required />
                        </div>
                        <div class="col-xs-2">
                            <label>Hasta</label>
                            <input type="date" class="form-control" ng-model="obj_consulta.FechaFinal" id="fch_final" required />
                        </div>
                        <div class="col-xs-4">
                            <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="Consultar()"><span class="glyphicon glyphicon-search"></span>Buscar</button>
                            <button style="margin-top:23px" class="btn btn-info" ng-click="_printPagos()"><span class="glyphicon glyphicon-print"></span> Imprimir</button>
                        </div>
                    </form>
                </div>        
                <div class="row">
                    <hr />
                    <h4>Listado de pagos</h4>
                    <div id="printPagos">                    
                    <table class="table table-bordered table-hover table-striped tablesorter" id="tblPagos" style="width:100%">
                        <thead>
                            <tr>                                
                                <th style="text-align:right">No. Pago</th>
                                <th style="text-align:right">Identificación</th>
                                <th style="text-align:left">Nombre Estudiante</th>
                                <th>Fecha Pago</th>
                                <th>Concepto</th>
                                <th style="text-align:right">Periodo</th>
                                <th style="text-align:right">Vigencia</th>
                                <th style="text-align:right">Valor</th>                              
                            </tr>
                        </thead>
                        <tbody ng-repeat="pago in pagos" ng-show="pagos.length > 0"> 
                            <tr>                                
                                <td rowspan="{{pago.detalles_pago.length + 2}}" style="text-align:right"><strong>{{pago.id}}</strong></td>
                                <td rowspan="{{pago.detalles_pago.length + 2}}" style="text-align:right"><strong>{{pago.id_estudiante}}</strong></td>
                                <td rowspan="{{pago.detalles_pago.length + 2}}" style="text-align:left"><strong>{{pago.nombre_estudiante}}</strong></td>
                                <td rowspan="{{pago.detalles_pago.length + 2}}">{{pago.fecha_pago | date:'medium'}}</td>       
                                <td>{{pago.detalles_pago2[0].nombre_concepto}}</td>       
                                <td style="text-align:right">{{pago.detalles_pago2[0].periodo}}</td>  
                                <td style="text-align:right">{{pago.detalles_pago2[0].vigencia}}</td>       
                                <td style="text-align:right">{{pago.detalles_pago2[0].valor  | currency:"$":0}}</td>                           
                            </tr>
                            <tr ng-repeat="detalle in pago.detalles_pago3">
                                <td>{{detalle.nombre_concepto}}</td>       
                                <td style="text-align:right">{{detalle.periodo}}</td>  
                                <td style="text-align:right">{{detalle.vigencia}}</td>       
                                <td style="text-align:right">{{detalle.valor  | currency:"$":0}}</td> 
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align:right"><strong>Total=</strong></td>
                                <td style="text-align:right"><strong>{{pago.detalles_pago |sumByKey:'valor'| currency:"$":0}}</strong></td>
                            </tr>        
                        </tbody>
                        <tbody ng-show="pagos.length > 0">
                            <tr>
                                <td colspan="7" style="text-align:right"><strong>Total pagos=</strong></td>
                                <td style="text-align:right"><strong>{{_valorPagos() | currency:"$":0}}</strong></td>
                            </tr>  
                        </tbody>
                    </table>
                    </div>
                    <div>
                        <table class="table table-bordered table-hover table-striped tablesorter" id="Table1" style="width:100%">
                            <tbody ng-show="pagos == null || pagos.length == 0">
                                <tr>
                                    <td colspan="7">No se han encontrado pagos</td>
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
</asp:Content>
