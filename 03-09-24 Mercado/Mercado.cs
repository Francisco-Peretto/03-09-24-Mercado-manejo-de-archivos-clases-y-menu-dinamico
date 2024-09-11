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
        float acAlmacen = 0, acLibreria = 0, acElectronica = 0;
        float promAlmacen = 0, promLibreria = 0, promElectronica = 0;

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
            ListaEmpleados.Clear();
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
            ListaArticulos.Clear();
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
            Console.WriteLine("\nInventario de artículos:");
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

        // Agregar Empleado
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

        // Modificar Empleado
        public void ModificarEmpleado()
        {
            int dni;
            string apellido = "", nombre = "";
            double sueldo = 0;
            bool encontrado = false;
            bool salir = false;
            char eleccion, terminar;

            Console.Write("Ingrese el DNI del empleado a modificar: ");
            dni = int.Parse(Console.ReadLine());

            Empleado empleadoModificado = null;
            foreach (Empleado empleado in ListaEmpleados)
            {
                if (empleado.Dni == dni)
                {
                    empleado.MostrarDatosEmpleado();
                    empleadoModificado = empleado;
                    encontrado = true;
                }
            }
            if (encontrado)
            {
                do
                {
                    Console.Write("¿Qué desea modificar de este empleado? 'd' (DNI) / 'a' (apellido) / 'n' (nombre) / 's' (sueldo) / 'x' (cancelar) ");
                    while (!(char.TryParse(Console.ReadLine().ToLower(), out eleccion) && (eleccion == 'd' || eleccion == 'a' || eleccion == 'n' || eleccion == 's' || eleccion == 'x')))
                    {
                        Console.WriteLine("Error en la elección. Inténtelo nuevamente\n");
                        Console.Write("¿Qué desea modificar de este empleado? 'd' (DNI) / 'a' (apellido) / 'n' (nombre) / 's' (sueldo) / 'x' (cancelar) ");
                    }

                    switch (eleccion)
                    {

                        case 'x':
                            Console.WriteLine("Volviendo al menú");
                            break;

                        case 'd':
                            Console.Write("Ingrese el nuevo DNI: ");
                            if (int.TryParse(Console.ReadLine(), out dni))
                            {
                                empleadoModificado.Dni = dni;
                            }
                            else
                            {
                                Console.WriteLine("DNI inválido.");
                            }
                            break;

                        case 'a':
                            Console.Write("Ingrese el nuevo apellido: ");
                            apellido = Console.ReadLine();
                            empleadoModificado.Apellido = apellido;
                            break;

                        case 'n':
                            Console.Write("Ingrese el nuevo nombre: ");
                            nombre = Console.ReadLine();
                            empleadoModificado.Nombre = nombre;
                            break;

                        case 's':
                            Console.Write("Ingrese el nuevo sueldo: ");
                            if (double.TryParse(Console.ReadLine(), out sueldo))
                            {
                                empleadoModificado.Sueldo = sueldo;
                            }
                            else
                            {
                                Console.WriteLine("Sueldo inválido.");
                            }
                            break;
                    }

                    FileStream archivo = new FileStream("empleados.txt", FileMode.Open);
                    StreamReader leer = new StreamReader(archivo);
                    List<string> stringEmpleados = new List<string>();

                    while (!leer.EndOfStream)
                    {
                        string stringDatos = leer.ReadLine();
                        string[] datos = stringDatos.Split(';');
                        int dniEmpleado = int.Parse(datos[0]);

                        if (dniEmpleado == empleadoModificado.Dni)
                        {
                            stringEmpleados.Add($"{empleadoModificado.Dni};{empleadoModificado.Apellido};{empleadoModificado.Nombre};{empleadoModificado.Sueldo}");
                        }
                        else
                        {
                            stringEmpleados.Add(stringDatos);
                        }
                    }

                    leer.Close();
                    archivo.Close();
                    FileStream archivoEscribir = new FileStream("empleados.txt", FileMode.Create);
                    StreamWriter escribir = new StreamWriter(archivoEscribir);

                    foreach (string empleado in stringEmpleados)
                    {
                        escribir.WriteLine(empleado);
                    }

                    escribir.Close();
                    archivoEscribir.Close();

                    Console.WriteLine("Empleado modificado.");

                    Console.Write("¿Desea realizar otro cambio? s/n: ");
                    while (!(char.TryParse(Console.ReadLine().ToLower(), out terminar) && (terminar == 's' || terminar == 'n')))
                    {
                        Console.WriteLine("Error en la elección. Inténtelo nuevamente\n");
                        Console.Write("¿Desea realizar otro cambio? s/n: ");
                    }
                    salir = (terminar == 'n');
                }
                

                while (!salir);
            }
            else
            {
                Console.WriteLine("No se encontró al empleado.");
            }
        }

        // Eliminar empleado
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

        // Agregar Artículo
        public void AgregarArticulo()
        {
            FileStream Archivo;
            StreamWriter Grabar;

            Archivo = new FileStream("articulos.txt", FileMode.Create);
            Grabar = new StreamWriter(Archivo);

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
            Archivo.Close();
        }
        
        // Modificar Artículo
        public void ModificarArticulo()
        {
            int id, stock = 0, precio = 0;
            string descripcion = "", categoria = "";
            bool encontrado = false;
            bool salir = false;
            char eleccion, terminar;

            Console.Write("Ingrese el ID del artículo a modificar: ");
            id = int.Parse(Console.ReadLine());

            Articulo articuloModificado = null;
            foreach (Articulo articulo in ListaArticulos)
            {
                if (articulo.IdArt == id)
                {
                    articulo.MostrarDatosArticulo();
                    articuloModificado = articulo;
                    encontrado = true;
                }
            }

            if (encontrado)
            {
                do
                {
                    Console.Write("¿Qué desea modificar de este artículo? 'i' (ID) / 'd' (descripción) / 'c' (categoría) / 's' (stock) / 'p' (precio) / 'x' (cancelar) ");
                    while (!(char.TryParse(Console.ReadLine().ToLower(), out eleccion) && (eleccion == 'i' || eleccion == 'd' || eleccion == 'c' || eleccion == 's' || eleccion == 'p' || eleccion == 'x')))
                    {
                        Console.WriteLine("Error en la elección. Inténtelo nuevamente\n");
                        Console.Write("¿Qué desea modificar de este artículo? 'i' (ID) / 'd' (descripción) / 'c' (categoría) / 's' (stock) / 'p' (precio) / 'x' (cancelar) ");
                    }

                    switch (eleccion)
                    {
                        case 'x':
                            Console.WriteLine("Volviendo al menú.");
                            return;

                        case 'i':
                            Console.Write("Ingrese el nuevo ID: ");
                            if (int.TryParse(Console.ReadLine(), out id))
                            {
                                articuloModificado.IdArt = id;
                            }
                            else
                            {
                                Console.WriteLine("ID inválido.");
                            }
                            break;

                        case 'd':
                            Console.Write("Ingrese la nueva descripción: ");
                            descripcion = Console.ReadLine();
                            articuloModificado.DescArt = descripcion;
                            break;

                        case 'c':
                            Console.Write("Ingrese la nueva categoría: ");
                            categoria = Console.ReadLine();
                            articuloModificado.Categoria = categoria;
                            break;

                        case 's':
                            Console.Write("Ingrese el nuevo stock: ");
                            if (int.TryParse(Console.ReadLine(), out stock))
                            {
                                articuloModificado.Stock = stock;
                            }
                            else
                            {
                                Console.WriteLine("Stock inválido.");
                            }
                            break;

                        case 'p':
                            Console.Write("Ingrese el nuevo precio: ");
                            if (int.TryParse(Console.ReadLine(), out precio))
                            {
                                articuloModificado.PrecioUnitario = precio;
                            }
                            else
                            {
                                Console.WriteLine("Precio inválido.");
                            }
                            break;
                    }

                    FileStream archivo = new FileStream("articulos.txt", FileMode.Open);
                    StreamReader leer = new StreamReader(archivo);
                    List<string> stringArticulos = new List<string>();

                    while (!leer.EndOfStream)
                    {
                        string stringDatos = leer.ReadLine();
                        string[] datos = stringDatos.Split(';');
                        int idArticulo = int.Parse(datos[0]);

                        if (idArticulo == articuloModificado.IdArt)
                        {
                            stringArticulos.Add($"{articuloModificado.IdArt};{articuloModificado.DescArt};{articuloModificado.Categoria};{articuloModificado.Stock};{articuloModificado.PrecioUnitario}");
                        }
                        else
                        {
                            stringArticulos.Add(stringDatos);
                        }
                    }

                    leer.Close();
                    archivo.Close();


                    FileStream archivoEscribir = new FileStream("articulos.txt", FileMode.Create);
                    StreamWriter escribir = new StreamWriter(archivoEscribir);

                    foreach (string articulo in stringArticulos)
                    {
                        escribir.WriteLine(articulo);
                    }

                    escribir.Close();
                    archivoEscribir.Close();

                    Console.WriteLine("Artículo modificado.");

                    Console.Write("¿Desea realizar otro cambio? s/n: ");
                    while (!(char.TryParse(Console.ReadLine().ToLower(), out terminar) && (terminar == 's' || terminar == 'n')))
                    {
                        Console.WriteLine("Error en la elección. Inténtelo nuevamente\n");
                        Console.Write("¿Desea realizar otro cambio? s/n: ");
                    }

                    salir = (terminar == 'n');
                }
                while (!salir);
            }
            else
            {
                Console.WriteLine("No se encontró el artículo.");
            }
        }


        // Eliminar articulo
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
