var byaPage = new Object();

var byaPage = {
    IntegradoSIF: true,
    editGrid: "dblclick",
    valorCalculado: 0,
    init: function () {

    },
    JSONtoString: function (json) {
        // aqui desarrollo una funcionalidad concreta
        return JSON.stringify(json);
    },
    msgJson: function (json) {
        alert(this.JSONtoString(json));
    },
    ajaxError: function (jqXHR, status, error) {
        //alert(error + "-" + jqXHR.responseText);
        alert("Ajax " + error + " - " + jqXHR.responseText);
        //alert(status);
    },
    POST_Sync: function (urlToHandler, jsonData, fnsuccess) {
        $.ajax({
            type: "POST",
            url: urlToHandler,
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: fnsuccess,
            error: byaPage.ajaxError
        });
    },
    POST_Async: function (urlToHandler, jsonData, fnsuccess) {
        $.ajax({
            type: "POST",
            url: urlToHandler,
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: fnsuccess,
            error: byaPage.ajaxError
        });
    },
    retObj: function (d) {
        return (typeof d) == 'string' ? eval('(' + d + ')') : d;
    },
    msgResult: function (msg, HayError) {
        if (!HayError) {
            msg.removeClass('error');
            msg.addClass('information');
        }
        else {
            msg.removeClass('information');
            msg.addClass('error');
        }
    },
    pantallacompleta: function (pagina) {
        var opciones = ("toolbar=no,location=no, directories=no, status=no, menubar=no , resizable=no, fullscreen=yes");
        window.open(pagina, "", opciones, false);
    },
    AbrirPagina: function (url) {
        self.location.href = url;
    },
    msgLimpiar: function (msg) {
        msg.removeClass('error');
        msg.removeClass('information');
        msg.html("");
    },
    getLocalization: function () {
        var localizationobj = {};
        localizationobj.percentsymbol = "%";
        localizationobj.currencysymbol = "$";
        localizationobj.decimalseparator = '.';
        localizationobj.thousandsseparator = ',';
        localizationobj.pagergotopagestring = "Ir a:";
        localizationobj.pagershowrowsstring = "Mostrar filas:";
        localizationobj.pagerrangestring = " de ";
        localizationobj.pagerpreviousbuttonstring = "anterior";
        localizationobj.pagernextbuttonstring = "siguiente";
        localizationobj.groupsheaderstring = "Arrastre una columna y dejarlo aquí para agrupar por esa columna";
        localizationobj.sortascendingstring = "Orden Ascendente";
        localizationobj.sortdescendingstring = "Orden Descendente";
        localizationobj.sortremovestring = "Quitar Orden";
        localizationobj.groupbystring = "Agrupar por esta columna";
        localizationobj.groupremovestring = "Elimnar de Grupo";
        localizationobj.filterclearstring = "Quitar Filtro";
        localizationobj.filterstring = "Filtrar";
        localizationobj.filtershowrowstring = "Mostrar Filas Donde :";
        localizationobj.filterorconditionstring = " Ó ";
        localizationobj.filterandconditionstring = " Y ";
        localizationobj.filterstringcomparisonoperators = ['vacio', 'no vacio', 'contains', 'contains(match case)',
        'does not contain', 'does not contain(match case)', 'empieze con', 'empieze con(match case)',
        'termine con', 'termine con(match case)', 'igual', 'igual(match case)', 'null', 'not null'];
        localizationobj.filternumericcomparisonoperators = ['igual', 'diferente', 'menor que', 'menor que o igual', 'mayor que', 'mayor que o igual', 'null', 'not null'];
        localizationobj.filterdatecomparisonoperators = ['igual', 'no igual', 'menor que', 'menor que o igual', 'mayor que', 'mayor que o igual', 'null', 'not null'];

        localizationobj.firstDay = 0;
        var days = {
            // full day names
            names: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            // abbreviated day names
            namesAbbr: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            // shortest day names
            namesShort: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"]
        };
        localizationobj.days = days;

        var months = {
            // full month names (13 months for lunar calendards -- 13th month should be "" if not lunar)
            names: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", ""],
            // abbreviated month names
            namesAbbr: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", ""]
        };
        localizationobj.months = months;

        localizationobj.twoDigitYearMax = 2029;
        var patterns = {
            // short date pattern
            d: "dd/MM/yyyy",
            // long date pattern
            D: "dddd, MMMM dd, yyyy",
            // short time pattern
            t: "h:mm tt",
            // long time pattern
            T: "h:mm:ss tt",
            // long date, short time pattern
            f: "dddd, MMMM dd, yyyy h:mm tt",
            // long date, long time pattern
            F: "dddd, MMMM dd, yyyy h:mm:ss tt",
            // month/day pattern
            M: "MMMM dd",
            // month/year pattern
            Y: "yyyy MMMM",
            // S is a sortable format that does not vary by culture
            S: "yyyy\u0027-\u0027MM\u0027-\u0027dd\u0027T\u0027HH\u0027:\u0027mm\u0027:\u0027ss"
        };
        localizationobj.patterns = patterns;
        return localizationobj;
    },
    returnNombreMes: function (IDMES) {
        if (IDMES == "01") return "Enero";
        if (IDMES == "02") return "Febrero";
        if (IDMES == "03") return "Marzo";
        if (IDMES == "04") return "Abril";
        if (IDMES == "05") return "Mayo";
        if (IDMES == "06") return "Junio";
        if (IDMES == "07") return "Julio";
        if (IDMES == "08") return "Agosto";
        if (IDMES == "09") return "Septiembre";
        if (IDMES == "10") return "Octubre";
        if (IDMES == "11") return "Noviembre";
        if (IDMES == "12") return "Diciembre";
    },
    converJSONDateMDY: function (dateTime) {
        if (dateTime != null) {
            var date = new Date(parseInt(dateTime.substr(6)));
            var formatted = ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                                        ("0" + date.getDate()).slice(-2) + "/" + date.getFullYear();
            //console.log(formatted);
            return formatted;
        }
    },
    converJSONDateDMY: function (dateTime) {
        if (dateTime != null) {
            var date = new Date(parseInt(dateTime.substr(6)));
            var formatted = ("0" + date.getDate()).slice(-2) + "/" +
                                        ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                                          date.getFullYear();
            //console.log(formatted);
            return formatted;
        }
    },
    converJSONDateTime: function (dateTime) {
        if (dateTime != null) {
            var date = new Date(parseInt(dateTime.substr(6)));
            var formatted = date.getFullYear() + "-" +
                                        ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
                                            ("0" + date.getDate() + 1).slice(-2) + " " + date.getHours() + ":" +
                                        date.getMinutes();
            return formatted;
        }
    },
    FechaShortX: function (f) {
        var sf = "";
        sf = f.getFullYear() + "-" + ("0" + (f.getMonth() + 1)).slice(-2) + "-" + ("0" + f.getDate()).slice(-2);
        //sf = ("0" + f.getDate()).slice(-2) + "/" + ("0" + (f.getMonth() + 1)).slice(-2) + "/" + f.getFullYear()
        console.log("Prueba de Asignacion de Fecha, se probo y funcionó..." + sf);
        return sf;
    },
    parseJsonDate: function (jsonDate) {
        var offset = new Date().getTimezoneOffset() * 60000;
        var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

        if (parts[2] == undefined)
            parts[2] = 0;

        if (parts[3] == undefined)
            parts[3] = 0;

        return new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
    },
    getASync: function (urlToHandler, jsonData, fnsuccess) {
        $.ajax({
            type: "GET",
            url: urlToHandler,
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: fnsuccess,
            error: byaPage.ajaxError
        });
    },
    getSource: function (Servicio, dataJSON) {
        var source = {};
        $.ajax({
            type: "GET",
            url: Servicio,
            data: dataJSON,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                source = result.d;
            },
            error: function (jqXHR, status, error) {
                alert(error + "-" + jqXHR.responseText);
            }
        });
        return source;
    },
    getSource: function (Servicio, dataJSON) {
        var source = {};
        $.ajax({
            type: "GET",
            url: Servicio,
            data: dataJSON,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                source = result.d;
            },
            error: function (jqXHR, status, error) {
                alert(error + "-" + jqXHR.responseText);
            }
        });
        return source;
    },
    postSource: function (Servicio, dataJSON) {
        var source = {};
        //this.msgJson  (dataJSON);
        $.ajax({
            type: "POST",
            url: Servicio,
            data: dataJSON,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                source = result.d;
            },
            error: function (jqXHR, status, error) {
                alert(error + "-" + jqXHR.responseText);
            }
        });
        return source;
    },
    quitarNumeral: function (cadena) {
        cadena = "" + cadena + "";
        cadena = cadena.replace("#", "");
        return cadena;
    },
    converJSONDate: function (dateTime) {
        if (dateTime != null) {
            var date = new Date(parseInt(dateTime.substr(6)));
            var formatted = date.getFullYear() + "-" +
                                        ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
                                            ("0" + date.getDate()).slice(-2);
            console.log(formatted);
            return formatted;
        }
    },
    _getDatosCampos: function (nomClass) {
        var e = {};
        $("." + nomClass).each(function (index) {
            var id = "" + $(this).attr("id") + "";
            var nomCampo = id.substring(3, id.length);
            e[nomCampo] = $(this).val();
        });
        return e;
    },
    _setDatosCampos: function (nomClass, obj) {
        var lPropiedades = Object.keys(obj);
        $.each(lPropiedades, function (indexobj, item) {
            $("." + nomClass).each(function (index) {
                var id = "" + $(this).attr("id") + "";
                var nomCampo = id.substring(3, id.length);
                if (nomCampo == item) {
                    if ($(this).attr("type") != "date") $(this).val(obj[item]);
                    else $(this).val(byaPage.converJSONDate(obj[item]));
                }

            });
        });
    },
    txtSoloNumeros: function () {
        if ((event.keyCode < 48) || (event.keyCode > 57))
            event.returnValue = false;
    },
    TieneLetras: function (texto) {
        var letras = "abcdefghyjklmnñopqrstuvwxyz";
        texto = texto.toLowerCase();
        for (i = 0; i < texto.length; i++) {
            if (letras.indexOf(texto.charAt(i), 0) != -1) {
                return true;
            }
        }
        return false;
    },
    generarAños: function (añoInicio) {
        var año = parseInt(añoInicio);
        var laños = new Array();
        var i;
        for (i = 0; i <= 100; i++) {
            var e = {}
            e.Año = año + i;
            laños.push(e);
        }
        return laños;
    },
    formatNumber: {
        separador: ".", // separador para los miles
        sepDecimal: ',', // separador para los decimales
        formatear: function (num) {
            num += '';
            var splitStr = num.split('.');
            var splitLeft = splitStr[0];
            var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
            var regx = /(\d+)(\d{3})/;
            while (regx.test(splitLeft)) {
                splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
            }
            return this.simbol + splitLeft + splitRight;
        },
        new: function (num, simbol) {
            this.simbol = simbol || '';
            return this.formatear(num);
        }
    },
    convertOBJ: function (result) {
        return (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d
    },
    irInicio: function () {
        $('html, body').animate({
            scrollTop: '0px'
        }, 1500);
    },
    irFin: function () {
        $('html, body').animate({
            scrollTop: $(document).height()
        }, 1500);
    },
    getSourceApi: function (Servicio, dataJSON) {
        var source = {};
        $.ajax({
            type: "GET",
            url: Servicio,
            data: dataJSON,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                source = result;
            },
            error: function (jqXHR, status, error) {
                alert(error + "-" + jqXHR.responseText);
            }
        });
        return source;
    },
    postSourceApi: function (Servicio, dataJSON) {
        var source = {};
        //this.msgJson  (dataJSON);
        $.ajax({
            type: "POST",
            url: Servicio,
            data: dataJSON,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                source = result;
            },
            error: function (jqXHR, status, error) {
                alert(error + "-" + jqXHR.responseText);
            }
        });
        return source;
    },
    parseStrDate: function (strDate) {
        strDate = "" + strDate + "";
        var año = parseInt(strDate.substr(0, 4));
        var mes = parseInt(strDate.substr(5, 2)) - 1;
        var dia = parseInt(strDate.substr(8, 2));
        var objDate = new Date(año, mes, dia);
        return objDate;
    },
    getDateNow: function () {
        var date = new Date();
        var datenow = new Date(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes());
        return datenow;
    }
}