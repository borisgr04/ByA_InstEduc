var byaSite = new Object();
var byaSite = {
    _setToken: function (token) {
        localStorage.setItem("TokenInst", token);
    },
    _getToken: function () {
        return localStorage.getItem("TokenInst");
    },
    _setUsername: function(username){
        localStorage.setItem("username", username)
    },
    _getUsername: function(){
        return localStorage.getItem("username");
    },
    _removeUsername: function () {
        return localStorage.removeItem("username");
    },
    _getUrl: function() {
        return "http://192.168.1.5:8997/api/";
    },
    _setNombreEstudiante: function(nombre_estudiante) {
        localStorage.setItem("nombre_estudiante", nombre_estudiante);
    },
    _getNombreEstudiante: function(){
        return localStorage.getItem("nombre_estudiante");
    },
    _setIdentificacionEstudiante: function(identificacion_estudiante) {
        localStorage.setItem("identificacion_estudiante", identificacion_estudiante);
    },
    _getIdentificacionEstudiante: function(){
        return localStorage.getItem("identificacion_estudiante");
    },
    _setSaldoEstudiante: function(saldo){
        localStorage.setItem('saldo_estudiante', saldo);
    },
    _getSaldoEstudiante: function () {
        return localStorage.getItem('saldo_estudiante');
    }
};
