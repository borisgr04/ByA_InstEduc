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
                showAlert("Error", "Verifique Username/Password")
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
app.controller('HomeCtrl', ['$scope', 'homeServices', '$ionicPopup', '$rootScope', function($scope, homeServices, $ionicPopup, $rootScope){
    $scope.username = byaSite._getUsername();
    $scope.Acudiente = {};
    $scope.Estudiantes = [];
    $scope.Mensajes = [];
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
                alert($rootScope.Mensajes);
            },
            function (errorPl) {
                console.log(JSON.stringify(errorPl));
            }
        );
    };
}]);

app.controller('MensajesCtrl', ['$scope', '$rootScope', '$ionicModal', function($scope, $rootScope, $ionicModal){

    $scope.mensajeActual;
    $scope.contador = 0;
    $scope.ColorMensaje = function(value){
        if(value == "Informativo") return "#5bc0de;";
        if(value == "Urgente") return "#d9534f";
    };
    $scope.iconoMensaje = function(estado){
        if(estado == "Sin revisar") return "icon ion-email-unread";
        if(estado == "Revisado") return "icon ion-android-mail";
    };
    $scope.abrirMensaje = function(mensaje, index) {
        $scope.modal.show();
        $scope.mensajeActual = mensaje;
        $scope.cambiarEstadoMensaje(index);
        contarMensajes();
    };
    $scope.cambiarEstadoMensaje = function(index){
        //$scope.Mensajes.splice(index, 1);
        $scope.mensajeActual.estado = "Revisado";
        //$scope.Mensajes.push($scope.mensajeActual);
    };
    _init();
    function _init()
    {
        crearModal();
        contarMensajes();
    }

    function crearModal() {
        $ionicModal.fromTemplateUrl('templates/modalMensaje.html', {
            scope: $scope
        }).then(function(modal) {
            $scope.modal = modal;
        });
    };

    function contarMensajes(){
        $scope.contador = 0;
        for(i in $rootScope.Mensajes)
        {
            if($rootScope.Mensajes[i].estado == "Sin revisar"){
                $scope.contador++;
            }
        }
    };
}]);