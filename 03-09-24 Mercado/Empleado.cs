using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_09_24_Mercado
{
    internal class Empleado
    {
        private int _dni;
        private string _apellido;
        private string _nombre;
        private double _sueldo;

        public Empleado()
        {
            this.Dni = 0;
            this.Apellido = "";
            this.Nombre = "";
            this.Sueldo = 0;
        }
        public Empleado(int _dni, string _apellido, string _nombre, double _sueldo)
        {
            this.Dni = _dni;
            this.Apellido = _apellido;
            this.Nombre = _nombre;
            this.Sueldo = _sueldo;
        }

        public int Dni
        {
            get { return this._dni; }
            set { this._dni = value; }
        }

        public string Apellido
        {
            get { return this._apellido; }
            set { this._apellido = value; }
        }
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public double Sueldo
        {
            get { return this._sueldo; }
            set { this._sueldo = value; }
        }

        public void MostrarDatosEmpleado()
        {
            Console.WriteLine($"DNI: {this.Dni} | Apellido: {this.Apellido} | Nombre: {this.Nombre} | Sueldo: {this.Sueldo}");
        }
        
    }
}
