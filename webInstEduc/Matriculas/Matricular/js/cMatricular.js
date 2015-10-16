// create the controller and inject Angular's $scope
app.controller('cMatricular', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService) {
    $scope.habGuardar = true;
    $scope.fecha_matricula = {};
    $scope.grados = [];
    $scope.grado = {};
    $scope.cursos = [];
    $scope.curso = {};
    $scope.carteras = [];
    $scope.cartera = {};
    $scope.estudiante = {};
    $scope.periodos = [];
    $scope.periodo = {};
    $scope.editCartera = true;
    $scope.verCartera = true;
    $scope.matriculado = false;
    $scope.objMatricula = {};
    $scope._traerestudiante = function () {
        $scope.matriculado = false;
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != 0) {
                $("#LbMsg").html("");
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                _establecerGradoEstudiante(pl.data.id_ultimo_grado);
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Estudiantes", mensaje: "El estudiante no se encuantra registrado", tipo: false });
                $scope.estudiante = {};
            }
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    $scope._editCartera = function () {
        $scope.editCartera = true;
    };
    $scope._buscarCurso = function () {
        _buscarCurso();
    };
    $scope._verificarPeriodosCartera = function (cartera) {
        $scope.cartera = cartera;
        if ($scope.cartera.periodo_hasta < $scope.cartera.periodo_desde) {
            $scope.cartera.periodo_hasta = $scope.cartera.periodo_desde
        }
    };
    $scope._matricular = function () {
        if (_esValido("datosMatricula")) {            
            _matricular();
        }
    };
    $scope._irPagos = function () {
        varLocal.Set("id_estudiante2", $scope.estudiante.identificacion);
        window.location.href = "/Liquidaciones/gLiquidaciones/gLiquidaciones.aspx";
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
    };
    $scope._irDetalleMatricula = function () {
        byaMsgBox.confirm("Se ha realizado la matricula correctamente. <br/> ¿Desea imprimir la matricula que ha realizado?", function (result) {
            if (result) {
                varLocal.Set("matricula_detalles", $scope.objMatricula);
                window.location.href = "/Matriculas/Gestion/DetallesMatricula.aspx";
            }
        });
    };
    $scope._traerVisualizacionCartera = function () {
        _traerVisualizacionCartera();
    };
    _init();

    function _init() {
        $scope.fecha_matricula = byaPage.getDateNow();
        byaSite.SetModuloP({ TituloForm: "Realizar Matricula", Modulo: "Matriculas", urlToPanelModulo: "Matricular.aspx", Cod_Mod: "MATRI", Rol: "MATRIMatricular" });
        if ((byaSite.getVigencia() == null) || (byaSite.getVigencia() == "")) {
            $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Debe especificar una vigencia", tipo: false });
        } else {
            _traerGrados();
            _traerPeriodos();
            var id_estudiante = varLocal.Get("id_estudiante");
            if (id_estudiante != null) {
                $scope.estudiante.identificacion = id_estudiante;
                $scope._traerestudiante();
            }            
        }
    };
    function _traerPeriodos() {
        var serPeriodos = periodosService.Gets(byaSite.getVigencia());
        serPeriodos.then(function (pl) {
            $scope.periodos = pl.data;
            $scope.periodo = $scope.periodos[0];
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _establecerGradoEstudiante(idGrado) {
        $.each($scope.grados, function (index, item) {
            if (item.id == idGrado) {
                $scope.grado = $scope.grados[index + 1];
                _buscarCurso();
            }
        });        
    };
    function _traerVisualizacionCartera() {        
        var strFech = $("#txtFechaActual").val().split("-");
        var serCarte = carteraService.GetVisualizacionCarteraAntes($scope.grado.id, byaSite.getVigencia(), strFech[0], strFech[1]);
        serCarte.then(function (pl) {
            $scope.carteras = pl.data;
            $scope.verCartera = true;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerGrados() {
        var promiseGet = gradosService.Gets();
        promiseGet.then(function (pl) {
            $scope.grados = pl.data;
            $scope.grado = $scope.grados[0];
            _buscarCurso();

            if ($scope.estudiante.id_ultimo_grado != null) {
                _establecerGradoEstudiante($scope.estudiante.id_ultimo_grado);
            }
        },
        function (errorPl) {
            console.log(errorPl);
        });
    };
    function _buscarCurso() {
        var serCurso = cursosService.GetsCursosGrado($scope.grado.id);
        serCurso.then(function (pl) {
            $scope.cursos = pl.data;
            $scope.curso = $scope.cursos[0];
            _traerVisualizacionCartera();
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    function _traerMatricula() {
        var serCurso = matriculasService.Get(byaSite.getVigencia(), $scope.estudiante.identificacion);
        serCurso.then(function (pl) {
            $scope.objMatricula = pl.data;
            $scope._irDetalleMatricula();
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    function _esValido(nameForm) {
        var error = false;
        var sRequired = $scope[nameForm].$error.required;
        if ((sRequired != null) && (sRequired != false)) {
            error = true;
        }

        if (error) {
            $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Los campos resaltados en rojo son obligatorios...", tipo: false });
            $("input.ng-invalid").css("border", " 1px solid red");
            $("select.ng-invalid").css("border", " 1px solid red");
            return !error;
        } else {
            $("input.ng-valid").css("border", "");
            $("select.ng-valid").css("border", "");
            $("#LbMsg").html("");
            return true;
        }
        
    };
    function _getDatosMatricula() {
        var e = {};
        e.id_estudiante = $scope.estudiante.identificacion;
        e.vigencia = byaSite.getVigencia();
        e.id_curso = $scope.curso.id;
        e.lCartera = $scope.carteras;
        e.fecha = $("#txtFechaActual").val();
        return e;
    };
    function _matricular() {
        byaMsgBox.confirm("¿Ha comprobado la información y está seguro de realizar esta matricula?", function (result) {
            if (result) {
                $scope.habGuardar = false;
                var serMatricula = matriculasService.Post(_getDatosMatricula());
                serMatricula.then(function (pl) {
                    $scope.habGuardar = true;
                    $("#LbMsg").msgBox({ titulo: "Matricula", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    if (!pl.data.Error) {
                        $scope.matriculado = true;
                        _traerMatricula();
                    }
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
        });        
    };
});