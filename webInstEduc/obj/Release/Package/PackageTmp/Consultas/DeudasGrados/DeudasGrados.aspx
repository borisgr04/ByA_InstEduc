<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="DeudasGrados.aspx.cs" Inherits="AspIdentity.Consultas.DeudasGrados.DeudasGrados" %>
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
    <script src="js/cDeudasGrados.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cDeudasGrados" ng-cloak>
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
                <div class="panel-heading">Deudas grados</div>
                <div class="panel-body">
                    <div class="row" style="margin:7px">
                        <div class="row">
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:70%">
                                <thead>
                                    <tr>                                        
                                        <th>Grado <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Deuda <i class="fa fa-sort"></i></th>    
                                        <th style="text-align:left"> <i class="fa fa-sort"></i></th>  
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="deudas_grados.length > 0" ng-repeat="deuda_grado in deudas_grados">                                        
                                        <td>{{deuda_grado.nombre_grado}}</td>
                                        <td style="text-align:right">{{deuda_grado.valor_deuda| currency:"$":0}}</td>
                                        <td style="text-align:left"><a href="javascript:void(0)" ng-click="_traerDeudasCursosGrado(deuda_grado)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>                             
                                    </tr>
                                    <tr ng-show="deudas_grados.length > 0">
                                        <td style="text-align:right">Total</td>
                                        <td style="text-align:right"><strong>{{deudas_grados|sumByKey:'valor_deuda' | currency:"$":0}}</strong></td>
                                        <th style="text-align:left"> <i class="fa fa-sort"></i></th> 
                                    </tr>      
                                    <tr ng-show="deudas_grados == null || deudas_grados.length == 0">
                                        <td colspan="3">No se han encontrado registros</td>
                                    </tr>                       
                                </tbody>
                            </table>
                        </div>
                        <div class="row" ng-show="deudas_cursos_grado.length > 0">
                            <hr />
                            <h4>Deudas de los cursos de grado: {{deuda_grado.nombre_grado}}</h4>
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:70%">
                                <thead>
                                    <tr>                                        
                                        <th>Curso <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Deuda <i class="fa fa-sort"></i></th>    
                                        <th style="text-align:left"> <i class="fa fa-sort"></i></th>  
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="deudas_cursos_grado.length > 0" ng-repeat="deuda_curso_grado in deudas_cursos_grado">                                        
                                        <td>{{deuda_curso_grado.nombre_curso}}</td>
                                        <td style="text-align:right">{{deuda_curso_grado.valor_deuda| currency:"$":0}}</td>
                                        <td style="text-align:left"><a href="javascript:void(0)" ng-click="_traerDeudasEstudiantesCursoGrado(deuda_curso_grado)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>                             
                                    </tr>
                                    <tr ng-show="deudas_cursos_grado.length > 0">
                                        <td style="text-align:right">Total</td>
                                        <td style="text-align:right"><strong>{{deudas_cursos_grado|sumByKey:'valor_deuda' | currency:"$":0}}</strong></td>
                                        <th style="text-align:left"> <i class="fa fa-sort"></i></th> 
                                    </tr>      
                                    <tr ng-show="deudas_cursos_grado == null || deudas_cursos_grado.length == 0">
                                        <td colspan="3">No se han encontrado registros</td>
                                    </tr>                       
                                </tbody>
                            </table>
                        </div>
                        <div class="row" ng-show="deudas_estudiante_curso_grado.length > 0">
                            <hr />
                            <h4>Deudas del curso '{{deuda_curso_grado.nombre_curso}}' del grado: {{deuda_grado.nombre_grado}}</h4>
                            <table class="table table-bordered table-hover table-striped tablesorter" style="width:70%">
                                <thead>
                                    <tr>                                        
                                        <th>Estudiante <i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Deuda <i class="fa fa-sort"></i></th>   
                                    </tr>
                                </thead>
                                <tbody> 
                                    <tr ng-show="deudas_estudiante_curso_grado.length > 0" ng-repeat="deuda_estudiante_curso_grado in deudas_estudiante_curso_grado">                                        
                                        <td>{{deuda_estudiante_curso_grado.nombre_estudiante}}</td>
                                        <td style="text-align:right">{{deuda_estudiante_curso_grado.valor_deuda| currency:"$":0}}</td>                            
                                    </tr>
                                    <tr ng-show="deudas_estudiante_curso_grado.length > 0">
                                        <td style="text-align:right">Total</td>
                                        <td style="text-align:right"><strong>{{deudas_estudiante_curso_grado |sumByKey:'valor_deuda' | currency:"$":0}}</strong></td>
                                    </tr>      
                                    <tr ng-show="deudas_estudiante_curso_grado == null || deudas_estudiante_curso_grado.length == 0">
                                        <td colspan="2">No se han encontrado registros</td>
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
