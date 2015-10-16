<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Tarifas.aspx.cs" Inherits="AspIdentity.DatosBasicos.Tarifas.Tarifas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/jqscripts/cssAngularPropios.css" rel="stylesheet" />
    <!--<script src="/jqscripts/qrcode.js"></script>-->
    <script src="../../jqscripts/qrcode.js"></script>
    <script src="/Angular/angular.min.js"></script>
    <script src="/Angular/angular-ui/ui-bootstrap-tpls.min.js"></script>
    <script src="/Angular/angular-route.js"></script>
    <script src="/Angular/angular-mocks.js"></script>
    <script src="/mAngular/angularModelo.js"></script>
    <script src="/mAngular/angularService.js"></script>
    <script src="../Tarifas/js/cTarifas.js"></script>
    <script src="/Angular/ng-table.js"></script>
    <script src="/Angular/ng-table.css"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cTarifas" ng-cloak>
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
            <div class="form-horizontal">
                <div class="row">                                                
                    <button class="btn btn-info btn-sm" ng-click="_nuevo()"><span class="glyphicon glyphicon-plus"></span> Nuevo</button>
                    <button class="btn btn-success btn-sm" ng-click="_guardar()"><span class="glyphicon glyphicon-ok"></span>Guardar</button>       
                </div>
                <div class="row" style="margin:10px">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Grados</label>
                        <div class="col-sm-10">
                            <select ng-model="grado" ng-options="grado as grado.nombre for grado in grados" ng-change="_cargarTarifas()"></select>
                        </div>
                    </div>
                    <div class="form-group">
                                            <div class="col-xs-10">                        
                        <form name="datosTarifas">
                        <table class="table table-bordered table-hover table-striped tablesorter">
                            <thead>
                                <tr>
                                    <th>Id. <i class="fa fa-sort"></i></th>
                                    <!--th>Vigencia<i class="fa fa-sort"></i></th>
                                    <th>Grado<i class="fa fa-sort"></i></th-->
                                    <th>Concepto<i class="fa fa-sort"></i></th>
                                    <th>Valor<i class="fa fa-sort"></i></th>
                                    <th>Periodo Desde<i class="fa fa-sort"></i></th>
                                    <th>Periodo hasta<i class="fa fa-sort"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="tarifa in tarifas">
                                    <td>
                                        <strong>{{tarifa.id}}</strong>
                                    </td>
                                    <!--td>
                                        {{vigencia}}                              
                                    </td>
                                    <td>
                                        {{grado.nombre}}                                       
                                    </td-->
                                    <td>
                                        <select ng-model="tarifa.id_concepto" ng-options="concepto.id as concepto.nombre for concepto in conceptos" required></select>                                      
                                    </td>
                                    <td>
                                        <input type="number" step="0.01" min="0" class="form-control transparente" ng-model="tarifa.valor" required />                                        
                                    </td>
                                    <td>
                                        
                                        <select class="form-control transparente" ng-model="tarifa.periodo_desde" ng-options="periodo.periodo as periodo.periodo for periodo in periodos" required></select>                                  
                                    </td>
                                    <td>
                                        <select class="form-control transparente" ng-model="tarifa.periodo_hasta" ng-options="periodo.periodo as periodo.periodo for periodo in periodos" required></select>                             
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </form>
                    </div>  
                    </div>
                 
                </div>
            </div>
        </div>
    </div>
</asp:Content>
