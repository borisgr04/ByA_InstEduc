// create the controller and inject Angular's $scope
app.controller('cConsultaPagosEstudiante', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, fechaCausacionService) {
    $scope.obj_consulta = {};
    $scope.estudiante = {};
    $scope.pagos = [];
    $scope.pago = {};
    $scope.detalles = [];
    $scope.detalle = {};
    $scope.pagoSel = {};
    $scope.Consultar = function () {
        if (_esValido('datosConsulta')) Consultar();
    };
    $scope._traerestudiante = function () {
        var serEstu = estudiantesService.Get($scope.obj_consulta.id_estudiante);
        serEstu.then(function (pl) {
            if (pl.data.id != 0) {
                $("#LbMsg").html("");
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                $scope.Consultar();
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Estudiantes", mensaje: "El estudiante no se encuantra registrado", tipo: false });
                $scope.estudiante = {};
                $scope.obj_consulta.id_estudiante = null;
            }
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    $scope._sumarValorDetalles = function (pago) {
        var Total = 0;
        $.each(pago.detalles_pago, function (index, item) {
            Total += item.valor;
        });
        return Total;
    };
    $scope._valorPagos = function () {
        var Total = 0;
        $.each($scope.pagos, function (index, item) {
            $.each(item.detalles_pago, function (index2, item2) {
                Total += item2.valor;
            });
        });
        return Total;
    };
    $scope._verDetalles = function (pago) {
        $scope.pagoSel = pago;
        $scope.detalles = pago.detalles_pago;
        byaPage.irFin();
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.obj_consulta.id_estudiante = "";
        $scope.estudiante = {};
        $scope._traerestudiante();
    };
    $scope._printPagos = function () {
        var classActual = $("#tblPagos").attr("class");
        $("#tblPagos").removeAttr("class");
        $("#tblPagos").addClass("tbconborde");
        var htmlPagos = $("#printPagos").html();
        $("#tblPagos").removeAttr("class");
        $("#tblPagos").addClass(classActual);

        var objListadoPagosImprimir = {
            FECHA_INICIAL: $("#fch_inicial").val(),
            FECHA_FINAL: $("#fch_final").val(),
            TABLA_PAGOS: htmlPagos
        };

        $.get("/DiseñosReportes/ListadoPagos.html", function (data) {
            $.each(camposImp, function (index, item) {
                data = data.split("{" + item + "}").join(objListadoPagosImprimir[item]);
            });

            var win;
            win = window.open();
            win.document.write(data);
            win.print();
            win.close();
        });
    };
    var camposImp = [];

    _init();

    function llenarcamposImp() {
        camposImp = new Array();
        camposImp.push("FECHA_INICIAL");
        camposImp.push("FECHA_FINAL");
        camposImp.push("TABLA_PAGOS");
    };
    function _init() {
        llenarcamposImp();
        FechaActualCausacion();        
        byaSite.SetModuloP({ TituloForm: "Pagos Estudiante", Modulo: "Consultas", urlToPanelModulo: "PagosEstudiante.aspx", Cod_Mod: "CONSU", Rol: "CONSULstPagos" });
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
    function Consultar() {
        $scope.obj_consulta.id_estudiante = "";
        var serPago = pagosService.PagosEstudiante($scope.obj_consulta);
        serPago.then(function (pl) {
            $scope.pagos = pl.data;
            $.each($scope.pagos, function (index, item) {
                item.detalles_pago2 = [];
                item.detalles_pago2.push(item.detalles_pago[0]);

                item.detalles_pago3 = [];
                $.each(item.detalles_pago, function (index2, item2) {                    
                    if (index2 > 0) {                        
                        item.detalles_pago3.push(item2);
                    }
                });
            });
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function FechaActualCausacion() {
        var fecCau = fechaCausacionService.Get();
        fecCau.then(function (pl) {
            $scope.fecha_causacion = pl.data;
            var srtDate = "" + $scope.fecha_causacion + "";
            var values = srtDate.split("-");
            var año = values[0];
            var mes = values[1];
            var dia =("" + values[2] + "").split("T")[0];
            var date = new Date(año,mes - 1,dia);
            var dateInicial = date;

            $scope.obj_consulta.FechaInicial = dateInicial;
            $scope.obj_consulta.FechaFinal = date;

            var id_estudiante = varLocal.Get("id_estudiante");
            if (id_estudiante != null) {
                $scope.obj_consulta.id_estudiante = id_estudiante;
                $scope._traerestudiante();
            } else $("#LbMsg").msgBox({ titulo: "Información", mensaje: "Digite la identificación del estudiante para ver sus liquidaciones", tipo: "info" });
        },
        function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});