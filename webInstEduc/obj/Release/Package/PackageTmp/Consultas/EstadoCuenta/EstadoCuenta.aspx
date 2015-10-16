<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="EstadoCuenta.aspx.cs" Inherits="AspIdentity.Consultas.EstadoCuenta.EstadoCuenta" %>
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
    <script src="js/cEstadoCuenta.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cEstadoCuenta" ng-cloak>
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
                <div class="panel-heading">Estado de cuenta</div>
                <div class="panel-body">
                    <div class="row" style="margin:7px">
                        <div class="row">
                            <form name="datosConsulta">
                                <div class="col-xs-2">
                                        <label>Id. Estudiante:</label>   
                                        <div class="input-group">
                                            <input type="text" class="form-control" ng-blur="_traerestudiante()" ng-model="obj_consulta.id_estudiante" required format="number"/>
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
                                    <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="_consultarSaldo()"><span class="glyphicon glyphicon-search"></span>Buscar</button>
                                </div>
                            </form>
                        </div>
                        <div class="row" ng-show="saldos.length > 0">
                            <hr />
                            <h4>Saldos por vigencias</h4>
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:70%">
                                <thead>
                                    <tr>                                        
                                        <th style="text-align:right">Vigencia <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Causado <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Pagado <i class="fa fa-sort"></i></th>  
                                        <th style="text-align:right">Deuda <i class="fa fa-sort"></i></th>  
                                        <th style="text-align:left">Detalle<i class="fa fa-sort"></i></th>                           
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="saldos.length > 0" ng-repeat="saldo in saldos">                                        
                                        <td style="text-align:right">{{saldo.Item}}</td>
                                        <td style="text-align:right">{{saldo.Valor| currency:"$":0}}</td>
                                        <td style="text-align:right">{{saldo.Pagado| currency:"$":0}}</td> 
                                        <td style="text-align:right">{{saldo.Valor - saldo.Pagado | currency:"$":0}}</td> 
                                        <td style="text-align:left"><a href="javascript:void(0)" ng-click="_consultarSaldoVigencia(saldo)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>                             
                                    </tr>
                                    <tr ng-show="saldos.length > 0">
                                        <td colspan="1" style="text-align:right"></td>
                                        <td style="text-align:right"><strong>{{saldos|sumByKey:'Valor' | currency:"$":0}}</strong></td>
                                        <td style="text-align:right"><strong>{{saldos|sumByKey:'Pagado'| currency:"$":0}}</strong></td>
                                        <td style="text-align:right"><strong>{{_DeudaTotalVigencias() | currency:"$":0}}</strong></td>
                                        <td colspan="1" style="text-align:right"></td>
                                    </tr>      
                                    <tr ng-show="saldos == null || saldos.length == 0">
                                        <td colspan="5">No se han encontrado registros</td>
                                    </tr>                       
                                </tbody>
                            </table>
                        </div>
                        <div class="row" ng-show="saldos_vigencias.length > 0">
                            <hr />
                            <h4>Saldos por periodos de la vigencia {{saldo.Item}}</h4>
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:70%">
                                <thead>
                                    <tr>                                        
                                        <th style="text-align:right">Periodo <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Causado <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Pagado <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Deuda <i class="fa fa-sort"></i></th>
                                        <th style="text-align:left">Detalle<i class="fa fa-sort"></i></th>                             
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="saldos_vigencias.length > 0" ng-repeat="saldo_vigencia in saldos_vigencias">                                        
                                        <td style="text-align:right">{{saldo_vigencia.Item}}</td>
                                        <td style="text-align:right">{{saldo_vigencia.Valor| currency:"$":0}}</td>
                                        <td style="text-align:right">{{saldo_vigencia.Pagado| currency:"$":0}}</td>  
                                        <td style="text-align:right">{{saldo_vigencia.Valor - saldo_vigencia.Pagado| currency:"$":0}}</td>          
                                        <td style="text-align:left"><a href="javascript:void(0)" ng-click="_consultarSaldoVigenciaPeriodo(saldo_vigencia)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>                    
                                    </tr>
                                    <tr ng-show="saldos_vigencias.length > 0">
                                        <td colspan="1" style="text-align:right"></td>
                                        <td style="text-align:right"><strong>{{saldos_vigencias|sumByKey:'Valor'| currency:"$":0}}</strong></td>
                                        <td style="text-align:right"><strong>{{saldos_vigencias|sumByKey:'Pagado'| currency:"$":0}}</strong></td>
                                        <td style="text-align:right"><strong>{{_DeudaTotalPeriodos() | currency:"$":0}}</strong></td>
                                        <td colspan="1" style="text-align:right"></td>
                                    </tr>      
                                    <tr ng-show="saldos_vigencias == null || saldos_vigencias.length == 0">
                                        <td colspan="5">No se han encontrado registros</td>
                                    </tr>                       
                                </tbody>
                            </table>
                        </div>
                        <div class="row" ng-show="saldos_vigencias_periodos.length > 0">
                            <hr />
                            <h4>Saldos por conceptos de la vigencia {{saldo.Item}} en el periodo {{saldo_vigencia.Item}}</h4>
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:70%">
                                <thead>
                                    <tr>
                                        <th>Concepto <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Causado <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Pagado <i class="fa fa-sort"></i></th>   
                                        <th style="text-align:right">Deuda <i class="fa fa-sort"></i></th>                             
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="saldos_vigencias_periodos.length > 0" ng-repeat="saldo_vigencia_periodo in saldos_vigencias_periodos">
                                        <td><strong>{{saldo_vigencia_periodo.Item.nombre}}</strong></td>
                                        <td style="text-align:right">{{saldo_vigencia_periodo.Valor| currency:"$":0}}</td>
                                        <td style="text-align:right">{{saldo_vigencia_periodo.Pagado| currency:"$":0}}</td>    
                                        <td style="text-align:right">{{saldo_vigencia_periodo.Valor - saldo_vigencia_periodo.Pagado| currency:"$":0}}</td>                              
                                    </tr>
                                    <tr ng-show="saldos_vigencias_periodos.length > 0">
                                        <td style="text-align:right"><strong>Totales = </strong></td>
                                        <td style="text-align:right"><strong>{{saldos_vigencias_periodos|sumByKey:'Valor'| currency:"$":0}}</strong></td>
                                        <td style="text-align:right"><strong>{{saldos_vigencias_periodos|sumByKey:'Pagado'| currency:"$":0}}</strong></td>
                                        <td style="text-align:right"><strong>{{_DeudaTotalConceptos() | currency:"$":0}}</strong></td>
                                    </tr>      
                                    <tr ng-show="saldos_vigencias_periodos == null || saldos_vigencias_periodos.length == 0">
                                        <td colspan="4">No se han encontrado registros</td>
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
