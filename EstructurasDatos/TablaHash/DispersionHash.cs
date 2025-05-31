namespace EstructurasDatos.TablaHash
{
    public class DispersionHash
    {
        public static readonly int Tam_tabla = 997;
        public static readonly double R = 0.618034;
        public object[] tabla = new object[Tam_tabla];

        public long transformaClave(string clave)
        {
            long d = 0;
            for (int j = 0; j < Math.Min(clave.Length, 10); j++)
            {
                d = d * 27 + (int)clave[j];
            }
            return d < 0 ? -d : d;
        }

        public int dispersion(long x)
        {
            double t = R * x - Math.Floor(R * x);
            return (int)(Tam_tabla * t);
        }

        public int PosMod(int x)
        {
            return x % Tam_tabla;
        }

        public int returnPosicion(string Clave)
        {
            return dispersion(transformaClave(Clave));
        }

        public int returnPosicion(int Clave)
        {
            return PosMod(Clave);
        }

        public void Insertar(object dato, int clave)
        {
            int pos = returnPosicion(clave);
            int original = pos;

            while (tabla[pos] != null)
            {
                pos = (pos + 1) % Tam_tabla;
                if (pos == original)
                    throw new Exception("Tabla hash llena");
            }

            tabla[pos] = dato;
        }

        public object Buscar(int clave)
        {
            int pos = returnPosicion(clave);
            int original = pos;

            while (tabla[pos] != null)
            {
                if (tabla[pos] is Datos.Tarjeta tarjeta && tarjeta.NumeroTarjeta == clave)
                    return tarjeta;

                pos = (pos + 1) % Tam_tabla;
                if (pos == original)
                    break;
            }

            return null;
        }

        public void Eliminar(int clave)
        {
            int pos = returnPosicion(clave);
            int original = pos;

            while (tabla[pos] != null)
            {
                if (tabla[pos] is Datos.Tarjeta tarjeta && tarjeta.NumeroTarjeta == clave)
                {
                    tabla[pos] = null;
                    return;
                }

                pos = (pos + 1) % Tam_tabla;
                if (pos == original)
                    break;
            }
        }
    }
}
