using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiry;
using DAL;

namespace BLL
{
    public class ServicioPublicoService
    {
        SevicioPublicoRepository repository = new SevicioPublicoRepository();
        public string Guardar(ServicioPublico  servicioPublico)
        {
            try
            {

                if (repository.Buscar(servicioPublico.NumeroReciboPago)==null)
                {
                    repository.Guardar(servicioPublico);
                    return $"el recibo {servicioPublico.NumeroReciboPago} Guardo";
                }
                return $"el recibo {servicioPublico.NumeroReciboPago} Ya existe";
            }
            catch (Exception e)
            {

                return $"error de reguistro"+e.Message;
            }
        
        }
        public string Reportar(string numero, DateTime fecha)
        {
            repository.UnNuevoArchivo(numero, fecha);
            return "reportado!";
        }
        public List<ServicioPublico> Consultar(string numero, DateTime fecha)
        {
            return repository.Consultar(numero, fecha);
        }
        public decimal Totalizar(string numero, DateTime fecha) { return repository.Totalizar(numero, fecha); }
    }
}
