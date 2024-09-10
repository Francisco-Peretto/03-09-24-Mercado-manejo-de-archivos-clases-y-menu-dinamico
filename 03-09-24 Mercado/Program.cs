using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_09_24_Mercado
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string archivoEmpleado = "empleados.txt";
            string archivoArticulo = "articulos.txt";

            Mercado mercado = new Mercado();
            mercado.llenarListaDeArticulos(archivoArticulo);
            mercado.llenarListaDeEmpleados(archivoEmpleado);

            string[] menuOpciones = {"1: Reporte de empleados", "2: Reporte de artículos", "3: Agregar Empleado", "... salir" };
            int posicionActual = 0;
            Console.CursorVisible = false;
            bool bucle = false;

            while (!bucle)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Seleccione una opción con las flechas ↑ y ↓");
                Console.WriteLine("-------------------------------------------");

                for (int i = 0; i < menuOpciones.Length; i++)
                {
                    if (posicionActual == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" " + (char)62 + " ");
                    }
                    Console.WriteLine(menuOpciones[i]);
                    Console.ResetColor();

                }

                ConsoleKeyInfo tecla = Console.ReadKey();
                switch (tecla.Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        if (posicionActual == menuOpciones.Length - 1)
                        {
                            Console.WriteLine("\n Saliendo");
                            bucle = true;
                        }
                        else if (posicionActual == 0)
                        {
                            // Opciones de empleados
                            mercado.GenerarReporteEmpleados();
                            Console.WriteLine("\nPresione una tecla para continuar.");
                            Console.ReadKey();
                        }
                        else if (posicionActual == 1)
                        {
                            // Opciones de artículos
                            mercado.GenerarReporteArticulos();
                            Console.WriteLine("\nPresione una tecla para continuar.");
                            Console.ReadKey();
                            break;
                        }
                        else if (posicionActual == 2)
                        {
                            int nuevoDni;
                            string nuevoNombre, nuevoApellido;
                            long nuevoSueldo;
                            //Agregar empleado
                            Console.WriteLine("Ingrese los datos requeridos a continuación:");
                            Console.Write("DNI: ");
                            nuevoDni = int.Parse(Console.ReadLine());
                            Console.Write("Apellido: ");
                            nuevoApellido = Console.ReadLine();
                            Console.Write("Nombre: ");
                            nuevoNombre = Console.ReadLine();
                            Console.Write("Sueldo: ");
                            nuevoSueldo = long.Parse(Console.ReadLine());

                            Empleado nuevo = new Empleado(nuevoDni, nuevoApellido, nuevoNombre, nuevoSueldo);
                            mercado.AgregarEmpleado(nuevo);
                        }


                        break;
                    case ConsoleKey.UpArrow:
                        posicionActual = Math.Max(0, posicionActual - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        posicionActual = Math.Min(menuOpciones.Length - 1, posicionActual + 1);

                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }

            Console.ReadKey();

            // Menú
            /*
            string[] menu = new string[] { "1: Reporte de empleados", "2: Reporte de artículos", "3: Salir" };
            int opc;
            bool salir = false;
            mercado.llenarListaDeArticulos(archivoArticulo);
            mercado.llenarListaDeEmpleados(archivoEmpleado);

            while (!salir)
            {
                Console.Clear();
                foreach (string opcion in menu)
                {
                    Console.WriteLine(opcion);
                }
                Console.Write("Ingrese una opción: ");
                opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        // Opciones de empleados
                        mercado.GenerarReporteEmpleados();
                        Console.WriteLine("\nPresione una tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 2:
                        // Opciones de artículos
                        mercado.GenerarReporteArticulos();
                        Console.WriteLine("\nPresione una tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 3:
                        salir = true;
                        break;
                }
            }*/
        }
    }
}
