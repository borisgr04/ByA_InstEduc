<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="PagosEstudiante.aspx.cs" Inherits="AspIdentity.Acudientes.Consultas.PagosEstudiante.PagosEstudiante" %>
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
    <script src="js/cPagosEstudiante.js"></script>
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
            <div class="panel-heading">Pagos Estudiante</div>
            <div class="panel-body">
            <div class="row" style="margin:7px">
                <div class="row">
                    <form name="datosConsulta">
                        <div class="col-xs-2">
                            <label>Id. Estudiante:</label>   
                                <div class="input-group">
                                    <input type="text" class="form-control" ng-blur="_traerestudiante()" ng-model="obj_consulta.id_estudiante" required="required" disabled="disabled" format="number"/>
                                </div> 
                        </div>
                        
                        <div class="col-xs-2">
                            <label>Nombre:</label>
                            <div class="input-group">
                                <input type="text" class="form-control" ng-model="estudiante.nombre_completo" disabled="disabled"/>
                            </div>                                     
                        </div>

                        <div class="col-xs-2">
                            <label>Desde</label>
                            <input type="date" class="form-control" ng-model="obj_consulta.FechaInicial" required />
                        </div>
                        <div class="col-xs-2">
                            <label>Hasta</label>
                            <input type="date" class="form-control" ng-model="obj_consulta.FechaFinal" required />
                        </div>
                        <div class="col-xs-2">
                            <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="Consultar()"><span class="glyphicon glyphicon-search"></span>Buscar</button>
                        </div>
                    </form>
                </div>
                <div class="row">
                    <hr />
                    <h4>Listado de pagos</h4>
                    <table class="table table-bordered table-hover table-striped tablesorter" id="tblDETLIQ">
                        <thead>
                            <tr>                                
                                <th>No. Pago <i class="fa fa-sort"></i></th>
                                <th>Fecha Liquidación <i class="fa fa-sort"></i></th>
                                <th>Fecha Pago <i class="fa fa-sort"></i></th>
                                <th style="text-align:right">Valor <i class="fa fa-sort"></i></th> 
                                <th style="text-align:right"><i class="fa fa-sort"></i></th>                               
                            </tr>
                        </thead>
                        <tbody> 
                            <tr ng-show="pagos.length > 0" ng-repeat="pago in pagos">                                
                                <td><strong>{{pago.id}}</strong></td>
                                <td>{{pago.fecha | date:'medium'}}</td>
                                <td>{{pago.fecha_pago | date:'medium'}}</td>
                                <td style="text-align:right">{{_sumarValorDetalles(pago) | currency:"$":0}}</td>     
                                <td style="text-align:right"><a href="javascript:;"><span class="glyphicon glyphicon-search" aria-hidden="true" ng-click="_verDetalles(pago)"></span></a></td>                           
                            </tr>
                            <tr ng-show="pagos.length > 0">
                                <td colspan="3" style="text-align:right"><strong>Total = </strong></td>
                                <td style="text-align:right">{{_valorPagos() | currency:"$":0}}</td>
                                <th><i class="fa fa-sort"></i></th>
                            </tr>      
                            <tr ng-show="pagos == null || pagos.length == 0">
                                <td colspan="5">No se han encontrado pagos</td>
                            </tr>                       
                        </tbody>
                    </table>
                </div>
                <div class="row">
                    <hr />
                    <h4>Detalles de pago</h4>
                    <table class="table table-bordered table-hover table-striped tablesorter" id="Table1">
                        <thead>
                            <tr>
                                <th>No. <i class="fa fa-sort"></i></th>
                                <th>Concepto <i class="fa fa-sort"></i></th>
                                <th style="text-align:right">Vigencia <i class="fa fa-sort"></i></th>
                                <th style="text-align:right">Periodo <i class="fa fa-sort"></i></th>
                                <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                            </tr>
                        </thead>
                        <tbody>   
                            <tr ng-show="detalles.length > 0" ng-repeat="detalle in detalles">
                                <td><strong>{{detalle.id}}</strong></td>
                                <td>{{detalle.nombre_concepto}}</td>
                                <td style="text-align:right">{{detalle.vigencia}}</td>
                                <td style="text-align:right">{{detalle.periodo}}</td>
                                <td style="text-align:right">{{detalle.valor | currency:"$":0}}</td>
                            </tr> 
                            <tr ng-show="detalles.length > 0">
                                <td colspan="4" style="text-align:right"><strong>Total = </strong></td>
                                <td style="text-align:right">{{detalles|sumByKey:'valor'| currency:"$":0}}</td>
                            </tr> 
                            <tr ng-show="detalles == null || detalles.length == 0">
                                <td colspan="5">No se ha seleccionado ningun pago</td>
                            </tr>                       
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
            </div>
        </div>
</asp:Content>
