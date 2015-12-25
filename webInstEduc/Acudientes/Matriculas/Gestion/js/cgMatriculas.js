app.controller('cgMatriculas', ["$scope", "entidadService", "gradosService", "estudiantesService", "cursosService", "matriculasService", "carteraService", "pagosService", "periodosService", "grupospagosService", "ngTableParams", "$filter", function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, grupospagosService, ngTableParams, $filter) {
    $scope.filtro = "";
    $scope.matriculas = [];
    $scope.tableMatriculas = {};
    $scope.grados = [];
    $scope.grado = {};
    $scope.cursos = [];
    $scope.curso = {};
    $scope.objConsulta = { Filtro : "", Curso : null, Vigencia : byaSite.getVigencia()};
    $scope._buscarCurso = function () {
        _buscarCurso();
    };
    $scope._nuevo = function () {
        window.location.href = "/Acudientes/Matriculas/Matricular/Matricular.aspx";
    };
    $scope._detallesMatricula = function (matricula) {
        varLocal.Set("matricula_detalles", matricula);
        window.location.href = "/Acudientes/Matriculas/Gestion/DetallesMatricula.aspx";
    };
    $scope._traerMatriculas = function () {
        _traerMatriculas();
    };
    $scope._print = function () {

        var printContents = document.getElementById("print").innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
    };

    _init();
    function _init() {
        _traerGrados();
        _crearTablaMatriculas();
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Matriculas", urlToPanelModulo: "gMatriculas.aspx", Cod_Mod: "ACUDI", Rol: "ACUDIMatriculas" });
        _traerMatriculas();
    };
    function _traerMatriculas() {
        var serEstu = matriculasService.Gets($scope.objConsulta);
        serEstu.then(function (pl) {
            $scope.matriculas = pl.data;
            $scope.tableMatriculas.reload();
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };

    function _crearTablaMatriculas() {
        $scope.tableMatriculas = new ngTableParams({
            page: 1,
            count: 10
        }, {
            filterDelay: 50,
            total: 1000,
            getData: function (a, b) {
                var c = b.filter().search,
                    f = [];
                c ? (c = c.toLowerCase(), f = $scope.matriculas.filter(function (a) {
                })) : f = $scope.matriculas, f = b.sorting() ? $filter("orderBy")(f, b.orderBy()) : f, a.resolve(f.slice((b.page() - 1) * b.count(), b.page() * b.count()))
            }
        });
    };
    function _traerGrados() {
        var promiseGet = gradosService.Gets();
        promiseGet.then(function (pl) {
            $scope.grados = pl.data;
        },
        function (errorPl) {
            console.log(errorPl);
        });
    };
    function _buscarCurso() {
        var serCurso = cursosService.GetsCursosGrado($scope.grado.id);
        serCurso.then(function (pl) {
            $scope.cursos = pl.data;
            $scope.objConsulta.Curso = $scope.cursos[0].id;
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
}]);