// create the controller and inject Angular's $scope
app.controller('cTarifas', function ($scope, ngTableParams, tarifasService, vigenciasService, gradosService, conceptosService, periodosService) {
    $scope.tarifas = [];
    $scope.vigencias = [];//'$filter','$http','$scope','ngTableParams'
    $scope.grados = [];
    $scope.conceptos = [];
    $scope.periodos = [];
    $scope.grado = null;
    $scope.tableParams;

    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosTarifas")) _guardar();
    };
    $scope._cargarTarifas = function () {
        if ($scope.grado != null && $scope.grado != undefined) {
            _traerTarifas();
        }
    }

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Tarifas", Modulo: "Datos Basicos", urlToPanelModulo: "Tarifas.aspx", Cod_Mod: "DATOB", Rol: "DATOBTarifas" });
        _traerVigencias();
        _traerPeriodos();
        _traerConceptos();
        _traerGrados();
        //_traerTarifas();
    };
    function _traerVigencias() {
        var servicio = vigenciasService.GetAll();
        servicio.then(function (pl) {
            $scope.vigencias = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerGrados() {
        var servicio = gradosService.Gets();
        servicio.then(function (pl) {
            $scope.grados = pl.data;
            if ($scope.grados.length > 0) {
                $scope.grado = $scope.grados[0];
                _traerTarifas();
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerPeriodos() {
        var serTasas = periodosService.GetVigencia(parseInt(byaSite.getVigencia()));
        serTasas.then(function (pl) {
            $scope.periodos = pl.data;
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
    function _traerTarifas() {
        $scope.vigencia = byaSite.getVigencia();
        var serTarifas = tarifasService.GetVigGrad($scope.vigencia, $scope.grado.id);
        serTarifas.then(function (pl) {
            $scope.tarifas = [];
            for (i = 0; i < pl.data.length; i++) {
                $scope.tarifas[i] = {};
                $scope.tarifas[i].id = pl.data[i].id;
                $scope.tarifas[i].vigencia = pl.data[i].vigencia;
                $scope.tarifas[i].id_grado = pl.data[i].id_grado;
                $scope.tarifas[i].id_concepto = pl.data[i].id_concepto;
                $scope.tarifas[i].valor = parseFloat( pl.data[i].valor);
                $scope.tarifas[i].periodo_desde = parseInt(pl.data[i].periodo_desde);
                $scope.tarifas[i].periodo_hasta =  parseInt(pl.data[i].periodo_hasta);
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
        var e = { valor: 0, periodo_desde: 2, periodo_hasta: 11, id_grado: $scope.grado.id, vigencia: $scope.vigencia };
        $scope.tarifas.push(e);
        byaPage.irFin();
    };

    function _guardar() {
        var serTasa = tarifasService.Post($scope.tarifas);
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
            _traerTarifas();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});