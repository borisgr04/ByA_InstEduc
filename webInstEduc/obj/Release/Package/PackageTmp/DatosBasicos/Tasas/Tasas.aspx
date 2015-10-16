<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Tasas.aspx.cs" Inherits="AspIdentity.DatosBasicos.Tasas.Tasas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/jqscripts/cssAngularPropios.css" rel="stylesheet" />
    <script src="/jqscripts/qrcode.js"></script>
    <script src="/Angular/angular.min.js"></script>
    <script src="/Angular/angular-ui/ui-bootstrap-tpls.min.js"></script>
    <script src="/Angular/angular-route.js"></script>
    <script src="/Angular/angular-mocks.js"></script>
    <script src="/mAngular/angularModelo.js"></script>
    <script src="/mAngular/angularService.js"></script>
    <script src="/Angular/ng-table.js"></script>
    <script src="../Tasas/js/cTasas.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cTasas" ng-cloak>
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
                    <button class="btn btn-info btn-sm" ng-click="_nuevo()"><span class="glyphicon glyphicon-plus"></span> Nuevo</button>
                    <button class="btn btn-success btn-sm" ng-click="_guardar()"><span class="glyphicon glyphicon-ok"></span>Guardar</button>       
                </div>
                <div class="row" style="margin:10px">
                    <div class="col-xs-10">                        
                        <form name="datosTasas">
                        <table class="table table-bordered table-hover table-striped tablesorter">
                            <thead>
                                <tr>
                                    <th>Id. <i class="fa fa-sort"></i></th>
                                    <th>Fecha Inicio <i class="fa fa-sort"></i></th>
                                    <th>Fecha Fin <i class="fa fa-sort"></i></th>
                                    <th>Tasa <i class="fa fa-sort"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="tasa in tasas">
                                    <td>
                                        <strong>{{tasa.id}}</strong>
                                    </td>
                                    <td>
                                        <input style="text-transform:uppercase;" type="date" class="form-control transparente" ng-model="tasa.fecha_inicio" required />                                        
                                    </td>
                                    <td>
                                        <input style="text-transform:uppercase;" type="date" class="form-control transparente" ng-model="tasa.fecha_fin" required />                                        
                                    </td>
                                    <td>
                                        <input style="text-transform:uppercase;" type="text" class="form-control transparente" ng-model="tasa.tasa" required />                                        
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
</asp:Content>
