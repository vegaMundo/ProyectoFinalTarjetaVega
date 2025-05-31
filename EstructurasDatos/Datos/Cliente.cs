using System;
using System.Collections.Generic;

namespace EstructurasDatos.Datos
{
    public class Cliente : Comparador
    {
        public int IdCliente { get; set; }
        public long Identificacion { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }


        // Lista de tarjetas asociadas
        public List<Tarjeta>? Tarjetas { get; set; } = new List<Tarjeta>();

        public Cliente() { }

        public Cliente(long identificacion, string nombre, DateTime fechaNacimiento,
                       int idCliente, string nit, string direccion, string email, int telefono)
        {
            Identificacion = identificacion;
            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
            IdCliente = idCliente;
            Nit = nit;
            Direccion = direccion;
            Email = email;
            Telefono = telefono;
            Tarjetas = new List<Tarjeta>();
        }

        // Comparaciones por ID
        public bool igualQue(object valorBuscado)
        {
            Cliente c = (Cliente)valorBuscado;
            return IdCliente == c.IdCliente;
        }

        public bool menorQue(object valorBuscado)
        {
            Cliente c = (Cliente)valorBuscado;
            return IdCliente < c.IdCliente;
        }

        public bool mayorQue(object valorBuscado)
        {
            Cliente c = (Cliente)valorBuscado;
            return IdCliente > c.IdCliente;
        }

        // Mostrar cliente + tarjetas asociadas
        public override string ToString()
        {
            string info = $"ID Cliente: {IdCliente}, Nombre: {Nombre}, NIT: {Nit}, Tel: {Telefono}, Email: {Email}\n";
            info += "Tarjetas:\n";

            if (Tarjetas != null && Tarjetas.Count > 0)
            {
                foreach (var tarjeta in Tarjetas)
                {
                    info += $"  - Número: {tarjeta.NumeroTarjeta}, Clase: {tarjeta.Clase}, Saldo: {tarjeta.Saldo}, Límite: {tarjeta.LimiteCredito}, Vence: {tarjeta.FechaVencimiento.ToShortDateString()}\n";
                }
            }
            else
            {
                info += "  No tiene tarjetas registradas.\n";
            }

            return info;
        }
    }
}
