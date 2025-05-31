namespace EstructurasDatos.Datos
{
    public enum GestionTarjeta { CambioPin, AumentoLimite, Renovacion } 
    public  class Gestion
    {
        public int IdGestion { get; set; }            // Identificador único para la gestión
        public GestionTarjeta TipoGestion { get; set; }       // Ej. "Cambio de PIN", "Aumento de límite", "Renovación"
        public DateTime FechaSolicitud { get; set; }  // Fecha de la solicitud
        public string EstadoTransaccion { get; set; }            // Estado de la gestión (Ej. Pendiente, Aprobada, Rechazada)
        public int IdCliente { get; set; }            // ID del cliente al que se le aplica la gestión
        public int? NuevoLimiteCredito { get; set; }  // Solo se aplica si es un aumento de límite
        public string NuevoPin { get; set; }          // Solo se aplica si es un cambio de PIN
        public DateTime? FechaRenovacion { get; set; } // Solo se aplica si es una renovación

        public DateTime FechaCancelacionTarjeta {  get; set; }
    }

}


