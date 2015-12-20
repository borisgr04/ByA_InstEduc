// create the controller and inject Angular's $scope
app.controller('cDefault', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, estudiantessaldosService, fechaCausacionService) {
    _init();
    function _init() {
        var rol = byaSite.getRol();
        if (rol == "administrador") window.location.href = "/Inicio/Administrativo/Inicio.aspx";
        if (rol == "acudiente") window.location.href = "/Inicio/Acudientes/Inicio.aspx";
    };
});