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

        public Mercado(string _razonSocial, string _direccionInstitucional)
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

        public void llenarListaDeEmpleados()
        {
            ListaEmpleados.Clear(); //Borrro la lista para arrancar
            FileStream Archivo;
            StreamReader leer;
            Archivo = new FileStream("empleados.txt", FileMode.Open);
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

        public void llenarListaDeArticulos()
        {
            ListaArticulos.Clear(); //Borrro la lista para arrancar
            FileStream Archivo;
            StreamReader leer;
            Archivo = new FileStream("articulos.txt", FileMode.Open);
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
            Console.WriteLine($"Cantidad de artículos de almacen: {acAlmacen}. Promedio de precio unitario: ${promAlmacen / acAlmacen}");
            Console.WriteLine($"Cantidad de artículos de libreria: {acLibreria}. Promedio de precio unitario: ${promLibreria / acLibreria}");
            Console.WriteLine($"Cantidad de artículos de electrónica: {acElectronica}. Promedio de precio unitario: ${promElectronica / acElectronica}");
        }

        public void AgregarEmpleado()
        {
            FileStream Archivo;
            StreamWriter Grabar;
            Archivo = new FileStream("empleados.txt", FileMode.Create);
            Grabar = new StreamWriter(Archivo);

            int nuevoDni;
            string nuevoNombre, nuevoApellido;
            long nuevoSueldo;
            bool loop = false;

            Console.WriteLine("Ingrese los datos requeridos a continuación:");
            Console.Write("DNI: ");
            nuevoDni = int.Parse(Console.ReadLine());
            Console.Write("Apellido: ");
            nuevoApellido = Console.ReadLine();
            Console.Write("Nombre: ");
            nuevoNombre = Console.ReadLine();
            Console.Write("Sueldo: ");
            nuevoSueldo = long.Parse(Console.ReadLine());

            while (!loop)
            {
                for (int i = 0; i < ListaEmpleados.Count(); i++)
                {
                    if (ListaEmpleados[i].Dni == nuevoDni)
                    {
                        Console.WriteLine("Error. El DNI del empleado ya existe.");
                        Console.Write("Reingrese el DNI: ");
                        nuevoDni = int.Parse(Console.ReadLine());
                    }
                    else
                    { loop = true; }
                }
            }

            Empleado nuevoEmpleado = new Empleado(nuevoDni, nuevoApellido, nuevoNombre, nuevoSueldo);

            ListaEmpleados.Add(nuevoEmpleado);
            foreach (Empleado empleado in ListaEmpleados)
            {
                Grabar.WriteLine(empleado.Dni + ";" + empleado.Apellido + ";" + empleado.Nombre + ";" + empleado.Sueldo);
            }

            Grabar.Close();
            Archivo.Close();
        }

        //Modificar Empleado

        //Eliminar empleado
        public void EliminarEmpleado()
        {
            int dni;
            bool encontrado = false;
            char eleccion;

            Console.Write("Ingrese el DNI del empleado a eliminar: ");
            dni = int.Parse(Console.ReadLine());

            for (int i = ListaEmpleados.Count() - 1; i >= 0; i--)
            {
                if (ListaEmpleados[i].Dni == dni)
                {
                    Console.WriteLine("Empleado encontrado:");
                    ListaEmpleados[i].MostrarDatosEmpleado();
                    encontrado = true;
                    ListaEmpleados.RemoveAt(i);
                }
            }
            if (encontrado)
            {

                Console.Write("¿Desea eliminar a este empleado? s/n: ");
                while(!(char.TryParse(Console.ReadLine().ToLower(), out eleccion) && (eleccion == 's' || eleccion == 'n')))
                {
                    Console.WriteLine("Error en la elección. Inténtelo nuevamente\n");
                    Console.Write("¿Desea eliminar a este empleado? s/n: ");
                }
                switch (eleccion)
                {
                    
                    case 'n':
                        Console.WriteLine("Volviendo al menú");
                    break;

                    case 's':
                        FileStream Archivo;
                        StreamWriter Grabar;
                        Archivo = new FileStream("empleados.txt", FileMode.Create);
                        Grabar = new StreamWriter(Archivo);

                        foreach (Empleado empleado in ListaEmpleados)
                        {
                            Grabar.WriteLine(empleado.Dni + ";" + empleado.Apellido + ";" + empleado.Nombre + ";" + empleado.Sueldo);
                        }

                        Grabar.Close();
                        Archivo.Close();
                        break;
                }
            }
            else
            {
                Console.WriteLine("No se encontró al empleado.");
            }
        }

        //Agregar Artículo
        public void AgregarArticulo()
        {
            FileStream Archivo;
            StreamWriter Grabar;
            StreamReader Leer;

            Archivo = new FileStream("articulos.txt", FileMode.Create);
            Grabar = new StreamWriter(Archivo);
            Leer = new StreamReader(Archivo);

            int nuevoId, nuevoPrecio, nuevoStock;
            string nuevoDescripcion, nuevoCategoria;
            bool loop = false;


            Console.WriteLine("Ingrese los datos requeridos a continuación:");
            Console.Write("ID: ");
            nuevoId = int.Parse(Console.ReadLine());
            Console.Write("Descripción: ");
            nuevoDescripcion = Console.ReadLine();
            Console.Write("Categoría (almacen/libreria/electronica): ");
            nuevoCategoria = Console.ReadLine().ToLower();
            Console.Write("Precio unitario: ");
            nuevoPrecio = int.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            nuevoStock = int.Parse(Console.ReadLine());

            while (!loop)
            {
                for (int i = 0; i < ListaArticulos.Count(); i++)
                {
                    if (ListaArticulos[i].IdArt == nuevoId)
                    {
                        Console.WriteLine("Error. El ID del artículo ya existe.");
                        Console.Write("Reingrese el ID: ");
                        nuevoId = int.Parse(Console.ReadLine());
                    }
                    else
                    { loop = true; }
                }
            }
            
            Articulo nuevoArticulo = new Articulo(nuevoId, nuevoDescripcion, nuevoCategoria, nuevoPrecio, nuevoStock);
            ListaArticulos.Add(nuevoArticulo);

            foreach (Articulo articulo in ListaArticulos)
            {
                Grabar.WriteLine(articulo.IdArt + ";" + articulo.DescArt + ";" + articulo.Categoria + ";" + articulo.PrecioUnitario + ";" + articulo.Stock);
            }

            Grabar.Close();
            Leer.Close();
            Archivo.Close();
        }
        //Modificar articulo

        //Eliminar articulo
        public void EliminarArticulo()
        {
            int id;
            bool encontrado = false;
            char eleccion;

            Console.Write("Ingrese el ID del artículo a eliminar: ");
            id = int.Parse(Console.ReadLine());

            for (int i = ListaArticulos.Count() - 1; i >= 0; i--)
            {
                if (ListaArticulos[i].IdArt == id)
                {
                    Console.WriteLine("Artículo encontrado:");
                    ListaArticulos[i].MostrarDatosArticulo();
                    encontrado = true;
                    ListaArticulos.RemoveAt(i);
                }
            }
            if (encontrado)
            {

                Console.Write("¿Desea eliminar a este artículo? s/n: ");
                while (!(char.TryParse(Console.ReadLine().ToLower(), out eleccion) && (eleccion == 's' || eleccion == 'n')))
                {
                    Console.WriteLine("Error en la elección. Inténtelo nuevamente\n");
                    Console.Write("¿Desea eliminar a este artículo? s/n: ");
                }
                switch (eleccion)
                {

                    case 'n':
                        Console.WriteLine("Volviendo al menú");
                        break;

                    case 's':
                        FileStream Archivo;
                        StreamWriter Grabar;
                        Archivo = new FileStream("articulos.txt", FileMode.Create);
                        Grabar = new StreamWriter(Archivo);

                        foreach (Articulo articulo in ListaArticulos)
                        {
                            Grabar.WriteLine(articulo.IdArt + ";" + articulo.DescArt + ";" + articulo.Categoria + ";" + articulo.PrecioUnitario + ";" + articulo.Stock);
                        }

                        Grabar.Close();
                        Archivo.Close();
                        break;
                }
            }
            else
            {
                Console.WriteLine("No se encontró al empleado.");
            }
        }
    }
}
