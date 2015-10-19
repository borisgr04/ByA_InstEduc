// create the controller and inject Angular's $scope
app.controller('cCarteraConceptos', function ($scope, deudasgradosService, carteraService) {
    $scope.carteras = [];
    $scope.cartera = {};
    $scope.filtro = "";
    var CarterasAux = [];
    $scope._filtrarCarteras = function () {
        _filtrarCarteras();
    };
    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Cartera Conceptos", Modulo: "Consultas", urlToPanelModulo: "CarteraConceptos.aspx", Cod_Mod: "CONSU", Rol: "CONSUCarConc" });
        _traerDeudasGrados();
    };
    function _traerDeudasGrados() {
        var serDeu = carteraService.GetCarteraConceptosEstudiantes();
        serDeu.then(function (pl) {
            $scope.carteras = pl.data;
            CarterasAux = $scope.carteras;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _filtrarCarteras() {
        $scope.carteras = [];
        $.each(CarterasAux, function (index, item) {
            console.log(item.id_estudiante);

            var filtro = "" + $scope.filtro + "";
            var id_estudiante = "" + item.id_estudiante + "";
            var nombre = "" + item.nombre_estudiante + "";

            if ((nombre.toUpperCase().indexOf(filtro.toUpperCase()) >= 0) ||
                (id_estudiante.toUpperCase().indexOf(filtro.toUpperCase()) >= 0)) {
                $scope.carteras.push(item);
            }
        });
    };
});