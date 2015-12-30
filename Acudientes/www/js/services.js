app.service('loginServices', ['$http', function($http){
    var url = "http://localhost:49811/";
    this.Login = function(username, password){
        var dat = 'grant_type=password&username=' + username + '&password=' + password;
        var pet = {
            method: 'POST',
            url: url + 'Token',
            headers: {
                'Accept': 'application/x-www-form-urlencoded',
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            data: dat
        };
        var req = $http(pet);
        return req;
    };
}]);

app.service('homeServices', ['$http', function($http){
    var url = "http://localhost:49811/api/terceros/InformacionAcudientesMensajes/username";
    this.getInformacionAcudienteMensajes = function(username){
        var pet = {
            method: 'GET',
            url: 'http://localhost:49811/api/terceros/InformacionAcudientesMensajes/username/' + username,
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + byaSite._getToken(),
                'Content-Type': 'application/json'
            }
        };
        var req = $http(pet);
        return req;
    };

    this._obtenerCuestionario = function (tip_ide, ide) {

        tip_ide = tip_ide != null ? tip_ide : "";
        ide = tip_ide != null ? ide : "";

        var pet = {
            method: 'GET',
            url: 'http://186.170.31.187/DPS/VerificacionCiudadano/InfraVerificaCiudadanoWebAPI/ObtenerCuestionario?CantidadPreguntas=3&CantidadRespuestas=3&NumeroDocumento=' + ide + '&TipoDocumento=' + tip_ide,
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + byaSite._getToken(),
                'Content-Type': 'application/json'
            }
        }
        var req = $http(pet);
        return req;
    };
}]);
