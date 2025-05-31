using EstructurasDatos.Lista;
using EstructurasDatos.TablaHash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Datos
{
    public class ServicioConsumo
    {
        private DispersionHash tablaHashTarjetas; // Tabla hash de tarjetas
        private ListaTransacciones listaTransacciones;
        private int ultimoIdTransaccion = 0;

        public ServicioConsumo(DispersionHash tablaHashTarjetas, ListaTransacciones listaTransacciones)
        {
            this.tablaHashTarjetas = tablaHashTarjetas;
            this.listaTransacciones = listaTransacciones;
        }

        public (bool exito, string mensaje, decimal nuevoSaldo) RealizarConsumo(int numeroTarjeta, decimal monto, string descripcion)
        {
            var tarjeta = tablaHashTarjetas.Buscar(numeroTarjeta) as Tarjeta;
            if (tarjeta == null)
                return (false, "Tarjeta no encontrada", 0);

            if (tarjeta.FechaVencimiento < DateTime.Now)
                return (false, "Tarjeta vencida", tarjeta.Saldo);

            decimal creditoDisponible = tarjeta.LimiteCredito - tarjeta.Saldo;
            if (creditoDisponible < monto)
                return (false, "Saldo insuficiente", tarjeta.Saldo);

            // Registrar consumo
            ultimoIdTransaccion++;
            var transaccion = new Transaccion(numeroTarjeta, DateTime.Now, ultimoIdTransaccion, monto, descripcion, "Consumo");

            listaTransacciones.InsertarAlInicio(transaccion);

            // Actualizar saldo de tarjeta
            tarjeta.Saldo += monto;

            return (true, "Consumo realizado con éxito", tarjeta.Saldo);
        }
    }

}
