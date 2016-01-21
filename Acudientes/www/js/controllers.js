app.controller('LoginCtrl', ['loginServices', '$scope', '$ionicPopup', '$state', 'notificacionesServices', function (loginServices, $scope, $ionicPopup, $state, notificacionesServices) {
    $scope.username;
    $scope.password;
    $scope.loading = false;
    $scope.login = function(username, password){
        $scope.username = username;
        $scope.password = password;
        $scope.loading = true;
        var promisePost = loginServices.Login($scope.username, $scope.password);
        promisePost.then(
            function (pl) {
                $scope.loading = false;
                byaSite._setToken(pl.data.access_token);
                byaSite._setUsername($scope.username);
                guardarTokenGCM($scope.username);
            },
            function (errorPl) {
                $scope.loading = false;
                showAlert("Error", "Verifique Username/Password");
            }
        );
    };
    $scope.$on('$ionicView.enter', function () {
        _init();
    });

    function _init() {
        var user = byaSite._getUsername();
        if (user != null && user != "") {
            window.location.href = "#/home";
        }
    };
    function guardarTokenGCM(identificacion) {
        var token = localStorage.getItem("GCM");
        var obj = {
            identificacion_acudiente: byaSite._getUsername(),
            token_notificacion: token
        };
        $scope.loading = true;
        var promisePost = notificacionesServices.PostIdentificacion(obj);
        promisePost.then(function (pl) {
            $scope.loading = false;
            $scope.password = "";
            window.location.href = "#/home";
        }, function (pl) {
            $scope.loading = false;
            window.location.href = "#/home";
        });
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
    $scope.username = {};
    $scope.Acudiente = {};
    $scope.Estudiantes = [];
    $scope.loading = true;
    $scope.ColorSaldo = function(value){
        if(value == 0) return "#5cb85c;";
        if(value > 0) return "#d9534f";
    };
    $scope._irMenuEstudiante = function(estudiante){
        byaSite._setNombreEstudiante(estudiante.nombre_completo);
        byaSite._setIdentificacionEstudiante(estudiante.identificacion);
        byaSite._setSaldoEstudiante(estudiante.saldo);
        $state.go("estudiante");
    };
    $scope.logout = function() {
        byaSite._removeUsername();
        $rootScope.Mensajes = [];
        $rootScope.estadoCuentaVigenciaActual = [];
        _redireccionar();
    };
    $scope.redireccionar = function () {
        _redireccionar();
    };
    $scope.$on('$ionicView.enter', function () {
        $scope.username = byaSite._getUsername();
        _init();
    });

    function _init() {
        _getInformacionAcudienteMensajes();
        $scope.redireccionar();
    };
    function _getInformacionAcudienteMensajes()
    {
        var promiseGet = homeServices.getInformacionAcudienteMensajes($scope.username);
        promiseGet.then(
            function (pl) {
                var respuesta = pl.data;
                $scope.Acudiente = respuesta.acudiente;
                $scope.Estudiantes = respuesta.estudiantes;
                $rootScope.Mensajes = respuesta.mensajes;
                console.log($rootScope.Mensajes);
                _contarMensajes();
                $scope.loading = false;
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
            if($rootScope.Mensajes[i].estado == "Sin Revisar"){
                $rootScope.contador++;
            }
        }
    };
    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            location.href = "#/login";
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
    $scope.MensajeLeido = function(estado) {
        if(estado == "Sin Revisar") return "bold";
        if(estado == "Revisado") return "normal";
    };
    $scope.iconoMensaje = function(estado){
        if(estado == "Sin Revisar") return "icon ion-android-mail";
        if(estado == "Revisado") return "icon ion-android-drafts";
        //ion-email-unread
    };
    $scope.abrirMensaje = function(mensaje, index) {
        $scope.modal.show();
        $scope.mensajeActual = $rootScope.Mensajes[index];
        if($rootScope.Mensajes[index].estado == "Sin Revisar")
        {
            $scope.cambiarEstado(index);
            mensaje.estado = "Revisado";
        }
    };

    $scope.cambiarEstado = function (index) {
        _getCambiarEstado(index);
        $rootScope.contador--;
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
            $scope.mensajeDto.splice($scope.mensajeDto.indexOf(msje), 1);
        }
        console.log($scope.mensajeDto);
    };

    $scope.showConfirm = function(){
        showConfirm('Advertencia!', 'Desea eliminar los mensajes seleccionados?');
    };

    $scope.EliminarMensajes = function() {
        //alert($scope.mensajeDto.length);
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
    };

    $scope.logout = function() {
        byaSite._removeUsername();
        $rootScope.Mensajes = [];
        $rootScope.estadoCuentaVigenciaActual = [];
        _redireccionar();
    };

    $scope.$on('$ionicView.enter', function () {
        _init();
    });
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
            if($rootScope.Mensajes[i].estado == "Sin Revisar"){
                $rootScope.contador++;
            }
        }
    };

    function _getCambiarEstado(index)
    {
        var promiseGet = mensajesServices.getCambiarEstado($rootScope.Mensajes[index].id_destinatario, $rootScope.Mensajes[index].id);
        promiseGet.then(
            function(pl){
                var respuesta = pl.data;
                if(respuesta.error == false)
                {
                    $rootScope.Mensajes[index].estado = "Revisado";
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

    function showConfirm(title, data) {
        var confirmPopup = $ionicPopup.confirm({
            title: title,
            template: data
        });

        confirmPopup.then(function(res) {
            if(res) {
                $scope.EliminarMensajes();
            } else {
            }
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
        _contarMensajes();
        $scope.mensajeDto = [];
    }

    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            location.href = "#/login";
        }
    };
}]);
app.controller('EstudianteCtrl', ['$scope', '$rootScope', '$state', function($scope, $rootScope, $state) {
    $scope.username = byaSite._getUsername();
    $scope.nombre_estudiante = byaSite._getNombreEstudiante();
    $scope.identificacion_estudiante = byaSite._getIdentificacionEstudiante();
    $scope.saldo_estudiante = byaSite._getSaldoEstudiante();

    $scope.redireccionar = function () {
        _redireccionar();
    };
    $scope.ColorSaldo = function(value){
        if(value == 0) return "#5cb85c;";
        if(value > 0) return "#d9534f";
    };
    $scope.logout = function() {
        byaSite._removeUsername();
        $rootScope.Mensajes = [];
        $rootScope.estadoCuentaVigenciaActual = [];
        _redireccionar();
    };
    $scope.$on('$ionicView.enter', function () {
        _init();
    });

    function _init() {
        $scope.redireccionar();
    };
    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            location.href = "#/login";
        }
    };
}]);
app.controller('CuentaCtrl', ['$scope', '$rootScope', '$ionicModal', 'estadoCuentaServices', '$state', function($scope, $rootScope, $ionicModal, estadoCuentaServices, $state) {
    $scope.username = byaSite._getUsername();
    $scope.nombre_estudiante = byaSite._getNombreEstudiante();
    $scope.identificacion_estudiante = byaSite._getIdentificacionEstudiante();
    $scope.estado_actual;
    $scope.estadoCuenta = [];

    $scope.ColorSaldo = function(value){
        if(value == 0) return "#5cb85c;";
        if(value > 0) return "#d9534f";
    };

    $scope.verEstadoCuenta = function(estado)
    {
        $scope.estado_actual = estado;
        $scope.modal.show();
    }

    $scope.irEstadoCuentaDeVigencia = function(itemEstado){
        $rootScope.estadoCuentaVigenciaActual = [];
        $rootScope.estadoCuentaVigenciaActual = itemEstado;
        $state.go("cuenta");
    };

    $scope.redireccionar = function () {
        _redireccionar();
    };
    $scope.logout = function() {
        byaSite._removeUsername();
        $rootScope.Mensajes = [];
        $rootScope.estadoCuentaVigenciaActual = [];
        _redireccionar();
    };

    $scope.$on('$ionicView.enter', function () {
        _init();
    });

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
                _cambiarFechaNull();
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };

    function _redireccionar() {
        if(byaSite._getUsername() == "" || byaSite._getUsername == undefined || byaSite._getUsername() == null || byaSite._getToken() == "" || byaSite._getToken() == undefined || byaSite._getToken() == null){
            location.href = "#/login";
        }
    };

    function _cambiarFechaNull() {
        for(i in $scope.estadoCuenta)
        {
            for(j in $scope.estadoCuenta[i].l_items)
            {
                if($scope.estadoCuenta[i].l_items[j].fecha_pago == null) $scope.estadoCuenta[i].l_items[j].fecha_pago = "Fecha de pago pendiente";
            }
        }
    };
}]);