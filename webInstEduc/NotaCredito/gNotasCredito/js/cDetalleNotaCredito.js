// create the controller and inject Angular's $scope
app.controller('cDetalleNotaCredito', function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService) {
    $scope.estudiante = {};
    $scope.entidad = {};
    $scope.nro_liquidacion;
    $scope.liquidacion = {};
    $scope.grupos_pagos = [];
    $scope.grupo_pago = {};
    $scope._getTotalLiquidacion = function () {
        var Total = 0;
        $.each($scope.liquidacion.detalles_nota_credito, function (index, item) {
            Total = Total + item.valor;
        });
        $scope.valor_liquidacion = Total;
        return Total;
    };
    $scope._print = function () {

        var printContents = document.getElementById("print").innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
    };
    $scope._anularLiquidacion = function () {
        _anularLiquidacion();
    };
    $scope._anularPago = function () {
        _anularPago();
    };
    $scope._Back = function () {
        window.history.back();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Notas Credito", urlToPanelModulo: "gNotasCredito.aspx", Cod_Mod: "PAGOS", Rol: "PAGOSNotaCredito" });
        $scope.estudiante = varLocal.Get("det_estudiante");
        varLocal.Set("id_estudiante2", $scope.estudiante.identificacion);
        $scope.nro_liquidacion = varLocal.Get("id_liquidacion");
        $scope.grupos_pagos = varLocal.Get("grupos_pagos");
        _traerInformacionEntidad();
        _traerLiquidacion();
    };
    function _traerLiquidacion() {
        var serLiqui = pagosService.GetNotaCredito($scope.nro_liquidacion);
        serLiqui.then(function (pl) {
            $scope.liquidacion = pl.data;
            $scope.liquidacion.nombre_estado = $scope.liquidacion.estado == "LI" ? "LIQUIDADO" : $scope.liquidacion.estado == "PA" ? "PAGADA" : "ANULADA";
            $("#qrNoLiq").html(update_qrcode("" + $scope.liquidacion.id + ""));
            varLocal.Set("id_grupo", $scope.liquidacion.id_grupo);
            $.each($scope.grupos_pagos, function (index, item) {
                if (item.id == $scope.liquidacion.id_grupo) $scope.grupo_pago = item;
            });
        }, function (errorPl) {
            Console.log(JSON.stringify(errorPl));
        });
    };
    function _traerInformacionEntidad() {
        var serEnti = entidadService.Get();
        serEnti.then(function (pl) {
            $scope.entidad = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    }
    function _anularLiquidacion() {
        byaMsgBox.confirm("¿Esta seguro de querer anular la liquidación No. " + $scope.liquidacion.id + "?", function (result) {
            if (result) {
                var serAnu = pagosService.AnularLiquidacion($scope.liquidacion.id);
                serAnu.then(function (pl) {
                    if (pl.data.Error == false) {
                        $("#LbMsg").msgBox({ titulo: "Detalles de liquidacion", mensaje: pl.data.Mensaje + ", se anulo la liquidación No. " + $scope.liquidacion.id, tipo: !pl.data.Error });
                    } else $("#LbMsg").msgBox({ titulo: "Detalles de liquidacion", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    _traerLiquidacion();
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
        });
    };
    function _anularPago() {
        byaMsgBox.confirm("¿Esta seguro de querer anular el pago No. " + $scope.liquidacion.id + "?", function (result) {
            if (result) {
                var serAnu = pagosService.AnularPago($scope.liquidacion.id);
                serAnu.then(function (pl) {
                    if (pl.data.Error == false) {
                        $("#LbMsg").msgBox({ titulo: "Detalles de liquidacion", mensaje: pl.data.Mensaje + ", se anulo el pago No. " + $scope.liquidacion.id, tipo: !pl.data.Error });
                    } else $("#LbMsg").msgBox({ titulo: "Detalles de liquidacion", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    _traerLiquidacion();
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
        });
    };
});