﻿// create the controller and inject Angular's $scope
app.controller('cDeudasGrados', function ($scope, deudasgradosService) {
    $scope.deudas_grados = [];
    $scope.deudas_cursos_grado = [];
    $scope.deudas_estudiante_curso_grado = [];
    $scope.deuda_grado = {};
    $scope.deuda_curso_grado = {};
    $scope.deuda_estudiante_curso_grado = {};
    $scope._traerDeudasCursosGrado = function (deuda) {
        $scope.deuda_grado = deuda;
        _traerDeudasCursosGrado();
    };
    $scope._traerDeudasEstudiantesCursoGrado = function (deuda) {
        $scope.deuda_curso_grado = deuda;
        _traerDeudasEstudianteCursoGrado();
    };


    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Deudas Grados", Modulo: "Consultas", urlToPanelModulo: "DeudasGrados.aspx", Cod_Mod: "CONSU", Rol: "CONSUDeuGra" });
        _traerDeudasGrados();
    };
    function _traerDeudasGrados() {
        var serDeu = deudasgradosService.DeudasGrados();
        serDeu.then(function (pl) {
            $scope.deudas_grados = pl.data;
            byaPage.irFin();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerDeudasCursosGrado() {
        var serDeu = deudasgradosService.DeudasCursosGrado($scope.deuda_grado.id_grado);
        serDeu.then(function (pl) {
            $scope.deudas_cursos_grado = pl.data;
            byaPage.irFin();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerDeudasEstudianteCursoGrado() {
        var serDeu = deudasgradosService.DeudasEstudiantesCursoGrado($scope.deuda_curso_grado.id_curso);
        serDeu.then(function (pl) {
            $scope.deudas_estudiante_curso_grado = pl.data;
            byaPage.irFin();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});