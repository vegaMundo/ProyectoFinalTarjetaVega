using EstructurasDatos.Datos;
using System.Collections.Generic;

namespace EstructurasDatos.Lista
{
    public class ListaTransacciones
    {
        private NodoTransaccion primero;
        private NodoTransaccion ultimo;
        private int tamaño;

        public ListaTransacciones()
        {
            primero = null;
            ultimo = null;
            tamaño = 0;
        }

        public void InsertarAlInicio(Transaccion nueva)
        {
            NodoTransaccion nuevo = new NodoTransaccion(nueva);

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

        public List<Transaccion> ObtenerPorTarjeta(int numeroTarjeta)
        {
            List<Transaccion> resultado = new List<Transaccion>();
            NodoTransaccion actual = primero;

            while (actual != null)
            {
                if (actual.dato.NumeroTarjeta == numeroTarjeta)
                    resultado.Add(actual.dato);

                actual = actual.enlace;
            }

            return resultado;
        }

        public List<Transaccion> ObtenerTodas()
        {
            List<Transaccion> resultado = new List<Transaccion>();
            NodoTransaccion actual = primero;

            while (actual != null)
            {
                resultado.Add(actual.dato);
                actual = actual.enlace;
            }

            return resultado;
        }
    }
}
