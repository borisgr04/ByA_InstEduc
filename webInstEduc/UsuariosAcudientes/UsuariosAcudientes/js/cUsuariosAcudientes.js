app.controller('cUsuariosAcudientes', ['$scope', 'tercerosService', 'ngTableParams', '$filter', function ($scope, tercerosService, ngTableParams, $filter) {
    $scope.filtro = "";
    $scope.tableUsuariosAcudientes = {}
    $scope.UsuariosAcudientes = [];
    var UsuariosAcudientesAux = [];

    $scope._traerAcudientes = function () {
        _traerAcudientes();
    };


    $scope._removerAcudiente = function (acudiente, index) {
        byaMsgBox.confirm("¿Desea remover éste acudiente de la tabla?", function (result) {
            if (result) {
                $scope.UsuariosAcudientes.splice(index, 1);
                $scope.tableUsuariosAcudientes.reload();
            }
        });
    };

    $scope._crearUsuariosAcudientes = function () {
        _crearUsuariosAcudientes();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Acudientes sin Usuarios", Modulo: "Seguridad", urlToPanelModulo: "UsuariosAcudientes.aspx", Cod_Mod: "SEGUD", Rol: "SEGUDUsuariosAcudientes" });
        _crearTablaAcudientes();
        _traerAcudientes();
    };

    function _traerAcudientes() {
        var acudientes = tercerosService.GetAcudientes();
        acudientes.then(function (pl) {
            $scope.UsuariosAcudientes = pl.data;
            $scope.tableUsuariosAcudientes.reload();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };


    function _crearUsuariosAcudientes() {
        var promisePost = tercerosService.PostUsuariosAcudientes($scope.UsuariosAcudientes);
        promisePost.then(
            function (pl) {
                var respuesta = pl.data;
                $("#LbMsg").msgBox({ titulo: "Asignación usuarios", mensaje: respuesta.Mensaje, tipo: !respuesta.Error });
                if (respuesta.Error == false) {
                    _traerAcudientes();
                    console.log(pl);
                }
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            });
    };

    function _crearTablaAcudientes() {
        $scope.tableUsuariosAcudientes = new ngTableParams({
            page: 1,
            count: 10,
            sorting: {
                nombre_completo: "asc"
            }
        }, {
            filterDelay: 50,
            total: 1000,
            getData: function (a, b) {
                var c = b.filter().search,
                    f = [];
                c ? (c = c.toLowerCase(), f = $scope.UsuariosAcudientes.filter(function (a) {
                    return a.identificacion.toLowerCase().indexOf(c) > -1
                })) : f = $scope.UsuariosAcudientes, f = b.sorting() ? $filter("orderBy")(f, b.orderBy()) : f, a.resolve(f.slice((b.page() - 1) * b.count(), b.page() * b.count()))
            }
        });
    };
}])