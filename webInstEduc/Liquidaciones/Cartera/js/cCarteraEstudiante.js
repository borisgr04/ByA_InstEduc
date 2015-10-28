app.controller('cCarteraEstudiante', function ($scope, estudiantesService, pagosService, grupospagosService, carteraService, fechaCausacionService, conceptoacarteraService) {
    $scope.habGuardar = true;
    $scope.estudiante = {};
    $scope.carteras = [];
    $scope.cartera = {};
    $scope.config_conceptos_periodos = {};
    $scope.concepto_seleccionado = {};
    $scope.perido_desde_seleccionado = {};
    $scope.perido_hasta_seleccionado = {};
    $scope.obj_nuevo_concepto = {};
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
        $scope._traerestudiante();
    };
    $scope._traerestudiante = function () {
        $("#LbMsg").html("");
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != null) {
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                _traerCarteraEstudiante();
            }
            else {
                alert("El estudiante no se encuentra registrado");
                $scope.estudiante = {};
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    $scope._restaurarValor = function (cartera) {
        cartera.valor = cartera.valorAux;
    };
    $scope._guardar = function () {
        if (_esValido()) _guardar();
    };
    $scope._verificarValor = function (cartera) {
        var Error = false;
        if (cartera.valor == "") {
            alert("No puede dejar el campo vacio, minimo debe digitar cero (0)");
            Error = true;            
        }
        if (cartera.valor < 0) {
            alert("No puede digitar un numero negativo");
            Error = true;
        }
        if (cartera.valor < cartera.pagado) {
            alert("No puede digitar un valor menor al que ya ha pagado");
            Error = true;
        }
        if(Error) cartera.valor = cartera.valorAux;
    };
    $scope._agregarNuevoConcepto = function () {
        $scope.obj_nuevo_concepto.concepto_seleccionado = "";
        $scope.obj_nuevo_concepto.perido_desde_seleccionado = "";
        $scope.obj_nuevo_concepto.perido_hasta_seleccionado = "";
        $scope.obj_nuevo_concepto.valor = "";
        $("#modalAgregarNuevoConcepto").modal("show");
    };
    $scope._guardarNuevoConcepto = function () {
        _guardarNuevoConcepto();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Cartera Estudiante", Modulo: "Matriculas", urlToPanelModulo: "/default.aspx", Cod_Mod: "MATRI", Rol: "MATRICarte" });
        _siInformacionEstudiante();
        _configuracionConceptosPeriodos();
    };
    function _siInformacionEstudiante() {
        var id_estudiante = varLocal.Get("id_estudiante");
        if (id_estudiante != null) {
            $scope.estudiante.identificacion = id_estudiante;
            $scope._traerestudiante();
        } else {
            $("#LbMsg").msgBox({ titulo: "Información", mensaje: "Digite la identificación del estudiante para ver su cartera", tipo: "info" });
        }
    };
    function _traerCarteraEstudiante() {
        var serCart = carteraService.GetCarteraEstudiantes($scope.estudiante.identificacion, byaSite.getVigencia());
        serCart.then(function (pl) {
            $scope.carteras = pl.data;
            $.each($scope.carteras, function (index, item) {
                item.valorAux = item.valor;
            });
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _guardar() {
        byaMsgBox.confirm("¿Ha comprobado la información y está seguro de modificar la información?", function (result) {
            if (result) {
                $scope.habGuardar = false;
                var serCartera = carteraService.PostCarteraEstudiante($scope.carteras);
                serCartera.then(function (pl) {
                    $scope.habGuardar = true;
                    $("#LbMsg").msgBox({ titulo: "Cartera Estudiante", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    if (!pl.data.Error) {
                        _traerCarteraEstudiante();
                    }
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
        });
    };
    function _esValido() {
        return true;
    };
    function _configuracionConceptosPeriodos() {
        var serCon = conceptoacarteraService.Get(byaSite.getVigencia());
        serCon.then(function (pl) {
            $scope.config_conceptos_periodos = pl.data;
            $scope.obj_nuevo_concepto.concepto_seleccionado = $scope.config_conceptos_periodos.lConceptos[0];
            $scope.obj_nuevo_concepto.perido_desde_seleccionado = $scope.config_conceptos_periodos.lPeriodos[0];
            $scope.obj_nuevo_concepto.perido_hasta_seleccionado = $scope.config_conceptos_periodos.lPeriodos[0];
        }, function (pl) {
            console.log(JSON.stringify(pl));
        });
    };
    function _guardarNuevoConcepto() {
        alert(JSON.stringify($scope.obj_nuevo_concepto));
    };
});