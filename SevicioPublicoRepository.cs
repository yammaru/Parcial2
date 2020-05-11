using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Entiry;

namespace DAL
{
    public class SevicioPublicoRepository
    {

        List<ServicioPublico> servicioPublicos;

        public SevicioPublicoRepository()
        {
            servicioPublicos = new List<ServicioPublico>();
        }
        string ruta=@"PagosBanco";
        public void Guardar(ServicioPublico servicioPublico)
        {
            FileStream fileStream = new FileStream(ruta, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine(servicioPublico.ToString());
            streamWriter.Close();
            fileStream.Close();

        }
        public List<ServicioPublico> Mostrar() 
        {
            List<ServicioPublico> servicios = new List<ServicioPublico>();
            FileStream fileStream = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);
            String linea = string.Empty;
            while ((linea =streamReader.ReadLine())!=null)
            {
                ServicioPublico servicioPublico =Map(linea);
                servicios.Add(servicioPublico);
            }
            return servicios;
        }

        private ServicioPublico Map(string linea)
        {
            string[] dato = linea.Split(';');
            ServicioPublico servicioPublico = new ServicioPublico();
            servicioPublico.NombreServicioPublico = dato[1];
            servicioPublico.NumeroReciboPago = dato[2];
            servicioPublico.FechaPago = DateTime.Parse(dato[3]);
            servicioPublico.ValorPago = decimal.Parse(dato[4]);
            return servicioPublico;
        }
        public List<ServicioPublico> Consultar(string nombre, DateTime fecha)
        {
            List<ServicioPublico> servicioPublicos = Mostrar();
            List<ServicioPublico> servicioPublicos1 = servicioPublicos.Where(S => S.NombreServicioPublico.Equals(nombre) && S.FechaPago.Equals(fecha.ToString("dd/MM/yyyy"))).ToList();
            return servicioPublicos1;   
        }
       
        public decimal  totalizar(string nombre, DateTime fecha) 
        {
            List<ServicioPublico> servicioPublicos = Mostrar();
            return servicioPublicos.Where(S => S.NombreServicioPublico.Equals(nombre) && S.FechaPago.Equals(fecha.ToString("dd/MM/yyyy"))).Sum(S=>S.ValorPago);
        }
        public int Cuenta(string nombre, DateTime fecha)
        {
            List<ServicioPublico> servicioPublicos = Mostrar();
            return servicioPublicos.Where(S => S.NombreServicioPublico.Equals(nombre) && S.FechaPago.Equals(fecha.ToString("dd/MM/yyyy"))).Count();

        }
        public void UnNuevoArchivo()
        {
            //string Ruta_UnNuevoArchivo = @"{}.txt"

            //FileStream fileStream = new FileStream(Ruta_UnNuevoArchivo, FileMode.Append);
            //StreamWriter streamWriter = new StreamWriter(fileStream);
            //streamWriter.WriteLine(servicioPublico.ToString());
            //streamWriter.Close();
            //fileStream.Close();
        }
    }
}
