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
    var url = byaSite._getUrl() + "terceros/InformacionAcudientesMensajes/username";
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
}]);

app.service('mensajesServices', ['$http', function($http){
    var url = byaSite._getUrl() + "Mensajes/";
    this.getCambiarEstado = function(id_acudiente, id_mensaje){
        var pet = {
            method: 'POST',
            url: url + "idAcudiente/" + id_acudiente + "/idMensaje/" + id_mensaje,
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + byaSite._getToken(),
                'Content-Type': 'application/json'
            }
        };
        var req = $http(pet);
        return req;
    };

    this.PostCambiarMensajeInactivo = function(ListMsjeDto)
    {
        var pet = {
            method: 'POST',
            url: url + '/EliminarMensajes',
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + byaSite._getToken(),
            },
            data: ListMsjeDto
        };
        var req = $http(pet);
        return req;
    };
}]);

app.service('estadoCuentaServices', ['$http', function($http){
    var url = byaSite._getUrl() + "EstadoCuentaResumen";
    this.getEstadoCuenta = function(id_estudiante){
        var pet = {
            method: 'GET',
            url: url + "/" + id_estudiante,
            headers: {
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + byaSite._getToken(),
                'Content-Type': 'application/json'
            }
        };
        var req = $http(pet);
        return req;
    };
}]);
