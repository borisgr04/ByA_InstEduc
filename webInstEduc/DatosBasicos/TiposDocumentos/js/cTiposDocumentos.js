// create the controller and inject Angular's $scope
app.controller('cTiposDocumentos', function ($scope, tipoDocumentosService) {
    $scope.tipoDocumentos = [];
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosTipoDocumentos")) _guardar();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Tipo Documentos", Modulo: "Datos Basicos", urlToPanelModulo: "TipoDocumentos.aspx", Cod_Mod: "DATOB", Rol: "DATOBTiposDocumentos" });
        _traerTipoDocumentos();
    };
    function _traerTipoDocumentos() {
        var serTipDocum = tipoDocumentosService.Gets();
        serTipDocum.then(function (pl) {
            $scope.tipoDocumentos = pl.data;
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
        $scope.tipoDocumentos.push(e);
        byaPage.irFin();
    };
    $(function() {
        $('input').focusout(function() {
            // Uppercase-ize contents
            this.value = this.value.toLocaleUpperCase();
        });
    });

    function _guardar() {
        var serTDocu = tipoDocumentosService.Post($scope.tipoDocumentos);
        serTDocu.then(function (pl) {
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
            _traerTipoDocumentos();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});