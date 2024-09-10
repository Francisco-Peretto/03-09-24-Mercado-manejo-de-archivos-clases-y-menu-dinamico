using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_09_24_Mercado
{
    internal class Mercado
    {
        private string _razonSocial;
        private string _direccionInstitucional;
        private List<Articulo> ListaArticulos;
        private List<Empleado> ListaEmpleados;

        double acSueldos;
        float acAlmacen, acLibreria, acElectronica;
        float promAlmacen, promLibreria, promElectronica;

        public Mercado()
        {
            this.RazonSocial = "";
            this.DireccionInstitucional = "";
            this.ListaArticulos = new List<Articulo>();
            this.ListaEmpleados = new List<Empleado>();
        }

    public Mercado(string _razonSocial, int _cantEmp, string _direccionInstitucional, int _cantArt)
        {
            this.RazonSocial = _razonSocial;
            this.DireccionInstitucional = _direccionInstitucional;
            this.ListaArticulos = new List<Articulo>();
            this.ListaEmpleados = new List<Empleado>();
        }

        public string RazonSocial 
        { 
            get { return this._razonSocial; }
            set { this._razonSocial = value; }
        }

        public string DireccionInstitucional
        {
            get { return this._direccionInstitucional; }
            set { this._direccionInstitucional = value; }
        }

        public int CantEmp
        {
            get { return ListaEmpleados.Count; }
        }
  
        public int CantArt
        {
            get { return ListaArticulos.Count; }
        }

        public void llenarListaDeEmpleados(string archivoEmpleado)
        {
                ListaEmpleados.Clear(); //Borrro la lista para arrancar
                FileStream Archivo;
                StreamReader leer;
                Archivo = new FileStream(archivoEmpleado, FileMode.Open);
                leer = new StreamReader(Archivo);

                while (leer.EndOfStream == false)
                {
                    string cadena = leer.ReadLine(); string[] datos = cadena.Split(';');
                    Empleado empleado = new Empleado(int.Parse(datos[0]), datos[1], (datos[2]), double.Parse(datos[3]));
                    ListaEmpleados.Add(empleado);
                }
                leer.Close();
                Archivo.Close();
        }

        public void llenarListaDeArticulos(string archivoArticulo)
        {
            ListaArticulos.Clear(); //Borrro la lista para arrancar
            FileStream Archivo;
            StreamReader leer;
            Archivo = new FileStream(archivoArticulo, FileMode.Open);
            leer = new StreamReader(Archivo);

            while (leer.EndOfStream == false)
            {
                string cadena = leer.ReadLine(); string[] datos = cadena.Split(';');
                Articulo articulo = new Articulo(int.Parse(datos[0]), datos[1], (datos[2]), int.Parse(datos[3]), int.Parse(datos[4]));
                ListaArticulos.Add(articulo);
            }
            leer.Close();
            Archivo.Close();
        }

        public void GenerarReporteEmpleados()
        {
            Console.WriteLine("\nListado de todos los empleados:");
            foreach (Empleado empleado in this.ListaEmpleados)
            {
                empleado.MostrarDatosEmpleado();
                acSueldos += empleado.Sueldo;
            }
            Console.WriteLine($"\nCantidad de empleados: {ListaEmpleados.Count}");
            Console.WriteLine($"Sueldos acumulados: ${acSueldos}");
        }

        public void GenerarReporteArticulos()
        {
            Console.WriteLine("Reporte de artículos:\n");
            foreach (Articulo articulo in this.ListaArticulos)
            {
                articulo.MostrarDatosArticulo();
                if (articulo.Categoria == "almacen")
                {
                    acAlmacen++;
                    promAlmacen += articulo.PrecioUnitario;
                }
                else if (articulo.Categoria == "libreria")
                {
                    acLibreria++;
                    promLibreria += articulo.PrecioUnitario;
                }
                else
                {
                    acElectronica++;
                    promElectronica += articulo.PrecioUnitario;
                }
            }
            Console.WriteLine("\nReporte de artículos:");
            Console.WriteLine($"Cantidad de artículos de almacen: {acAlmacen}. Promedio de precio unitario: ${promAlmacen/ acAlmacen}");
            Console.WriteLine($"Cantidad de artículos de libreria: {acLibreria}. Promedio de precio unitario: ${promLibreria / acLibreria}");
            Console.WriteLine($"Cantidad de artículos de electrónica: {acElectronica}. Promedio de precio unitario: ${promElectronica / acElectronica}");
        }

        public void AgregarEmpleado(Empleado nuevoEmpleado)
        {
            FileStream Archivo;
            StreamWriter Grabar;

            Archivo = new FileStream("empleados.txt", FileMode.Create);
            Grabar = new StreamWriter(Archivo);

            ListaEmpleados.Add(nuevoEmpleado);
            foreach (Empleado empleado in ListaEmpleados)
            {
                Grabar.WriteLine(empleado.Dni + ";" + empleado.Apellido + ";" + empleado.Nombre + ";" + empleado.Sueldo);
            }

            Grabar.Close();
            Archivo.Close();
        }


        //Modificar
    }
}
