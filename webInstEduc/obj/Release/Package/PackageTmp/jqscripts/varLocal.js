var varLocal = {
    Set: function (name,obj) {
        localStorage.setItem(name, JSON.stringify(obj));
    },
    Get: function (name) {
        return JSON.parse(localStorage.getItem(name));
    },
    Remove: function (name) {
        localStorage.removeItem(name);
    }
}