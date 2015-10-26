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
    $scope.estadocuentaresumen = {};
    $scope.spensiones = 0;
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
            _calcularSaldoPensiones();
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
        $.get("/DiseñosReportes/ImpLiq.html", function (data) {
            var win;
            win = window.open();
            win.document.write(data);
            win.print();
            win.close();
        });
    };
    

    _init();

    
    function _calcularSaldoPensiones(){
        $scope.spensiones = 0;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension2.valor - $scope.estadocuentaresumen.pension2.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension3.valor - $scope.estadocuentaresumen.pension3.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension4.valor - $scope.estadocuentaresumen.pension4.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension5.valor - $scope.estadocuentaresumen.pension5.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension6.valor - $scope.estadocuentaresumen.pension6.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension7.valor - $scope.estadocuentaresumen.pension7.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension8.valor - $scope.estadocuentaresumen.pension8.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension9.valor - $scope.estadocuentaresumen.pension9.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension10.valor - $scope.estadocuentaresumen.pension10.pagado;
        $scope.spensiones = $scope.spensiones + $scope.estadocuentaresumen.pension11.valor - $scope.estadocuentaresumen.pension11.pagado;
    };
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Estado de cuenta", Modulo: "Consultas", urlToPanelModulo: "EstadoCuenta.aspx", Cod_Mod: "CONSU", Rol: "CONSUEstCuenta" });
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