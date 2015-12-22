// create the controller and inject Angular's $scope
app.controller('cEstadoCuenta', function ($scope, gradosService, estadocuentaresumenService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, estudiantessaldosService) {
    $scope.estudiante = {};
    $scope.obj_consulta = {};
    $scope.saldos = [];
    $scope.saldo = {};
    $scope.saldos_vigencias = [];
    $scope.saldo_vigencia = {};
    $scope.saldos_vigencias_periodos = [];
    $scope.saldo_vigencia_periodo = {};
    $scope.vigencia = byaSite.getVigencia();
    $scope.fecha_actual = new Date();
    $scope.estadocuentaresumen = [];
    $scope.spensiones = 0;
    $scope.xx = -1000 ;
    $scope.consolidado_estado_cuenta = {};
    $scope._traerestudiante = function () {
        var serEstu = estudiantesService.Get($scope.obj_consulta.id_estudiante);
        serEstu.then(function (pl) {
            if (pl.data.id != 0) {
                $("#LbMsg").html("");
                $scope.estudiante = pl.data;
                _consultarSaldo();
                $scope._traerEstadocuenta();
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Estudiantes", mensaje: "El estudiante no se encuantra registrado", tipo: false });
                $scope.estudiante = {};
                $scope.obj_consulta.id_estudiante = "";
            }
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    $scope._traerEstadocuenta = function () {
        var serEstu = estadocuentaresumenService.Get($scope.estudiante.identificacion, $scope.vigencia);
        serEstu.then(function (pl) {
            $scope.estadocuentaresumen = pl.data;
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    $scope._consultarSaldo = function () {
        if (_esValido('datosConsulta')) _consultarSaldo();
    };
    $scope._consultarSaldoVigencia = function (saldo) {
        $scope.saldo = saldo;
        $scope.saldos_vigencias_periodos = [];
        _consultarSaldoVigencia(saldo.Item);
    };
    $scope._consultarSaldoVigenciaPeriodo = function (saldo_vigencia) {
        $scope.saldo_vigencia = saldo_vigencia;
        _consultarSaldoVigenciaPeriodo(saldo_vigencia.Item);
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.obj_consulta.id_estudiante = "";
        $scope.estudiante = {};
        $scope._traerestudiante();
    };
    $scope._DeudaTotalVigencias = function () {
        var valor = 0;
        var pagado = 0;
        $.each($scope.saldos, function (index, item) {
            valor += item.Valor;
            pagado += item.Pagado;
        });
        return valor - pagado;
    };
    $scope._DeudaTotalPeriodos = function () {
        var valor = 0;
        var pagado = 0;
        $.each($scope.saldos_vigencias, function (index, item) {
            valor += item.Valor;
            pagado += item.Pagado;
        });
        return valor - pagado;
    };
    $scope._DeudaTotalConceptos = function () {
        var valor = 0;
        var pagado = 0;
        $.each($scope.saldos_vigencias_periodos, function (index, item) {
            valor += item.Valor;
            pagado += item.Pagado;
        });
        return valor - pagado;
    };
    $scope._verModalEstadoCuenta = function () {
        $("#modalResumenEstadoCuenta").modal("show");
    };
    $scope._imprimirEstadoCuenta = function () {
        var classActualTitulos = $("#tblEstadoCuentaTitulos").attr("class");
        $("#tblEstadoCuentaTitulos").removeAttr("class");
        $("#tblEstadoCuentaTitulos").addClass("tbconborde");

        var classActualEstado = $("#tblItemsEstadoCuenta").attr("class");
        $("#tblItemsEstadoCuenta").removeAttr("class");
        $("#tblItemsEstadoCuenta").addClass("tbconborde");

        var classActualConsolidado = $("#tblConsolidadoEstadoCuenta").attr("class");
        $("#tblConsolidadoEstadoCuenta").removeAttr("class");
        $("#tblConsolidadoEstadoCuenta").addClass("tbconborde");

        var htmlTablaEstado = $("#dvdEstadoCuenta").html();

        $("#tblEstadoCuentaTitulos").removeAttr("class");
        $("#tblEstadoCuentaTitulos").addClass(classActualTitulos);

        $("#tblItemsEstadoCuenta").removeAttr("class");
        $("#tblItemsEstadoCuenta").addClass(classActualEstado);

        $("#tblConsolidadoEstadoCuenta").removeAttr("class");
        $("#tblConsolidadoEstadoCuenta").addClass(classActualConsolidado);

        var objEstadoCuentaImprimir = {
            TABLA_ESTADO_CUENTA: htmlTablaEstado
        };
        var camposImp = ["TABLA_ESTADO_CUENTA"];
        $.get("/DiseñosReportes/EstadoCuenta.html", function (data) {
            $.each(camposImp, function (index, item) {
                data = data.split("{" + item + "}").join(objEstadoCuentaImprimir[item]);
            });

            var win;
            win = window.open();
            win.document.write(data);
            win.print();
            win.close();
        });


    };    

    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Estado de cuenta", Modulo: "Acudientes", urlToPanelModulo: "EstadoCuenta.aspx", Cod_Mod: "ACUDI", Rol: "ACUDIEstadoCuenta" });
        var id_estudiante = varLocal.Get("id_estudiante");
        if (id_estudiante != null) {
            $scope.obj_consulta.id_estudiante = id_estudiante;
            $scope._traerestudiante();
        } else $("#LbMsg").msgBox({ titulo: "Información", mensaje: "Digite la identificación del estudiante para ver sus liquidaciones", tipo: "info" });
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
    function _consultarSaldo() {
        var sersaldos1 = estudiantessaldosService.GetSaldo($scope.estudiante.identificacion);
        sersaldos1.then(function (pl) {
            $scope.saldos = pl.data;
            byaPage.irFin();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _consultarSaldoVigencia(vig) {
        var sersaldos1 = estudiantessaldosService.GetSaldoVigencia($scope.estudiante.identificacion, vig);
        sersaldos1.then(function (pl) {
            $scope.saldos_vigencias = pl.data;
            byaPage.irFin();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _consultarSaldoVigenciaPeriodo(per) {
        var sersaldos1 = estudiantessaldosService.GetSaldoVigenciaPeriodo($scope.estudiante.identificacion, $scope.saldo.Item, per);
        sersaldos1.then(function (pl) {
            $scope.saldos_vigencias_periodos = pl.data;
            byaPage.irFin();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});