using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos.Datos
{
    public class ConsumoRequest
    {
        public int NumeroTarjeta { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }
}
