app.service("gradosService", function ($http) {
    this.Gets = function () {
        var req = $http.get('/api/Grados');
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/Grados', lReg);
        return req;
    };
});
app.service("cursosService", function ($http) {
    this.GetsCursosGrado = function (id_grado) {
        var req = $http.get('/api/Cursos/Grado/' + id_grado);
        return req;
    };
    this.Gets = function () {
        var req = $http.get('/api/Cursos');
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/Cursos', lReg);
        return req;
    };
});
app.service("estudiantesService", function ($http) {
    this.Get = function (id) {
        var req = $http.get('/api/Estudiantes/' + id);
        return req;
    };
    this.GetXId = function (id) {
        var req = $http.get('/api/Estudiantes/Id/' + id);
        return req;
    };
    this.Insert = function (estudiante) {
        var req = $http.post('/api/Estudiantes',estudiante);
        return req;
    };
    this.Update = function (estudiante) {
        var req = $http.put('/api/Estudiantes', estudiante);
        return req;
    };
    this.GetsFiltro = function (id) {
        var req = $http.get('/api/Estudiantes/Filtro/' + id);
        return req;
    };
    this.GetsOrden = function (orden, filtro) {
        var req = $http.get('/api/Estudiantes/Orden/' + orden + '/' + filtro);
        return req;
    };
});
app.service("tercerosService", function ($http) {
    this.Get = function (id) {
        var req = $http.get('/api/Terceros/' + id);
        return req;
    };
    this.Gets = function () {
        var req = $http.get('/api/Terceros/');
        return req;
    };
    this.InsertOrUpdate = function (acudiente) {
        var req = $http.post('/api/Acudientes', estudiante);
        return req;
    };
    this.Post = function (estudiante) {
        var req = $http.post('/api/Terceros/', estudiante);
        return req;
    };
    this.Put = function (estudiante) {
        var req = $http.put('/api/Terceros/', estudiante);
        return req;
    };
});
app.service("matriculasService", function ($http) {
    this.Post = function (matricula) {
        var req = $http.post('/api/Matriculas/', matricula);
        return req;
    };
    this.PostAnular = function (id_matricula) {
        var req = $http.post('/api/Matriculas/Anular/' + id_matricula, null);
        return req;
    };
    this.RetirarEstudiante = function (id_estudiante) {
        var req = $http.post('/api/Matriculas/Retirar/Estudiante/' + id_estudiante, null);
        return req;
    };
    this.Get = function (vigencia, id_estudiante) {
        var req = $http.get('/api/Matriculas/Vigencia/' + vigencia + "/Estudiante/" + id_estudiante);
        return req;
    };
    this.Gets = function (objconsulta) {
        var req = $http.post('/api/Matriculas/Vigencia/Grado/Curso/', objconsulta);
        return req;
    };
});
app.service("carteraService", function ($http) {
    this.GetVisualizacionCarteraAntes = function (grado, vigencia, vigenciaActual, periodActual) {
        var req = $http.get('/api/Cartera/Visualizacion/Grado/' + grado + '/Vigencia/' + vigencia + '/VigenciaActual/' + vigenciaActual + '/PeriodoActual/' + periodActual);
        return req;
    };
    this.GetCarteraCausadaEstudiante = function (id_estudiante, id_grupo) {
        var req = $http.get('/api/Cartera/Causado/Estudiante/' + id_estudiante + '/Grupo/' + id_grupo);
        return req;
    };
    this.GetCarteraCausadaEstudianteValor = function (id_estudiante, id_grupo, Valor) {
        var req = $http.get('/api/Cartera/Causado/Estudiante/' + id_estudiante + '/Grupo/' + id_grupo + '/Valor/' + Valor);
        return req;
    };
    this.GetEstadoCuentaEstudiante = function (id_estudiante) {
        var req = $http.get('/api/Cartera/EstadoCuenta/Estudiante/' + id_estudiante);
        return req;
    };
    this.GetCarteraConceptosEstudiantes = function () {
        var req = $http.get('/api/Cartera/Conceptos');
        return req;
    };
    this.GetCarteraEstudiantes = function (id_estudiante) {
        var req = $http.get('/api/Cartera/' + id_estudiante);
        return req;
    };
    this.PostCarteraEstudiante = function (lCarteras) {
        var req = $http.post('/api/Cartera/', lCarteras);
        return req;
    };

    // Pagar Solo
    this.GetCarteraCausadaEstudiantePS = function (objConsulta) {
        var req = $http.post('/api/Cartera/Causado/Estudiante', objConsulta);
        return req;
    };
    this.GetCarteraCausadaEstudianteValorPS = function (objConsulta) {
        var req = $http.post('/api/Cartera/Causado/Estudiante/Valor', objConsulta);
        return req;
    };
    // Liquidar Provisionar
    this.GetCarteraCausadaEstudianteL = function (objConsulta) {
        var req = $http.post('/api/Cartera/Causado/Estudiante/Liquidacion', objConsulta);
        return req;
    };
    this.GetCarteraCausadaEstudianteValorL = function (objConsulta) {
        var req = $http.post('/api/Cartera/Causado/Estudiante/Valor/Liquidacion', objConsulta);
        return req;
    };

});
app.service("pagosService", function ($http) {
    this.GetLiquidacion = function (id_liquidacion) {
        var req = $http.get('/api/Pagos/' + id_liquidacion);
        return req;
    };
    this.GetsLiquidacionesEstudiante = function (id_estudiante, id_grupo) {
        var req = $http.get('/api/Pagos/Liquidaciones/Estudiante/' + id_estudiante + '/grupo/' + id_grupo);
        return req;
    };
    this.GetsLiquidacionesEstudianteSG = function (id_estudiante) {
        var req = $http.get('/api/Pagos/Liquidaciones/Estudiante/' + id_estudiante);
        return req;
    };
    this.Liquidar = function (objLiquidacion) {
        var req = $http.post('/api/Pagos/Liquidar', objLiquidacion);
        return req;
    };
    this.Pagar = function (objLiquidacion) {
        var req = $http.post('/api/Pagos/Pagar', objLiquidacion);
        return req;
    };
    this.PagarLiquidacion = function (id_liquidacion) {
        var req = $http.post('/api/Pagos/PagarLiquidacion', id_liquidacion);
        return req;
    };
    this.PagosEstudiante = function (obj_consulta) {
        var req = $http.post('/api/Pagos/Estudiante', obj_consulta);
        return req;
    };
    this.AnularPago = function (id_pago) {
        var req = $http.post('/api/Pagos/Anular/' + id_pago, null);
        return req;
    };
    this.AnularLiquidacion = function (id_pago) {
        var req = $http.post('/api/Pagos/Anular/Liquidacion/' + id_pago, null);
        return req;
    };
   
});
app.service("periodosService", function ($http) {
    this.GetAll = function () {
        var req = $http.get('/api/Periodos');
        return req;
    };
    this.GetVigencia = function (vigencia) {
        var req = $http.get('/api/Periodos/' + vigencia);
        return req;
    };

    this.Gets = function (vigencia) {
        var req = $http.get('/api/Periodos/Gets/' + vigencia);
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/Periodos', lReg);
        return req;
    };
});
app.service("tarifasService", function ($http) {
    this.GetAll = function () {
        var req = $http.get('/api/Tarifa');
        return req;
    };
    this.Get = function (id) {
        var req = $http.get('/api/Tarifa/' + id);
        return req;
    };
    this.GetVigencia = function (vigencia) {
        var req = $http.get('/api/Tarifa/Vigencia/' + vigencia);
        return req;
    };
    this.GetVigGrad = function (vigencia,grado) {
        var req = $http.get('/api/Tarifa/Vigencia/' + vigencia + "/Grado/" + grado);
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/Tarifa', lReg);
        return req;
    };
});
app.service("grupospagosService", function ($http) {
    this.Gets = function () {
        var req = $http.get('/api/GruposPagos/');
        return req;
    };
});
app.service("estudiantessaldosService", function ($http) {
    this.GetSaldo = function (id_estudiante) {
        var req = $http.get('/api/Estudiantes/' + id_estudiante + '/Saldos');
        return req;
    };
    this.GetSaldoVigencia = function (id_estudiante,vigencia) {
        var req = $http.get('/api/Estudiantes/' + id_estudiante + '/Saldos/vigencia/' + vigencia);
        return req;
    };
    this.GetSaldoVigenciaPeriodo = function (id_estudiante, vigencia,periodo) {
        var req = $http.get('/api/Estudiantes/' + id_estudiante + '/Saldos/vigencia/' + vigencia + '/periodo/' + periodo);
        return req;
    };
});
app.service("conceptosService", function ($http) {
    this.Gets = function () {
        var req = $http.get('/api/Conceptos');
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/Conceptos',lReg);
        return req;
    };
});
app.service("tipoDocumentosService", function ($http) {
    this.Gets = function () {
        var req = $http.get('/api/TipoDocumentos');
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/TipoDocumentos', lReg);
        return req;
    };
});
app.service("tasasService", function ($http) {
    this.Gets = function () {
        var req = $http.get('/api/Tasas');
        return req;
    };
    this.Post = function (lReg) {
        var req = $http.post('/api/Tasas', lReg);
        return req;
    };
});
app.service("entidadService", function ($http) {
    this.Get = function () {
        var req = $http.get('/api/Entidad');
        return req;
    };
    this.Gets = function () {
        var req = $http.get('/api/Entidad');
        return req;
    };
    this.GetLogo = function () {
        var req = $http.get('/api/Entidad/Logo');
        return req;
    };
    this.Put = function (Reg) {
        var request = $http({
            method: "put",
            url: uri + '/api/Entidad',
            data: Reg
        });
        return request;
    };
    this.Patch = function (frmimagen) {
        var request = $http({
            method: 'patch',
            url: "/api/Entidad/Logo",
            headers: { 'Content-Type': false },
            data: frmimagen
        }).
        success(function (data, status, headers, config) {
            return data;
        }).
        error(function (data, status, headers, config) {
            return data;
        });
    };
});
app.service("movimientosService", function ($http) {
    this.MovimientosEstudiante = function (obj_consulta) {
        var req = $http.post('/api/Movimientos/Estudiante', obj_consulta);
        return req;
    };
});
app.service("deudasgradosService", function ($http) {
    this.DeudasGrados = function () {
        var req = $http.get('/api/DeudaGrados');
        return req;
    };
    this.DeudasCursosGrado = function (id_grado) {
        var req = $http.get('/api/DeudaGrados/' + id_grado);
        return req;
    };
    this.DeudasEstudiantesCursoGrado = function (id_curso) {
        var req = $http.get('/api/DeudaGrados/Curso/' + id_curso);
        return req;
    };
});
app.service("vigenciasService", function ($http) {
    this.Post = function (lVigencias) {
        var req = $http.post('/api/Vigencias', lVigencias);
        return req;
    };
    this.Gets = function () {
        var req = $http.get('/api/Vigencias/Gets');
        return req;
    };
    this.GetAll = function () {
        var req = $http.get('/api/Vigencias');
        return req;
    };
});
app.service("configGruposPagosService", function ($http) {
    this.Post = function (lGrupos) {
        var req = $http.post('/api/ConfiguracionGrupoPago', lGrupos);
        return req;
    };
    this.GetVigencia = function (vigencia) {
        var req = $http.get('/api/ConfiguracionGrupoPago/Vigencia/' + vigencia);
        return req;
    };
    this.GetAll = function () {
        var req = $http.get('/api/ConfiguracionGrupoPago');
        return req;
    };
});
app.service("fechaCausacionService", function ($http) {
    this.Get = function () {
        var req = $http.get('/api/FechaCausacion/');
        return req;
    };
    this.Post = function (Fecha) {
        var req = $http.post('/api/FechaCausacion/', Fecha);
        return req;
    };
});