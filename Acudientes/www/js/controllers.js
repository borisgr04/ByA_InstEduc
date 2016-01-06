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

    $scope.logout = function() {
        localStorage.clear();
        $rootScope.Mensajes = [];
        $rootScope.estadoCuentaVigenciaActual = [];
        $state.go('login');
    };

    $scope.redireccionar = function () {
        _redireccionar();
    }

    _init();
    function _init()
    {
        _getInformacionAcudienteMensajes();
        $scope.redireccionar();
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

    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            $state.go('login');
        }
    };
}]);

app.controller('MensajesCtrl', ['$scope', '$rootScope', '$ionicModal', 'mensajesServices', '$ionicPopup', '$state', function($scope, $rootScope, $ionicModal, mensajesServices, $ionicPopup, $state){

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

    $scope.checkear_Mensajes = function(){
        if($scope.check_mensajes == undefined)
        {
            $scope.check_mensajes = true;
        }
        else
        {
            $scope.check_mensajes = !$scope.check_mensajes;
        }
        if($scope.check_mensajes == true){
            console.log($rootScope.Mensajes);
            $.each($rootScope.Mensajes, function (index, ite_mensajes) {
                ite_mensajes.activo = true;
                $scope.mensajeDto = [];
                for(i in $rootScope.Mensajes)
                {
                    $scope.mensajeDto.push($rootScope.Mensajes[i]);
                }
            });
            console.log($scope.mensajeDto);
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
            $scope.mensajeDto.push(msje);
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
        alert($scope.mensajeDto.length);
        if($scope.mensajeDto.length > 0)
        {
            var promisePost = mensajesServices.PostCambiarMensajeInactivo($scope.mensajeDto);
            promisePost.then(
                function (pl) {
                    var respuesta = pl.data;
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
        }
        else
        {
            showAlert("Alerta!", "Para eliminar los mensajes, debe checkear al menos un mensaje");
        }
    };

    $scope.redireccionar = function () {
        _redireccionar();
    }

    _init();
    function _init()
    {
        _crearModal();
        _contarMensajes();
        $scope.redireccionar();
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
        $scope.mensajeDto = [];
    }

    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            $state.go('login');
        }
    };
}]);

app.controller('EstudianteCtrl', ['$scope', '$rootScope', '$state', function($scope, $rootScope, $state) {
    $scope.username = byaSite._getUsername();
    $scope.nombre_estudiante = byaSite._getNombreEstudiante();
    $scope.identificacion_estudiante = byaSite._getIdentificacionEstudiante();

    $scope.redireccionar = function () {
        _redireccionar();
    }

    _init();

    function _init() {
        $scope.redireccionar();
    };
    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            $state.go('login');
        }
    };
}]);

app.controller('CuentaCtrl', ['$scope', '$rootScope', '$ionicModal', 'estadoCuentaServices', '$state', function($scope, $rootScope, $ionicModal, estadoCuentaServices, $state) {
    $scope.username = byaSite._getUsername();
    $scope.nombre_estudiante = byaSite._getNombreEstudiante();
    $scope.identificacion_estudiante = byaSite._getIdentificacionEstudiante();
    $scope.estado_actual;
    $scope.estadoCuenta = [];

    $scope.verEstadoCuenta = function(estado)
    {
        $scope.estado_actual = estado;
        $scope.modal.show();
    }

    $scope.irEstadoCuentaDeVigencia = function(itemEstado){
        $rootScope.estadoCuentaVigenciaActual = []
        $rootScope.estadoCuentaVigenciaActual = itemEstado;
        $state.go("cuenta");
    };

    $scope.redireccionar = function () {
        _redireccionar();
    }

    _init();

    function _init()
    {
        _crearModal();
        _getEstadoCuenta();
        $scope.redireccionar();
    }

    function _crearModal() {
        $ionicModal.fromTemplateUrl('templates/modalEstadoCuenta.html', {
            scope: $scope
        }).then(function(modal) {
            $scope.modal = modal;
        });
    };

    function _getEstadoCuenta()
    {
        var promiseGet = estadoCuentaServices.getEstadoCuenta($scope.identificacion_estudiante);
        promiseGet.then(
            function(pl){
                var respuesta = pl.data;
                $scope.estadoCuenta = [];
                $scope.estadoCuenta = respuesta;
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };

    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            $state.go('login');
        }
    };
}]);