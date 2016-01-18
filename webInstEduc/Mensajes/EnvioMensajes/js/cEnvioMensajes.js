app.controller('cEstudiantes', ["$scope", "entidadService", "gradosService", "estudiantesService", "cursosService", "matriculasService", "carteraService", "pagosService", "periodosService", "grupospagosService", "ngTableParams", "$filter", "mensajesService", function ($scope, entidadService, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, periodosService, grupospagosService, ngTableParams, $filter, mensajesService) {
    $scope.orden = 0;
    $scope.filtro = "";
    $scope.estudiantes = [];
    $scope.listaEstudiantes = [];
    $scope.tableEstudiantes = {};
    $scope.grados = [];
    $scope.cursos = [];
    $scope.ListCursos = [];
    $scope.estudianteDto = [];
    $scope.mensajeMoroso = "Estimado Acudiente, le recordamos que tiene una deuda de xxxxx más intereses. Por favor coloquese al día lo más pronto posible";
    $scope.recordatorioDePago = "Estimado Acudiente, le recordamos que si cancela el valor de la pensión entre los días del 1 al 15 no se adicionaran intereses";
    $scope.ContenidoMensaje;
    $scope.asunto;
    $scope.tipoMensaje = [
        {
            id: 1,
            nombre: "Urgente"
        },
        {
            id: 2,
            nombre: "Informativo"
        }
    ]
    $scope.tipoAcudiente = [
        {
            id: 1,
            nombre: "Morosos"
        },
        {
            id: 2,
            nombre: "Recordatorio de Pago"
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
        var todosCursos = { id: -1, nombre: "Todos", id_grado: -1 };
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
            $scope.estudianteDto.splice($scope.estudianteDto.indexOf(estudiante), 1);
        }
    };
    $scope.checkear_todosEstudiantes = function () {
        if ($scope.check_todos_estudiantes == true) {
            $.each($scope.estudiantes, function (index, ite_estudiantes) {
                ite_estudiantes.activo = true;
                $scope.estudianteDto = [];
                for (i in $scope.listaEstudiantes) {
                    $scope.estudianteDto.push($scope.listaEstudiantes[i]);
                }
            });
        }
        if ($scope.check_todos_estudiantes == false) {
            $.each($scope.estudiantes, function (index, ite_estudiantes) {
                ite_estudiantes.activo = false;
            });
            $scope.estudianteDto = [];
        }
    };

    $scope.ColorSaldo = function (saldo) {
        if (saldo > 0) return "#d9534f";
        if (saldo == 0) return "#5cb85c";
    };

    $scope.filtrar = function () {
        $scope.check_todos_estudiantes = false;
        $scope.checkear_todosEstudiantes();
        $scope.listaEstudiantes = [];
        if ($scope.gradoSeleccionado.id == -1) { //$scope.gradoSeleccionado = TODOS
            if ($scope.cursoSeleccionado.id == -1) { //$scope.cursoSeleccionado = TODOS
                $scope.listaEstudiantes = $scope.estudiantes;
            }
            /*else {
                for (i in $scope.estudiantes)
                {
                    if ($scope.cursoSeleccionado.nombre == $scope.estudiantes[i].nombre_curso) {
                        $scope.listaEstudiantes.push($scope.estudiantes[i]);
                    }
                }
            }*/
        }
        else {
            if ($scope.cursoSeleccionado.id == -1) { //$scope.cursoSeleccionado = TODOS
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

    $scope.abrirModal = function () {
        $("#modalMensaje").modal("show");
    };

    $scope.registrarMensajes = function () {
        _mensajesAcudientes();
    };

    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Enviar Mensajes", Modulo: "Mensajes", urlToPanelModulo: "Estudiantes.aspx", Cod_Mod: "MSJE", Rol: "MSJEEnvioMsje" });
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
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerGrados() {
        var promiseGet = gradosService.Gets();
        promiseGet.then(
            function (pl) {
                var respuesta = pl.data;
                var todos = { id: -1, nombre: "Todos" };
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

    function _mensajesAcudientes() {
        for (i in $scope.estudianteDto) {
            $scope.estudianteDto[i].mensaje = $scope.ContenidoMensaje;
            $scope.estudianteDto[i].asunto = $scope.asunto;
            $scope.estudianteDto[i].tipo_mensaje = $scope.tipoMsje.nombre;
        }
        _registrarMensajes();
    };

    function _registrarMensajes() {
        var promisePost = mensajesService.PostMensajes($scope.estudianteDto, byaSite.getUsuario());
        promisePost.then(
            function(pl){
                alert(pl.data.Mensaje);
                //_enviarMensajes();
            },
            function(errorPl){
                console.log(JSON.stringify(errorPl));
            });
    }

    /*function _enviarMensajes() {
        var chat = $.connection.chatHub;
        console.log(chat);
        $.connection.hub.start().done(function () {
            chat.server.registerConId(byaSite.getUsuario());
            for (i in $scope.estudianteDto)
            {
                chat.server.registerConId($scope.estudianteDto[i].identificacion_acudiente);
            }
            for(j in $scope.estudianteDto)
            {
                alert(j);
                chat.server.send(byaSite.getUsuario(), $scope.estudianteDto[j].mensaje, $scope.estudianteDto[j].identificacion_acudiente);
            }
            // Call the Send method on the hub.
            //console.log(chat.server.numId());
        });
    }*/
}]);