<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="DetallesMatricula.aspx.cs" Inherits="AspIdentity.Acudientes.Matriculas.Gestion.DetallesMatricula" %>
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
    <script src="js/cDetallesMatricula.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cDetallesMatricula" ng-cloak>
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
            <div class="container">
                <div class="row">
                            <div class="row">
                                <div class="row">
                                    <div class="col-xs-2">                                        
                                    </div>
                                    <div class="col-xs-8">  
                                        <div style="margin:5px">
                                            <button ng-disabled="editCartera" class="btn btn-info" ng-click="_print()"><span class="glyphicon glyphicon-print"></span> Imprimir</button>                                            
                                            <button ng-click="_Back()" class="btn btn-success"><span class="glyphicon glyphicon-chevron-left"></span> Atrás</button>
                                        </div>                                        
                                        <div class="panel panel-default" id="print">                                            
                                            <div class="panel-body">       
                                                <div class="row">
                                                    <div class="col-xs-4 text-center" style="margin-top:15px">
                                                        <img src="/wsFoto.ashx" style="width:40%;height:80px" />
                                                    </div>
                                                    <div class="col-xs-4 text-center">
                                                        <h5><strong>{{entidad.nombre}}</strong></h5>
                                                        <h5><strong>Dirección: </strong> {{entidad.direccion}}</h5>
                                                        <h5><strong>Teléfono: </strong> {{entidad.telefono}}</h5>
                                                    </div>
                                                    <div class="col-xs-4 text-center" style="margin-top:15px" id="qrNoLiq">
                                                        <h4 style="margin:0px">FICHA DE MATRICULA</h4>
                                                        <h5><strong>No.</strong> {{matricula.id_matricula}}</h5>
                                                    </div>
                                                </div>                                         
                                                <div class="row text-center">                                                    
                                                    <hr style="margin:0px"/>
                                                </div>
                                                <div class="row container" style="margin:10px;">
                                                    <div class="row">
                                                        <h4>1. Matricula</h4>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Grado: </strong></label><br />
                                                            <label>{{matricula.nombre_grado}} -  {{matricula.nombre_curso}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Vigencia: </strong></label><br />
                                                            <label>{{matricula.vigencia}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Fecha Matricula: </strong></label><br />
                                                            <label>{{matricula.fecha | date : 'yyyy-MM-dd HH:mm'}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Folio: </strong></label><br />
                                                            <label>{{matricula.folio}}</label>
                                                        </div>
                                                    </div>                                                    
                                                </div>
                                                <hr style="margin:0px"/>
                                                <div class="row container" style="margin:10px;">
                                                    <div class="row">
                                                        <h4>2. Estudiante</h4>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Documento: </strong></label><br />
                                                            <label>{{estudiante.terceros.tipo_identificacion}} {{estudiante.terceros.identificacion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Código: </strong></label><br />
                                                            <label>{{estudiante.codigo}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Nombres: </strong></label><br />
                                                            <label>{{estudiante.terceros.nombre}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Apellido: </strong></label><br />
                                                            <label>{{estudiante.terceros.apellido}}</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Lugar nacimiento: </strong></label><br />
                                                            <label>{{estudiante.lugar_nacimiento}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Fecha nacimiento: </strong></label><br />
                                                            <label>{{estudiante.fecha_nacimiento | date : 'yyyy-MM-dd'}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Edad: </strong></label><br />
                                                            <label>{{_calcular_edad(estudiante.fecha_nacimiento)}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Sexo: </strong></label><br />
                                                            <label>{{estudiante.terceros.sexo}}</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Dirección: </strong></label><br />
                                                            <label>{{estudiante.terceros.direccion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Teléfono: </strong></label><br />
                                                            <label>{{estudiante.terceros.telefono}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Vive con: </strong></label><br />
                                                            <label>{{estudiante.vive_con}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Colegio de procedencia: </strong></label><br />
                                                            <label>{{estudiante.colegio_procedencia}}</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr style="margin:0px;"/>
                                                <div class="row container" style="margin:10px;">
                                                    <div class="row">
                                                        <h4>3. Madre</h4>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Documento: </strong></label><br />
                                                            <label>{{estudiante.terceros1.tipo_identificacion}} {{estudiante.terceros1.identificacion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Nombres: </strong></label><br />
                                                            <label>{{estudiante.terceros1.nombre}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Apellido: </strong></label><br />
                                                            <label>{{estudiante.terceros1.apellido}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Ocupacion: </strong></label><br />
                                                            <label>{{estudiante.terceros1.ocupacion}}</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Dirección: </strong></label><br />
                                                            <label>{{estudiante.terceros1.direccion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Teléfono: </strong></label><br />
                                                            <label>{{estudiante.terceros1.telefono}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Celular: </strong></label><br />
                                                            <label>{{estudiante.terceros1.celular}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Email: </strong></label><br />
                                                            <label>{{estudiante.terceros1.email}}</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr style="margin:0px;"/>
                                                <div class="row container" style="margin:10px;">
                                                    <div class="row">
                                                        <h4>4. Padre</h4>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Documento: </strong></label><br />
                                                            <label>{{estudiante.terceros2.tipo_identificacion}} {{estudiante.terceros1.identificacion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Nombres: </strong></label><br />
                                                            <label>{{estudiante.terceros2.nombre}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Apellido: </strong></label><br />
                                                            <label>{{estudiante.terceros2.apellido}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Ocupacion: </strong></label><br />
                                                            <label>{{estudiante.terceros2.ocupacion}}</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Dirección: </strong></label><br />
                                                            <label>{{estudiante.terceros2.direccion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Teléfono: </strong></label><br />
                                                            <label>{{estudiante.terceros2.telefono}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Celular: </strong></label><br />
                                                            <label>{{estudiante.terceros2.celular}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Email: </strong></label><br />
                                                            <label>{{estudiante.terceros2.email}}</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr style="margin:0px;"/>
                                                <div class="row container" style="margin:10px;">
                                                    <div class="row">
                                                        <h4>5. Acudiente</h4>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Documento: </strong></label><br />
                                                            <label>{{estudiante.terceros3.tipo_identificacion}} {{estudiante.terceros1.identificacion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Nombres: </strong></label><br />
                                                            <label>{{estudiante.terceros3.nombre}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Apellido: </strong></label><br />
                                                            <label>{{estudiante.terceros3.apellido}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Ocupacion: </strong></label><br />
                                                            <label>{{estudiante.terceros3.ocupacion}}</label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-3">
                                                            <label><strong>Dirección: </strong></label><br />
                                                            <label>{{estudiante.terceros3.direccion}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Teléfono: </strong></label><br />
                                                            <label>{{estudiante.terceros3.telefono}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Celular: </strong></label><br />
                                                            <label>{{estudiante.terceros3.celular}}</label>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <label><strong>Email: </strong></label><br />
                                                            <label>{{estudiante.terceros3.email}}</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div> 
                                    <div class="col-xs-2"></div>
                                </div>  
                            </div>
                        </div>
                    </div>            
        </div>
    </div>
</asp:Content>
