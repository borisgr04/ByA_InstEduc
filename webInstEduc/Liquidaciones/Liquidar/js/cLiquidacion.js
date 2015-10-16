// create the controller and inject Angular's $scope
app.controller('cLiquidacion', function ($scope, estudiantesService, pagosService, grupospagosService, carteraService, fechaCausacionService) {
    $scope.habGuardar = true;
    $scope.estudiante = {};
    $scope.grupos_pagos = [];
    $scope.grupo_pago = {};
    $scope.verLiquidacion = false;
    $scope.carteras = [];
    $scope.cartera = {};
    $scope.selectLiquidacion = "";
    $scope.liquidacion = {};
    $scope.valor_a_liquidar = 0;
    $scope._traerestudiante = function () {
        $scope.verLiquidacion = false;
        $("#LbMsg").html("");
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != null) {
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                _GetCarteraCausada();
            }
            else {
                alert("El estudiante no se encuentra registrado");
                $scope.estudiante = {};
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    $scope._liquidar = function () {
        if (_esValido("datosLiquidacion")) _Liquidar();
    };
    $scope._getTotalLiquidacion = function () {
        var Total = 0;
        $.each($scope.carteras, function (index, item) {
            Total = Total + item.valor;
        });
        $scope.valor_liquidacion = Total;
        return Total;
    };
    $scope._GetCarteraCausadaValor = function () {
        _GetCarteraCausadaValor();
    };
    $scope._GetCarteraCausada = function () {
        _GetCarteraCausada();
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
        $scope._traerestudiante();
    };
    $scope._Back = function () {
        window.history.back();
    };
    $scope._irDetalleLiquidacion = function () {
        byaMsgBox.confirm("Se ha generado la liquidación No. " + $scope.selectLiquidacion + " <br/> ¿Desea imprimir la liquidación que ha realizado?", function (result) {
            if (result) {  
                varLocal.Set("grupos_pagos", $scope.grupos_pagos);
                varLocal.Set("det_estudiante", $scope.estudiante);
                varLocal.Set("id_liquidacion", $scope.selectLiquidacion);
                window.location.href = "/Liquidaciones/gLiquidaciones/DetalleLiquidacion.aspx";
            }
        });
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Liquidar", Modulo: "Liquidaciones", urlToPanelModulo: "/Liquidaciones/gLiquidaciones/gLiquidaciones.aspx", Cod_Mod: "PAGOS", Rol: "PAGOSGLiquid" });
        _traerGruposPagos();
        FechaActualCausacion();
        
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
    function _getDatosLiquidacion() {
        var e = {};
        e.id_estudiante = $scope.estudiante.identificacion;
        e.id_grupo = $scope.carteras[0].id_grupo;
        e.detalles_pago = $scope.carteras;
        e.fecha = $("#txtFechaPago").val();
        return e;
    };
    function _Liquidar() {
        byaMsgBox.confirm("¿Ha comprobado la información y está seguro de realizar esta liquidación?", function (result) {
            if (result) {
                $scope.habGuardar = false;
                var serLiquidar = pagosService.Liquidar(_getDatosLiquidacion());
                serLiquidar.then(function (pl) {
                    $scope.habGuardar = true;
                    if (pl.data.Error == true) $("#LbMsg").msgBox({ titulo: "Liquidaciones", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
                    else {
                        $("#LbMsg").msgBox({ titulo: "Liquidaciones", mensaje: pl.data.Mensaje + ", No. Liquidación: " + pl.data.id, tipo: !pl.data.Error });
                        $scope.selectLiquidacion = pl.data.id;
                        $scope._irDetalleLiquidacion();
                    }
                }, function (errorPl) {
                    console.log(JSON.stringify(errorPl));
                });
            }
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
    function _traerGruposPagos() {
        var serGrupos = grupospagosService.Gets();
        serGrupos.then(function (pl) {
            $scope.grupos_pagos = pl.data;
            var id_grupo = varLocal.Get("id_grupo");
            if ((id_grupo != null)&&(id_grupo != 0)) {
                $.each($scope.grupos_pagos, function (index, item) {
                    if (item.id == id_grupo) $scope.grupo_pago = item;
                });
            }else $scope.grupo_pago = $scope.grupos_pagos[0];
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _getObjetoConsulta() {
        var e = {};
        e.id_estudiante = $scope.estudiante.identificacion;
        e.ValorPagar = $scope.valor_a_liquidar;
        e.fecha = $("#txtFechaPago").val();
        return e;
    };
    function _GetCarteraCausada() {
        var serCartera = carteraService.GetCarteraCausadaEstudianteL(_getObjetoConsulta());
        serCartera.then(function (pl) {
            $scope.carteras = pl.data;

            $scope.valor_a_liquidar = 0;
            $.each($scope.carteras, function (index, item) {
                $scope.valor_a_liquidar = $scope.valor_a_liquidar + item.valor;
            });
            _seleccionarGrupoPago($scope.carteras[0].id_grupo);
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _GetCarteraCausadaValor() {
        var serCartera = carteraService.GetCarteraCausadaEstudianteValorL(_getObjetoConsulta());
        serCartera.then(function (pl) {
            $scope.carteras = pl.data;
            $scope.valor_a_liquidar = 0;
            $.each($scope.carteras, function (index, item) {
                $scope.valor_a_liquidar = $scope.valor_a_liquidar + item.valor;
            });
            _seleccionarGrupoPago($scope.carteras[0].id_grupo);
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _seleccionarGrupoPago(id_grupo) {
        $.each($scope.grupos_pagos, function (index, item) {
            if (item.id == id_grupo) $scope.grupo_pago = item;
        });
    };
    function FechaActualCausacion() {
        var fecCau = fechaCausacionService.Get();
        fecCau.then(function (pl) {
            $scope.fecha_pago = pl.data;

            var fecSrt = "" + $scope.fecha_pago + "";
            $("#txtFechaPago").val(fecSrt);

            _siInformacionEstudiante();
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});