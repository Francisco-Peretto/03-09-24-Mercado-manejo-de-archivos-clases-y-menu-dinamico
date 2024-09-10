using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_09_24_Mercado
{
    internal class Articulo 
    {
        private int _idArt;
        private string _descArt;
        private string _categoria;
        private int _stock;
        private int _precioUnitario;

        public Articulo()
        {
            this.IdArt = 0;
            this.DescArt = "";
            this.Categoria = "";
            this.Stock = 0;
            this.PrecioUnitario = 0;
        }

        public Articulo(int _idArt, string _descArt, string _categoria, int _stock, int _precioUnitario)
        {
            this.IdArt = _idArt;
            this.DescArt = _descArt;
            this.Categoria = _categoria;
            this.Stock = _stock;
            this.PrecioUnitario = _precioUnitario;
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

        public int Stock
        {
            get { return this._stock; }
            set { this._stock = value; }
        }

        public int PrecioUnitario
        {
            get { return this._precioUnitario; }
            set { this._precioUnitario = value; }
        }

        public void MostrarDatosArticulo()
        {
            Console.WriteLine($"Id de Artículo: {this.IdArt} | Descripción: {this.DescArt} | Categoria: {this.Categoria} | Stock: {this.Stock} | Precio Unitario: {this.PrecioUnitario} ");
        }
    }
}
