using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Arboles
{
    public class Nodo
    {
        public object dato;
        protected Nodo izquierdo;
        protected Nodo derecho;

        //Constructor parametrizado 
        public Nodo(object valor)
        {
            dato = valor;
            izquierdo = derecho = null;
        }
        public Nodo(Nodo ramaIzquierdo, object valor, Nodo ramaDerecho)
        {
            this.dato = valor;
            izquierdo = ramaIzquierdo;
            derecho = ramaDerecho;
        }
        //operaciones de acceso
        public object valorNodo()
        {
            return dato;
        }

        public void setValor(object nuevoValor)
        {
            dato = nuevoValor;
        }
        public Nodo subarbolIzquierdo()
        {
            return izquierdo;
        }
        public Nodo subarbolDerecho()
        {
            return derecho;
        }

        public void nuevoValor(Object d)
        {
            dato = d;
        }

        public void ramaIzquierdo(Nodo n)
        {
            izquierdo = n;
        }
        public void ramaDerecho(Nodo n)
        {
            derecho = n;
        }

        public string visitar()
        {
            return dato.ToString();
        }
    }
}