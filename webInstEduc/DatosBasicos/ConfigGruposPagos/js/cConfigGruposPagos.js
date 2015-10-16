// create the controller and inject Angular's $scope
app.controller('cConfigGruposPagos', function ($scope, configGruposPagosService, grupospagosService, conceptosService, vigenciasService) {
    $scope.configGruposP = [];
    $scope.intereses = [{ id: "SI" }, { id: "NO" }];
    $scope.vigencias = [];
    $scope.grupos = [];
    $scope.conceptos = [];
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosConfig")) _guardar();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Configuración de grupos de pago", Modulo: "Datos Basicos", urlToPanelModulo: "ConfigGruposPagos.aspx", Cod_Mod: "DATOB", Rol: "DATOBConfigGruposPagos" });
        _traerVigencias();
        _traerGrupos();
        _traerConceptos();
        _traerConfiguraciones();
    };
    function _traerVigencias() {
        var servicio = vigenciasService.GetAll();
        servicio.then(function (pl) {
            $scope.vigencias = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerGrupos() {
        var servicio = grupospagosService.Gets();
        servicio.then(function (pl) {
            $scope.grupos = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerConceptos() {
        var servicio = conceptosService.Gets();
        servicio.then(function (pl) {
            $scope.conceptos = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerConfiguraciones() {
        var serTasas = configGruposPagosService.GetVigencia(byaSite.getVigencia());
        serTasas.then(function (pl) {
            $scope.configGruposP = pl.data;
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
        $scope.configGruposP.push(e);
        byaPage.irFin();
    };

    function _guardar() {
        var serTasa = configGruposPagosService.Post($scope.configGruposP);
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
            _traerConfiguraciones();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});