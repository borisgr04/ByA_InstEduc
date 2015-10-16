<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Matricular.aspx.cs" Inherits="Skeleton.WebAPI.Matriculas.Matricular" %>
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
    <script src="js/cMatricular.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cMatricular" ng-cloak>
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
            <div class="row" id="secInfo"></div>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">Matrícula Estudiante</div>
                    <div class="panel-body">
                        <div class="row" style="margin-left:0px">
                            <div class="col-xs-6 text-left">
                                <h5 class="text-left">1. Datos de la matrícula</h5>
                            </div>                            
                        </div>
                        <div class="row">
                            <form name="datosMatricula">                                
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
                                    <label>Grado:</label>
                                    <select ng-model="grado" ng-change="_buscarCurso()" ng-options="grado as grado.nombre for grado in grados" class="form-control" required></select>
                                </div>
                                <div class="col-xs-2">
                                    <label>Curso:</label>
                                    <select ng-model="curso" ng-options="curso as curso.nombre for curso in cursos" class="form-control" required></select>
                                </div>
                                <div class="col-xs-3">
                                    <label>Fecha matricula:</label>
                                    <input type="datetime-local" ng-model="fecha_matricula" ng-change="_traerVisualizacionCartera()" class="form-control" id="txtFechaActual"/>
                                </div>
                            </form>
                            <div class="col-xs-2 text-left">
                                
                            </div>
                        </div>                        
                        <div class="row" ng-show="verCartera">
                            <hr style="margin: 15px 4px 4px 4px" />
                            <div class="row" style="margin-left:10px">
                                <div class="col-xs-6">
                                    <h5 class="text-left">2. Cartera del estudiante</h5>
                                </div> 
                                <div class="col-xs-6 text-right">
                                    <div style="margin:10px">
                                    <button type="button" class="btn btn-success btn-sm" ng-disabled="!habGuardar" ng-click="_matricular()"><span class="glyphicon glyphicon-ok"></span>Matrícular</button>
                                    <%--<button ng-hide="editCartera" ng-disabled="matriculado" class="btn btn-success" ng-click="editCartera=true"><span class="glyphicon glyphicon-pencil"></span> Editar cartera</button>
                                    <button ng-show="editCartera" class="btn btn-primary" ng-click="editCartera=false"><span class="glyphicon glyphicon-ok"></span> Guardar cartera</button>--%>
                                    <button ng-show="matriculado" ng-click="_irPagos()" class="btn btn-info"><span class="glyphicon glyphicon-usd"></span> Ir a Liquidaciones</button>
                                    </div>
                                </div>                              
                            </div>
                            <div class="row" style="margin:10px">
                                <table class="table table-bordered table-hover table-striped tablesorter" id="tblDETLIQ">
                                    <thead>
                                        <tr>
                                            <th>Concepto <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Desde <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Hasta <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-repeat="cartera in carteras">
                                            <td><strong>{{cartera.nombre_concepto}}</strong></td>
                                            <td style="text-align:right">
                                                <p ng-hide="editCartera">{{ cartera.periodo_desde }}</p>
                                                <select ng-show="editCartera" ng-model="cartera.periodo_desde" ng-options="periodo.periodo as periodo.periodo for periodo in periodos" ng-change="_verificarPeriodosCartera(cartera)" class="form-control text-right transparente">
                                                </select>                                            
                                            </td>
                                            <td style="text-align:right">
                                                <p ng-hide="editCartera">{{ cartera.periodo_hasta }}</p>
                                                <select ng-show="editCartera" ng-model="cartera.periodo_hasta" ng-options="periodo.periodo as periodo.periodo for periodo in periodos" ng-change="_verificarPeriodosCartera(cartera)" class="form-control text-right transparente">
                                                </select>
                                            </td>
                                            <td style="text-align:right">
                                                <p ng-hide="editCartera">{{ cartera.valor | currency:"$":0 }}</p>
                                                <input ng-show="editCartera" type="text" ng-model="cartera.valor" class="form-control text-right transparente" format="number" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>  
                            </div>                       
                        </div>
                        <div class="row" ng-show="verPrimeraLiquidacion">
                            <hr style="margin: 15px 4px 4px 4px" />
                            <div class="row" style="margin-left:10px">
                                <h5 class="text-left">3. Liquidación primer periodo</h5>
                            </div>
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
                                                        <strong>Id Estudiante: </strong> {{estudiante.id}} 
                                                    </div>
                                                    <div class="col-xs-5">
                                                        <strong>Nombre: </strong> {{estudiante.nombre}} 
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-left:5px">
                                                    <div class="col-xs-3">
                                                        <strong>Desde: </strong> {{liquidacion.perido_desde}} 
                                                    </div>
                                                    <div class="col-xs-4">
                                                        <strong>Hasta: </strong> {{liquidacion.perido_hasta}} 
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
                                                                <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="">
                                                            <tr ng-repeat="detalle in liquidacion.detalles_pago">
                                                                <td><strong>{{detalle.nombre_concepto}}</strong></td>
                                                                <td class="text-right">{{detalle.periodo}}</td>
                                                                <td class="text-right">{{detalle.valor | currency}}</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-right" colspan="2"><strong> Total = </strong></td>
                                                                <td class="text-right">{{_getTotalLiquidacion() | currency}}</td>
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
</asp:Content>
