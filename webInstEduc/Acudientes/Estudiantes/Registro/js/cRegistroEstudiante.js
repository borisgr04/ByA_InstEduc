// create the controller and inject Angular's $scope
app.controller('cRegistroEstudiante', function ($scope, gradosService, estudiantesService, cursosService, matriculasService, carteraService, pagosService, tercerosService) {
    $scope.habGuardar = true;
    $scope.operacionEjecutar = "N";
    $scope.direcciones = [];
    $scope.telefonos = [];
    $scope.celulares = [];
    $scope.grados = [];
    $scope.grado = {};
    $scope.quien_acudiente = "OTRO";
    $scope.estudiante = {};
    $scope._traerestudiante = function () {
        var serEstu = estudiantesService.Get($scope.estudiante.identificacion);
        serEstu.then(function (pl) {
            if (pl.data.id != 0) {
                $scope.estudiante = pl.data;
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                $scope.estudiante.fecha_nacimiento = byaPage.parseStrDate($scope.estudiante.fecha_nacimiento);
                $scope.estudiante.edad = _calcular_edad($scope.estudiante.fecha_nacimiento);
                _vertificarQuienAcudiente();
                $scope.operacionEjecutar = "E";
            }
        }, function (errorPl) {
            console.log(errorPl);
        });
    };
    $scope.mostrarE = function () {
        $scope.estudiante.edad = _calcular_edad($scope.estudiante.fecha_nacimiento);
    };
    $scope._guardarEstudiante = function () {
        if (_esValido("datosEstudiante")) {
            $scope.habGuardar = false;
            if ($scope.operacionEjecutar == "N") _GuardarEstudianteNuevo();
            else _GuardarEstudianteModificado();
        }
    };
    $scope._traerAcudiente = function () {
        _traerAcudiente();
    };
    $scope._cambiarAcudiente = function () {
        var value = $scope.quien_acudiente;
        if (value == "MADRE") {
            $scope.estudiante.terceros3 = $scope.estudiante.terceros1;
            $scope.estudiante.terceros3.sexo = "FEMENINO";
            $scope.estudiante.parentesco_acudiente = value;
        }
        else {
            if (value == "PADRE") {
                $scope.estudiante.terceros3 = $scope.estudiante.terceros2;
                $scope.estudiante.terceros3.sexo = "MASCULINO";
                $scope.estudiante.parentesco_acudiente = value;
            }
            else {
                $scope.estudiante.terceros3 = {}
                $scope.estudiante.parentesco_acudiente = "";
            }
        }
    };
    $scope._traerTerceroMadre = function () {
        _traerTerceroMadre();
    };
    $scope._traerTerceroPadre = function () {
        _traerTerceroPadre();
    };
    $scope._traerTerceroAcudiente = function () {
        _traerTerceroAcudiente();
    };
    $scope._limpiarEstudiante = function () {
        varLocal.Remove("id_estudiante");
        $scope.estudiante = {};
        $scope.operacionEjecutar = "N";
    };
    $scope._agregarDireccionEstudiante = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros.direccion) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros.direccion);
    };
    $scope._agregarDireccionPadre = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros2.direccion) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros2.direccion);
    };
    $scope._agregarDireccionPadreTrabajo = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros2.direccion_trabajo) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros2.direccion_trabajo);
    };
    $scope._agregarDireccionMadre = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros1.direccion) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros1.direccion);
    };
    $scope._agregarDireccionMadreTrabajo = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros1.direccion_trabajo) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros1.direccion_trabajo);
    };
    $scope._agregarDireccionAcudiente = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros3.direccion) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros3.direccion);
    };
    $scope._agregarDireccionAcudienteTrabajo = function () {
        var ban = false;
        $.each($scope.direcciones, function (index, item) {
            if (item == $scope.estudiante.terceros3.direccion_trabajo) ban = true;
        });
        if (!ban) $scope.direcciones.push($scope.estudiante.terceros3.direccion_trabajo);
    };
    $scope._agregarCelularEstudiante = function () {
        var ban = false;
        $.each($scope.celulares, function (index, item) {
            if (item == $scope.estudiante.terceros.celular) ban = true;
        });
        if (!ban) $scope.celulares.push($scope.estudiante.terceros.celular);
    };
    $scope._agregarCelularPadre = function () {
        var ban = false;
        $.each($scope.celulares, function (index, item) {
            if (item == $scope.estudiante.terceros2.celular) ban = true;
        });
        if (!ban) $scope.celulares.push($scope.estudiante.terceros2.celular);
    };
    $scope._agregarCelularMadre = function () {
        var ban = false;
        $.each($scope.celulares, function (index, item) {
            if (item == $scope.estudiante.terceros1.celular) ban = true;
        });
        if (!ban) $scope.celulares.push($scope.estudiante.terceros1.celular);
    };
    $scope._agregarCelularAcudiente = function () {
        var ban = false;
        $.each($scope.celulares, function (index, item) {
            if (item == $scope.estudiante.terceros3.celular) ban = true;
        });
        if (!ban) $scope.celulares.push($scope.estudiante.terceros3.celular);
    };
    $scope._agregarTelefonoEstudiante = function () {
        var ban = false;
        $.each($scope.telefonos, function (index, item) {
            if (item == $scope.estudiante.terceros.telefono) ban = true;
        });
        if (!ban) $scope.telefonos.push($scope.estudiante.terceros.telefono);
    };
    $scope._agregarTelefonoPadre = function () {
        var ban = false;
        $.each($scope.telefonos, function (index, item) {
            if (item == $scope.estudiante.terceros2.telefono) ban = true;
        });
        if (!ban) $scope.telefonos.push($scope.estudiante.terceros2.telefono);
    };
    $scope._agregarTelefonoMadre = function () {
        var ban = false;
        $.each($scope.telefonos, function (index, item) {
            if (item == $scope.estudiante.terceros1.telefono) ban = true;
        });
        if (!ban) $scope.telefonos.push($scope.estudiante.terceros1.telefono);
    };
    $scope._agregarTelefonoAcudiente = function () {
        var ban = false;
        $.each($scope.telefonos, function (index, item) {
            if (item == $scope.estudiante.terceros3.telefono) ban = true;
        });
        if (!ban) $scope.telefonos.push($scope.estudiante.terceros3.telefono);
    };
    $scope._Back = function () {
        window.history.back();
    };

    _init();

    function _init() {
        byaSite.SetModuloP({ TituloForm: "Gestión", Modulo: "Estudiantes", urlToPanelModulo: "Estudiantes.aspx", Cod_Mod: "ACUDI", Rol: "ACUDIEstudiantes" });
        _traerGrados();

        var id_estudiante = varLocal.Get("id_estudiante");
        if (id_estudiante != null) {
            $scope.operacionEjecutar = "E";
            $scope.estudiante.identificacion = id_estudiante;
            $scope._traerestudiante();
        } else $scope.operacionEjecutar = "N";
    };
    function _esValido(nameForm) {
        var error = false;
        var sRequired = $scope[nameForm].$error.required;
        if ((sRequired != null) && (sRequired != false)) {
            error = true;
        }

        if (error) {
            $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Los campos resaltados en rojo son obligatorios...", tipo: false });
            $("input.ng-invalid").css("border", " 1px solid red");
            $("select.ng-invalid").css("border", " 1px solid red");
            return !error;
        } else {
            $("input.ng-valid").css("border", "");
            $("select.ng-valid").css("border", "");
            $("#LbMsg").html("");
            return true;
        }

    };
    function _traerGrados() {
        var promiseGet = gradosService.Gets();
        promiseGet.then(function (pl) {
            $scope.grados = pl.data;
            $scope.grado = $scope.grados[0];
        },
        function (errorPl) {
            console.log(errorPl);
        });
    };
    function _GuardarEstudianteNuevo() {
        if ($scope.estudiante.terceros1 != null) $scope.estudiante.terceros1.sexo = "FEMENINO";
        if ($scope.estudiante.terceros2 != null) $scope.estudiante.terceros2.sexo = "MASCULINO";

        var serEst = estudiantesService.Insert($scope.estudiante);
        serEst.then(function (pl) {
            $scope.habGuardar = true;
            $("#LbMsg").msgBox({ titulo: "Registro de estudiante", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
            if (!pl.data.Error) {
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                $scope._traerestudiante();
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _GuardarEstudianteModificado() {
        if ($scope.estudiante.terceros1 != null) $scope.estudiante.terceros1.sexo = "FEMENINO";
        if ($scope.estudiante.terceros2 != null) $scope.estudiante.terceros2.sexo = "MASCULINO";

        var serEst = estudiantesService.Update($scope.estudiante);
        serEst.then(function (pl) {
            $scope.habGuardar = true;
            $("#LbMsg").msgBox({ titulo: "Registro de estudiante", mensaje: pl.data.Mensaje, tipo: !pl.data.Error });
            if (!pl.data.Error) {
                varLocal.Set("id_estudiante", $scope.estudiante.identificacion);
                $scope._traerestudiante();
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerAcudiente() {
        var serAcu = tercerosService.Get($scope.estudiante.acudiente.identificacion);
        serAcu.then(function (pl) {
            if (pl.data.id != 0) {
                $("#LbMsg").html("");
                $scope.estudiante.acudiente = pl.data;
            }
            else {
                var id = $scope.estudiante.acudiente.identificacion;
                $scope.estudiante.acudiente = {};
                $scope.estudiante.acudiente.identificacion = id;
            }
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _calcular_edad(fecha) {
        fecha = byaPage.FechaShortX(fecha);
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
    function _vertificarQuienAcudiente() {
        var acu = "";
        if ($scope.estudiante.id_acudiente == $scope.estudiante.id_tercero_madre) acu = "MADRE";
        else if ($scope.estudiante.id_acudiente == $scope.estudiante.id_tercero_padre) acu = "PADRE";
        else acu = "OTRO";
        $scope.quien_acudiente = acu;
    };
    function _traerTerceroMadre(){
        var terSer = tercerosService.Get($scope.estudiante.terceros1.identificacion);
        terSer.then(function (pl) {
            if (pl.data != null) {
                $scope.estudiante.terceros1 = pl.data;
            }
            //else {
            //    var ide = $scope.estudiante.terceros1.identificacion;
            //    var tip = $scope.estudiante.terceros1.tipo_identificacion;
            //    $scope.estudiante.terceros1 = {};
            //    $scope.estudiante.terceros1.identificacion = ide;
            //    $scope.estudiante.terceros1.tipo_identificacion = tip;
            //}
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerTerceroPadre() {
        var terSer = tercerosService.Get($scope.estudiante.terceros2.identificacion);
        terSer.then(function (pl) {
            if (pl.data != null) {
                $scope.estudiante.terceros2 = pl.data;
            }
            //else {
            //    var ide = $scope.estudiante.terceros2.identificacion;
            //    var tip = $scope.estudiante.terceros2.tipo_identificacion;
            //    $scope.estudiante.terceros2 = {};
            //    $scope.estudiante.terceros2.identificacion = ide;
            //    $scope.estudiante.terceros2.tipo_identificacion = tip;
            //}
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _traerTerceroAcudiente() {
        var terSer = tercerosService.Get($scope.estudiante.terceros3.identificacion);
        terSer.then(function (pl) {
            if (pl.data != null) {
                $scope.estudiante.terceros3 = pl.data;
            }
            //else {
            //    var ide = $scope.estudiante.terceros3.identificacion;
            //    var tip = $scope.estudiante.terceros3.tipo_identificacion;
            //    $scope.estudiante.terceros3 = {};
            //    $scope.estudiante.terceros3.identificacion = ide;
            //    $scope.estudiante.terceros3.tipo_identificacion = tip;
            //}
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
});