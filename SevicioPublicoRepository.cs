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
        public ServicioPublico Buscar(string numero)
        {
            return servicioPublicos.Find(S => S.NumeroReciboPago.Equals(numero));
        }
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
       
        public decimal  Totalizar(string nombre, DateTime fecha) 
        {
            List<ServicioPublico> servicioPublicos = Mostrar();
            return servicioPublicos.Where(S => S.NombreServicioPublico.Equals(nombre) && S.FechaPago.Equals(fecha.ToString("dd/MM/yyyy"))).Sum(S=>S.ValorPago);
        }
        public int Contar(string nombre, DateTime fecha)
        {
            List<ServicioPublico> servicioPublicos = Mostrar();
            return servicioPublicos.Where(S => S.NombreServicioPublico.Equals(nombre) && S.FechaPago.Equals(fecha.ToString("dd/MM/yyyy"))).Count();

        }
        public void UnNuevoArchivo(string nombre, DateTime fecha)
        {

            List<ServicioPublico> servicioPublicos1 = Consultar(nombre,fecha);
            int contar = Contar(nombre, fecha);
            decimal totalizar= Totalizar(nombre, fecha);

            string Ruta_UnNuevoArchivo = $@"{nombre}{fecha}.txt";

            FileStream fileStream = new FileStream(Ruta_UnNuevoArchivo, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream);
           
            streamWriter.WriteLine(nombre+";"+fecha.ToString("dd/MM/yyyy")) ;
            streamWriter.WriteLine(totalizar.ToString()+";"+contar.ToString());
            foreach (var item in servicioPublicos1)
            {
                streamWriter.WriteLine(item.NumeroReciboPago+";"+item.FechaPago.ToString("dd/MM/yyyy")+";"+item.ValorPago.ToString());
            }
            
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
