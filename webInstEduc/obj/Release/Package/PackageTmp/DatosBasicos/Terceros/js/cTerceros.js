app.controller('cTerceros', ["$scope", "ngTableParams", "$filter", "tercerosService", function ($scope, ngTableParams, $filter, tercerosService) {
    $scope.orden = 0;
    $scope.terceros = [];
    $scope.tercero = {};
    $scope.tableTerceros = {};
    $scope.editar = false;
    $scope._verDetallesTercero = function (ter) {
        $scope.editar = true;
        _verDetallesTercero(ter);
    };
    $scope._nuevo = function () {
        $scope.editar = false;
        _nuevo();
    };
    $scope._guardo = function () {
        if (_esValido("datosTercero")) {
            if ($scope.editar) _guardarModificado();
            else _guardarNuevo();
        }
    };

    _init();
    function _init() {        
        _crearTablaTerceros();
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Terceros", urlToPanelModulo: "Terceros.aspx", Cod_Mod: "DATOB", Rol: "DATOBTerceros" });
        _traerTerceros();
    };
    function _traerTerceros() {
        var serTer = tercerosService.Gets();
        serTer.then(function (pl) {
            $scope.terceros = pl.data;
            $scope.tableTerceros.reload();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _crearTablaTerceros() {
        $scope.tableTerceros = new ngTableParams({
            page: 1,
            count: 10
        }, {
            filterDelay: 50,
            total: 1000,
            getData: function (a, b) {
                var c = b.filter().search,
                    f = [];
                c ? (c = c.toLowerCase(), f = $scope.terceros.filter(function (a) {
                })) : f = $scope.terceros, f = b.sorting() ? $filter("orderBy")(f, b.orderBy()) : f, a.resolve(f.slice((b.page() - 1) * b.count(), b.page() * b.count()))
            }
        });
    };
    function _verDetallesTercero(ter) {
        $scope.tercero = ter;
        $("#modalRegistroTercero").modal("show");
    };
    function _nuevo() {
        $scope.tercero = {};
        $("#modalRegistroTercero").modal("show");
    };
    function _guardarNuevo() {
        var serPost = tercerosService.Post($scope.tercero);
        serPost.then(function (pl) {
            var res = pl.data;
            if (res.Error) $("#msgModal").msgBox({ titulo: "", mensaje: res.Mensaje, tipo: !res.Error });
            else {
                $("#LbMsg").msgBox({ titulo: "", mensaje: res.Mensaje, tipo: !res.Error });
                $("#modalRegistroTercero").modal("hide");
                _traerTerceros();
            }            
        });
    };
    function _guardarModificado() {
        var serPut = tercerosService.Put($scope.tercero);
        serPut.then(function (pl) {
            var res = pl.data;
            if (res.Error) $("#msgModal").msgBox({ titulo: "", mensaje: res.Mensaje, tipo: !res.Error });
            else {
                $("#LbMsg").msgBox({ titulo: "", mensaje: res.Mensaje, tipo: !res.Error });
                $("#modalRegistroTercero").modal("hide");
                _traerTerceros();
            }
        });
    };
    function _esValido(nameForm) {
        var error = false;
        var sRequired = $scope[nameForm].$error.required;
        if ((sRequired != null) && (sRequired != false)) {
            error = true;
        }

        if (error) {
            $("#msgPartido").msgBox({ titulo: "Error:", mensaje: "Los campos resaltados en rojo son obligatorios...", tipo: false });
            $("input.ng-invalid").css("border", " 1px solid red");
            $("select.ng-invalid").css("border", " 1px solid red");
            return !error;
        } else {
            $("input.ng-valid").css("border", "");
            $("select.ng-valid").css("border", "");
            $("#msgPartido").html("");
            return true;
        }
    };
}]);