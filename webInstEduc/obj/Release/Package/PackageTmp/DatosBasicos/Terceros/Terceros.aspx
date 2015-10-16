<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="Terceros.aspx.cs" Inherits="AspIdentity.DatosBasicos.Terceros.Terceros" %>
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
    <script src="js/cTerceros.js"></script>
    <div class="container" ng-app="Model">
        <div ng-controller="cTerceros" ng-cloak>
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
                <div class="row form-group">
                    <button class="btn btn-info btn-sm" ng-click="_nuevo()"><span class="glyphicon glyphicon-plus"></span> Nuevo</button>
                </div>
                <div class="row">
                    <table ng-table="tableTerceros" template-pagination="/data-table-pager.html"  class="table table-bordered table-hover table-striped tablesorter">
                        <tr ng-repeat="item in $data">
                                <td data-title="'Tip. Id.'">{{item.tipo_identificacion}}</td>
                                <td class="text-right" header-class="'text-right'" data-title="'Identificación'"><strong>{{item.identificacion | currency :"":0}}</strong></td>
                                <td data-title="'Nombre'">{{item.nombre}}</td>
                                <td data-title="'Apellido'">{{item.apellido}}</td>
                                <td data-title="'Sexo'">{{item.sexo}}</td>
                                <td data-title="'Dirección'">{{item.direccion}}</td>
                                <td data-title="'Teléfono'">{{item.telefono}}</td>                                
                                <td style="text-align:left"><a href="javascript:void(0)" ng-click="_verDetallesTercero(item)"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></a></td>
                        </tr>                        
                    </table>
                </div>
            </div>
            <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="modalRegistroTercero">
              <div class="modal-dialog modal-lg">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Tercero</h4>
                  </div>
                  <div class="modal-body">
                      <form name="datosTercero">
                          <div class="row form-group container">
                              <div id="msgModal"></div>
                              <div class="row form-group">
                                  <div class="col-xs-6">
                                      <label>Tipo Identificación</label>
                                      <select class="form-control" ng-model="tercero.tipo_identificacion" required>
                                          <option value="RC">Registro civil</option>
                                          <option value="TI">Tarjeta de identidad</option>
                                          <option value="CC">Cedula de ciudadanía</option>
                                          <option value="CE">Cedula extranjera</option>
                                          <option value="PAS">Pasaporte</option>
                                      </select>
                                  </div>
                                  <div class="col-xs-6">
                                      <label>Identificación</label>
                                      <input type="text" class="form-control" ng-model="tercero.identificacion" ng-disabled="editar" required/>
                                  </div>
                              </div>
                              <div class="row form-group">
                                  <div class="col-xs-6">
                                      <label>Nombre</label>
                                      <input type="text" class="form-control" ng-model="tercero.nombre" required/>
                                  </div>
                                  <div class="col-xs-6">
                                      <label>Apellido</label>
                                      <input type="text" class="form-control" ng-model="tercero.apellido" required/>
                                  </div>
                              </div>
                              <div class="row form-group">
                                  <div class="col-xs-6">
                                      <label>Dirección</label>
                                      <input type="text" class="form-control" ng-model="tercero.direccion" required/>
                                  </div>
                                  <div class="col-xs-6">
                                      <label>Teléfono</label>
                                      <input type="text" class="form-control" ng-model="tercero.telefono" required/>
                                  </div>
                              </div>
                              <div class="row form-group">
                                  <div class="col-xs-6">
                                      <label>Celular</label>
                                      <input type="text" class="form-control" ng-model="tercero.celular"/>
                                  </div>
                                  <div class="col-xs-6">
                                      <label>Email</label>
                                      <input type="text" class="form-control" ng-model="tercero.email"/>
                                  </div>
                              </div>
                              <div class="row form-group">
                                  <div class="col-xs-6">
                                      <label>Sexo</label>
                                      <select class="form-control" ng-model="tercero.sexo" required>
                                          <option value="MASCULINO">MASCULINO</option>
                                          <option value="FEMENINO">FEMENINO</option>
                                      </select>
                                  </div>
                                  <div class="col-xs-6">
                                      <label>GS-RH</label>
                                      <input type="text" class="form-control" ng-model="tercero.RH"/>
                                  </div>
                              </div>
                              <div class="row form-group">
                                  <div class="col-xs-6">
                                      <label>Ocupación</label>
                                      <input type="text" class="form-control" ng-model="tercero.ocupacion"/>
                                  </div>
                                  <div class="col-xs-6">
                                      <label>Direccion Trabajo</label>
                                      <input type="text" class="form-control" ng-model="tercero.direccion_trabajo"/>
                                  </div>
                              </div>
                          </div>
                      </form>
                  </div>
                  <div class="modal-footer">                      
                    <button type="button" class="btn btn-primary" ng-click="_guardo()">Guardar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                  </div>
                </div>
              </div>
            </div>
        </div>
    </div>
</asp:Content>
