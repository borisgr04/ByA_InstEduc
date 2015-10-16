$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    },
    getCookie: function (c_name) {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    },
    setCookie: function (c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    }
});
jQuery.fn.extend({
    byaCombo: function (options) {
        placeHolder = options.placeHolder == null ? "Seleccione" : options.placeHolder;
        var IdCombo = $(this);
        IdCombo.children().remove().end();
        //alert($(this).id);
        //PlaceHolder Automatico
        IdCombo.get(0).options[IdCombo.get(0).options.length] = new Option(placeHolder, "");

        $.each(options.DataSource, function (index, item) {
            IdCombo.get(0).options[IdCombo.get(0).options.length] = new Option(item[options.Display], item[options.Value]);
        });
    }
});
jQuery.fn.extend({
    byaSetHabilitar: function (value) {

        if (value) {
            $(this).removeAttr('disabled');

        }
        else {
            $(this).attr('disabled', '-1');

        }
    }
});
jQuery.fn.extend({
    byaSetDecimal: function (valor) {
        $(this).val(valor);
        $(this).formatCurrency();
        $(this).css("text-align", "right");
    }
});
jQuery.fn.extend({
    byaGetDecimal: function () {
        return $(this).asNumber();
    }
});


jQuery.fn.extend({
    DetailsJSON: function (oJSON, DataFields, Titulo) {
        var Tabla = '<table class="table table-hover table-bordered">';
        if (Titulo != null) {
            Tabla += "<tr class='active'><th colspan=2 > " + Titulo + "</th></tr>";
        }
        $.each(DataFields, function (i, itemS) {
            var clase = '';// i % 2 ? 'active' : '';
            if (oJSON[itemS.Campo] != null) Tabla += "<tr class='" + clase + "'  ><th width='30%'>" + itemS.Titulo + "</th><td>" + byaFormatos.aplicarFormato(oJSON[itemS.Campo], itemS.Tipo) + "</td></tr>";
            else Tabla += "<tr class='" + clase + "'  ><th width='30%'>" + itemS.Titulo + "</th><td>" + byaFormatos.aplicarFormato("No especificado", itemS.Tipo) + "</td></tr>";
        });
        Tabla += '</table>';
        $(this).html(Tabla);
    }
});


jQuery.fn.extend({
    msgBox: function (options) {
        var bu = new BootStrap.Utils();
        options.tipo = options.tipo == true ? "success" : options.tipo;
        options.tipo = options.tipo == false ? "danger" : options.tipo;
        bu.crearAlert(options.titulo, options.mensaje, options.tipo, $(this));
    }
});


byaFormatos = (function () {
    "use strict";
    var formatoFechaDMY = function (dateTime) {
        if (dateTime != null) {
            return moment(dateTime).format("DD-MM-YYYY");
        }
    };
    var formatNumerico = function (input) {
        var num = input;
        if (!isNaN(num)) {
            num = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1.');
            num = num.split('').reverse().join('').replace(/^[\.]/, '');
            return num;
        }
        else {
            return "";
        }
    };
    return {
        aplicarFormato: function (valor, tipo) {
            if (tipo == "D") return formatoFechaDMY(valor);
            else if (tipo == "N") return formatNumerico(valor);
            else return valor;
        }
    }
})();



//Patron Módulo
byaMsgBox = (function () {
    "use strict";
    //Zona Privada Auto Ejecutable
    return {
        alert: function (message, callback) {
            bootbox.alert(message, callback); //callback(alert(message));
        },
        confirm: function (message, callback) {
            bootbox.confirm(message, callback);//callback(confirm(message));
        },
        prompt: function (message, callback) {
            bootbox.prompt(message, callback);//callback(prompt(message));
        }
    };
})(); // copia cacheada


(function (BootStrap, $, undefined) {

    var Utils = (function () {
        function Utils() {
            //ctor
        }

        Utils.prototype.createAlert = function (title, message, alertType, targetElement) {
            var html = '<div class="alert alert-' + alertType + '">' +
                                    '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
                                    '<h4>' + title + '</h4>' + message +
                                '</div>'
            $(targetElement).prepend(html);
        }
        Utils.prototype.crearAlert = function (title, message, alertType, targetElement) {
            var html = '<div class="alert alert-' + alertType + '">' +
                                    '<button type="button" class="close" data-dismiss="alert">&times;</button>' +
                                    '<h4>' + title + '</h4>' + message +
                                '</div>'
            targetElement.html(html);
        }

        return Utils;

    })();

    BootStrap.Utils = Utils;

})(window.BootStrap || (window.BootStrap = {}), jQuery);

function byaTablaG(config) {
    config = {
        Id: null,
        Source: null,
        Display: null,
        Value: null,
        fnFormatItem: null,
        lEliminar: false,
        lEditar: false,
        lSeleccionar: false,
        fn_Editar: null,
        fn_Seleccionar: null,
        Enabled: true,
        EnabledDelete: true
    };

    var Seleccionado;

    var _create = function () {

        crearElementos(config.Source, config.fn_callback)
    };

    var crearElementos = function (Source, fn_callback) {
        $(config.Id + " tbody").empty();
        var items = "";
        $.each(Source, function (index, item) {
            //items += '<a href="#" class="list-group-item ' + getClassItem() + '" id="' + item[config.Value] + '">' + config.fnFormatItem(item) + '</a>';
            items += '<tr >' + config.fnFormatItem(item, index) + '</tr>';
        });
        $(config.Id + " tbody").append(items);


        //$('.' + getClassItem()).click(function () {
        //    Seleccionado = $(this).prop("id");
        //    $('.' + getClassItem()).removeClass("active");
        //    $(this).addClass("active");
        //    fn_callback(_getItem(Seleccionado));
        //});

        if (config.lEliminar) {
            $('.' + getClassItem() + "Delete").click(function () {
                if ((config.Enabled) || (config.EnabledDelete)) {
                    if (confirm("¿Desea Eliminar el Registro?")) {
                        var index = $(this).prop("id");
                        delete config.Source[index];
                        config.Source.splice(index, 1);
                        _create();
                    }
                }
            });
        }
        if (config.lEditar) {
            $('.' + getClassItem() + "Edit").click(function () {
                if (config.Enabled) {
                    var index = $(this).prop("id");
                    config.fn_Editar(config.Source[index], index);
                }
            });
        }
        if (config.lSeleccionar) {
            //if (config.Enabled) {
            $('.' + getClassItem() + "Select").click(function () {
                var index = $(this).prop("id");
                config.fn_Seleccionar(config.Source[index], index);
            });
            //}
        }

    };
    var _getItem = function (key) {
        var found = null;
        $.each(config.Source, function (index, item) {
            if (item[config.Value] == key) {
                found = item;
                return;
            }

        });
        return found;
    };
    var _setSeleccionado = function (Value) {
        $('.' + config.ClassItem).removeClass("active");
        $.each(config.Source, function (index, item) {
            if (item[config.Value] == Value) {
                $('#' + Value).addClass("active");
                return;
            }
        });
    };
    var _getSeleccionado = function () {
        return Seleccionado;
    };
    var getClassItem = function () {
        return "clss" + config.Id.substring(1, config.Id.length);
    };
    this.getClassItem = function () {
        return ClassItem();
    };
    this.init = function (Config) {
        config = Config;
        _create();
    };
    this.setSource = function (Source) {
        config.Source = Source;
        _create();
    };
    this.setSeleccionado = function (Value) {
        return _setSeleccionado(Value);
    };
    this.getSeleccionado = function () {
        return _getSeleccionado();
    };
    this.getItem = function () {
        return _getItem(Seleccionado);
    };
    this.getSource = function () {
        return config.Source;
    };
    this.addItem = function (item) {
        config.Source.push(item);
    };
    this.setEnabled = function (value) {
        config.Enabled = value;
    };
    this.getEnabled = function (value) {
        return config.Enabled;
    };
    this.setEnabledDelete = function (value) {
        config.EnabledDelete = value;
    };
    this.getEnabledDelete = function (value) {
        return config.EnabledDelete;
    };
};

function byaListaG(config) {
    config = {
        Id: null,
        Source: null,
        fn_callback: null,
        Display: null,
        Value: null,
        fnFormatItem: null,
    };
    var Seleccionado;
    var _create = function () {
        crearElementos(config.Source, config.fn_callback)
    };

    var crearElementos = function (Source, fn_callback) {
        $(config.Id).html('');
        var items = "";
        $.each(Source, function (index, item) {
            items += '<a href="#" class="list-group-item ' + getClassItem() + '" id="' + item[config.Value] + '">' + config.fnFormatItem(item) + '</a>';
        });
        $(config.Id).append(items);

        $('.' + getClassItem()).click(function () {
            Seleccionado = $(this).prop("id");
            $('.' + getClassItem()).removeClass("active");
            $(this).addClass("active");
            fn_callback(_getItem(Seleccionado));
        });

    };
    var _getItem = function (key) {
        var found = null;
        $.each(config.Source, function (index, item) {
            if (item[config.Value] == key) {
                found = item;
                return;
            }
        });
        return found;
    };
    var _setSeleccionado = function (Value) {
        $('.' + config.ClassItem).removeClass("active");
        $.each(config.Source, function (index, item) {
            if (item[config.Value] == Value) {
                $('#' + Value).addClass("active");
                return;
            }
        });
    };
    var _getSeleccionado = function () {
        return Seleccionado;
    };
    var getClassItem = function () {
        return "clss" + config.Id.substring(1, config.Id.length);
    };
    this.getClassItem = function () {
        return ClassItem();
    };
    this.init = function (Config) {
        config = Config;
        _create();
    };
    this.setSource = function (Source) {
        config.Source = Source;
        _create();
    };
    this.setSeleccionado = function (Value) {
        return _setSeleccionado(Value);
    };
    this.getSeleccionado = function () {
        return _getSeleccionado();
    };
    this.getItem = function () {
        return _getItem(Seleccionado);
    };

};

function byaLista(config) {
    config = {
        Id: null,
        ClassItem: null,
        Source: null,
        fn_callback: null,
        Display: null,
        Value: null,
        Count: null
    };
    var Seleccionado;
    var _create = function () {
        crearEstados(config.Source, config.fn_callback)
    };

    var crearEstados = function (Source, fn_callback) {
        $(config.Id).html('');
        var items = "";
        $.each(Source, function (index, item) {
            items += '<a href="#" class="list-group-item ' + config.ClassItem + '" id="' + item[config.Value] + '"><span class="badge">' + item[config.Count] + '</span>' + item[config.Display] + '</a>';
        });
        $(config.Id).append(items);

        $('.' + config.ClassItem).click(function () {
            Seleccionado = $(this).prop("id");
            $('.' + config.ClassItem).removeClass("active");
            $(this).addClass("active");
            fn_callback();
        });
    };
    var _setSeleccionado = function (Value) {
        Seleccionado = Value;
        $('.' + config.ClassItem).removeClass("active");
        $.each(config.Source, function (index, item) {
            if (item[config.Value] == Value) {
                $('#' + Value).addClass("active");
                return;
            }
        });
    };
    var _getSeleccionado = function () {
        return Seleccionado;
    };

    this.init = function (Config) {
        config = Config;
        _create();
    };
    this.setSource = function (Source) {
        config.Source = Source;
        _create();
    };
    this.setSeleccionado = function (Value) {
        return _setSeleccionado(Value);
    };
    this.getSeleccionado = function () {
        return _getSeleccionado();
    };

};

function byaComboBox(config) {
    config = {
        Id: null,
        Value: null,
        Display: null,
        Source: null
    };
    var _create = function () {
        $(config.Id).byaCombo({ DataSource: config.Source, Value: config.Value, Display: config.Display, placeHolder: config.placeHolder });
    };
    this.getID = function () {
        return $(config.Id);
    };
    var _getItem = function (key) {
        var found = null;
        $.each(config.Source, function (index, item) {
            if (item[config.Value] == key) {
                found = item;
                return;
            }
        });
        return found;
    };
    this.init = function (Config) {
        config = Config;
        _create();
    };
    this.getSource = function () {
        return config.Source;
    };
    this.getSeleccionado = function () {
        return _getItem($(config.Id).val());
    };
    this.DesHabilitar = function () {
        $(config.Id).find("option:selected").attr('disabled', true);
    };
};

Number.prototype.padLeft = function (width, char) {
    if (!char) {
        char = " ";
    }

    if (("" + this).length >= width) {
        return "" + this;
    }
    else {
        return arguments.callee.call(
          char + this,
          width,
          char
        );
    }
};

var ModTer = (function () {
    "use strict";

    var msgPopup = "#msgTer";
    var gridCon = '#jqxgridTer';
    var urlToGridCon = "/Servicios/wsDatosBasicos.asmx/GetTerceros";
    var TerceroSelect = {};
    //crea GridTipos
    var _createGridCon = function () {
        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'IDE_TER', type: 'number' },
                    { name: 'NOMBRE' }
            ],
            async: true,
            record: 'Table',
            url: urlToGridCon
        };
        var cellsrendererNOM = function (row, column, value) {
            return '<div style="margin-left: 5px;margin-top: 5px; font-size: 11px">' + value + '</div>';
        }
        var cellsrendererIDE = function (row, column, value) {
            return '<div style="margin-left: 5px;margin-top: 5px; font-size: 11px">' + value + '</div>';
        }
        var dataAdapter = new $.jqx.dataAdapter(source, { contentType: 'application/json; charset=utf-8' });

        $(gridCon).jqxGrid(
                    {
                        width: '100%',
                        source: dataAdapter,
                        theme: ModTer.config.theme,
                        localization: byaPage.getLocalization(),
                        height: 350,
                        sortable: true,
                        altrows: true,
                        showfilterrow: true,
                        filterable: true,
                        pageable: true,
                        enabletooltips: true,
                        columns: [
                          { text: 'Identificación', datafield: 'IDE_TER', width: 150, cellsformat: 'd', cellsalign: 'right' },
                          { text: 'Apellidos y Nombre', datafield: 'NOMBRE', cellsrenderer: cellsrendererNOM }
                        ]
                    });

        //rowselect
        $(gridCon).bind('rowdoubleclick', function (event) {
            var selectedRowIndex = event.args.rowindex;
            var datarow = {};
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'IDE_TER');
            datarow["IDE_TER"] = cell.value;
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'NOMBRE');
            datarow["NOMBRE"] = cell.value;
            ModTer.fnresultado(datarow);
            _cerrarVentana();
        });
        $(gridCon).bind('rowclick', function (event) {
            var selectedRowIndex = event.args.rowindex;
            var datarow = {};
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'IDE_TER');
            TerceroSelect.IDE_TER = "" + cell.value + "";

            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'NOMBRE');
            TerceroSelect.NOMBRE = cell.value;
        });

    };
    var _cerrarVentana = function () {

        $('#modalTer').modal('hide');
        //_createGridCon();
    };
    var _verVentana = function () {

        $('#modalTer').modal('show');
        //_createGridCon();
    };
    return {
        fnresultado: null,
        config: {
            theme: null
        },
        init: function () {

            this.config.theme = byaSite.tema;
            _createGridCon();
        },
        showWindow: function (fnresultado) {
            this.fnresultado = fnresultado;
            _verVentana();
        },
        getTerceroSelect: function () {
            return TerceroSelect;
        }
    };
}());

(function (a) {
    a.fn.byaFormatInput = function (b) {
        a(this).on({
            keypress: function (a) {
                var c = a.which,
                  d = a.keyCode,
                  e = String.fromCharCode(c).toLowerCase(),
                  f = b;
                (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault()
            }
        })
    }
})(jQuery);


/* EJEMPLO DE USU DE CLASES
var Example = (function () {
    "use strict";

    var elem,
        hideHandler,
        that = {};

    that.init = function (options) {
        elem = $(options.selector);
    };

    that.show = function (text) {
        clearTimeout(hideHandler);

        elem.find("span").html(text);
        elem.delay(200).fadeIn().delay(4000).fadeOut();
    };

    return that;
}());
var modalMsgBox = (function () {
    "use strict";

    var elem,
        hideHandler,
        that = {};

    that.init = function (options) {
        elem = $(options.selector);
    };

    that.show = function (text) {
        //clearTimeout(hideHandler);
        elem.find("span").html(text);
        $(elem).modal('show');
        //elem.delay(200).fadeIn().delay(4000).fadeOut();
    };

    return that;
}());
*/