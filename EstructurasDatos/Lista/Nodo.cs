using EstructurasDatos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Lista
{
    public class Nodo
    {
        public Cliente dato;
        public Nodo enlace;

        public Nodo(Cliente dato)
        {
            this.dato = dato;
            this.enlace = null;
        }
    }
}
