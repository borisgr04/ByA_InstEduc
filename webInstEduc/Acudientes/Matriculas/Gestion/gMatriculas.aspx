<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="gMatriculas.aspx.cs" Inherits="AspIdentity.Acudientes.Matriculas.Gestion.gMatriculas" %>
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
    <script src="js/cgMatriculas.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cgMatriculas" ng-cloak>
            <div class="loading-spiner-holder" data-loading>
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
                <div class="row">
                    <div class="col-xs-6 text-left">
                        <button ng-disabled="editCartera" class="btn btn-info" ng-click="_print()"><span class="glyphicon glyphicon-print"></span> Imprimir</button>
                    </div>
                    <div class="col-xs-6 text-right">
                        <h4><strong>Resultado: </strong> {{matriculas.length}} Matriculas</h4>
                    </div>                    
                </div>
                <div class="row" id="print">
                    <table ng-table="tableMatriculas" template-pagination="/data-table-pager.html"  class="table table-bordered table-hover table-striped tablesorter">
                        <tr ng-repeat="item in $data">
                            <td class="text-right" header-class="'text-right'" data-title="'Id. Matricula'" filter="{ 'id_matricula': 'text' }" sortable="'id_matricula'"><strong>{{item.id_matricula}}</strong></td>
                            <td class="text-right" header-class="'text-right'" data-title="'Código Estudiante'" filter="{ 'codigo_estudiante': 'text' }" sortable="'codigo_estudiante'"><strong>{{item.codigo_estudiante}}</strong></td>
                            <td class="text-right" header-class="'text-right'" data-title="'Id. Estudiante'" filter="{ 'id_estudiante': 'text' }" sortable="'id_estudiante'"><strong>{{item.id_estudiante}}</strong></td>
                            <td class="text-left" header-class="'text-left'" data-title="'Estudiante'" filter="{ 'nombre_estudiante': 'text' }" sortable="'nombre_estudiante'"><strong>{{item.nombre_estudiante}}</strong></td>
                            <td class="text-left" header-class="'text-left'" data-title="'Grado'" filter="{ 'nombre_grado': 'text' }" sortable="'nombre_grado'"><strong>{{item.nombre_grado}}</strong></td>
                            <td class="text-left" header-class="'text-left'" data-title="'Curso'" filter="{ 'nombre_curso': 'text' }" sortable="'nombre_curso'"><strong>{{item.nombre_curso}}</strong></td>
                            <td style="text-align:left"><a href="javascript:void(0)" ng-click="_detallesMatricula(item)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>
                        </tr>
                        <tr ng-show="matriculas.length == 0">
                            <td colspan="7"><strong>No se ha registrado ningúna matricula</strong></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </div>
    </div>
</asp:Content>
