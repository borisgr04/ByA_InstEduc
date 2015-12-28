var byaSite = new Object();
var byaSite = {
    pedirTokenSiempre: true,
    siAlert:true,
     _setVar: function (name,obj) {
        localStorage.setItem(name, JSON.stringify(obj));
     },
     _getVar: function (name) {
        return JSON.parse(localStorage.getItem(name));
     },
     _removeVar: function (name) {
        localStorage.removeItem(name);
     },
     alert: function (value) {
         if (this.siAlert) {
             alert(JSON.stringify(value));
         }
     },
     console: function (value) {
         if (this.siAlert) {
             console.log(JSON.stringify(value));
         }
     }
};
