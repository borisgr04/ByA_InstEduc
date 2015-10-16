<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBS.Master" AutoEventWireup="true" CodeBehind="PerfilxUsuario.aspx.cs" Inherits="webInstEduc.Seguridad.PerfilxUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-2">
            <label>
            Nombre de Usuario</label>
            </div>
            <div class="col-md-3">
                
            <div id="username"></div>
                </div>
        </div>
    <div class="row">
        <div class="col-md-2">
            <label for="CboFil">
                Dependencia:</label>
        </div>
        <div class="col-md-3" >
            <select id="CboFil" class="form-control input-sm">
                <optgroup label="Matricula">
                    <option value="MATRI">Matricula</option>
                </optgroup>
                <optgroup label="Pagos">
                    <option value="PAGOS">Pagos</option>
                </optgroup>
                <optgroup label="Estudiante">
                    <option value="ESTUD">Estudiantes</option>
                </optgroup>
                <optgroup label="Consultas">
                    <option value="CONSU">Consultas</option>
                </optgroup>
                <optgroup label="Datos basicos">
                    <option value="DATOB">Datos basicos</option>
                </optgroup>
                <optgroup label="Seguridad">
                    <option value="SEGUD">Seguridad</option>
                </optgroup>
                


            </select>
        </div>
        <div class="col-md-7">
            <div class="btn-toolbar">

                <button type="button" value="Consultar" id="BtnConsulta" data-loading-text="Loading..." class="btn btn-warning">
                    <span class="glyphicon glyphicon-search"></span>Consultar
                </button>

                <button type="button" value="Nuevo" id="BtnGuardar" class="btn btn-danger">
                    <span class="glyphicon glyphicon-book"></span>Guardar
                </button>
            </div>
        </div>
    </div>
    
    <div class="col-lg-6">
                <div class="form-group">
                    <div class="checkbox" >
                        <input type="checkbox" id="chkTodos"  > Todos
                    </div>
                </div>
            </div>

    <div id="jqxgrid">
    </div>



    <script src="js/PerfilxUsuario.js"></script>
</asp:Content>
