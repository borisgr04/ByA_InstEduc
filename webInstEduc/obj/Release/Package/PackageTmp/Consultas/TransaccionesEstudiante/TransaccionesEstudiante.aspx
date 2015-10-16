<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="TransaccionesEstudiante.aspx.cs" Inherits="AspIdentity.Consultas.TransaccionesEstudiante.TransaccionesEstudiante" %>
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
    <script src="js/cTransaccionesEstudiante.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cTransaccionesEstudiante" ng-cloak>
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
            <div class="panel-heading">Transacciones Estudiante</div>
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
                            <label>Desde</label>
                            <input type="date" class="form-control" ng-model="obj_consulta.fecha_inicio" required />
                        </div>
                        <div class="col-xs-2">
                            <label>Hasta</label>
                            <input type="date" class="form-control" ng-model="obj_consulta.fecha_final" required />
                        </div>
                        <div class="col-xs-2">
                            <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="Consultar()"><span class="glyphicon glyphicon-search"></span>Buscar</button>
                        </div>
                    </form>
                </div>
                <div class="row">
                    <hr />
                    <h4>Listado de transacciones</h4>
                    <table class="table table-bordered table-hover table-striped tablesorter">
                        <thead>
                            <tr>                                
                                <th>Concepto <i class="fa fa-sort"></i></th>                                
                                <th class="text-right">Vigencia <i class="fa fa-sort"></i></th>
                                <th class="text-right">Periodo <i class="fa fa-sort"></i></th>
                                <th>Documento <i class="fa fa-sort"></i></th>
                                <th>Fecha <i class="fa fa-sort"></i></th>         
                                <th class="text-right">Valor debito <i class="fa fa-sort"></i></th>  
                                <th class="text-right">Valor credito <i class="fa fa-sort"></i></th>                                 
                            </tr>
                        </thead>
                        <tbody> 
                            <tr ng-show="movimientos.length > 0" ng-repeat="movimiento in movimientos">                                
                                <td><strong>{{movimiento.nombre_concepto}}</strong></td>                                
                                <td class="text-right">{{movimiento.vigencia}}</td>
                                <td class="text-right">{{movimiento.periodo}}</td>     
                                <td>{{movimiento.tipo_documento}}</td>
                                <td>{{movimiento.fecha_movimiento | date:'medium'}}</td>  
                                <td class="text-right">{{movimiento.valor_debito | currency:"$":0}}</td>  
                                <td class="text-right">{{movimiento.valor_credito | currency:"$":0}}</td>                           
                            </tr>     
                            <tr ng-show="movimientos == null || movimientos.length == 0">
                                <td colspan="7">No se han encontrado transacciones</td>
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
