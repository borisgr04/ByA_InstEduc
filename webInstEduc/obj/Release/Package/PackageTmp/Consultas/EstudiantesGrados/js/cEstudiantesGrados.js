app.controller('cEstudiantesGrados', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService) {
    $scope.grados = [];
    $scope.grado = {};
    $scope.cursos = [];
    $scope.curso = {};
    $scope.contactos_grados = {};
    $scope._buscarCurso = function () {
        _buscarCurso();
    };
    $scope._traerEstudiantes = function () {
        _traerEstudiantes();
    };
    $scope._imprimirListado = function () {
        _imprimirListado();
    };
    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Estudiantes Grados", Modulo: "Consultas", urlToPanelModulo: "EstudiantesGrados.aspx", Cod_Mod: "CONSU", Rol: "CONSUEstuGrados" });
        _traerGrados();
    };
    function _traerGrados() {
        var promiseGet = gradosService.Gets();
        promiseGet.then(function (pl) {
            $scope.grados = pl.data;
            $scope.grado = $scope.grados[0];
            _buscarCurso();

            if ($scope.estudiante.id_ultimo_grado != null) {
                _establecerGradoEstudiante($scope.estudiante.id_ultimo_grado);
            }
        },
        function (errorPl) {
            console.log(errorPl);
        });
    };
    function _buscarCurso() {
        var serCurso = cursosService.GetsCursosGrado($scope.grado.id);
        serCurso.then(function (pl) {
            $scope.cursos = pl.data;
            $scope.curso = $scope.cursos[0];
            _traerVisualizacionCartera();
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    function _traerEstudiantes() {
        var serEstu = estudiantesService.GetXGrado(byaSite.getVigencia(), $scope.grado.id, $scope.curso.id);
        serEstu.then(function (pl) {
            $scope.contactos_grados = pl.data;
        }, function (pl) {
            console.log(JSON.stringify(pl));
        });
    };
    function _imprimirListado() {
        var classActual = $("#tblEstudiantesGrados").attr("class");
        $("#tblEstudiantesGrados").removeAttr("class");
        $("#tblEstudiantesGrados").addClass("tbconborde");
        var htmlListado = $("#printListado").html();
        $("#tblEstudiantesGrados").removeAttr("class");
        $("#tblEstudiantesGrados").addClass(classActual);
        var camposImp = ["TABLA_ESTUDIANTES"];
        var objListadoEstudiantesImprimir = {
            TABLA_ESTUDIANTES: htmlListado
        };
        $.get("/DiseñosReportes/ListadoEstudiantes.html", function (data) {
            $.each(camposImp, function (index, item) {
                data = data.split("{" + item + "}").join(objListadoEstudiantesImprimir[item]);
            });

            var win;
            win = window.open();
            win.document.write(data);
            win.print();
            win.close();
        });
    };
});