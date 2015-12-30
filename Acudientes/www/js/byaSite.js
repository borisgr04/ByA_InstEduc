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
    }
};
