// create the controller and inject Angular's $scope
app.controller('cCursos', function ($scope, cursosService, gradosService) {
    $scope.cursos = [];
    $scope.grados = [];
    $scope.grado = null;
    $scope.vigencia;
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosCursos")) _guardar();
    };
    $scope._cargarCursos = function () {
        if ($scope.grado != null && $scope.grado != undefined) {
            _traerCursos();
        }
    }
    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Cursos", Modulo: "Datos Basicos", urlToPanelModulo: "Cursos.aspx", Cod_Mod: "DATOB", Rol: "DATOBCursos" });
        _traerGrados();
        //_traerCursos();
    };

    function _traerGrados() {
        var servicio = gradosService.Gets();
        servicio.then(function (pl) {
            $scope.grados = pl.data;
            if ($scope.grados.length > 0) {
                $scope.grado = $scope.grados[0];
                _traerCursos();
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerCursos() {
        if ($scope.grado == null)
            return;
        var serConcep = cursosService.GetsCursosGrado($scope.grado.id);
        serConcep.then(function (pl) {
            $scope.cursos = pl.data;
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
        var e = {id_grado:$scope.grado.id};
        $scope.cursos.push(e);
        byaPage.irFin();
    };
    function _guardar() {
        var serConcep = cursosService.Post($scope.cursos);
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
            _traerCursos();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});