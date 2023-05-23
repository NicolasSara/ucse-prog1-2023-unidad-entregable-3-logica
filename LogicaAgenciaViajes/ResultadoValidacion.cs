using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAgenciaViajes
{
    public class ResultadoValidacion
    {
        public bool Success { get; set; }

        public List<ResultadoValidacionItem> Detail { get; set; }

        public ResultadoValidacion()
        {
            Success = true;
            Detail = new List<ResultadoValidacionItem>();
        }
    }
}
