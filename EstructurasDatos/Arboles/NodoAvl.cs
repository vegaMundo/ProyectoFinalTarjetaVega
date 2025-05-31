using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Arboles
{
    public class NodoAvl : Nodo
    {
        public int fe;
        public NodoAvl(object valor) : base(valor)
        {
            fe = 0;
        }
        public NodoAvl(object valor, NodoAvl ramaIzquierdo, NodoAvl ramaDerecho) : base(ramaIzquierdo, valor, ramaDerecho)
        {
            fe = 0;
        }
    }
}
