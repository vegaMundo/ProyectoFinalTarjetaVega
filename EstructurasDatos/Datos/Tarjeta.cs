using System;
using System.Collections.Generic;

namespace EstructurasDatos.Datos
{
    public enum EstadoTarjeta
    {
        Activa,
        Bloqueada,
        Vencida,
        Renovada
    }

    public class Tarjeta
    {
        public int NumeroTarjeta { get; set; }
        public int IdCliente { get; set; }
        public string Clase { get; set; }
        public decimal Saldo { get; set; }
        public decimal LimiteCredito { get; set; }
        public DateTime FechaActivacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoTarjeta Estado { get; set; }  // Nuevo: estado de la tarjeta

        public List<Transaccion> Transacciones { get; set; }

        public Tarjeta(int numeroTarjeta, int idCliente, string clase, decimal saldo, decimal limiteCredito, DateTime fechaActivacion, DateTime fechaVencimiento)
        {
            NumeroTarjeta = numeroTarjeta;
            IdCliente = idCliente;
            Clase = clase;
            Saldo = saldo;
            LimiteCredito = limiteCredito;
            FechaActivacion = fechaActivacion;
            FechaVencimiento = fechaVencimiento;
            Estado = EstadoTarjeta.Activa; // Por defecto al crear es activa
            Transacciones = new List<Transaccion>();
        }

        public bool EstaVencida()
        {
            return FechaVencimiento < DateTime.Now;
        }

    }
}

