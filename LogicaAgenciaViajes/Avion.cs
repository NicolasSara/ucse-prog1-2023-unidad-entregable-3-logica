using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAgenciaViajes
{
    public class Avion : MedioDeTransporte
    {
        public int NumeroMotores { get; set; }
        public int CantidadMaletas { get; set; }
        public double AltitudMaxima { get; set; }

        public override string DevolverDescripcion()
        {
            string descripcionAvion = $" Este avion tiene {NumeroMotores} motores y puede llevar hasta {CantidadMaletas} maletas, llegando hasta {AltitudMaxima:F2} metros sobre el nivel del mar";
            return $"{base.DevolverDescripcion()}{descripcionAvion}";
        }

        public override double DevolverDemoraViajeEnMinutos(int cantidadKilometros, int cantidadProvinciasRecorridas, Enums.MomentoDelDia momentoDia)
        {
            double factorMomentoDia = momentoDia == Enums.MomentoDelDia.Mañana ?
                1.25 : 1.35;

            return (cantidadKilometros / 180) * factorMomentoDia;
        }
    }
}
