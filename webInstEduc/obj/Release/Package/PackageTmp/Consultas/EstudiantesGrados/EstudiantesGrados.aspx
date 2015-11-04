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
                            <select class="form-control"></select>
                        </div>
                        <div class="col-xs-2">
                            <label>Curso</label>
                            <select class="form-control"></select>
                        </div>
                        <div class="col-xs-2">
                            <button style="margin-top:23px" type="button" class="btn btn-success btn-sm" ng-click="Consultar()"><span class="glyphicon glyphicon-search"></span>Buscar</button>
                        </div>
                    </form>
                </div>
                <div class="row">
                    <hr />
                </div>
            </div>
        </div>
    </div>
            </div>
        </div>
</asp:Content>
