// create the controller and inject Angular's $scope
app.controller('cPeriodos', function ($scope, ngTableParams, periodosService, vigenciasService) {
    $scope.periodos = [];
    $scope.estados = [{ id: "AC", name: "Activo" }, { id: "IN", name: "Inactivo" }];
    $scope.vigencias = [];

    $scope.vigencia = parseInt(byaSite.getVigencia());
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosPeriodos")) _guardar();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Periodos", Modulo: "Datos Basicos", urlToPanelModulo: "Periodos.aspx", Cod_Mod: "DATOB", Rol: "DATOBPeriodos" });
        _traerVigencias();
        _traerPeriodos();
    };
    function _traerVigencias() {
        var servicio = vigenciasService.GetAll();
        servicio.then(function (pl) {
            $scope.vigencias = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerPeriodos() {
        var serTasas = periodosService.GetVigencia(parseInt(byaSite.getVigencia()));
        serTasas.then(function (pl) {
            $scope.periodos = [];
            for (i = 0; i < pl.data.length; i++) {
                $scope.periodos[i] = {};
                $scope.periodos[i].id = pl.data[i].id;
                $scope.periodos[i].periodo = parseInt(pl.data[i].periodo);
                $scope.periodos[i].vigencia = parseInt(pl.data[i].vigencia);
                $scope.periodos[i].estado = pl.data[i].estado;
                $scope.periodos[i].vence_dia = parseInt(pl.data[i].vence_dia);
            }

        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
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
    function _nuevo() {
        var e = {vigencia:$scope.vigencia,estado:"AC"};
        $scope.periodos.push(e);
        byaPage.irFin();
    };

    function _guardar() {
        var serTasa = periodosService.Post($scope.periodos);
        serTasa.then(function (pl) {
            _revisarErrores(pl.data);
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _revisarErrores(lResp) {
        var error = false;
        var cadenaRespuesta = "Error: ";
        $.each(lResp, function (index, item) {
            if (item.Error) {
                error = true;
                if (item.id != null) cadenaRespuesta += "<br/>- " + item.id + " : " + item.Mensaje;
                else cadenaRespuesta += "<br/>- Nuevo : " + item.Mensaje;
            }
        });
        if (!error) {
            cadenaRespuesta = "Operación Realizada Satisfactoriamente";
            _traerPeriodos();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});