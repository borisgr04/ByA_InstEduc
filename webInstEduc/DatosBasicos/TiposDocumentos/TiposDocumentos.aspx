﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="TiposDocumentos.aspx.cs" Inherits="AspIdentity.DatosBasicos.TiposDocumentos.TiposDocumentos" %>
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
    <script src="../TiposDocumentos/js/cTiposDocumentos.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cTiposDocumentos" ng-cloak>
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
                        <form name="datosTipoDocumentos">
                        <table class="table table-bordered table-hover table-striped tablesorter">
                            <thead>
                                <tr>
                                    <th>Id. <i class="fa fa-sort"></i></th>
                                    <th>Nombre <i class="fa fa-sort"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr ng-repeat="tipoDoc in tipoDocumentos">
                                    <td>
                                        <strong><input style="text-transform:uppercase;" type="text" class="form-control transparente" ng-model="tipoDoc.id" required /></strong>
                                    </td>
                                    <td>
                                        <input style="text-transform:uppercase;" type="text" class="form-control transparente" ng-model="tipoDoc.nombre" required />                                        
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
