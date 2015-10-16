// create the controller and inject Angular's $scope
app.controller('cGrados', function ($scope, gradosService) {
    $scope.grados = [];
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosGrados")) _guardar();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Grados", Modulo: "Datos Basicos", urlToPanelModulo: "Grados.aspx", Cod_Mod: "DATOB", Rol: "DATOBGrados" });
        _traerGrados();
    };
    function _traerGrados() {
        var serConcep = gradosService.Gets();
        serConcep.then(function (pl) {
            $scope.grados = pl.data;
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
        $scope.grados.push(e);
        byaPage.irFin();
    };
    function _guardar() {
        var serConcep = gradosService.Post($scope.grados);
        serConcep.then(function (pl) {
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
            _traerGrados();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});