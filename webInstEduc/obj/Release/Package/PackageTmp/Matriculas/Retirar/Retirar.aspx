<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Retirar.aspx.cs" Inherits="AspIdentity.Matriculas.Retirar.Retirar" %>
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
    <script src="js/cRetirar.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cRetirar" ng-cloak>
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
                    <div class="panel-heading">Retirar Estudiante</div>
                    <div class="panel-body">
                        <div class="row">
                            <form name="datosRetiro">                                
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
                                <div class="col-xs-2 text-left">
                                    <button ng-click="_RetirarEstudiante()" ng-disabled="!habGuardar" type="button" style="margin-top:23px" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-remove"></span>Retirar estudiante</button>
                                </div>
                            </form>
                        </div>
                        <div class="row" style="margin-left:30px; margin-right:30px; margin-top:20px;" ng-show="lDeuda.length > 0">
                            <div class="row">
                                <h4>Deuda actual estudiante</h4>
                            </div>
                            <div class="row">
                                <table class="table table-bordered table-hover table-striped tablesorter" id="Table2">
                                    <thead>
                                        <tr>
                                            <th>Concepto <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Periodo <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Vigencia <i class="fa fa-sort"></i></th>
                                            <th style="text-align:right">Valor <i class="fa fa-sort"></i></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-show="lDeuda.length > 0" ng-repeat="cartera in lDeuda">
                                            <td><strong>{{cartera.nombre_concepto}}</strong></td>
                                            <td class="text-right">{{cartera.periodo}}</td>
                                            <td class="text-right">{{cartera.vigencia}}</td>
                                            <td class="text-right">{{cartera.valor | currency:"$":0}}</td>
                                        </tr>
                                        <tr ng-show="lDeuda.length > 0">
                                            <td class="text-right" colspan="3"><strong> Total = </strong></td>
                                            <td class="text-right">{{lDeuda|sumByKey:'valor'| currency:"$":0}}</td>
                                        </tr>
                                        <tr ng-show="lDeuda.length == 0">
                                            <td colspan="4">No se han encontrado registros</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row" ng-show="lAdelantos.length > 0" style="margin-left:30px; margin-right:30px; margin-top:10px;">
                            <div class="row">
                                <h4>Adelantos estudiante</h4>
                            </div>
                            <div class="row">
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
                                        <tr ng-show="lAdelantos.length > 0" ng-repeat="cartera in lAdelantos">
                                            <td><strong>{{cartera.nombre_concepto}}</strong></td>
                                            <td class="text-right">{{cartera.periodo}}</td>
                                            <td class="text-right">{{cartera.vigencia}}</td>
                                            <td class="text-right">{{cartera.valor | currency:"$":0}}</td>
                                        </tr>
                                        <tr ng-show="lAdelantos.length > 0">
                                            <td class="text-right" colspan="3"><strong> Total = </strong></td>
                                            <td class="text-right">{{lAdelantos|sumByKey:'valor'| currency:"$":0}}</td>
                                        </tr>
                                        <tr ng-show="lAdelantos.length == 0">
                                            <td colspan="4">No se han encontrado registros</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row" ng-show="lAdelantos.length == 0 && lDeuda.length == 0" style="margin-left:30px; margin-right:30px; margin-top:10px;">
                            <h4>Actualmente el estudiante se encuentra a paz y salvo</h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
