// create the controller and inject Angular's $scope
app.controller('cAnularMatricular', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService) {
    $scope.habGuardar = true;
    $scope.verMatricula = false;
    $scope.matricula = {};
    $scope.estudiante = {};
    $scope._traerestudiante = function () {
        $scope.matriculado = false;
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != 0) {
                $("#LbMsg").html("");
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                _traerMatricula();
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Estudiantes", mensaje: "El estudiante no se encuantra registrado", tipo: false });
                $scope.estudiante = {};
            }
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
    };
    $scope._traerMatriculaEstudiante = function () {
        _traerMatricula();
    };
    $scope._AnularMatricula = function () {
        _AnularMatricula();
    };

    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Anular Matrícula", Modulo: "Matrículas", urlToPanelModulo: "AnularMatricula.aspx", Cod_Mod: "MATRI", Rol: "MATRIAnuMat" });
        if ((byaSite.getVigencia() == null) || (byaSite.getVigencia() == "")) {
            $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Debe especificar una vigencia", tipo: false });
        } else {
            var id_estudiante = varLocal.Get("id_estudiante");
            if (id_estudiante != null) {
                $scope.estudiante.identificacion = id_estudiante;
                $scope._traerestudiante();
            }
        }
    };
    function _traerMatricula() {
        var serMatri = matriculasService.Get(byaSite.getVigencia(), $scope.estudiante.identificacion);
        serMatri.then(function (pl) {
            if (pl.data != null) {
                $scope.matricula = pl.data;
                $scope.verMatricula = true;
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Anularción de matrícula", mensaje: "El estudiante no se encuentra matrículado en la vigencia seleccionda", tipo: false });
                $scope.verMatricula = false;
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _AnularMatricula() {
        byaMsgBox.confirm("¿Ha comprobado la información y está seguro de anular esta matrícula?", function (result) {
            if (result) {
                $scope.habGuardar = false;
                var serMatr = matriculasService.PostAnular($scope.matricula.id);
                serMatr.then(function (pl) {
                    $scope.habGuardar = true;
                    $("#LbMsg").msgBox({ titulo: "Anularción de matrícula", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    if (!pl.data.Error) $scope.verMatricula = false;
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
        });
    };
});