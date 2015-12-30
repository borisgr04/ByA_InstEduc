app.controller('LoginCtrl', ['loginServices', '$scope', function(loginServices, $scope){

}]);
app.controller('HomeCtrl', function($scope){
    $scope.Acudiente = {
        nombre: "Carlos Tirado",
        cedula: "1,890,567,234",
        email: "visual-andrea@gmail.com"
    };
    $scope.Estudiantes = [
        {
            idEstudiante: "1,899,909,786",
            nombre: "C# José"
        },
        {
            idEstudiante: "1,678,998,112",
            nombre: "Visual Andréa"
        }
    ];
    $scope.mensajes = 3;
});

app.controller('MensajesCtrl', function($scope, $ionicModal){
    $scope.Mensajes = [
       {
           nombre: 'mensaje 1',
           tipo: 'Urgente',
           contenido: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
           estado: 'Revisado'
       },
       {
           nombre: 'mensaje 2',
           tipo: 'Informativo',
           contenido: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
           estado: 'Sin revisar'
       },
       {
           nombre: 'mensaje 3',
           tipo: 'Informativo',
           contenido: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
           estado: 'Sin revisar'
       },
       {
           nombre: 'mensaje 4',
           tipo: 'Informativo',
           contenido: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
           estado: 'Sin revisar'
       }
   ];
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
      for(i in $scope.Mensajes)
      {
          if($scope.Mensajes[i].estado == "Sin revisar"){
              $scope.contador++;
          }
      }
    };
});
