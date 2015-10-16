// create the controller and inject Angular's $scope
app.controller('cDetallesMatricula', function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService) {
    $scope.estudiante = {};
    $scope.entidad = {};
    $scope.matricula = {};
    $scope._print = function () {
        var printContents = document.getElementById("print").innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
    };
    $scope._Back = function () {
        window.history.back();
    };
    $scope._calcular_edad = function (fecha) {
        var FechaNac = fecha.split("-");
        var ano = parseInt(FechaNac[0]);
        var mes = parseInt(FechaNac[1]);
        var dia = parseInt(FechaNac[2]);


        fecha_hoy = new Date();
        ahora_ano = fecha_hoy.getYear();
        ahora_mes = fecha_hoy.getMonth();
        ahora_dia = fecha_hoy.getDate();
        edad = (ahora_ano + 1900) - ano;

        if (ahora_mes < (mes - 1)) {
            edad--;
        }
        if (((mes - 1) == ahora_mes) && (ahora_dia < dia)) {
            edad--;
        }
        if (edad > 1900) {
            edad -= 1900;
        }
        return edad;
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Matriculas", urlToPanelModulo: "gMatriculas.aspx", Cod_Mod: "MATRI", Rol: "MATRIGestion" });
        $scope.matricula = varLocal.Get("matricula_detalles");
        _traerestudiante($scope.matricula.id_est);
        _traerInformacionEntidad();
    };
    function _traerInformacionEntidad() {
        var serEnti = entidadService.Get();
        serEnti.then(function (pl) {
            $scope.entidad = pl.data;
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerestudiante (id_estudiante){
        var serEstu = estudiantesService.GetXId(id_estudiante);
        serEstu.then(function (pl) {
            if (pl.data.id != null) {
                $scope.estudiante = pl.data;
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };    
});