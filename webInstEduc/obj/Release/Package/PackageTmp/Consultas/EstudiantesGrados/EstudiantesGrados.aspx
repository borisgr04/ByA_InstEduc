<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="EstudiantesGrados.aspx.cs" Inherits="AspIdentity.Consultas.EstudiantesGrados.EstudiantesGrados" %>
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
    <script src="js/cEstudiantesGrados.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cEstudiantesGrados" ng-cloak>
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
                <div class="panel-heading">Estudiante</div>
                <div class="panel-body">
                    <div class="row" style="margin:7px">
                        <div class="row">
                            <form name="datosConsulta">
                                <div class="col-xs-2">
                                    <label>Grado</label>
                                    <select ng-model="grado" ng-change="_buscarCurso()" ng-options="grado as grado.nombre for grado in grados" class="form-control" required></select>
                                </div>
                                <div class="col-xs-2">
                                    <label>Curso</label>
                                    <select ng-model="curso" ng-options="curso as curso.nombre for curso in cursos" class="form-control"></select>
                                </div>
                                <div class="col-xs-4">
                                    <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="_traerEstudiantes()"><span class="glyphicon glyphicon-search"></span>Buscar</button>
                                    <button style="margin-top:23px" type="button" class="btn btn-info btn-sm" ng-click="_imprimirListado()"><span class="glyphicon glyphicon-print"></span>Imprimir</button>
                                </div>
                            </form>
                        </div>
                        <div class="row">
                            <hr />
                        </div>
                        <div class="row" id="printListado">
                            <table class="table table-bordered table-hover table-striped tablesorter" id="tblEstudiantesGrados">
                                <thead>
                                    <tr>
                                        <td colspan="8" style="text-align:center">
                                            <strong>Colegio Calleja Real</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align:center">
                                            <strong>Grado: </strong> {{contactos_grados.nombre_grado}}
                                        </td>
                                        <td colspan="2" style="text-align:center">
                                            <strong>Curso: </strong> {{contactos_grados.nombre_curso}}
                                        </td>
                                        <td colspan="3" style="text-align:center">
                                            <strong>Vigencia: </strong> {{contactos_grados.vigencia}}
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="text-align:right">Identificación<i class="fa fa-sort"></i></th>
                                        <th style="text-align:right">Código<i class="fa fa-sort"></i></th>
                                        <th>Nombre<i class="fa fa-sort"></i></th>
                                        <th>Acudiente<i class="fa fa-sort"></i></th>
                                        <th>Dirección<i class="fa fa-sort"></i></th>
                                        <th>Teléfono<i class="fa fa-sort"></i></th>
                                        <th>Celular<i class="fa fa-sort"></i></th>
                                        <th>Correo<i class="fa fa-sort"></i></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr ng-repeat="estudiante in contactos_grados.l_estudiantes">
                                        <td style="text-align:right">{{estudiante.id_estudiante}}</td>
                                        <td style="text-align:right">{{estudiante.codigo_estudiante}}</td>
                                        <td>{{estudiante.nombre_estudiante}}</td>
                                        <td>{{estudiante.nombre_acudiente}}</td>
                                        <td>{{estudiante.direccion_acudiente}}</td>
                                        <td>{{estudiante.telefono_acudiente}}</td>
                                        <td>{{estudiante.celular_acudiente}}</td>
                                        <td>{{estudiante.correo_acudiente}}</td>
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
