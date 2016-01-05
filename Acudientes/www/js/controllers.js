app.controller('LoginCtrl', ['loginServices', '$scope', '$ionicPopup', '$state', function(loginServices, $scope, $ionicPopup, $state){
    $scope.username;
    $scope.password;
    $scope.login = function(){
        var promisePost = loginServices.Login($scope.username, $scope.password);
        promisePost.then(
            function (pl) {
                byaSite._setToken(pl.data.access_token);
                byaSite._setUsername($scope.username);
                $state.go("home");
            },
            function (errorPl){
                showAlert("Error", "Verifique Username/Password");
            }
        );
    };

    function showAlert(title, data) {
        var alertPopup = $ionicPopup.alert({
            title: title,
            template: data
        });
        alertPopup.then(function (res) {
            console.log('Thank you');
        });
    };
}]);
app.controller('HomeCtrl', ['$scope', 'homeServices', '$ionicPopup', '$rootScope', '$state', function($scope, homeServices, $ionicPopup, $rootScope, $state){
    $scope.username = byaSite._getUsername();
    $scope.Acudiente = {};
    $scope.Estudiantes = [];

    $scope._irMenuEstudiante = function(estudiante){
        byaSite._setNombreEstudiante(estudiante.nombre_completo);
        byaSite._setIdentificacionEstudiante(estudiante.identificacion);
        $state.go("estudiante");
    };

    _init();
    function _init()
    {
        _getInformacionAcudienteMensajes();
    };

    function _getInformacionAcudienteMensajes()
    {
        var promiseGet = homeServices.getInformacionAcudienteMensajes($scope.username);
        promiseGet.then(
            function(pl){
                var respuesta = pl.data;
                $scope.Acudiente = respuesta.acudiente;
                $scope.Estudiantes = respuesta.estudiantes;
                $rootScope.Mensajes = respuesta.mensajes;
                _contarMensajes();
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };

    function _contarMensajes(){
        $rootScope.contador = 0;
        for(i in $rootScope.Mensajes)
        {
            if($rootScope.Mensajes[i].estado_mensaje_acudiente == "Sin Revisar"){
                $rootScope.contador++;
            }
        }
    };
}]);

app.controller('MensajesCtrl', ['$scope', '$rootScope', '$ionicModal', 'mensajesServices', '$ionicPopup', function($scope, $rootScope, $ionicModal, mensajesServices, $ionicPopup){

    $scope.mensajeActual;
    $scope.contador;
    $scope.mensajeDto = [];
    $scope.ColorMensaje = function(value){
        if(value == "Informativo") return "#5bc0de;";
        if(value == "Urgente") return "#d9534f";
    };
    $scope.iconoMensaje = function(estado){
        if(estado == "Sin Revisar") return "icon ion-email-unread";
        if(estado == "Revisado") return "icon ion-android-mail";
    };
    $scope.abrirMensaje = function(mensaje, index) {
        $scope.modal.show();
        $scope.mensajeActual = $rootScope.Mensajes[index];
        if($rootScope.Mensajes[index].estado_mensaje_acudiente == "Sin Revisar")
        {
            $scope.cambiarEstado(index);
            mensaje.estado_mensaje_acudiente = "Revisado";
        }
    };

    $scope.cambiarEstado = function (index) {
        _getCambiarEstado(index);
    };

    $scope.checkearMensajes = function(){
        console.log($scope.mensajeDto);
        if($scope.check_mensajes == true){
            $.each($rootScope.Mensajes, function (index, ite_mensajes) {
                ite_mensajes.activo = true;
                $scope.mensajeDto = [];
                for(i in $rootScope.Mensajes)
                {
                    $scope.mensajeDto.push($rootScope.Mensajes[i]);
                }
            });
        }
        if($scope.check_mensajes == false)
        {
            $.each($rootScope.Mensajes, function (index, ite_mensajes) {
                ite_mensajes.activo = false;
            });
            $scope.mensajeDto = [];
            console.log($scope.mensajeDto);
        }
    };

    $scope.checkearMensaje = function(msje, index){
        if(msje.activo)
        {
            $scope.mensaje_auxiliar = {};
            $scope.mensaje_auxiliar.asunto = msje.asunto;
            $scope.mensaje_auxiliar.estado_mensaje_acudiente = msje.estado_mensaje_acudiente;
            $scope.mensaje_auxiliar.fecha = msje.fecha;
            $scope.mensaje_auxiliar.id = msje.id;
            $scope.mensaje_auxiliar.id_mensaje_acudiente = msje.id_estado_mensaje_acudiente;
            $scope.mensaje_auxiliar.id_remitente = msje.id_remitente;
            $scope.mensaje_auxiliar.mensaje = msje.mensaje;
            $scope.mensaje_auxiliar.tipo = msje.tipo;
            $scope.mensajeDto.push($scope.mensaje_auxiliar);
        }
        if(msje.activo == false)
        {
            if($scope.mensajeDto.length == 1)
            {
                $scope.mensajeDto = [];
            }
            $scope.mensajeDto.splice(index, 1);
        }
        console.log($scope.mensajeDto);
    };

    $scope.EliminarMensajes = function() {
        var promisePost = mensajesServices.PostCambiarMensajeInactivo($scope.mensajeDto);
        promisePost.then(
            function (pl) {
                var respuesta = pl.data;
                alert(JSON.stringify(respuesta));
                if(respuesta.Error == false)
                {
                    showAlert("Exito!", respuesta.Mensaje);
                    _EliminarItemMensajes();
                }
                if(respuesta.Error)
                {
                    showAlert("Error!", respuesta.Mensaje);
                }
            },
            function (errorPl){
                console.log(JSON.stringify(errorPl));
            }
        );
        _contarMensajes();
    };

    _init();
    function _init()
    {
        _crearModal();
        _contarMensajes();
    }

    function _crearModal() {
        $ionicModal.fromTemplateUrl('templates/modalMensaje.html', {
            scope: $scope
        }).then(function(modal) {
            $scope.modal = modal;
        });
    };

    function _contarMensajes(){
        $rootScope.contador = 0;
        for(i in $rootScope.Mensajes)
        {
            if($rootScope.Mensajes[i].estado_mensaje_acudiente == "Sin Revisar"){
                $rootScope.contador++;
            }
        }
    };

    function _getCambiarEstado(index)
    {
        var promiseGet = mensajesServices.getCambiarEstado($rootScope.Mensajes[index].id_mensaje_acudiente);
        promiseGet.then(
            function(pl){
                var respuesta = pl.data;
                if(respuesta.error == false)
                {
                    $rootScope.Mensajes[index].estado_mensaje_acudiente = "Revisado";
                    _contarMensajes();
                }
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };

    function showAlert(title, data) {
        var alertPopup = $ionicPopup.alert({
            title: title,
            template: data
        });
        alertPopup.then(function (res) {
            console.log('Thank you');
        });
    };

    function _EliminarItemMensajes()
    {
        for(i in $scope.mensajeDto)
        {
            for(j in $rootScope.Mensajes)
            {
                if($rootScope.Mensajes[j].id == $scope.mensajeDto[i].id)
                {
                    $rootScope.Mensajes.splice(j, 1);
                }
            }
        }
        console.log($rootScope.Mensajes);
    }
}]);

app.controller('EstudianteCtrl', ['$scope', '$rootScope', function($scope, $rootScope) {
    $scope.username = byaSite._getUsername();
    $scope.nombre_estudiante = byaSite._getNombreEstudiante();
    $scope.identificacion_estudiante = byaSite._getIdentificacionEstudiante();
}]);

app.controller('CuentaCtrl', ['$scope', '$rootScope', function($scope, $rootScope) {
    $scope.username = byaSite._getUsername();
    $scope.nombre_estudiante = byaSite._getNombreEstudiante();
    $scope.identificacion_estudiante = byaSite._getIdentificacionEstudiante();
    $scope.informacionGeneral =
    {
            "causado": "2,786,600",
            "interes": "40,000",
            "pagado": "2,346,600",
            "total": "480,000"
    };

    $scope.estadoCuenta =
        [
            {
                "concepto": "Matricula",
                "periodo": 2,
                "causado": "286,600",
                "interes": 0,
                "pagado": "286,600",
                "fecha_pago": "12/09/2014",
                "saldo": 0
            },
            {
                "concepto": "Proyectos Pedagogicos",
                "periodo": 2,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "12/09/2014",
                "saldo": 0
            },
            {
                "concepto": "Sistematizacion",
                "periodo": 2,
                "causado": "60,00",
                "interes": 0,
                "pagado": "60,000",
                "fecha_pago": "12/09/2014",
                "saldo": 0
            },
            {
                "concepto": "Seguro Estudiantil",
                "periodo": 2,
                "causado": "20,000",
                "interes": 0,
                "pagado": "20,000",
                "fecha_pago": "12/09/2014",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 2,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "02/03/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 3,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "03/02/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 4,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "04/13/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 5,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "05/05/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 6,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "06/03/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 7,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "07/03/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 8,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "07/30/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 9,
                "causado": "220,000",
                "interes": 0,
                "pagado": "220,000",
                "fecha_pago": "09/02/2015",
                "saldo": 0
            },
            {
                "concepto": "Pension",
                "periodo": 10,
                "causado": "220,000",
                "interes": "20,000",
                "pagado": 0,
                "fecha_pago": "Fecha Pendiente",
                "saldo": "240,000"
            },
            {
                "concepto": "Pension",
                "periodo": 2,
                "causado": "220,000",
                "interes": "20,000",
                "pagado": 0,
                "fecha_pago": "Fecha Pendiente",
                "saldo": "240,000"
            },
        ];
}]);