<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="RegistroEstudiante.aspx.cs" Inherits="AspIdentity.Acudientes.Estudiantes.Registro.RegistroEstudiante" %>
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
    <script src="js/cRegistroEstudiante.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cRegistroEstudiante">
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
            <div class="row">
                <div class="panel panel-default">
                    <div class="panel-heading">Estudiante</div>
                    <div class="panel-body">
                        <form name="datosEstudiante">
                        <div class="row" style="margin-left:0px">
                            <div class="text-left col-xs-6">
                                <h4 class="text-left">1. Datos del estudiante</h4>
                            </div>
                            <div class="text-right col-xs-6">
                                <button type="button" ng-click="_Back()" ng-disabled="!habGuardar" class="btn btn-success"><span class="glyphicon glyphicon-chevron-left"></span> Atrás</button>
                            </div>                            
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Tipo Identificación</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros.tipo_identificacion" required>
                                    <option value="RC">Registro civil</option>
                                    <option value="TI">Tarjeta de identidad</option>
                                    <option value="CC">Cedula de ciudadanía</option>
                                    <option value="CE">Cedula extranjera</option>
                                    <option value="PAS">Pasaporte</option>
                                </select>
                            </div>
                            <div class="col-xs-3">
                                <label>Identificación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.identificacion" ng-blur="_traerestudiante()" format="number" required/>
                            </div>
                            <div class="col-xs-3">
                                <label>Nombres</label>                                
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros.nombre" required/>
                            </div>
                            <div class="col-xs-3">
                                <label>Apellidos</label>                                
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros.apellido" required/>
                            </div>                            
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Lugar de nacimiento</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.lugar_nacimiento" required/>
                            </div>
                            <div class="col-xs-3">
                                <label>Fecha de nacimiento</label>
                                <input disabled="disabled" type="date" ng-change="mostrarE()" class="form-control input-sm" ng-model="estudiante.fecha_nacimiento" required/>
                            </div>
                            <div class="col-xs-3">
                                <label>Edad</label>
                                <input type="text" class="form-control input-sm" ng-model="estudiante.edad" format="number" required disabled="disabled"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Sexo</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros.sexo" required>
                                    <option value="MASCULINO">MASCULINO</option>
                                    <option value="FEMENINO">FEMENINO</option>
                                </select>
                            </div>                          
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>GS - RH</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros.RH" required/>
                            </div>
                            <div class="col-xs-3">                                
                                <label>Dirección residencia</label>
                                <input type="text" class="form-control input-sm" disabled="disabled" ng-model="estudiante.terceros.direccion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Teléfono residencia</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros.telefono"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Celular</label>
                                <input disabled="disabled" class="form-control input-sm" type="text"  ng-model="estudiante.terceros.celular"/>
                            </div>                                                      
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Email</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros.email"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Colegio de procedencia</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.colegio_procedencia"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Grado anterior</label>
                                <select disabled="disabled" ng-model="estudiante.id_ultimo_grado" ng-options="grado.id as grado.nombre for grado in grados" class="form-control"></select>
                            </div>   
                            <div class="col-xs-3">
                                <label>Estado civil padres</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.estado_civil_padres" required>
                                    <option value="CASADOS">CASADOS</option>
                                    <option value="UNION LIBRE">UNION LIBRE</option>
                                    <option value="DIVORCIADOS">DIVORCIADOS</option>
                                </select>
                            </div>                                                   
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Vive con</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.vive_con" required>
                                    <option value="PADRES">PADRES</option>
                                    <option value="ACUDIENTE">ACUDIENTE</option>
                                </select>
                            </div>  
                            <div class="col-xs-3">
                                <label>Código Estudiante</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.codigo" required/>
                            </div> 
                        </div>
                        <hr />
                        <div class="row" style="margin-left:0px">
                            <h4 class="text-left">2. Datos del padre</h4>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Tipo Identificación</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros2.tipo_identificacion">
                                    <option value="RC">Registro civil</option>
                                    <option value="TI">Tarjeta de identidad</option>
                                    <option value="CC">Cedula de ciudadanía</option>
                                    <option value="CE">Cedula extranjera</option>
                                    <option value="PAS">Pasaporte</option>
                                </select>
                            </div>
                            <div class="col-xs-3">
                                <label>Identificación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros2.identificacion" ng-blur="_traerTerceroPadre()" format="number"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Nombres</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros2.nombre"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Apellidos</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros2.apellido"/>
                            </div>                            
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>GS - RH</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros2.RH"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Dirección residencia</label>
                                <input type="text" disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros2.direccion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Teléfono residencia</label>
                                <input type="text" disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros2.telefono"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Celular</label>
                                <input type="text" disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros2.celular"/>
                            </div>                                                      
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Email</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros2.email"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Ocupación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros2.ocupacion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Dirección y lugar de trabajo</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros2.direccion_trabajo"/>
                            </div>                                                    
                        </div>
                        <hr />
                        <div class="row" style="margin-left:0px">
                            <h4 class="text-left">3. Datos de la madre</h4>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Tipo Identificación</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros1.tipo_identificacion">
                                    <option value="RC">Registro civil</option>
                                    <option value="TI">Tarjeta de identidad</option>
                                    <option value="CC">Cedula de ciudadanía</option>
                                    <option value="CE">Cedula extranjera</option>
                                    <option value="PAS">Pasaporte</option>
                                </select>
                            </div>
                            <div class="col-xs-3">
                                <label>Identificación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros1.identificacion" ng-blur="_traerTerceroMadre()" format="number"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Nombres</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros1.nombre"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Apellidos</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros1.apellido"/>
                            </div>                            
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>GS - RH</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros1.RH"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Dirección residencia</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros1.direccion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Teléfono residencia</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros1.telefono"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Celular</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros1.celular"/>
                            </div>                                                      
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Email</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros1.email"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Ocupación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros1.ocupacion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Dirección y lugar de trabajo</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros1.direccion_trabajo"/>
                            </div>                                                    
                        </div>
                        <hr />
                        <div class="row" style="margin-left:0px">
                            <div class="col-xs-2">
                                <h4 class="text-left">4. Datos acudiente</h4>
                            </div>
                            <div class="col-xs-2">
                                <select disabled="disabled" class="form-control" ng-model="quien_acudiente" ng-change="_cambiarAcudiente()">
                                    <option value="PADRE">PADRE</option>
                                    <option value="MADRE">MADRE</option>
                                    <option value="OTRO" selected="selected">OTRO</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Tipo Identificación</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros3.tipo_identificacion" required>
                                    <option value="RC">Registro civil</option>
                                    <option value="TI">Tarjeta de identidad</option>
                                    <option value="CC">Cedula de ciudadanía</option>
                                    <option value="CE">Cedula extranjera</option>
                                    <option value="PAS">Pasaporte</option>
                                </select>
                            </div>
                            <div class="col-xs-3">
                                <label>Identificación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros3.identificacion" format="number" ng-blur="_traerTerceroAcudiente()" required/>
                            </div>
                            <div class="col-xs-3">
                                <label>Nombres</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros3.nombre" required/>
                            </div>
                            <div class="col-xs-3">
                                <label>Apellidos</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros3.apellido" required/>
                            </div>                            
                        </div>
                        <div class="form-group row">
                             <div class="col-xs-3">
                                <label>Sexo</label>
                                <select disabled="disabled" class="form-control input-sm" ng-model="estudiante.terceros3.sexo" required>
                                    <option value="MASCULINO">MASCULINO</option>
                                    <option value="FEMENINO">FEMENINO</option>
                                </select>
                            </div>     
                            <div class="col-xs-3">
                                <label>GS - RH</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros3.RH"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Dirección residencia</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros3.direccion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Teléfono residencia</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros3.telefono"/>
                            </div>                                                                                 
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Celular</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros3.celular"/>
                            </div> 
                            <div class="col-xs-3">
                                <label>Email</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros3.email"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Ocupación</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.terceros3.ocupacion"/>
                            </div>
                            <div class="col-xs-3">
                                <label>Dirección y lugar de trabajo</label>
                                <input disabled="disabled" class="form-control input-sm" type="text" ng-model="estudiante.terceros3.direccion_trabajo"/>
                            </div>                                                                               
                        </div>
                        <div class="form-group row">
                            <div class="col-xs-3">
                                <label>Parentesco del acudiente</label>
                                <input disabled="disabled" type="text" class="form-control input-sm" ng-model="estudiante.parentesco_acudiente" required/>
                            </div> 
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
