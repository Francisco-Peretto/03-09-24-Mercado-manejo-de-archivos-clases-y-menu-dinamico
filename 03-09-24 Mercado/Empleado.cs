using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_09_24_Mercado
{
    internal class Empleado // REporte c todos los empleados c detalles y una lyenda que muestre la cantidad y el total del sueldo de todos
    {
        private int _dni;
        private string _nombre;
        private string _apellido;
        private double _sueldo;

        public Empleado()
        {
            this.Dni = 0;
            this.Nombre = "";
            this.Apellido = "";
            this.Sueldo = 0;
        }
        public Empleado(int _dni, string _nombre, string _apellido, double _sueldo)
        {
            this.Dni = _dni;
            this.Nombre = _nombre;
            this.Apellido = _apellido;
            this.Sueldo = _sueldo;
        }

        public int Dni
        {
            get { return this._dni; }
            set { this._dni = value; }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public string Apellido
        {
            get { return this._apellido; }
            set { this._apellido = value; }
        }

        public double Sueldo
        {
            get { return this._sueldo; }
            set { this._sueldo = value; }
        }

        // MostrarDatosEmpleado
        public void MostrarDatosEmpleado()
        {
            Console.WriteLine($"DNI: {this.Dni} | Nombre: {this.Nombre} | Apellido: {this.Apellido} | Sueldo: {this.Sueldo}");
        }
        
        // CargarEmpleados
    }
}
