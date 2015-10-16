<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Estudiantes.aspx.cs" Inherits="AspIdentity.Estudiantes.Estudiantes.Estudiantes" %>
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
    <script src="js/cEstudiantes.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cEstudiantes" ng-cloak>
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
                    <div class="panel-heading">Matrículas Estudiantes</div>
                    <div class="panel-body">
            <div class="container">
                <div class="row form-group">
                    <div class="col-xs-2">
                        <button class="btn btn-info btn-sm" ng-click="_nuevo()"><span class="glyphicon glyphicon-plus" style="margin-top:50px;"></span> Nuevo</button>
                    </div>
                    <div class="col-xs-4">
                    </div>
                    <div class="col-xs-1">
                        <strong>Buscar: </strong>
                    </div>
                    <div class="col-xs-4">                        
                        <input type="text" class="form-control" ng-model="filtro" /> 
                    </div> 
                    <div class="col-xs-1">
                         <button type="button" class="btn btn-info no-border btn-sm" data-toggle="dropdown" ng-click="_traerEstudiantes()"><span class="glyphicon glyphicon-search"></span></button>
                    </div>                   
                </div>
                <div class="row text-right">
                    <h4>
                        <strong>Resultado: </strong> {{estudiantes.length}} Estudiantes
                    </h4>
                </div>
                <div class="row">
                    <table ng-table="tableEstudiantes" template-pagination="/data-table-pager.html"  class="table table-bordered table-hover table-striped tablesorter">
                        <tr ng-repeat="estudiante in $data">
                                <td class="text-right" header-class="'text-right'" data-title="'Código'" filter="{ 'codigo': 'text' }" sortable="'codigo'"><strong>{{estudiante.codigo}}</strong></td>
                                <td class="text-right" header-class="'text-right'" data-title="'Identificación'" filter="{ 'identificacion': 'text' }" sortable="'identificacion'"><strong>{{estudiante.identificacion | currency :"":0}}</strong></td>
                                <td data-title="'Nombre'" filter="{ 'nombre_completo': 'text' }" sortable="'nombre_completo'">{{estudiante.nombre_completo}}</td>
                                <td data-title="'Grado'" filter="{ 'nombre_grado': 'text' }" sortable="'nombre_grado'">{{estudiante.nombre_grado}}</td>
                                <td data-title="'Curso'" filter="{ 'nombre_curso': 'text' }" sortable="'nombre_curso'">{{estudiante.nombre_curso}}</td>
                                <td style="text-align:left"><a href="javascript:void(0)" ng-click="_detallesEstudiantes(estudiante)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>
                        </tr>
                        <tr ng-show="estudiantes.length == 0">
                            <td colspan="6"><strong>No se ha registrado ningún estudiante</strong></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
        </div>
</asp:Content>
