using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Pilas
{
    public class Nodo
    {
        public object Dato;
        public Nodo Siguiente;

        public Nodo(object dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
