// create the controller and inject Angular's $scope
app.controller('cPagarLiquidacion', function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, grupospagosService, fechaCausacionService) {
    $scope.habGuardar = true;
    $scope.VerificadoIntereses = false;
    $scope.estudiante = {};
    $scope.nro_liquidacion;
    $scope.liquidacion = {};
    $scope.grupos_pagos = [];
    $scope.entidad = {};
    $scope.verLiquidacion = false;
    $scope.grupo_pago = {};
    $scope.fecha_pago = {};
    $scope.causar_intereses = false;
    $scope.observacion = "";
    $scope._esPagar = false;
    $scope._OperacionIntereses = function (value) {
        $("#modalRptaPago").modal("hide");
        $scope.causar_intereses = value == 'causar' ? true : false;
        $scope.VerificadoIntereses = true;
        _RealizarPago();
    };
    $scope._getTotalLiquidacion = function () {
        var Total = 0;
        $.each($scope.liquidacion.detalles_pago, function (index, item) {
            Total = Total + item.valor;
        });
        $scope.valor_liquidacion = Total;
        return Total;
    };
    $scope._pagar = function () {
        _pagar();
    };
    $scope._traerLiquidacion = function () {
        _traerLiquidacion();
    };
    $scope._limpiar = function () {
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
        //FechaActualCausacion();
        $scope.liquidacion.detalles_pago = [];
        byaSite.SetModuloP({ TituloForm: "Pagar liquidación", Modulo: "Liquidaciones", urlToPanelModulo: "gLiquidaciones.aspx", Cod_Mod: "PAGOS", Rol: "PAGOSGLiquid" });
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
                if (!$scope._esPagar) $("#LbMsg").html("");
                $scope._esPagar = false;
                $scope.fecha_pago = $scope.liquidacion.fecha;
                var fecSrt = "" + $scope.fecha_pago + "";
                $("#txtFechaActual").val(fecSrt);
            } else {
                $scope.verLiquidacion = false;
                $("#LbMsg").msgBox({ titulo: "Información", mensaje: "No se ha encontrado la liquidación", tipo: false });
            }
        }, function (errorPl) {
            Console.log(JSON.stringify(errorPl));
        });
    };
    function _pagar() {
        $scope.VerificadoIntereses = false;
        byaMsgBox.confirm("¿Ha comprobado la información y está seguro de realizar este pago?", function (result) {
            if (result) {                
                _RealizarPago();
            }
        });
    };
    function _RealizarPago() {
        $scope.habGuardar = false;
        var e = {};
        e.id = $scope.liquidacion.id;
        e.observacion = $scope.observacion;
        e.VerificadoIntereses = $scope.VerificadoIntereses;
        e.causar_intereses = $scope.causar_intereses;
        e.fecha_pago = $("#txtFechaActual").val();
        var serLiquidar = pagosService.PagarLiquidacion(e);
        serLiquidar.then(function (pl) {
            $scope.habGuardar = true;
            if (pl.data.id == "false") {
                $("#LbMsg").msgBox({ titulo: "Liquidaciones", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                $scope._esPagar = true;
                $scope.VerificadoIntereses = false;
                _traerLiquidacion();
            } else {
                $("#dvdRptaPago").html(pl.data.Mensaje);
                $("#modalRptaPago").modal("show");                
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    }
    function FechaActualCausacion() {
        var fecCau = fechaCausacionService.Get();
        fecCau.then(function (pl) {
            $scope.fecha_pago = pl.data;

            var fecSrt = "" + $scope.fecha_pago + "";
            $("#txtFechaActual").val(fecSrt);
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});