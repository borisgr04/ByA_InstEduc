app.controller('gcNotaCredito', function ($scope, estudiantesService, pagosService, grupospagosService) {
    $scope.estudiante = {};
    $scope.NotasCredito = [];
    $scope._traerestudiante = function () {
        $("#LbMsg").html("");
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != null) {
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                _traerNotasCredito();
            }
            else {
                $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "El estudiante no se encuentra registrado", tipo: false });
                $scope.estudiante = {};
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    $scope._irNuevaNotaCredito = function () {
        window.location.href = "/NotaCredito/NotaCredito/NotaCredito.aspx";
    };
    $scope._irDetalleNota = function (nota) {
        varLocal.Set("id_liquidacion", nota.id);
        window.location.href = "DetalleNotaCredito.aspx";
    };
    
    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Notas Credito", urlToPanelModulo: "gNotasCredito.aspx", Cod_Mod: "PAGOS", Rol: "PAGOSNotaCredito" });
        var id_estudiante = varLocal.Get("id_estudiante");
        if (id_estudiante != null) {
            $scope.estudiante.identificacion = id_estudiante;
            $scope._traerestudiante();
        } else $("#LbMsg").msgBox({ titulo: "Información", mensaje: "Digite la identificación del estudiante para ver sus liquidaciones", tipo: "info" });
    };
    function _traerNotasCredito() {
        var send = pagosService.GestNotasCredito($scope.estudiante.identificacion);
        send.then(function (pl) {
            $scope.NotasCredito = pl.data;
            $.each($scope.NotasCredito, function (index, item) {
                var sum = 0;
                $.each(item.detalles_nota_credito, function (index2, item2) {
                    sum = sum + item2.valor;
                });
                item.ValorTotal = sum;
            });
        }, function () { });
    };
});