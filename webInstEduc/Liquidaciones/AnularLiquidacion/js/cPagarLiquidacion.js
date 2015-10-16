// create the controller and inject Angular's $scope
app.controller('cPagarLiquidacion', function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, grupospagosService) {
    $scope.habGuardar = true;
    $scope.estudiante = {};
    $scope.nro_liquidacion;
    $scope.liquidacion = {};
    $scope.grupos_pagos = [];
    $scope.entidad = {};
    $scope.verLiquidacion = false;
    $scope.grupo_pago = {};
    $scope.mostrarInfo = false;
    $scope._getTotalLiquidacion = function () {
        var Total = 0;
        $.each($scope.liquidacion.detalles_pago, function (index, item) {
            Total = Total + item.valor;
        });
        $scope.valor_liquidacion = Total;
        return Total;
    };
    $scope._anularLiquidacion = function () {
        _anularLiquidacion();
    };
    $scope._traerLiquidacion = function () {
        _traerLiquidacion();
    };
    $scope._limpiar = function () {
        $scope.mostrarInfo = false;
        $scope.verLiquidacion = false;
        $scope.liquidacion.detalles_pago = [];
        $scope.nro_liquidacion = "";
    };
    $scope._Back = function () {
        window.history.back();
    };
    $scope._print = function () {

        var printContents = document.getElementById("print").innerHTML;

        //var ancho = document.getElementById('print').offsetWidth;
        //var alto = document.getElementById('print').offsetHeight;
        //alert("ancho: " + ancho + "  ---- alto: " + alto);

        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
    };

    _init();
    function _init() {
        $scope.liquidacion.detalles_pago = [];
        byaSite.SetModuloP({ TituloForm: "Anular liquidación", Modulo: "Liquidaciones", urlToPanelModulo: "gLiquidaciones.aspx", Cod_Mod: "PAGOS", Rol: "PAGOSAnuLiq" });
        _traerInformacionEntidad();
        if ((varLocal.Get("det_estudiante") != null) && (varLocal.Get("id_liquidacion") != null)) {
            $scope.estudiante = varLocal.Get("det_estudiante");
            varLocal.Remove("det_estudiante");
            varLocal.Set("id_estudiante2", $scope.estudiante.identificacion);
            $scope.nro_liquidacion = varLocal.Get("id_liquidacion");
            varLocal.Remove("id_liquidacion");
            $scope.grupos_pagos = varLocal.Get("grupos_pagos");            
            _traerLiquidacion();
            $scope.verLiquidacion = true;
        }
        else {
            $("#LbMsg").msgBox({ titulo: "Información", mensaje: "Digite el número de la liquidación que desea pagar", tipo: "info" });
            _traerGruposPagos();
        }
    };
    function _traerInformacionEntidad() {
        var serEnti = entidadService.Get();
        serEnti.then(function (pl) {
            $scope.entidad = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    }
    function _traerGruposPagos() {
        var serGrupos = grupospagosService.Gets();
        serGrupos.then(function (pl) {
            $scope.grupos_pagos = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerLiquidacion() {
        var serLiqui = pagosService.GetLiquidacion($scope.nro_liquidacion);
        serLiqui.then(function (pl) {
            if (pl.data.id != 0) {
                $scope.liquidacion = pl.data;
                $scope.liquidacion.nombre_estado = $scope.liquidacion.estado == "LI" ? "LIQUIDADO" : $scope.liquidacion.estado == "PA" ? "PAGADA" : "ANULADA";
                varLocal.Set("id_grupo", $scope.liquidacion.id_grupo);
                $.each($scope.grupos_pagos, function (index, item) {
                    if (item.id == $scope.liquidacion.id_grupo) $scope.grupo_pago = item;
                });
                $("#qrNoLiq").html(update_qrcode("" + $scope.liquidacion.id + ""));
                $scope.estudiante.identificacion = $scope.liquidacion.id_estudiante;
                $scope.estudiante.nombre_completo = $scope.liquidacion.nombre_estudiante;
                $scope.verLiquidacion = true;
                if ($scope.mostrarInfo == false) $("#LbMsg").html("");
            } else {
                $scope.verLiquidacion = false;
                $("#LbMsg").msgBox({ titulo: "Información", mensaje: "No se ha encontrado la liquidación", tipo: false });
            }
        }, function (errorPl) {
            Console.log(JSON.stringify(errorPl));
        });
    };
    function _anularLiquidacion() {
        byaMsgBox.confirm("¿Esta seguro de querer anular la liquidación No. " + $scope.liquidacion.id + "?", function (result) {
            if (result) {
                $scope.habGuardar = false;
                var serAnu = pagosService.AnularLiquidacion($scope.liquidacion.id);
                serAnu.then(function (pl) {
                    $scope.habGuardar = true;
                    if (pl.data.Error == false) {
                        $("#LbMsg").msgBox({ titulo: "Detalles de liquidacion", mensaje: pl.data.Mensaje + ", se anulo la liquidación No. " + $scope.liquidacion.id, tipo: !pl.data.Error });
                    } else $("#LbMsg").msgBox({ titulo: "Detalles de liquidacion", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    $scope.mostrarInfo = true;
                    _traerLiquidacion();
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
        });
    };
});