namespace EstructurasDatos.Cola
{
    public class Cola
    {
        public Nodo Frente;  // Apunta al primer nodo de la cola
        public Nodo Fin;     // Apunta al último nodo de la cola

        public Cola()
        {
            Frente = null;
            Fin = null;
        }

        // Método para insertar un elemento en la cola
        public void Insertar(object dato)
        {
            Nodo nuevo = new Nodo(dato);  // Crear un nuevo nodo con el elemento

            if (Frente == null)  // Si la cola está vacía
                Frente = nuevo;
            else
                Fin.Enlace = nuevo;  // Agregar el nuevo nodo al final de la cola

            Fin = nuevo;  // El nuevo nodo es ahora el último
        }

        // Método para extraer un elemento de la cola
        public object Extraer()
        {
            if (Frente != null)
            {
                object elemento = Frente.Dato;  // Obtener el valor del nodo Frente
                Frente = Frente.Enlace;  // Avanzar el puntero Frente
                return elemento;
            }

            return null;  // Si la cola está vacía, retornamos null
        }

        // Método para obtener todos los elementos sin extraerlos
        public List<object> ObtenerTodos()
        {
            List<object> lista = new List<object>();
            Nodo actual = Frente;
            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Enlace;
            }
            return lista;
        }
    }
}




