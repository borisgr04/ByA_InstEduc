// create the controller and inject Angular's $scope
app.controller('cDefault', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, estudiantessaldosService, fechaCausacionService) {
    $scope.estudiantes = [];
    $scope.estudiante = {};
    $scope.filtro = "";
    $scope.fecha_causacion = {};
    $scope.editFecha = false;
    $scope._traerestudiante = function () {
            $("#LbMsg").html("");
            var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
            serEstu.then(function (pl) {
                if (pl.data.id != null) {
                    $scope.estudiante = pl.data;
                    $scope.estudiantes.push($scope.estudiante);
                    varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                }
                else {
                    $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "El estudiante no se encuentra registrado", tipo: false });
                    $scope.estudiante = {};
                }
            }, function (errorPl) {
                console.log(JSON.stringify(errorPl));
            });
    }; 
    $scope._traerestudiantes = function () {
        if ($scope.filtro != "") {
            $("#LbMsg").html("");
            var serEstu = estudiantesService.GetsFiltro($scope.filtro);
            serEstu.then(function (pl) {
                $scope.estudiantes = pl.data;
                if ($scope.estudiantes.length == 1) {
                    varLocal.Set("id_estudiante", $scope.estudiantes[0].identificacion);
                }
            }, function (errorPl) {
                console.log(JSON.stringify(errorPl));
            });
        }
    };
    $scope._irInformacion = function () {
        window.location.href = "/Estudiantes/Registro/RegistroEstudiante.aspx";
    };
    $scope._irMatricula = function () {
        window.location.href = "/Matriculas/Matricular/Matricular.aspx";
    };
    $scope._irLiquidaciones = function () {
        window.location.href = "/Liquidaciones/gLiquidaciones/gLiquidaciones.aspx";
    };
    $scope._irLiquidar = function () {
        window.location.href = "/Liquidaciones/Liquidar/Liquidacion.aspx";
    };
    $scope._irTransacciones = function () {
        window.location.href = "/Consultas/TransaccionesEstudiante/TransaccionesEstudiante.aspx";
    };
    $scope._irRealizarPago = function () {
        window.location.href = "/Liquidaciones/PagarSolo/PagarSolo.aspx";
    };
    $scope._irPagosEstudiante = function () {
        window.location.href = "/Consultas/PagosEstudiante/PagosEstudiante.aspx";
    };
    $scope._irEstadoCuentaEstudiante = function () {
        window.location.href = "/Consultas/EstadoCuenta/EstadoCuenta.aspx";
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
        $scope.estudiantes = [];
        //$scope._traerestudiante();
    };
    $scope._seleccionarEstudiante = function (estu) {
        $scope.estudiantes = [];
        $scope.estudiantes.push(estu);
        varLocal.Set("id_estudiante", $scope.estudiantes[0].identificacion);
    };
    $scope.SiEditarFechaCausacion = function () {
        $scope.editFecha = true;
        var fecSrt = "" + $scope.fecha_causacion + "";
        fecSrt = fecSrt.substring(0, 10);
        $("#txtFechaActual").val(fecSrt);
    };
    $scope.GuardarFechaCausacion = function () {
        GuardarFechaCausacion();
    };

    _init();

    function _init() {
        FechaActualCausacion();
        byaSite.SetModuloP({ TituloForm: "", Modulo: "Búsqueda", urlToPanelModulo: "/" });
        var id_estudiante = varLocal.Get("id_estudiante");
        if (id_estudiante != null) {
            $scope.estudiante.identificacion = id_estudiante;
            $scope._traerestudiante();
        }
    };
    function FechaActualCausacion() {
        var fecCau = fechaCausacionService.Get();
        fecCau.then(function (pl) {
            $scope.fecha_causacion = pl.data;
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function GetDatosFechaCausacion() {
        var e = {};
        e.fecha = $("#txtFechaActual").val();
        return e;
    };
    function GuardarFechaCausacion() {
        var fecCau = fechaCausacionService.Post(GetDatosFechaCausacion());
        fecCau.then(function (pl) {
            $("#LbMsg").msgBox({ titulo: "", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
            if (!pl.data.Error) $scope.editFecha = false;
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});