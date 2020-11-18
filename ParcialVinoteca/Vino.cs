using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialVinoteca
{
    public class Vino
    {
        int codigo;
        string nombre;
        int bodega;
        int presentacion;
        double precio;
        DateTime fecha;
       
     

        public int pCodigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string pNombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
              
        public int pBodega
        {
            get { return bodega; }
            set { bodega = value; }
        }

        public int pPresentacion
        {
            get { return presentacion; }
            set { presentacion = value; }
        }

        public DateTime pFecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public double pPrecio
        {
            get { return precio; }
            set { precio = value; }
        }

        override public string ToString()
        {
            return codigo + " - " + nombre;
        }

    }
}
