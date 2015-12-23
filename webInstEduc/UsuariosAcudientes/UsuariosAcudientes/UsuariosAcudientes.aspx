<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="UsuariosAcudientes.aspx.cs" Inherits="AspIdentity.UsuariosAcudientes.UsuariosAcudientes.UsuariosAcudientes" %>
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
    <script src="js/cUsuariosAcudientes.js"></script>

    <div class="container" ng-app="Model">
        <div ng-controller="cUsuariosAcudientes" ng-cloak>
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
                    <div class="panel-heading">Acudientes sin Usuarios</div>
                    <div class="panel-body">
            <div class="container">                
                <div class="row" style="margin-bottom:15px;">
                    <%--<div class="col-xs-6 text-left">
                        <button ng-disabled="editCartera" class="btn btn-info" ng-click="_print()"><span class="glyphicon glyphicon-print"></span> Imprimir</button>
                    </div>--%>
                    <%--<div class="col-xs-6 text-right">
                        <h4><strong>Resultado: </strong> {{matriculas.length}} Matriculas</h4>
                    </div> --%>                   
                    <div class="col-xs-4">
                        <label>Filtrar:</label>   
                        <div class="input-group">
                            <input type="text" class="form-control" ng-model="filtro"/>
                            <div class="input-group-btn ">
                                <button type="button" class="btn btn-info no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-search"></span></button>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <button style="margin-top:21px" class="btn btn-info btn" ng-click="_crearUsuariosAcudientes()"><span class="glyphicon glyphicon-user"></span> Crear Usuarios Acudientes</button>
                    </div>,
                    <div class="col-xs-2">
                        <button ng-click="_traerAcudientes()" style="margin-top:21px" class="btn btn-info btn"><span class="glyphicon glyphicon-refresh"></span> Refrescar</button>
                    </div>
                </div>
                <div class="row text-right">
                    <h4>
                        <strong>Resultado: </strong> {{UsuariosAcudientes.length}} Acudientes sin Usuario
                    </h4>
                </div>
                <div class="row" id="print">
                    <table ng-table="tableUsuariosAcudientes" template-pagination="/data-table-pager.html"  class="table table-bordered table-hover table-striped tablesorter">
                        <tr ng-repeat="item in $data | filter:filtro">
                            <td class="text-right" header-class="'text-right'" data-title="'Identificación'" filter="{ 'identificacion': 'text' }" sortable="'identificacion'"><strong>{{item.identificacion}}</strong></td>
                            <td class="text-right" header-class="'text-right'" data-title="'Nombre'" filter="{ 'nombre': 'text' }" sortable="'nombre'"><strong>{{item.apellido}} {{item.nombre}}</strong></td>
                            <td class="text-right" header-class="'text-right'" data-title="'Email'" filter="{ 'email': 'text' }" sortable="'email'"><strong>{{item.email}}</strong></td>
                            <td class="text-left" header-class="'text-left'" data-title="'Dirección'" filter="{ 'direccion': 'text' }" sortable="'direccion'"><strong>{{item.direccion}}</strong></td>
                            <td class="text-left" header-class="'text-left'" data-title="'Teléfono'" filter="{ 'telefono': 'text' }" sortable="'telefono'"><strong>{{item.telefono}}</strong></td>
                            <td class="text-left" header-class="'text-left'" data-title="'Célular'" filter="{ 'celular': 'text' }" sortable="'celular'"><strong>{{item.celular}}</strong></td>
                            <td style="text-align:left"><a href="javascript:;" ng-click="_removerAcudiente(item, $index)"><span class="glyphicon glyphicon-remove" style="color: #d15b47!important;"></span></a></td>
                        </tr>
                        <tr ng-show="UsuariosAcudientes.length == 0">
                            <td colspan="7"><strong>No se hay acudientes sin usuarios</strong></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </div>
    </div>
</asp:Content>
