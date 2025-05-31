using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Pilas
{
    public class Pila
    {
        private Nodo cima;

        public Pila()
        {
            cima = null;
        }

        public void Apilar(object dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.Siguiente = cima;
            cima = nuevo;
        }

        public object Desapilar()
        {
            if (cima == null)
                return null;

            object dato = cima.Dato;
            cima = cima.Siguiente;
            return dato;
        }

        public List<object> ObtenerTodos()
        {
            List<object> elementos = new List<object>();
            Nodo actual = cima;
            while (actual != null)
            {
                elementos.Add(actual.Dato);
                actual = actual.Siguiente;
            }
            return elementos;
        }
    }
}
