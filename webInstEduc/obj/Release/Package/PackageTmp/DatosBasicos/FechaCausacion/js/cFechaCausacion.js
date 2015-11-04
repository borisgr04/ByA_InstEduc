// create the controller and inject Angular's $scope
app.controller('cFechaCausacion', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, estudiantessaldosService, fechaCausacionService) {
    $scope.fecha_causacion = {};
    $scope.cambiarFechaCausacion = function () {
        CambiarFechaCausacion();
    };

    _init();

    function _init() {
        FechaActualCausacion();
        byaSite.SetModuloP({ TituloForm: "Fecha de Causación", Modulo: "Datos Basicos", urlToPanelModulo: "/", Cod_Mod: "DATOB", Rol: "DATOBFechaCausar" });
    };
    function FechaActualCausacion() {
        var fecCau = fechaCausacionService.Get();
        fecCau.then(function (pl) {
            $scope.fecha_causacion = pl.data;
            var fecSrt = "" + pl.data + "";
            var fec = fecSrt.substr(0, 10);
            $("#txtFechaActual").val(fec);
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function CambiarFechaCausacion() {
        var e = {};
        e.Fecha = $("#txtFechaActual").val();

        var fecCau = fechaCausacionService.Post(e);
        fecCau.then(function (pl) {
            $("#LbMsg").msgBox({ titulo: "Fecha de Causación", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});