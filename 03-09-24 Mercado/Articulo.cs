using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_09_24_Mercado
{
    internal class Articulo // detalle de todos los articulos y mostrar la cnatidad de articulos de cada categoría, también mostrar el promedio del precio unitario de cada categoría
    {
        private int _idArt;
        private string _descArt; //nombre art
        private string _categoria;
        private int _precioUnitario;
        private int _stock;

        public Articulo()
        {
            this.IdArt = 0;
            this.DescArt = "";
            this.Categoria = "";
            this.PrecioUnitario = 0;
            this.Stock = 0;
        }

        public Articulo(int _idArt, string _descArt, string _categoria, int _precioUnitario, int _stock)
        {
            this.IdArt = _idArt;
            this.DescArt = _descArt;
            this.Categoria = _categoria;
            this.PrecioUnitario = _precioUnitario;
            this.Stock = _stock;
        }

        public int IdArt
        {
            get { return this._idArt; }
            set { this._idArt = value; }
        }

        public string DescArt
        {
            get { return this._descArt; }
            set { this._descArt = value; }
        }

        public string Categoria
        {
            get { return this._categoria; }
            set { this._categoria = value; }
        }

        public int PrecioUnitario
        {
            get { return this._precioUnitario; }
            set { this._precioUnitario = value; }
        }

        public int Stock
        {
            get { return this._stock; }
            set { this._stock = value; }
        }


        // MostrarDatosArticulo
        public void MostrarDatosArticulo()
        {
            Console.WriteLine($"Id de Artículo: {this.IdArt} | Descripción: {this.DescArt} | Categoria: {this.Categoria} | Precio Unitario: {this.PrecioUnitario} | Stock: {this.Stock}");
        }


        // Cargar artículos

    }
}
