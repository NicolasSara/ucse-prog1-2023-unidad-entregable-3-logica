using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAgenciaViajes
{
    public class Principal
    {
        private List<MedioDeTransporte> MediosDeTransporte;

        public Principal()
        {
            MediosDeTransporte = new List<MedioDeTransporte>();
        }

        public ResultadoValidacion CrearMedioDeTransporte(MedioDeTransporte medio)
        {
            ResultadoValidacion result = new ResultadoValidacion();

            validateCompletitudDatos(ref result, medio);

            if (medio is Avion)
            {
                validarCompletitudDatosAvion(ref result, medio);
            }

            if (medio is Traffic)
            {
                validarCompletitudDatosTraffic(ref result, medio);
            }

            if (medio is Colectivo)
            {
                validarCompletitudDatosColectivo(ref result, medio);
            }

            if (result.Success)
            {
                medio.Id = MediosDeTransporte.Count + 1;
                MediosDeTransporte.Add(medio);
            }

            return result;
        }

        public MedioDeTransporte ObtenerMedioDeTransporte(int id)
        {
            return MediosDeTransporte.FirstOrDefault(x => x.Id == id && !x.FechaEliminacion.HasValue);
        }

        public ResultadoValidacion ActualizarMedioDeTransporte(int id, MedioDeTransporte medio)
        {
            ResultadoValidacion result = new ResultadoValidacion();

            MedioDeTransporte medioTransporteGuardado = ObtenerMedioDeTransporte(id);
            if (medioTransporteGuardado == null)
            {
                result.Success = false;
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "NotFound",
                    Message = "No se encontro el medio de transporte que se quiere editar"
                });

                return result;
            }

            validateCompletitudDatos(ref result, medio);

            if (result.Success)
            {
                medioTransporteGuardado.Marca = medio.Marca;
                medioTransporteGuardado.Modelo = medio.Modelo;
                medioTransporteGuardado.CapacidadPasajeros = medio.CapacidadPasajeros;
                medioTransporteGuardado.Año = medio.Año;
            }

            return result;
        }

        public string EliminarMedioDeTransporte(int id)
        {
            MedioDeTransporte medio = ObtenerMedioDeTransporte(id);

            if (medio == null)
            {
                return "No se encontró medio de transporte para eliminar";
            }

            medio.FechaEliminacion = DateTime.Now;

            return "Medio de transporte eliminado con éxico.";
        }

        public string ObtenerDescripcionMedioDeTransporte(int id)
        {
            MedioDeTransporte medio = ObtenerMedioDeTransporte(id);

            if (medio == null)
            {
                return $"No existe un medio para el id: {id}";
            }

            return medio.DevolverDescripcion();
        }

        public List<MedioDeTransporte> FiltrarMediosDeTransportePorAño(int añoMinimo)
        {
            List<MedioDeTransporte> mediosFiltrados = new List<MedioDeTransporte>();
            foreach (var medio in MediosDeTransporte)
            {
                if (medio.Año >= añoMinimo)
                {
                    mediosFiltrados.Add(medio);
                }
            }

            return mediosFiltrados;
        }

        public double CalcularDemoraDeViaje(int id, int cantidadKilometros, int cantidadProvinciasRecorridas, Enums.MomentoDelDia momentoDia) {
            MedioDeTransporte medio = ObtenerMedioDeTransporte(id);

            if (medio == null) {
                return 0.0;
            }

            return medio.DevolverDemoraViajeEnMinutos(cantidadKilometros, cantidadProvinciasRecorridas, momentoDia);
        }

        private void validarCompletitudDatosColectivo(ref ResultadoValidacion result, MedioDeTransporte medio)
        {
            Colectivo avion = medio as Colectivo;
            if (avion.CantidadBaños == 0)
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "CantidadBaños",
                    Message = "Colocar la cantidad de baños, al menos debe haber."
                });
            }

            if (result.Detail.Count > 0)
                result.Success = false;
        }

        private void validarCompletitudDatosTraffic(ref ResultadoValidacion result, MedioDeTransporte medio)
        {
            Traffic avion = medio as Traffic;
            if (avion.NumeroPuertas == 0)
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "NumeroPuertas",
                    Message = "Colocar la número de puertas"
                });
            }

            if (result.Detail.Count > 0)
                result.Success = false;
        }

        private void validarCompletitudDatosAvion(ref ResultadoValidacion result, MedioDeTransporte medio)
        {
            Avion avion = medio as Avion;
            if (avion.AltitudMaxima == 0)
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "AltitudMaxima",
                    Message = "Colocar la altitud máxima"
                });
            }

            if (avion.CantidadMaletas == 0)
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "CantidadMaletas",
                    Message = "Colocar la cantidad de maletas"
                });
            }

            if (result.Detail.Count > 0)
                result.Success = false;
        }

        private void validateCompletitudDatos(ref ResultadoValidacion result, MedioDeTransporte medio)
        {
            if (string.IsNullOrEmpty(medio.Marca))
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "Marca",
                    Message = "Marca incorrecta"
                });
            }

            if (string.IsNullOrEmpty(medio.Modelo))
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "Modelo",
                    Message = "Modelo incorrecto"
                });
            }

            if (medio.CapacidadPasajeros == 0)
            {
                result.Detail.Add(new ResultadoValidacionItem()
                {
                    Code = "CapacidadPasajeros",
                    Message = "Se debe colocar la cantidad de pasajeros posibles"
                });
            }

            if (result.Detail.Count > 0)
                result.Success = false;
        }
    }
}
