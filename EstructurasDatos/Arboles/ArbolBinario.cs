using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Arboles
{
    public class ArbolBinario
    {
        protected Nodo raiz;

        public ArbolBinario()
        {
            raiz = null;
        }

        public ArbolBinario(Nodo raiz)
        {
            this.raiz = raiz;
        }

        public Nodo raizArbol()
        {
            return raiz;
        }
        public bool estaVacio()
        {
            return raiz == null;
        }

        public static Nodo nuevoArbol(Nodo ramaIzquierdo, object dato, Nodo ramaDerecho)
        {
            return new Nodo(ramaIzquierdo, dato, ramaDerecho);
        }

        //  Búsqueda de un valor
        public bool Buscar(Nodo r, object valor)
        {
            if (r == null) return false;
            if (r.valorNodo().Equals(valor)) return true;

            return Buscar(r.subarbolIzquierdo(), valor) || Buscar(r.subarbolDerecho(), valor);
        }
    }

}
