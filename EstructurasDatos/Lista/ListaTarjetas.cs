using EstructurasDatos.Datos;
using System.Collections.Generic;

namespace EstructurasDatos.Lista
{
    public class ListaTarjetas
    {
        private NodoTarjeta primero;
        private NodoTarjeta ultimo;
        private int tamaño;

        public ListaTarjetas()
        {
            primero = null;
            ultimo = null;
            tamaño = 0;
        }

        public void InsertarAlInicio(Tarjeta nuevaTarjeta)
        {
            NodoTarjeta nuevo = new NodoTarjeta(nuevaTarjeta);

            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                nuevo.enlace = primero;
                primero = nuevo;
            }

            tamaño++;
        }

        public List<Tarjeta> ObtenerTarjetasPorCliente(int idCliente)
        {
            List<Tarjeta> resultado = new List<Tarjeta>();
            NodoTarjeta actual = primero;

            while (actual != null)
            {
                if (actual.dato.IdCliente == idCliente)
                {
                    resultado.Add(actual.dato);
                }
                actual = actual.enlace;
            }

            return resultado;
        }



        public Tarjeta BuscarPorNumero(int numeroTarjeta)
        {
            NodoTarjeta actual = primero;
            while (actual != null)
            {
                if (actual.dato.NumeroTarjeta == numeroTarjeta)
                    return actual.dato;

                actual = actual.enlace;
            }
            return null;
        }

        public void EliminarPorNumero(int numeroTarjeta)
        {
            if (primero == null) return;

            if (primero.dato.NumeroTarjeta == numeroTarjeta)
            {
                primero = primero.enlace;
                tamaño--;
                return;
            }

            NodoTarjeta actual = primero;
            while (actual.enlace != null)
            {
                if (actual.enlace.dato.NumeroTarjeta == numeroTarjeta)
                {
                    actual.enlace = actual.enlace.enlace;
                    tamaño--;
                    return;
                }
                actual = actual.enlace;
            }
        }

        public bool ActualizarTarjeta(int numero, Tarjeta actualizada)
        {
            NodoTarjeta actual = primero;
            while (actual != null)
            {
                if (actual.dato.NumeroTarjeta == numero)
                {
                    actual.dato.FechaVencimiento = actualizada.FechaVencimiento;
                    actual.dato.LimiteCredito = actualizada.LimiteCredito;
                    actual.dato.Saldo = actualizada.Saldo;
                    actual.dato.Clase = actualizada.Clase;
                    actual.dato.FechaActivacion = actualizada.FechaActivacion;
                    // Puedes agregar más campos si los defines
                    return true;
                }
                actual = actual.enlace;
            }
            return false;
        }
    }
}
