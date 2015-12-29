app.controller('LoginCtrl', function($scope){

});

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

})
