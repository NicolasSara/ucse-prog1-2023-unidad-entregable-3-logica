using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAgenciaViajes
{
    public class Traffic : MedioDeTransporte
    {
        public int NumeroPuertas { get; set; }

        public override double DevolverDemoraViajeEnMinutos(int cantidadKilometros, int cantidadProvinciasRecorridas, Enums.MomentoDelDia momentoDia)
        {
            double factorMomentoDia = momentoDia == Enums.MomentoDelDia.Mañana ?
                1.25 : momentoDia == Enums.MomentoDelDia.Tarde ? 1.35 : 1.15;

            return (cantidadKilometros / 90 + cantidadProvinciasRecorridas * 15) * factorMomentoDia;
        }
    }
}
