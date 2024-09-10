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
            Mercado mercado = new Mercado();
            mercado.llenarListaDeArticulos();
            mercado.llenarListaDeEmpleados();

            string[] menuOpciones = { "Reporte de empleados", "Reporte de artículos", "Agregar Empleado", "Modificar empleado", "Eliminar empleado", "Agregar artículo", "Modificar artículo", "Eliminar artículo", "Salir" };
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
                            Console.WriteLine("\nSaliendo...");
                            bucle = true;
                        }
                        else if (posicionActual == 0) // Reporte de empleados
                        {
                            mercado.GenerarReporteEmpleados();
                        }
                        else if (posicionActual == 1) // Reporte de artículos
                        {
                            mercado.GenerarReporteArticulos();
                        }
                        else if (posicionActual == 2) // Agregar empleado
                        {
                            mercado.AgregarEmpleado();
                        }
                        else if (posicionActual == 3) // Modificar empleado
                        {
                            //mercado.ModificarEmpleado();
                        }
                        else if (posicionActual == 4) // Eliminar empleado
                        {
                            mercado.EliminarEmpleado();
                        }
                        else if (posicionActual == 5) // Agregar artículo
                        {
                            mercado.AgregarArticulo();
                        }
                        else if (posicionActual == 6) // Modificar artículo
                        {
                            // mercado.ModificarArticulo();
                        }
                        else if (posicionActual == 7) // Eliminar artículo
                        {
                            mercado.EliminarArticulo();
                        }
                        Console.WriteLine("\nPresione una tecla para continuar.");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.UpArrow:
                        posicionActual = Math.Max(0, posicionActual - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        posicionActual = Math.Min(menuOpciones.Length - 1, posicionActual + 1);
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}


