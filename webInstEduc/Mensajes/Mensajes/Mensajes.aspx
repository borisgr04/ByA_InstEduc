﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Mensajes.aspx.cs" Inherits="AspIdentity.DatosBasicos.Mensajes.Mensajes.Mensajes" %>
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
    <script src="js/cMensajes.js"></script>
    <script src="../../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../../Scripts/jquery.signalR-2.0.0.js"></script>
    <script src="../../signalr/hubs"></script>
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
                    <div class="panel-heading">Envío de Mensajes a Acudientes</div>
                    <div class="panel-body">
            <div class="container">
                <div class="row form-group">
                    <div class="col-xs-4">
                        <label>Filtrar:</label>   
                        <div class="input-group">
                            <input type="text" class="form-control" ng-model="filtro"/>
                            <div class="input-group-btn ">
                                <button type="button" class="btn btn-info no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-search"></span></button>
                            </div>
                        </div>
                    </div> 
                    <div class="col-xs-2 form-group">
                        <label class="control-label">Grado:</label>
                        <select class="form-control text-center" style="text-align:center" ng-model="gradoSeleccionado" ng-change="cargarCurso()" ng-options="grado as grado.nombre for grado in grados track by grado.id" >
                        </select>
                    </div>
                    <div class="col-xs-2 form-group">
                        <label class="control-label">Curso:</label>
                        <select class="form-control text-center" style="text-align:center" ng-model="cursoSeleccionado" ng-options="curso as curso.nombre for curso in ListCursos track by curso.id" required>
                        </select>
                    </div>
                    <div class="col-xs-2 form-group">
                        <label class="control-label">Tipo de Mensajes:</label>
                        <select class="form-control text-center" style="text-align:center" ng-model="tipoAlerta" ng-options="tipo as tipo.nombre for tipo in tipoAcudiente track by tipo.id">
                        </select>
                    </div>
                    <div class="col-xs-2 form-group">
                        <label class="control-label"></label>
                        <button class="btn btn-info form-control" ng-click="filtrar()">
                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                            Filtrar
                        </button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2">
                        <label style="font-size: 14px;"><input type="checkbox" ng-model="check_todos_estudiantes" ng-click="checkear_todosEstudiantes()"/>     Seleccionar Todos</label>
                    </div>

                    <div class="col-xs-2 form-group">
                        <button class="btn btn-info form-control" ng-click="abrirModal()" ng-disabled="estudianteDto.length == 0">
                            <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                            Redactar Mensaje
                        </button>
                    </div>

                    <div class="col-xs-3"></div>
                    <div class="col-xs-3">
                        <h4 class="text-right">
                            <strong>Resultado: </strong> {{listaEstudiantes.length}} Estudiantes
                        </h4>
                    </div>
                </div>
                <div class="row">
                    <table class="table table-bordered table-hover table-striped tablesorter">
                        <thead>
                            <th></th>
                            <th>Identificación</th>
                            <th>Estudiante</th>
                            <th>Acudiente</th>
                            <th>Grado</th>
                            <th>Curso</th>
                            <th>Saldo</th>
                        </thead>
                        <tbody>
                            <tr ng-repeat="estudiante in listaEstudiantes | filter:filtro"">
                                <td style="text-align:center;"><label><input type="checkbox" ng-model="estudiante.activo" ng-change="checkear_estudiante(estudiante, $index)"></input></label></td>
                                <td style="text-align: right;" filter="{ 'identificacion': 'text' }" sortable="'identificacion'"><strong>{{estudiante.identificacion | currency :"":0}}</strong></td>
                                <td filter="{ 'nombre_completo': 'text' }" sortable="'nombre_completo'">{{estudiante.nombre_completo}}</td>
                                <td >{{estudiante.nombre_completo_acudiente}}</td>
                                <td filter="{ 'nombre_grado': 'text' }" sortable="'nombre_grado'">{{estudiante.nombre_grado}}</td>
                                <td filter="{ 'nombre_curso': 'text' }" sortable="'nombre_curso'">{{estudiante.nombre_curso}}</td>
                                <td style="text-align: right; font-weight: bold; color: {{ColorSaldo(estudiante.saldo)}}" filter="{ 'saldo': 'text' }" sortable="'saldo'">{{estudiante.saldo | currency:"$":0}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
            <!--Modal Para el envio de mensajes-->
        <div class="modal fade bs-example-modal-lg" id="modalMensaje" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
            <div class="modal-dialog" role="document" style="width: 800px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title text-center" id="myLargeModalLabel">Envíar Mensaje</h4>
                    </div>                
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xs-6">
                                <label>Asunto</label>
                                <input type="text" class="form-control" ng-model="asunto.nombre" disabled="disabled"/>
                            </div>         
                            <div class="col-xs-6">
                                <label>Tipo mensaje</label>
                                <select class="form-control" ng-model="tipoMsje" disabled="disabled" ng-options="tipo as tipo.nombre for tipo in tipoMensaje track by tipo.id"></select>
                            </div>                            
                        </div>
                        <div class="row">
                            <div class="col-xs-2">
                                <label>Mensaje</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <textarea class="form-control" disabled="disabled" rows="4">{{ContenidoMensaje}}</textarea>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" ng-click="registrarMensajes()">Enviar mensajes</button>
                    </div>
                </div>
            </div>
        </div>
</div>
    </div>
</asp:Content>
