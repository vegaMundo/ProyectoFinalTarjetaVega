using EstructurasDatos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Arboles
{
    public class ArbolAVL
    {
        protected NodoAvl raiz;
        public ArbolAVL()
        {
            raiz = null;
        }
        public bool EstaVacio()
        {
            return raiz == null;
        }
        public NodoAvl raizArbol()
        {
            return raiz;
        }
        private NodoAvl rotacionII(NodoAvl n, NodoAvl n1)
        {
            n.ramaIzquierdo(n1.subarbolDerecho());
            n1.ramaDerecho(n);
            // actualización de los factores de equilibrio
            if (n1.fe == -1) // se cumple en la inserción
            {
                n.fe = 0;
                n1.fe = 0;
            }
            else
            {
                n.fe = -1;
                n1.fe = 1;
            }
            return n1;
        }

        private NodoAvl rotacionDD(NodoAvl n, NodoAvl n1)
        {
            n.ramaDerecho(n1.subarbolIzquierdo());
            n1.ramaIzquierdo(n);
            // actualización de los factores de equilibrio
            if (n1.fe == +1) // se cumple en la inserción
            {
                n.fe = 0;
                n1.fe = 0;
            }
            else
            {
                n.fe = +1;
                n1.fe = -1;
            }
            return n1;
        }
        private NodoAvl rotacionID(NodoAvl n, NodoAvl n1)
        {
            NodoAvl n2;
            n2 = (NodoAvl)n1.subarbolDerecho();
            n.ramaIzquierdo(n2.subarbolDerecho());
            n2.ramaDerecho(n);
            n1.ramaDerecho(n2.subarbolIzquierdo());
            n2.ramaIzquierdo(n1);
            // actualización de los factores de equilibrio
            if (n2.fe == +1)
                n1.fe = -1;
            else
                n1.fe = 0;
            if (n2.fe == -1)
                n.fe = 1;
            else
                n.fe = 0;
            n2.fe = 0;
            return n2;
        }
        private NodoAvl rotacionDI(NodoAvl n, NodoAvl n1)
        {
            NodoAvl n2;
            n2 = (NodoAvl)n1.subarbolIzquierdo();
            n.ramaDerecho(n2.subarbolIzquierdo());
            n2.ramaIzquierdo(n);
            n1.ramaIzquierdo(n2.subarbolDerecho());
            n2.ramaDerecho(n1);
            // actualización de los factores de equilibrio
            if (n2.fe == +1)
                n.fe = -1;
            else
                n.fe = 0;
            if (n2.fe == -1)
                n1.fe = 1;
            else
                n1.fe = 0;
            n2.fe = 0;
            return n2;
        }
        public void Insertar(Object valor)//throws Exception
        {
            Comparador dato;
            Logical h = new Logical(false); // intercambia un valor booleano
            dato = (Comparador)valor;
            raiz = insertarAvl(raiz, dato, h);
        }
        private NodoAvl insertarAvl(NodoAvl raiz, Comparador dt, Logical h)
        //throws Exception
        {
            NodoAvl n1;
            if (raiz == null)
            {
                raiz = new NodoAvl(dt);
                h.setLogical(true);
            }
            else if (dt.menorQue(raiz.valorNodo()))
            {
                NodoAvl iz;
                iz = insertarAvl((NodoAvl)raiz.subarbolIzquierdo(), dt, h);
                raiz.ramaIzquierdo(iz);
                // regreso por los nodos del camino de búsqueda
                if (h.booleanValue())
                {
                    // decrementa el fe por aumentar la altura de rama izquierda
                    switch (raiz.fe)
                    {
                        case 1:
                            raiz.fe = 0;
                            h.setLogical(false);
                            break;
                        case 0:
                            raiz.fe = -1;
                            break;
                        case -1: // aplicar rotación a la izquierda
                            n1 = (NodoAvl)raiz.subarbolIzquierdo();
                            if (n1.fe == -1)
                                raiz = rotacionII(raiz, n1);
                            else
                                raiz = rotacionID(raiz, n1);
                            h.setLogical(false);
                            break;
                    }
                }
            }
            else if (dt.mayorQue(raiz.valorNodo()))
            {
                NodoAvl dr;
                dr = insertarAvl((NodoAvl)raiz.subarbolDerecho(), dt, h);
                raiz.ramaDerecho(dr);
                // regreso por los nodos del camino de búsqueda
                if (h.booleanValue())
                {
                    // incrementa el fe por aumentar la altura de rama izquierda
                    switch (raiz.fe)
                    {
                        case 1: // aplicar rotación a la derecha
                            n1 = (NodoAvl)raiz.subarbolDerecho();
                            if (n1.fe == +1)
                                raiz = rotacionDD(raiz, n1);
                            else
                                raiz = rotacionDI(raiz, n1);
                            h.setLogical(false);
                            break;
                        case 0:
                            raiz.fe = +1;
                            break;
                        case -1:
                            raiz.fe = 0;
                            h.setLogical(false);
                            break;
                    }
                }
            }
            else
                throw new Exception("No puede haber claves repetidas ");
            return raiz;
        }
        private NodoAvl equilibrar1(NodoAvl n, Logical cambiaAltura)
        {
            NodoAvl n1;
            switch (n.fe)
            {
                case -1:
                    n.fe = 0;
                    break;
                case 0:
                    n.fe = 1;
                    cambiaAltura.setLogical(false);
                    break;
                case +1: //se aplicar un tipo de rotación derecha
                    n1 = (NodoAvl)n.subarbolDerecho();
                    if (n1.fe >= 0)
                    {
                        if (n1.fe == 0) //la altura no vuelve a disminuir
                            cambiaAltura.setLogical(false);
                        n = rotacionDD(n, n1);
                    }
                    else
                        n = rotacionDI(n, n1);
                    break;
            }
            return n;
        }
        private NodoAvl equilibrar2(NodoAvl n, Logical cambiaAltura)
        {
            NodoAvl n1;
            switch (n.fe)
            {
                case -1: // Se aplica un tipo de rotación izquierda
                    n1 = (NodoAvl)n.subarbolIzquierdo();
                    if (n1.fe <= 0)
                    {
                        if (n1.fe == 0)
                            cambiaAltura.setLogical(false);
                        n = rotacionII(n, n1);
                    }
                    else
                        n = rotacionID(n, n1);
                    break;
                case 0:
                    n.fe = -1;
                    cambiaAltura.setLogical(false);
                    break;
                case +1:
                    n.fe = 0;
                    break;
            }
            return n;
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
    }
}
