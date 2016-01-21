// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
var app = angular.module('app', ['ionic'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    if(window.cordova && window.cordova.plugins.Keyboard) {
      // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
      // for form inputs)
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);

      // Don't remove this line unless you know what you are doing. It stops the viewport
      // from snapping when text inputs are focused. Ionic handles this internally for
      // a much nicer keyboard experience.
      cordova.plugins.Keyboard.disableScroll(true);
    }
    if(window.StatusBar) {
      StatusBar.styleDefault();
    }


    var push = new Ionic.Push({
    });

    push.register(function (token) {
        var GCM = token.token;
        localStorage.setItem("GCM", GCM);
    });

      // kick off the platform web client
    Ionic.io();

      // this will give you a fresh user or the previously saved 'current user'
    var user = Ionic.User.current();

      // if the user doesn't have an id, you'll need to give it one.
    if (!user.id) {
        user.id = Ionic.User.anonymousId();
        // user.id = 'your-custom-user-id';
    }

      //persist the user
    user.save();

  });
})





.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
  $stateProvider
    .state('login', {
      url: '/login',
      templateUrl: 'templates/login.html',
      controller: 'LoginCtrl'
    })

      .state('home', {
        url: '/home',
        templateUrl: 'templates/home.html',
        controller: 'HomeCtrl'
      })

      .state('mensajes', {
        url: '/mensajes',
        templateUrl: 'templates/mensajes.html',
        controller: 'MensajesCtrl'
      })

      .state('estudiante', {
        url: '/estudiante',
        templateUrl: 'templates/menuEstudiante.html',
        controller: 'EstudianteCtrl'
      })

      .state('vigencia', {
        url: '/vigencia',
        templateUrl: 'templates/menuVigencia.html',
        controller: 'CuentaCtrl'
      })

      .state('cuenta', {
        url: '/cuenta',
        templateUrl: 'templates/estadoDeCuenta.html',
        controller: 'CuentaCtrl'
      });
  $urlRouterProvider.otherwise('/login');


  $httpProvider.defaults.useXDomain = true;
  delete $httpProvider.defaults.headers.common['X-Requested-With'];
});