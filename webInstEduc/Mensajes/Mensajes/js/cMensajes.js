app.controller('cEstudiantes', ["$scope", "entidadService", "gradosService", "estudiantesService", "cursosService", "matriculasService", "carteraService", "pagosService", "periodosService", "grupospagosService", "ngTableParams", "$filter", function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, grupospagosService, ngTableParams, $filter) {
    $scope.orden = 0;
    $scope.filtro = "";
    $scope.estudiantes = [];
    $scope.listaEstudiantes = [];
    $scope.tableEstudiantes = {};
    $scope.grados = [];
    $scope.cursos = [];
    $scope.ListCursos = [];
    $scope.estudianteDto = [];
    $scope.tipoAcudiente = [
        {
            id: 0,
            nombre: ""
        },
        {
            id: 1,
            nombre: "MOROSOS"
        },
        {
            id: 2,
            nombre: "RECORDATORIO DE PAGO"
        }
    ];
    $scope.tipoAlerta = $scope.tipoAcudiente[0];
    $scope._nuevo = function () {
        varLocal.Remove("id_estudiante");
        window.location.href = "/Estudiantes/Registro/RegistroEstudiante.aspx";
    };
    $scope._detallesEstudiantes = function (estudiante) {
        varLocal.Set("id_estudiante", estudiante.identificacion);
        window.location.href = "/Estudiantes/Registro/RegistroEstudiante.aspx";
    };
    $scope._traerEstudiantes = function () {
        $scope.orden = 0;
        $scope.estudiantes = [];
        _traerEstudiante();
    };
    $scope.cargarCurso = function () {
        var todosCursos = { id: -1, nombre: "TODOS", id_grado: -1 };
        $scope.ListCursos = [];
        $scope.ListCursos.unshift(todosCursos);
        $scope.cursoSeleccionado = $scope.ListCursos[0];
        for (i in $scope.cursos) {
            if ($scope.cursos[i].id_grado == $scope.gradoSeleccionado.id) {
                $scope.ListCursos.push($scope.cursos[i]);
            }
        }
    };
    $scope.checkear_estudiante = function (estudiante, index) {
        if (estudiante.activo) {
            $scope.estudianteDto.push(estudiante);
        }
        if (estudiante.activo == false) {
            if ($scope.estudianteDto.length == 1) {
                $scope.estudianteDto = [];
            }
            $scope.estudianteDto.splice(index, 1);
        }
        console.log($scope.estudianteDto);
    };
    $scope.checkear_todosEstudiantes = function () {
        //alert($scope.check_todos_estudiantes);
        if ($scope.check_todos_estudiantes == true) {
            //console.log($rootScope.Mensajes);
            $.each($scope.estudiantes, function (index, ite_estudiantes) {
                ite_estudiantes.activo = true;
                $scope.estudianteDto = [];
                for (i in $scope.estudiantes) {
                    $scope.estudianteDto.push($scope.estudiantes[i]);
                }
            });
            console.log($scope.estudianteDto);
        }
        if ($scope.check_todos_estudiantes == false) {
            $.each($scope.estudiantes, function (index, ite_estudiantes) {
                ite_estudiantes.activo = false;
            });
            $scope.estudianteDto = [];
            console.log($scope.estudianteDto);
        }
    };

    $scope.ColorSaldo = function (saldo) {
        if (saldo > 0) return "#d9534f";
        if (saldo == 0) return "#5cb85c";
    };

    $scope.filtrar = function () {
        $scope.listaEstudiantes = [];
        if ($scope.gradoSeleccionado.id == -1) {
            if ($scope.cursoSeleccionado.id == -1) {
                $scope.listaEstudiantes = $scope.estudiantes;
            }
            else {
                for (i in $scope.estudiantes) {
                    if ($scope.cursoSeleccionado.nombre == $scope.estudiantes[i].nombre_curso) {
                        $scope.listaEstudiantes.push($scope.estudiantes[i]);
                    }
                }
            }
        }
        else {
            if ($scope.cursoSeleccionado.id == -1) {
                for (i in $scope.estudiantes) {
                    if ($scope.gradoSeleccionado.nombre == $scope.estudiantes[i].nombre_grado) {
                        $scope.listaEstudiantes.push($scope.estudiantes[i]);
                    }
                }
            }
            else {
                for (i in $scope.estudiantes) {
                    if ($scope.gradoSeleccionado.nombre == $scope.estudiantes[i].nombre_grado && $scope.cursoSeleccionado.nombre == $scope.estudiantes[i].nombre_curso) {
                        $scope.listaEstudiantes.push($scope.estudiantes[i]);
                    }
                }
            }
        }
    };

    _init();
    function _init() {
        //_crearTablaEstudiantes();
        byaSite.SetModuloP({ TituloForm: "Enviar Mensajes", Modulo: "Mensajes", urlToPanelModulo: "Estudiantes.aspx", Cod_Mod: "MSJE", Rol: "MSJEMensajes" });
        _traerEstudiante();
        _traerCursos();
        _traerGrados();
    };
    function _traerEstudiante() {
        var serEstu = estudiantesService.GetEstudiantes();
        serEstu.then(function (pl) {
            var respuesta = pl.data;
            $scope.estudiantes = respuesta;
            $scope.listaEstudiantes = $scope.estudiantes;
            console.log($scope.listaEstudiantes);
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerGrados() {
        var promiseGet = gradosService.Gets();
        promiseGet.then(
            function (pl) {
                var respuesta = pl.data;
                var todos = { id: -1, nombre: "TODOS" };
                $scope.grados = [];
                $scope.grados = respuesta;
                $scope.grados.unshift(todos);
                $scope.gradoSeleccionado = $scope.grados[0];
                $scope.cargarCurso();
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };
    function _traerCursos() {
        var promiseGet = cursosService.Gets();
        promiseGet.then(
            function(pl) {
                respuesta = pl.data;
                $scope.cursos = [];
                $scope.cursos = respuesta;
            },
            function(errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };
    function _crearTablaEstudiantes() {
        $scope.tableEstudiantes = new ngTableParams({
            page: 1,
            count: 10,
            sorting: {
                nombre_completo: "asc"
            }
        }, {
            counts: [],
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