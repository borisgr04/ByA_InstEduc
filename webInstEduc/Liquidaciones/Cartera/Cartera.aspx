<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Cartera.aspx.cs" Inherits="AspIdentity.Liquidaciones.Cartera.Cartera" %>
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
    <script src="js/cCarteraEstudiante.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cCarteraEstudiante" ng-cloak>
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
                        <div class="panel-heading">Cartera del Estudiante</div>
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
                                    <div class="col-xs-1">
                                        <button style="margin-top:21px" ng-disabled="!habGuardar" ng-click="_guardar()" class="btn btn-success btn-xs"><span class="glyphicon glyphicon-floppy-disk"></span> Guardar</button>
                                    </div>
                                </form>
                            </div>
                            <hr />
                            <div class="row" style="margin:0px">
                                 <table class="table table-bordered table-hover table-striped tablesorter" id="tableCartera">
                                    <thead>
                                        <tr>
                                            <th>Concepto <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Periodo <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Vigencia <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Pagado <i class="fa fa-sort"></i></th>
                                            <th>Causado <i class="fa fa-sort"></i></th>
                                            <th style="text-align:center; width:30px;">Generar Nota <i class="fa fa-sort"></i></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-show="carteras.length > 0" ng-repeat="cartera in carteras">
                                            <td><strong>{{cartera.nombre_concepto}}</strong></td>
                                            <td class="text-right">{{cartera.periodo}}</td>
                                            <td class="text-right">{{cartera.vigencia}}</td>

                                            <td class="text-right"><input type="text" ng-disabled="(cartera.causado == 'SI' && cartera.generar_nota == 'NO')" ng-model="cartera.valor" id="cartera.id" ng-blur="_verificarValor(cartera)" class="form-control text-right transparente" format="number" /></td>
                                            <td class="text-right">{{cartera.pagado | currency:"$":0}}</td>
                                            <td>{{cartera.causado}}</td>
                                            <td class="text-center"><input type="checkbox" ng-change="_restaurarValor(cartera)" ng-disabled="cartera.causado == 'NO'" ng-model="cartera.generar_nota" ng-true-value="'SI'" ng-false-value="'NO'"/></td>
                                        </tr>
                                        <tr ng-show="carteras.length > 0">
                                            <td class="text-right" colspan="3"><strong> Total = </strong></td>
                                            <td class="text-right">{{carteras |sumByKey:'valor' | currency:"$":0}}</td>
                                            <td class="text-right">{{carteras |sumByKey:'pagado' | currency:"$":0}}</td>
                                            <td class="text-right" colspan="2"></td>
                                        </tr>
                                        <tr ng-show="carteras.length == 0">
                                            <td colspan="7">No se han encontrado registros</td>
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
