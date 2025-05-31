using EstructurasDatos.Datos;
using System;

namespace EstructurasDatos.Lista
{
    public class NodoTarjeta
    {
        public Tarjeta dato;
        public NodoTarjeta enlace;

        public NodoTarjeta(Tarjeta dato)
        {
            this.dato = dato;
            this.enlace = null;
        }
    }
}
