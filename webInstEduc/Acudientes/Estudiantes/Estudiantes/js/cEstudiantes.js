app.controller('cEstudiantes', ["$scope", "entidadService", "gradosService", "estudiantesService", "cursosService", "matriculasService", "carteraService", "pagosService", "periodosService", "grupospagosService", "ngTableParams", "$filter", function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, grupospagosService, ngTableParams, $filter) {
    $scope.orden = 0;
    $scope.filtro = "";
    $scope.estudiantes = [];
    $scope.tableEstudiantes = {};
    $scope._nuevo = function () {
        varLocal.Remove("id_estudiante");
        window.location.href = "/Acudientes/Estudiantes/Registro/RegistroEstudiante.aspx";
    };
    $scope._detallesEstudiantes = function (estudiante) {
        varLocal.Set("id_estudiante", estudiante.identificacion);
        window.location.href = "/Acudientes/Estudiantes/Registro/RegistroEstudiante.aspx";
    };
    $scope._traerEstudiantes = function () {
        $scope.orden = 0;
        $scope.estudiantes = [];
        _traerEstudiante();
    };

    _init();
    function _init() {
        _crearTablaEstudiantes();
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Estudiantes", urlToPanelModulo: "Estudiantes.aspx", Cod_Mod: "ACUDI", Rol: "ACUDIEstudiantes" });
        _traerEstudiante();
    };
    function _traerEstudiante() {
        var serEstu = estudiantesService.GetsOrden($scope.orden, $scope.filtro);
        serEstu.then(function (pl) {
            var objRes = pl.data;
            $.each(objRes.lEstudiantes, function(index,item){
                $scope.estudiantes.push(item);
            });

            if (!objRes.Ultimo) {
                $scope.orden = $scope.orden + 1;
                _traerEstudiante();
            }
            $scope.tableEstudiantes.reload();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };

    function _crearTablaEstudiantes() {
        $scope.tableEstudiantes = new ngTableParams({
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
                c ? (c = c.toLowerCase(), f = $scope.lPlantillas.filter(function (a) {
                    return a.nombre_completo.toLowerCase().indexOf(c) > -1
                })) : f = $scope.estudiantes, f = b.sorting() ? $filter("orderBy")(f, b.orderBy()) : f, a.resolve(f.slice((b.page() - 1) * b.count(), b.page() * b.count()))
            }
        });
    };
}]);