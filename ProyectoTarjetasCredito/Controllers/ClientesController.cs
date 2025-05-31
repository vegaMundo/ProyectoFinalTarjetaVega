using EstructurasDatos.Arboles;
using EstructurasDatos.Cola;
using EstructurasDatos.Datos;
using EstructurasDatos.Lista;
using EstructurasDatos.Pilas;
using EstructurasDatos.TablaHash;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTarjetasCredito.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        // Estructuras de datos estáticas compartidas entre peticiones
        public static ListaClientes Clientes = new ListaClientes();
        public static ArbolBinarioBusqueda arbolclienteid = new ArbolBinarioBusqueda();
        public static ListaTarjetas tarjetas = new ListaTarjetas();
        public static DispersionHash tablaHash = new DispersionHash();
        public static ListaTransacciones Transacciones = new ListaTransacciones();
        public static Cola colaGestiones = new Cola();
        public static Pila pilaBloqueosTemporales = new Pila(); // Guarda tarjetas bloqueadas temporalmente
        public static ArbolAVL arbolClienteDPI = new ArbolAVL();
        // public static ArbolAVL arbolAprobados = new ArbolAVL(); // Opcional, si lo necesitas

        // 1. API CRUD de Clientes de Tarjeta de Crédito

        // GET: api/clientes
        [HttpGet]
        public IActionResult ObtenerListaClientes()
        {
            var lista = Clientes.ObtenerListaClientes();
            return Ok(lista);
        }

        // GET: api/clientes/id/123
        [HttpGet("id/{id}")]
        public IActionResult GetClientePorId(int id)
        {
            Cliente clienteBuscado = new Cliente { IdCliente = id };
            var nodo = arbolclienteid.buscar(clienteBuscado);

            if (nodo == null)
                return NotFound($"No se encontró el cliente con Id {id}");

            Cliente cliente = (Cliente)nodo.valorNodo();
            return Ok(cliente);
        }

        // POST: api/clientes/INSERTARNUEVO
        [HttpPost("INSERTARNUEVO")]
        public IActionResult InsertarCliente([FromBody] Cliente nuevoCliente)
        {
            if (nuevoCliente == null)
                return BadRequest("El cliente no puede ser nulo");

            Cliente clienteExistente = new Cliente { IdCliente = nuevoCliente.IdCliente };
            if (arbolclienteid.buscar(clienteExistente) != null)
                return Conflict($"Ya existe un cliente con ID {nuevoCliente.IdCliente}");

            arbolclienteid.insertar(nuevoCliente);
            arbolClienteDPI.Insertar(nuevoCliente);
            Clientes.InsertarAlInicio(nuevoCliente);

            if (nuevoCliente.Tarjetas != null)
            {
                foreach (var tarjeta in nuevoCliente.Tarjetas)
                {
                    tarjeta.IdCliente = nuevoCliente.IdCliente;
                    tarjetas.InsertarAlInicio(tarjeta);
                    tablaHash.Insertar(tarjeta, tarjeta.NumeroTarjeta);
                }
            }

            return CreatedAtAction(nameof(GetClientePorId), new { id = nuevoCliente.IdCliente }, nuevoCliente);
        }

        // PUT: api/clientes/actualizar?id={id}
        [HttpPut("actualizar")]
        public IActionResult ActualizarCliente(int id, [FromBody] Cliente clienteActualizado)
        {
            if (clienteActualizado == null || clienteActualizado.IdCliente != id)
                return BadRequest("Datos inválidos");

            Cliente clienteBuscado = new Cliente { IdCliente = id };
            EstructurasDatos.Arboles.Nodo nodo = arbolclienteid.buscar(clienteBuscado);

            if (nodo == null)
                return NotFound($"Cliente con ID {id} no encontrado");

            arbolclienteid.eliminar(clienteBuscado);
            arbolclienteid.insertar(clienteActualizado);

            bool actualizadoEnLista = Clientes.ActualizarCliente(id, clienteActualizado);
            if (!actualizadoEnLista)
                return StatusCode(500, "Error al actualizar en la lista");

            return Ok(clienteActualizado);
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var clienteBuscado = new Cliente { IdCliente = id };
            var nodo = arbolclienteid.buscar(clienteBuscado);

            if (nodo == null)
                return NotFound($"No se encontró el cliente con Id {id}");

            arbolclienteid.eliminar(clienteBuscado);

            return Ok($"Cliente {id} eliminado correctamente.");
        }


        // 2. API de Carga Inicial
        // GET: api/clientes/Tarjetas Carga Inicial
        [HttpGet("Tarjetas Carga Inicial")]
        public IActionResult VerArbol()
        {
            var resultado = arbolclienteid.inorden();
            return Ok(resultado);
        }
        // 3. API de Consulta de Saldo
        [HttpGet("tarjeta/{numeroTarjeta}/saldo")]
        public IActionResult ConsultarSaldo(int numeroTarjeta)
        {
            var tarjeta = tablaHash.Buscar(numeroTarjeta) as Tarjeta;

            if (tarjeta == null)
                return NotFound($"No se encontró la tarjeta con número {numeroTarjeta}");

            return Ok(new { tarjeta.NumeroTarjeta, Saldo = tarjeta.Saldo });
        }
        // 4. API de Realización de Pagos
        [HttpPost("tarjeta/{numeroTarjeta}/pago")]
        public IActionResult RealizarPago(int numeroTarjeta, [FromBody] Transaccion pago)
        {
            var tarjeta = tablaHash.Buscar(numeroTarjeta) as Tarjeta;

            if (tarjeta == null)
                return NotFound($"No se encontró la tarjeta con número {numeroTarjeta}");

            if (pago.Monto <= 0)
                return BadRequest("El monto debe ser mayor a cero");

            // Reducir saldo (asumiendo que es un pago)
            tarjeta.Saldo -= pago.Monto;
            if (tarjeta.Saldo < 0)
                tarjeta.Saldo = 0;

            // Registrar la transacción
            pago.Tipo = "Pago";
            pago.Fecha = DateTime.Now;
            pago.NumeroTarjeta = numeroTarjeta;

            // Asegúrate de tener la lista de transacciones si no la tienes aún
            ClientesController.Transacciones.InsertarAlInicio(pago);

            return Ok(new
            {
                mensaje = "Pago realizado con éxito",
                tarjeta.NumeroTarjeta,
                nuevoSaldo = tarjeta.Saldo
            });
        }
        // GET: api/clientes/transacciones
        [HttpGet("transacciones")]
        public IActionResult ObtenerTodasLasTransacciones()
        {
            var lista = Transacciones.ObtenerTodas();
            return Ok(lista);
        }
        // POST: api/clientes/tarjeta/{numeroTarjeta}/consumo
        [HttpPost("tarjeta/{numeroTarjeta}/consumo")]
        public IActionResult RealizarConsumo(int numeroTarjeta, [FromBody] Transaccion consumo)
        {
            var tarjeta = tablaHash.Buscar(numeroTarjeta) as Tarjeta;

            if (tarjeta == null)
                return NotFound($"No se encontró la tarjeta con número {numeroTarjeta}");

            if (consumo == null || consumo.Monto <= 0)
                return BadRequest("El monto debe ser mayor a cero");

            // Validar crédito disponible
            decimal creditoDisponible = tarjeta.LimiteCredito - tarjeta.Saldo;
            if (creditoDisponible < consumo.Monto)
                return BadRequest("Saldo insuficiente para realizar el consumo");

            // Actualizar saldo
            tarjeta.Saldo += consumo.Monto;

            // Registrar transacción
            consumo.Tipo = "Consumo";
            consumo.Fecha = DateTime.Now;
            consumo.NumeroTarjeta = numeroTarjeta;

            // Insertar en la lista de transacciones
            Transacciones.InsertarAlInicio(consumo);

            return Ok(new
            {
                mensaje = "Consumo realizado con éxito",
                tarjeta.NumeroTarjeta,
                nuevoSaldo = tarjeta.Saldo
            });
        }
        // 6. API de Consulta de Movimientos
        [HttpGet("tarjeta/{numeroTarjeta}/movimientos")]
        public IActionResult ConsultarMovimientos(int numeroTarjeta)
        {
            var listaMovimientos = Transacciones.ObtenerPorTarjeta(numeroTarjeta);

            if (listaMovimientos == null || listaMovimientos.Count == 0)
                return NotFound($"No se encontraron movimientos para la tarjeta {numeroTarjeta}");

            return Ok(listaMovimientos);
        }

        [HttpPost("tarjeta/{numeroTarjeta}/renovar")]
        public IActionResult RenovarTarjeta(int numeroTarjeta)
        {
            // Buscar tarjeta vieja
            var tarjetaVieja = tarjetas.BuscarPorNumero(numeroTarjeta);
            if (tarjetaVieja == null)
                return NotFound($"No se encontró la tarjeta con número {numeroTarjeta}");

            // Validar si está vencida (debes implementar este método en Tarjeta)
            if (!tarjetaVieja.EstaVencida())
                return BadRequest("La tarjeta aún no está vencida");

            // Marcar tarjeta vieja como renovada o inactiva
            tarjetaVieja.Estado = EstadoTarjeta.Renovada; // Si tienes este enum

            // Crear nueva tarjeta con nueva fecha y número
            var nuevaTarjeta = new Tarjeta(
                numeroTarjeta: GenerarNuevoNumeroTarjeta(),
                idCliente: tarjetaVieja.IdCliente,
                clase: tarjetaVieja.Clase,
                saldo: 0,
                limiteCredito: tarjetaVieja.LimiteCredito,
                fechaActivacion: DateTime.Now,
                fechaVencimiento: DateTime.Now.AddYears(3)
            )
            {
                Estado = EstadoTarjeta.Activa,
                Transacciones = new List<Transaccion>()
            };

            // Insertar la nueva tarjeta en la lista
            tarjetas.InsertarAlInicio(nuevaTarjeta);

            return Ok(new
            {
                mensaje = "Tarjeta renovada con éxito",
                tarjeta = new
                {
                    nuevaTarjeta.NumeroTarjeta,
                    nuevaTarjeta.IdCliente,
                    nuevaTarjeta.FechaActivacion,
                    nuevaTarjeta.FechaVencimiento
                }
            });
        }

        private static int ultimoNumeroTarjeta = 100000000; // Inicia desde algún número base

        private int GenerarNuevoNumeroTarjeta()
        {
            // Aquí podrías mejorar para evitar duplicados
            return ++ultimoNumeroTarjeta;
        }

        [HttpPost("cambio-pin")]
        public IActionResult SolicitarCambioPin([FromBody] Gestion solicitud)
        {
            if (solicitud == null || solicitud.TipoGestion != GestionTarjeta.CambioPin)
                return BadRequest("Solicitud inválida para cambio de PIN.");

            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EstadoTransaccion = "Pendiente";

            colaGestiones.Insertar(solicitud);

            return Ok(new { mensaje = "Solicitud de cambio de PIN recibida y en proceso.", gestion = solicitud });
        }

        // Ejemplo para procesar (extraer) la siguiente gestión pendiente
        [HttpPost("procesar-gestion")]
        public IActionResult ProcesarGestion()
        {
            object objGestion = colaGestiones.Extraer();

            if (objGestion == null)
                return Ok("No hay gestiones pendientes");

            Gestion gestion = objGestion as Gestion;
            if (gestion == null)
                return BadRequest("Error en el tipo de gestión.");

            // Aquí procesarías la gestión (ejemplo: cambiar el PIN, etc.)
            gestion.EstadoTransaccion = "Procesada";

            return Ok(new { mensaje = "Gestión procesada", gestion });
        }


        [HttpPost("bloqueo-temporal/{numeroTarjeta}")]
        public IActionResult BloqueoTemporal(int numeroTarjeta)
        {
            var tarjeta = ClientesController.tarjetas.BuscarPorNumero(numeroTarjeta);


            if (tarjeta == null)
                return NotFound("Tarjeta no encontrada.");

            if (tarjeta.Estado == EstadoTarjeta.Bloqueada)
                return BadRequest("La tarjeta ya está bloqueada.");

            tarjeta.Estado = EstadoTarjeta.Bloqueada;
            ClientesController.pilaBloqueosTemporales.Apilar(tarjeta);

            return Ok($"✅ Tarjeta {numeroTarjeta} bloqueada temporalmente.");
        }

        [HttpGet("bloqueo-temporal")]
        public IActionResult VerBloqueosTemporales()
        {
            var bloqueadas = ClientesController.pilaBloqueosTemporales.ObtenerTodos();
            return Ok(bloqueadas);
        }

        // Solicitud aumento de límite de crédito
        [HttpPost("aumento-limite")]
        public IActionResult SolicitarAumentoLimite([FromBody] Gestion solicitud)
        {
            if (solicitud == null || solicitud.TipoGestion != GestionTarjeta.AumentoLimite)
                return BadRequest("Solicitud inválida para aumento de límite de crédito.");

            if (!solicitud.NuevoLimiteCredito.HasValue || solicitud.NuevoLimiteCredito <= 0)
                return BadRequest("Debe especificar un nuevo límite de crédito válido.");

            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EstadoTransaccion = "Pendiente";

            colaGestiones.Insertar(solicitud);

            return Ok(new { mensaje = "Solicitud de aumento de límite recibida y en proceso.", gestion = solicitud });
        }

    
    }
}
