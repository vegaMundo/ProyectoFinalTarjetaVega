using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Arboles
{
    public class ArbolBinarioBusqueda : ArbolBinario
    {
        public ArbolBinarioBusqueda() : base() { }

        public ArbolBinarioBusqueda(Nodo nodo) : base(nodo)
        {

        }
        // Método público para insertar un valor
        public void insertar(Object valor)
        {
            Comparador dato = (Comparador)valor;
            raiz = insertar(raiz, dato);
        }
        // Método recursivo para insertar un valor en el arbol binario
        private Nodo insertar(Nodo raizSub, Comparador dato)
        {
            if (raizSub == null)
            {
                raizSub = new Nodo(dato);   //creamos un nuevo nodo con el proyecto
            }
            else if (dato.menorQue(raizSub.valorNodo()))
            {
                Nodo iz = insertar(raizSub.subarbolIzquierdo(), dato);
                raizSub.ramaIzquierdo(iz);
            }
            else if (dato.mayorQue(raizSub.valorNodo()))
            {
                Nodo dr = insertar(raizSub.subarbolDerecho(), dato);
                raizSub.ramaDerecho(dr);
            }
            else
            {
                throw new Exception("Nodo duplicado");
            }
            return raizSub;
        }

        // Método público para imprimir en orden (sin parámetros)
        public string inorden()
        {
            return inorden(raiz); // Llama al método privado usando la raíz del árbol
        }
        // Método recursivo privado (con parámetro)
        private string inorden(Nodo r)
        {
            if (r != null)
            {
                return inorden(r.subarbolIzquierdo()) + r.visitar() + inorden(r.subarbolDerecho());
            }
            return "";
        }

        // Método público para eliminar un valor
        public void eliminar(Object valor)
        {
            Comparador dato = (Comparador)valor;
            raiz = eliminar(raiz, dato);
        }

        // Método recursivo para eliminar un valor
        private Nodo eliminar(Nodo raizSub, Comparador dato)
        {
            if (raizSub == null)
            {
                throw new Exception("No encontrado el nodo con la clave");
            }
            else if (dato.menorQue(raizSub.valorNodo()))
            {
                Nodo iz = eliminar(raizSub.subarbolIzquierdo(), dato);
                raizSub.ramaIzquierdo(iz);
            }
            else if (dato.mayorQue(raizSub.valorNodo()))
            {
                Nodo dr = eliminar(raizSub.subarbolDerecho(), dato);
                raizSub.ramaDerecho(dr);
            }
            else // Nodo encontrado
            {
                Nodo q = raizSub; // nodo a quitar del árbol

                if (q.subarbolIzquierdo() == null)
                    raizSub = q.subarbolDerecho();
                else if (q.subarbolDerecho() == null)
                    raizSub = q.subarbolIzquierdo();
                else
                    q = reemplazar(q);
            }

            return raizSub;
        }
        // Método para reemplazar un nodo con su sucesor en orden no lo usamos en este laboratorio
        private Nodo reemplazar(Nodo nodoReemplazo)
        {
            Nodo sucesorPadre = nodoReemplazo;
            Nodo sucesor = nodoReemplazo.subarbolDerecho();
            // Busca el sucesor más a la izquierda en el subárbol derecho
            while (sucesor.subarbolIzquierdo() != null)
            {
                sucesorPadre = sucesor;
                sucesor = sucesor.subarbolIzquierdo();
            }
            // Reemplaza el sucesor con su subárbol derecho
            if (sucesorPadre.subarbolIzquierdo() == sucesor)
                sucesorPadre.ramaIzquierdo(sucesor.subarbolDerecho());
            else
                sucesorPadre.ramaDerecho(sucesor.subarbolDerecho());
            return sucesor;
        }
        // Método público para buscar un valor
        public Nodo buscar(Object buscado)
        {
            Comparador dato = (Comparador)buscado;
            if (raiz == null)
                return null;
            else
                return buscar(raiz, dato);
        }
        // Método recursivo para buscar un valor
        private Nodo buscar(Nodo raizSub, Comparador buscado)
        {
            if (raizSub == null)
                return null;

            if (buscado.igualQue(raizSub.valorNodo()))
                return raizSub;

            else if (buscado.menorQue(raizSub.valorNodo()))
                return buscar(raizSub.subarbolIzquierdo(), buscado);

            else
                return buscar(raizSub.subarbolDerecho(), buscado);
        }
    }
}
