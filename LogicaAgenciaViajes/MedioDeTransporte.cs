using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAgenciaViajes
{
    public abstract class MedioDeTransporte
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int CapacidadPasajeros { get; set; }

        public virtual string DevolverDescripcion() {
            return $"El medio de transporte es de marca: {Marca}, modelo: {Modelo}, del año {Año}.";
        }

        public abstract double DevolverDemoraViajeEnMinutos(int cantidadKilometros, int cantidadProvinciasRecorridas, Enums.MomentoDelDia momentoDia);
    }
}
