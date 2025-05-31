using EstructurasDatos.Arboles;
using EstructurasDatos.Cola;
using EstructurasDatos.Datos;
using EstructurasDatos.Lista;
using EstructurasDatos.Pilas;
using EstructurasDatos.TablaHash;
using System.Text.Json;

namespace ProyectoTarjetasCredito.Utils
{
    public static class CargaDatosClientes
    {
        public static void CargarDesdeJson(
            ArbolBinarioBusqueda arbolcliente,
            ListaClientes lista,
            ListaTarjetas listaTarjetas,
            DispersionHash tablaHash,
            ListaTransacciones listaTransacciones,
            Cola colaGestiones,
            Pila pilaBloqueosTemporales,
            ArbolAVL arbolClienteAVL)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "registros_tarjetas.json");

            Console.WriteLine($"Buscando archivo en: {ruta}");

            if (!File.Exists(ruta))
            {
                Console.WriteLine("❌ Archivo no encontrado");
                return;
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                };
                string json = File.ReadAllText(ruta);
                var clientes = JsonSerializer.Deserialize<List<Cliente>>(json, options);

                if (clientes == null || clientes.Count == 0)
                {
                    Console.WriteLine("⚠️ El archivo está vacío o no contiene clientes");
                    return;
                }

                foreach (var cliente in clientes)
                {
                    // Insertar en lista y árbol cliente original
                    lista.InsertarAlInicio(cliente);
                    arbolcliente.insertar(cliente);
                   

                    // Insertar tarjetas en listas y tabla hash
                    if (cliente.Tarjetas != null)
                    {
                        foreach (var tarjeta in cliente.Tarjetas)
                        {
                            tarjeta.IdCliente = cliente.IdCliente;
                            listaTarjetas.InsertarAlInicio(tarjeta);
                            tablaHash.Insertar(tarjeta, tarjeta.NumeroTarjeta);

                            // Si la tarjeta está bloqueada, apilarla
                            if (tarjeta.Estado == EstadoTarjeta.Bloqueada)
                            {
                                pilaBloqueosTemporales.Apilar(tarjeta);
                            }

                            // Cargar transacciones de la tarjeta
                            if (tarjeta.Transacciones != null)
                            {
                                foreach (var transaccion in tarjeta.Transacciones)
                                {
                                    transaccion.NumeroTarjeta = tarjeta.NumeroTarjeta;
                                    listaTransacciones.InsertarAlInicio(transaccion);
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"✅ Se cargaron {clientes.Count} clientes con sus tarjetas correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al cargar datos: {ex.Message}");
            }
        }
    }
}
