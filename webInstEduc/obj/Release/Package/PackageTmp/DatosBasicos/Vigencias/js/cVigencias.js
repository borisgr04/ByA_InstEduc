app.controller('cVigencias', function ($scope, vigenciasService) {
    $scope.vigencias = [];
    $scope.vigencia = {};
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosConceptos")) _guardar();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Vigencias", Modulo: "Datos Basicos", urlToPanelModulo: "Vigencias.aspx", Cod_Mod: "DATOB", Rol: "DATOBVigencia" });
        _traerVigencias();
    };
    function _traerVigencias() {
        var serConcep = vigenciasService.Gets();
        serConcep.then(function (pl) {
            $scope.vigencias = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _esValido(nameForm) {
        var error = false;
        var sRequired = $scope[nameForm].$error.required;
        var numRequired = $scope[nameForm].$error.number;
        if (((sRequired != null) && (sRequired != false)) || ((numRequired != null) && (numRequired != false))) {
            error = true;
        }

        if (error) {
            $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Los campos resaltados en rojo son obligatorios o son erróneos<br/>Una vigencia solo puede tener numeros", tipo: false });
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
        var obj_vigencias = $scope.vigencias;
        $scope.vigencias = [];
        $scope.vigencias.push(e);
        $.each(obj_vigencias, function (index, item) {
            $scope.vigencias.push(item);
        });
    };
    function _guardar() {
        var serConcep = vigenciasService.Post($scope.vigencias);
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
                cadenaRespuesta += "<br/>- " + item.id + " : " + item.Mensaje;
            }
        });
        if (!error) {
            cadenaRespuesta = "Operación Realizada Satisfactoriamente<br/>Se crearon tambien los periodos y tarifas en base al año anterior";
            _traerVigencias();
        }
        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});