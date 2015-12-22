// create the controller and inject Angular's $scope
app.controller('cLiquidaciones', function ($scope, estudiantesService, pagosService, grupospagosService) {
    $scope.estudiante = {};
    $scope.grupos_pagos = [];
    $scope.grupo_pago = {};
    $scope.verLiquidaciones = false;
    $scope.liquidacion_pagada = false;    
    $scope.verLiquidacion = false;
    $scope.liquidarPeriodo = false;
    $scope.liquidacion = {};
    $scope.liquidaciones = [];
    $scope.periodo_desde = "";
    $scope.periodo_hasta = "";
    $scope.periodos_desde = [];
    $scope.noSeleccionadoLiq = true;
    $scope.periodos_hasta = [];
    $scope.data = { selectLiquidacion: "" };
    $scope._traerestudiante = function () {
        $scope.verLiquidaciones = false;
        $scope.verLiquidacion = false;
        $("#LbMsg").html("");
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != null) {
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                _generarPeriodos();
                _traerLiquidacionesEstudiante();
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "El estudiante no se encuentra registrado", tipo: false });
                $scope.estudiante = {};
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    $scope._traerLiquidacionesEstudiante = function () {
        if (byaSite.getVigencia() != null) {
            if ($scope.estudiante.id != null)  _traerLiquidacionesEstudiante();
            else $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Debe digitar la identificacion de un estudiante", tipo: false });
        }
        else $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Debe digitar una vigencia", tipo: false });
    };
    $scope._getTotalLiquidacion = function (liqui) {
        var total = 0;
        $.each(liqui.detalles_pago, function (index, item) {
            total = total + item.valor;
        });
        return total;
    };    
    $scope._getNombreEstadoLiquidacion = function (liqui) {
        return liqui.estado == "LI" ? "LIQUIDADO" : liqui.estado == "PA" ? "PAGADO" : "";
    };
    $scope._getTotalDetalles = function () {
        var Total = 0;
        $.each($scope.liquidacion.detalles_pago, function (index, item) {
            Total = Total + item.valor;
        });

        return Total;
    };
    $scope._irLiquidar = function () {
        varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
        varLocal.Set("id_grupo", $scope.grupo_pago.id);
        window.location.href = "/Acudientes/Liquidaciones/Liquidar/Liquidacion.aspx";
    };
    $scope._irPagar = function () {
        varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
        varLocal.Set("id_grupo", $scope.grupo_pago.id);
        window.location.href = "/Liquidaciones/PagarSolo/PagarSolo.aspx";
    };
    $scope._irPagarLiquidacion = function () {
        varLocal.Set("grupos_pagos", $scope.grupos_pagos);
        varLocal.Set("det_estudiante", $scope.estudiante);
        varLocal.Set("id_liquidacion", $scope.data.selectLiquidacion.id);
        window.location.href = "/Liquidaciones/PagarLiquidacion/PagarLiquidacion.aspx";
    };
    $scope._irDetalleLiquidacion = function () {
        varLocal.Set("grupos_pagos", $scope.grupos_pagos);
        varLocal.Set("det_estudiante", $scope.estudiante);
        varLocal.Set("id_liquidacion", $scope.data.selectLiquidacion.id);
        window.location.href = "/Acudientes/Liquidaciones/gLiquidaciones/DetalleLiquidacion.aspx";
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
        $scope._traerestudiante();
    };
    $scope._selectLiquidacion = function () {
        if ($scope.data.selectLiquidacion != null) {
            $scope.noSeleccionadoLiq = false;
        }
    };
    $scope._irAnularLiquidacion = function () {
        varLocal.Set("grupos_pagos", $scope.grupos_pagos);
        varLocal.Set("det_estudiante", $scope.estudiante);
        varLocal.Set("id_liquidacion", $scope.data.selectLiquidacion.id);
        window.location.href = "/Acudientes/Liquidaciones/AnularLiquidacion/AnularLiquidacion.aspx";
    };
    $scope._irAnularPago = function () {
        varLocal.Set("grupos_pagos", $scope.grupos_pagos);
        varLocal.Set("det_estudiante", $scope.estudiante);
        varLocal.Set("id_liquidacion", $scope.data.selectLiquidacion.id);
        window.location.href = "/Liquidaciones/AnularPago/AnularPago.aspx";
    };


    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Liquidaciones", Modulo: "Acudientes", urlToPanelModulo: "gLiquidaciones.aspx", Cod_Mod: "ACUDI", Rol: "ACUDILiquidaciones" });
        _traerGruposPagos();
        var id_estudiante = varLocal.Get("id_estudiante");
        if (id_estudiante != null) {
            $scope.estudiante.identificacion = id_estudiante;
            $scope._traerestudiante();
        }else $("#LbMsg").msgBox({ titulo: "Información", mensaje: "Digite la identificación del estudiante para ver sus liquidaciones", tipo: "info" });
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
    function _getDatosTraerLiquidacion() {
        var e = {};
        e.id_estudiante = $scope.estudiante.id;
        e.periodo_desde = $scope.periodo_desde;
        e.periodo_hasta = $scope.periodo_hasta;
        return e;
    };
    function _traerLiquidacion() {
        var serLiq = pagosService.LiquidarObtener(_getDatosTraerLiquidacion());
        serLiq.then(function (pl) {
            if (pl.data.RespuestaGenerarLiquidacion.Error) $("#LbMsg").msgBox({ titulo: "Liquidación", mensaje: pl.data.RespuestaGenerarLiquidacion.Mensaje, tipo: !pl.data.RespuestaGenerarLiquidacion.Error });
            else {
                $scope.estudiante.periodo_hasta_ultima_liquidacion = $scope.periodo_hasta;
                _generarPeriodos();

                $scope.liquidacion = pl.data.Liquidacion;
                $scope.liquidacion.nombre_estado = $scope.liquidacion.estado == "LI" ? "LIQUIDADO" : $scope.liquidacion.estado == "PA" ? "PAGADO" : "";

                if ($scope.liquidacion.estado == "PA") $scope.liquidacion_pagada = true;
                else $scope.liquidacion_pagada = false;

                $scope.verLiquidaciones = false;
                $scope.verLiquidacion = true;
                $scope.liquidarPeriodo = false;
                $("#LbMsg").msgBox({ titulo: "Liquidación", mensaje: "Se liquidaron los periodos desde " + $scope.liquidacion.perido_desde + " hasta " + $scope.liquidacion.perido_hasta, tipo: true });
                byaPage.irFin();
                
                var serLiqEst = pagosService.GetsLiquidacionesEstudiante($scope.estudiante.id, byaSite.getVigencia());
                serLiqEst.then(function (pl) {
                    $scope.liquidaciones = pl.data;
                }, function (errorPl) {
                });
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _generarPeriodos() {
        $scope.periodos_desde = [];
        $scope.periodos_hasta = [];
        if ($scope.estudiante.periodo_hasta_ultima_liquidacion != 12) {
            if ($scope.estudiante.periodo_hasta_ultima_liquidacion == null) {
                for (i = 1; i <= 12; i++) {
                    $scope.periodos_desde.push(i);
                    $scope.periodos_hasta.push(i);
                }
            } else {
                var ini = parseInt($scope.estudiante.periodo_hasta_ultima_liquidacion) + 1;
                for (i = ini; i <= 12; i++) {
                    $scope.periodos_desde.push(i);
                    $scope.periodos_hasta.push(i);
                }
            }
        } else {
            //$("#LbMsg").msgBox({ titulo: "Liquidación", mensaje: "El estudiante indicado ya ha liquidado hasta el ultimo periodo", tipo: false });
        }
        $scope.periodo_desde = $scope.periodos_desde[0];
        $scope.periodo_hasta = $scope.periodos_hasta[0];
    };
    function _traerGruposPagos() {
        var serGrupos = grupospagosService.Gets();
        serGrupos.then(function (pl) {
            $scope.grupos_pagos = pl.data;
            $scope.grupos_pagos.push({ id: 0, nombre: "Todos" })
            var id_grupo = 0;
            var ban = false;
            $.each($scope.grupos_pagos, function (index, item) {
                if (id_grupo == item.id) { $scope.grupo_pago = item; ban = true; }
            });
            if (!ban) $scope.grupo_pago = $scope.grupos_pagos[0];
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerLiquidacionesEstudiante() {
        if (($scope.grupo_pago.id != null) && ($scope.grupo_pago.id != 0)) {
            var serLiqEst = pagosService.GetsLiquidacionesEstudiante($scope.estudiante.identificacion, $scope.grupo_pago.id);
            serLiqEst.then(function (pl) {
                $scope.liquidaciones = pl.data;
                $scope.verLiquidaciones = true;
            }, function (errorPl) {
                console.log(JSON.stringify(errorPl));
            });
        } else {
            var serLiqEst = pagosService.GetsLiquidacionesEstudianteSG($scope.estudiante.identificacion);
            serLiqEst.then(function (pl) {
                $scope.liquidaciones = pl.data;
                $scope.verLiquidaciones = true;
            }, function (errorPl) {
                console.log(JSON.stringify(errorPl));
            });
        }
    };
});