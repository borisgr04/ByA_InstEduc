// create the controller and inject Angular's $scope
app.controller('cTasas', function ($scope, tasasService) {
    $scope.tasas = [];
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosTasas")) _guardar();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Tasas", Modulo: "Datos Basicos", urlToPanelModulo: "Tasas.aspx", Cod_Mod: "DATOB", Rol: "DATOBTasas" });
        _traerTasas();
    };
    function _traerTasas() {
        var serTasas = tasasService.Gets();
        var vig = parseInt(byaSite.getVigencia());
        serTasas.then(function (pl) {
            $scope.tasas = [];
            for (i = 0; i < pl.data.length; i++) {
                pl.data[i].fecha_inicio = new Date(pl.data[i].fecha_inicio);
                pl.data[i].fecha_fin = new Date(pl.data[i].fecha_fin);
                if (vig == pl.data[i].fecha_inicio.getFullYear()) {
                    $scope.tasas.push(pl.data[i]);
                }
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
        var e = {};
        $scope.tasas.push(e);
        byaPage.irFin();
    };

    function _guardar() {
        var serTasa = tasasService.Post($scope.tasas);
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
            _traerTasas();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});