// create the controller and inject Angular's $scope
app.controller('cCarteraConceptos', function ($scope, deudasgradosService, carteraService) {
    $scope.carteras = [];
    $scope.cartera = {};
    
    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Cartera Conceptos", Modulo: "Consultas", urlToPanelModulo: "CarteraConceptos.aspx", Cod_Mod: "CONSU", Rol: "CONSUCarConc" });
        _traerDeudasGrados();
    };
    function _traerDeudasGrados() {
        var serDeu = carteraService.GetCarteraConceptosEstudiantes();
        serDeu.then(function (pl) {
            $scope.carteras = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});