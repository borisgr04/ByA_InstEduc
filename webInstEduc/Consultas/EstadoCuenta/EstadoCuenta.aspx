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
                                <div class="col-xs-4">
                                    <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="_consultarSaldo()"><span class="glyphicon glyphicon-search">  </span>Buscar</button>
                                    <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="_verModalEstadoCuenta()"><span class="glyphicon glyphicon-list-alt">  </span>Resumen</button>
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
            <!-- Modal Estado de Cuenta -->
            <div class="modal fade" id="modalResumenEstadoCuenta" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Resumen Estado de Cuenta</h4>
                  </div>
                  <div class="modal-body" id="dvdEstadoCuenta">
                    <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                        <tr class="text-center">
                            <td><strong>Estado de Cuenta</strong></td>
                            <td><strong>Fecha: </strong>{{fecha_actual  | date:'MMM d, y'}}</td>
                            <td class="text-center"><strong>Vigencia: </strong> {{vigencia}}</td>
                        </tr>
                        <tr class="text-left">
                            <td><strong>Estudiante: </strong> {{estudiante.nombre_completo}}</td>
                            <td><strong>Identificación: </strong> {{estudiante.identificacion}}</td>
                            <td><strong>Código: </strong> {{estudiante.codigo}}</td>
                        </tr>
                        <tr class="text-left">
                            <td><strong>Matricula: </strong> {{estadocuentaresumen.id_matricula}}</td>
                            <td><strong>Grado: </strong> {{estadocuentaresumen.nombre_grado}}</td>
                            <td><strong>Curso: </strong> {{estadocuentaresumen.nombre_curso}}</td>
                        </tr>
                    </table>
                    <div class="row">
                        <div class="col-xs-6">
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                                <tr class="text-center">
                                    <td><strong>Concepto</strong></td>
                                    <td><strong>Valor</strong></td>
                                    <td><strong>Pagado</strong></td>
                                    <td><strong>Saldo</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>Matricula</strong></td>
                                    <td>{{estadocuentaresumen.matricula.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.matricula.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.matricula.valor - estadocuentaresumen.matricula.pagado | currency:"$":0}}</strong></td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-6">
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                                <tr class="text-center">
                                    <td><strong>Concepto</strong></td>
                                    <td><strong>Valor</strong></td>
                                    <td><strong>Pagado</strong></td>
                                    <td><strong>Saldo</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>Otros</strong></td>
                                    <td>{{estadocuentaresumen.otros.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.otros.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.otros.valor - estadocuentaresumen.otros.pagado | currency:"$":0}}</strong></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                        <tr class="text-center">
                            <td><strong>Pensiones</strong></td>
                        </tr>
                    </table>
                    <div class="row">
                        <div class="col-xs-6">
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                                <tr class="text-center">
                                    <td><strong>No.</strong></td>
                                    <td><strong>Valor</strong></td>
                                    <td><strong>Pagado</strong></td>
                                    <td><strong>Saldo</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>2</strong></td>
                                    <td>{{estadocuentaresumen.pension2.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension2.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension2.valor - estadocuentaresumen.pension2.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>3</strong></td>
                                    <td>{{estadocuentaresumen.pension3.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension3.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension3.valor - estadocuentaresumen.pension3.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>4</strong></td>
                                    <td>{{estadocuentaresumen.pension4.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension4.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension4.valor - estadocuentaresumen.pension4.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>5</strong></td>
                                    <td>{{estadocuentaresumen.pension5.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension5.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension5.valor - estadocuentaresumen.pension5.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>6</strong></td>
                                    <td>{{estadocuentaresumen.pension6.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension6.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension6.valor - estadocuentaresumen.pension6.pagado | currency:"$":0}}</strong></td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-6">
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                                <tr class="text-center">
                                    <td><strong>No.</strong></td>
                                    <td><strong>Valor</strong></td>
                                    <td><strong>Pagado</strong></td>
                                    <td><strong>Saldo</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>7</strong></td>
                                    <td>{{estadocuentaresumen.pension7.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension7.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension7.valor - estadocuentaresumen.pension7.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>8</strong></td>
                                    <td>{{estadocuentaresumen.pension8.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension8.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension8.valor - estadocuentaresumen.pension8.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>9</strong></td>
                                    <td>{{estadocuentaresumen.pension9.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension9.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension9.valor - estadocuentaresumen.pension9.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>10</strong></td>
                                    <td>{{estadocuentaresumen.pension10.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension10.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension10.valor - estadocuentaresumen.pension10.pagado | currency:"$":0}}</strong></td>
                                </tr>
                                <tr class="text-right">
                                    <td class="text-left"><strong>11</strong></td>
                                    <td>{{estadocuentaresumen.pension11.valor | currency:"$":0}}</td>
                                    <td>{{estadocuentaresumen.pension11.pagado | currency:"$":0}}</td>
                                    <td><strong>{{estadocuentaresumen.pension11.valor - estadocuentaresumen.pension11.pagado | currency:"$":0}}</strong></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <table class="table table-bordered table-hover table-striped tablesorter" style="width:100%; font-size:80%; margin:2px;">
                        <tr class="text-center">
                            <td><strong>Saldos</strong></td>
                            <td><strong>Matricula:  </strong>  {{estadocuentaresumen.matricula.valor - estadocuentaresumen.matricula.pagado | currency:"$":0}}</td>
                            <td><strong>Pensiones:  </strong> {{spensiones | currency:"$":0}} </td>
                            <td><strong>Otros:  </strong>  {{estadocuentaresumen.otros.valor - estadocuentaresumen.otros.pagado | currency:"$":0}}</td>
                        </tr>
                    </table>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" ng-click="_imprimirEstadoCuenta()" class="btn btn-primary">Imprimir</button>
                  </div>
                </div>
              </div>
            </div>
        </div>
    </div>
</asp:Content>
