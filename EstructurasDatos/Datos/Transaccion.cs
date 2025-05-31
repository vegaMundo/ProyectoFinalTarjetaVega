namespace EstructurasDatos.Datos
{
    public class Transaccion
    {
        public int NumeroTarjeta {  get; set; }
        public DateTime Fecha { get; set; }
        public int IdTransaccion { get; set; }
        public decimal Monto { get; set; }          // Negativo = consumo, Positivo = pago
        public string Descripcion { get; set; }     // Ej: "Compra en Amazon"
        public string Tipo { get; set; }            // "Pago", "Consumo", "Interés"

        public Transaccion(int numeroTarjeta, DateTime fecha, int idTransaccion, decimal monto, string descripcion, string tipo)
        {
            NumeroTarjeta = numeroTarjeta;
            Fecha = fecha;
            IdTransaccion = idTransaccion;
            Monto = monto;
            Descripcion = descripcion;
            Tipo = tipo;
        }
    }
}
