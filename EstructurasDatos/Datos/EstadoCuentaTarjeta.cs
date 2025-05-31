namespace EstructurasDatos.Datos
{
    public class EstadoCuentaTarjeta
    {
        public int NumeroTarjeta { get; set; }
        public decimal TotalPagadoMes { get; set; }
        public decimal InteresesGenerados { get; set; }

        public decimal SaldoActual { get; set; }
        public decimal CreditoDisponible { get; set; }  // Límite - SaldoActual
        public decimal PagoMinimo { get; set; }
        public decimal PagoContado { get; set; }     // Saldo al corte
        public decimal PagoAtrasado { get; set; }
        public int CuotasVencidas { get; set; }
    }
}
