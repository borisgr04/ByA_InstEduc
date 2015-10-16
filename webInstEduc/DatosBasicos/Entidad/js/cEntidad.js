// create the controller and inject Angular's $scope
app.controller('cEntidad', function ($scope, entidadService) {
    $scope.entidad = {};
    $scope._nuevo = function () {
        _nuevo();
    };
    $scope._guardar = function () {
        if (_esValido("datosEntidad")) _guardar();
    };
    $scope._guardarImagen = function () {
        var file = $("#file").get(0).files;
        if (file.length > 0) {
            file = file[0];
            var data = new FormData();
            data.append("file", file);
            alert(file.name)
            _guardarImagen(data);
        }
    };
    _init();
    function _init() {
        byaSite.SetModuloP({ TituloForm: "Entidad", Modulo: "Datos Basicos", urlToPanelModulo: "Entidad.aspx", Cod_Mod: "DATOB", Rol: "DATOBEntidad" });
        _traerEntidad();
    };
    $("#cancelar").click(function () {
        $("#imagen").attr("src", "/api/Entidad/Logo");
        $('input[type=file]').val(null);
    });

    //Esta funcion se enecarga de colocar la imagen en la pagina.
    $('input[type=file]').change(function (e) {
        var file = $(this)[0].files[0],
      imageType = /image.*/;
        if (!file.type.match(imageType)) {
            $("#LbMsg").msgBox({ titulo: "", mensaje: "<br/>- Actualizar : No es un archivo de imagen valido", tipo: false });
            return;
        }
        var reader = new FileReader();
        reader.onload = fileOnload;
        reader.readAsDataURL(file);
    });
    function fileOnload(e) {
        var result = e.target.result;
        cargarImagen(result)
    }
    function cargarImagen(result) {
        if (result == null) {
            $scope.entidad.logo = null;
            $("#imagen").removeAttr("src");
        }
        else {
            $scope.entidad.logo = null;
            $("#imagen").attr("src", result);
        }
    }

    function _traerEntidad() {
        var serEntidad = entidadService.Gets();
        serEntidad.then(function (pl) {
            $scope.entidad = pl.data;
            if (pl.data.logo == undefined) {
                $scope.entidad.logo = null;
            }
            else {
                if (pl.data.logo == "" || pl.data.logo == null) {
                    $scope.entidad.logo = null;
                }
            }

        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _esValido(nameForm) {
        var error = false;
        var sRequired = $scope[nameForm].$error.required;
        if ((sRequired != null) && (sRequired != false)) {
            error = true;
        }

        if (error) {
            $("#LbMsg").msgBox({ titulo: "Error:", mensaje: "Los campos resaltados en rojo son obligatorios...", tipo: false });
            $("input.ng-invalid").css("border", " 1px solid red");
            $("select.ng-invalid").css("border", " 1px solid red");
            return !error;
        } else {
            $("input.ng-valid").css("border", "");
            $("select.ng-valid").css("border", "");
            $("#LbMsg").html("");
            return true;
        }

    };
    function _nuevo() {
        //var e = {};
        //$scope.tipoDocumentos.push(e);
        //byaPage.irFin();
    };



    function _guardar() {
        var serEntidad = entidadService.Post($scope.entidad);
        serEntidad.then(function (pl) {
            _revisarErrores(pl.data);
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _guardarImagen(data) {
        var serEntidad = entidadService.Patch(data);
        serEntidad.then(function (pl) {
            _revisarErrores(pl.data);
        }, function (errorPl) {
            console.log(JSON.stringify(errorPl));
        });
    };
    function _revisarErrores(lResp) {
        var error = false;
        var cadenaRespuesta = "Error: ";
        $.each(lResp, function (index, item) {
            if (item.Error) {
                error = true;
                if (item.id != null) cadenaRespuesta += "<br/>- " + item.id + " : " + item.Mensaje;
                else cadenaRespuesta += "<br/>- Nuevo : " + item.Mensaje;
            }
        });

        if (!error) {
            cadenaRespuesta = "Operación Realizada Satisfactoriamente";
            _traerEntidad();
        }

        $("#LbMsg").msgBox({ titulo: "", mensaje: cadenaRespuesta, tipo: !error });
    };
});