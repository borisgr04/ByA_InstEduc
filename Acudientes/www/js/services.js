app.service('loginServices', ['$http', function($http){
    var url = "http://localhost:17293/";
    this.Login = function(username, password){
        var req = $http.get(url + "/username" + username + "/password" + password);
        return req;
    };
}]);
