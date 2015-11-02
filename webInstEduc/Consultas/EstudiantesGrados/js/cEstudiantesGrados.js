app.controller('cEstudiantesGrados', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService) {
    _init();
    $scope.contactos_grados = {};

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Estudiantes Grados", Modulo: "Consultas", urlToPanelModulo: "EstudiantesGrados.aspx", Cod_Mod: "CONSU", Rol: "CONSUEstuGrados" });
    };
});