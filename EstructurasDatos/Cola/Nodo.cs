using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Cola
{
    public class Nodo
    {

        public object Dato;
        public Nodo Enlace;              //EN EL PDF ESTA DECLARADO COMO siguiente 

        public Nodo(object dato)
        {
            Dato = dato;
            Enlace = null;                   //IGUAL AQUI ES siguiente 
        }

    }
}
