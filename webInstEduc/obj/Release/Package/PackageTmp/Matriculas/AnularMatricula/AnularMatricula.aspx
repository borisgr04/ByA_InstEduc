<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="AnularMatricula.aspx.cs" Inherits="AspIdentity.Matriculas.AnularMatricula.AnularMatricula" %>
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
    <script src="js/cAnularMatricula.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cAnularMatricular" ng-cloak>
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
            <div class="row" id="secInfo"></div>
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">Anulación Matrícula</div>
                    <div class="panel-body">                        
                        <div class="row">
                            <form name="datosMatricula">                                
                                <div class="col-xs-2">
                                    <label>Id. Estudiante:</label>   
                                    <div class="input-group">
                                        <input type="text" class="form-control" ng-blur="_traerestudiante()" ng-model="estudiante.identificacion" required format="number"/>
                                        <div class="input-group-btn ">
                                            <button ng-click="_traerestudiante()" type="button" class="btn btn-info no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-search"></span></button>
                                        </div>
                                    </div> 
                                </div>
                                <div class="col-xs-3">
                                    <label>Nombre:</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" ng-model="estudiante.nombre_completo" disabled="disabled"/>
                                        <div class="input-group-btn ">
                                            <button ng-click="_limpiarEstudiante()" type="button" class="btn btn-danger no-border btn-sm" data-toggle="dropdown"><span class="glyphicon glyphicon-remove"></span></button>                  
                                        </div>
                                    </div>                                     
                                </div>
                                <div class="col-xs-5">
                                    <button style="margin-top:23px" class="btn btn-info btn-xs" ng-click="_traerMatriculaEstudiante()"><span class="glyphicon glyphicon-search"></span> Buscar Matricula</button>
                                    <button style="margin-top:23px" class="btn btn-danger btn-xs" ng-disabled="!habGuardar" ng-click="_AnularMatricula()"><span class="glyphicon glyphicon-remove"></span> Anular Matrícula</button>
                                </div>
                            </form>
                        </div>
                        <div class="row" ng-show="verMatricula" style="margin-left:20px; margin-top:10px;">
                            <div class="row" style="margin-left:0px">
                                <div class="col-xs-6 text-left">
                                    <h4 class="text-left"><strong>Datos de la matrícula</strong></h4>
                                </div>                            
                            </div>
                            <div class="row">
                                <div class="col-xs-3">
                                    <h5><strong>Identificacion: </strong></h5>
                                    <h5>{{matricula.id_estudiante | currency:"":0}}</h5>                              
                                </div>
                                <div class="col-xs-3">
                                    <h5><strong>Nombre: </strong></h5>
                                    <h5>{{estudiante.nombre_completo}}</h5>                              
                                </div>
                            <%--</div>
                            <div class="row">--%>
                                <div class="col-xs-3">
                                    <h5><strong>Fecha de matrícula:</strong></h5>
                                    <h5>{{matricula.fecha | date:'medium'}}</h5>                           
                                </div>
                                <div class="col-xs-3">
                                    <h5><strong>Vigencia: </strong></h5>
                                    <h5>{{matricula.vigencia}}</h5>                             
                                </div>
                            <%--</div>
                            <div class="row">--%>
                                <div class="col-xs-3">
                                    <h5><strong>Grado:</strong></h5>
                                    <h5>{{matricula.nombre_grado}}</h5>                              
                                </div>
                                <div class="col-xs-3">
                                    <h5><strong>Curso:</strong></h5>
                                    <h5>{{matricula.nombre_curso}}</h5>                          
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
