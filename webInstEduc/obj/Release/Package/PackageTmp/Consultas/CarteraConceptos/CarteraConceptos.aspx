<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="CarteraConceptos.aspx.cs" Inherits="AspIdentity.Consultas.CarteraConceptos.CarteraConceptos" %>
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
    <script src="js/cCarteraConceptos.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cCarteraConceptos" ng-cloak>
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
            <%--<div class="panel panel-default">
                <div class="panel-heading">Cartera Conceptos</div>
                <div class="panel-body">--%>

                    <div class="row" style="margin-bottom:15px;">
                        <div class="col-xs-4">
                            <label>Filtrar:</label>   
                            <div class="input-group">
                                <input type="text" class="form-control" ng-model="filtro"/>
                                <div class="input-group-btn ">
                                    <button ng-click="_filtrarCarteras()" type="button" class="btn btn-info no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-search"></span></button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <table class="table table-bordered table-hover table-striped tablesorter">
                                <thead>
                                    <tr>                                        
                                        <th rowspan="2" class="text-left">Id. <i class="fa fa-sort"></i></th>
                                        <th rowspan="2" class="text-left">Nombre <i class="fa fa-sort"></i></th>   
                                        <th rowspan="2" class="text-center">Cart. Vencida <i class="fa fa-sort"></i></th>   
                                        <th class="text-center" colspan="2">Matrícula <i class="fa fa-sort"></i></th>  
                                        <th class="text-center" colspan="2">Seguro <i class="fa fa-sort"></i></th>  
                                        <th class="text-center" colspan="2">Sistematización <i class="fa fa-sort"></i></th>  
                                        <th class="text-center" colspan="2">Proy. Pedagogicos <i class="fa fa-sort"></i></th>  
                                        <th class="text-center" colspan="2">Pensión <i class="fa fa-sort"></i></th> 
                                        <th rowspan="2" class="text-center">Total <i class="fa fa-sort"></i></th>  
                                    </tr>
                                    <tr>                                           
                                        <th class="text-center">Periodos<i class="fa fa-sort"></i></th>   
                                        <th class="text-center">Deuda <i class="fa fa-sort"></i></th>  
                                        <th class="text-center">Periodos<i class="fa fa-sort"></i></th>   
                                        <th class="text-center">Deuda <i class="fa fa-sort"></i></th>  
                                        <th class="text-center">Periodos<i class="fa fa-sort"></i></th>   
                                        <th class="text-center">Deuda <i class="fa fa-sort"></i></th>  
                                        <th class="text-center">Periodos<i class="fa fa-sort"></i></th>   
                                        <th class="text-center">Deuda <i class="fa fa-sort"></i></th>  
                                        <th class="text-center">Periodos<i class="fa fa-sort"></i></th>   
                                        <th class="text-center">Deuda <i class="fa fa-sort"></i></th>  
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="carteras.length > 0" ng-repeat="cartera in carteras">                                        
                                        <td>{{cartera.id_estudiante}}</td>
                                        <td>{{cartera.nombre_estudiante}}</td>
                                        <td class="text-right">{{cartera.valor_carteravencida | currency:"$":0}}</td>
                                        <td class="text-center">{{cartera.per_matricula}}</td>
                                        <td class="text-right">{{cartera.valor_matricula | currency:"$":0}}</td>
                                        <td class="text-center">{{cartera.per_seguro}}</td>
                                        <td class="text-right">{{cartera.valor_seguro | currency:"$":0}}</td>
                                        <td class="text-center">{{cartera.per_sistematizacion}}</td>
                                        <td class="text-right">{{cartera.valor_sistematizacion | currency:"$":0}}</td>
                                        <td class="text-center">{{cartera.per_propeda}}</td>
                                        <td class="text-right">{{cartera.valor_propeda | currency:"$":0}}</td>
                                        <td class="text-center">{{cartera.per_pension}}</td>
                                        <td class="text-right">{{cartera.valor_pension | currency:"$":0}}</td>
                                        <td class="text-right">{{cartera.total_deuda | currency:"$":0}}</td>
                                    </tr>                      
                                </tbody>
                            </table>
                    </div>
                </div>
            </div>
        <%--</div>
    </div>--%>
</asp:Content>
