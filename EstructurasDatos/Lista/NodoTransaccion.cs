using EstructurasDatos.Datos;

namespace EstructurasDatos.Lista
{
    public class NodoTransaccion
    {
        public Transaccion dato;
        public NodoTransaccion enlace;

        public NodoTransaccion(Transaccion dato)
        {
            this.dato = dato;
            this.enlace = null;
        }
    }
}
