using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAgenciaViajes
{
    public class Colectivo : MedioDeTransporte
    {
        public bool EsCocheCama { get; set; }
        public int CantidadBaños { get; set; }

        public override string DevolverDescripcion()
        {
            string detalleCollectivo = EsCocheCama ? 
                $" Este colectivo es coche cama y tiene {CantidadBaños} baños." : $" Tiene {CantidadBaños} baños.";
            return $"{base.DevolverDescripcion()}{detalleCollectivo}";
        }

        public override double DevolverDemoraViajeEnMinutos(int cantidadKilometros, int cantidadProvinciasRecorridas, Enums.MomentoDelDia momentoDia)
        {
            double factorMomentoDia = momentoDia == Enums.MomentoDelDia.Mañana ?
                1.35 : momentoDia == Enums.MomentoDelDia.Tarde ? 1.45 : 1.55;

            return (cantidadKilometros / 90 + cantidadProvinciasRecorridas * 30) * factorMomentoDia;
        }
    }
}
