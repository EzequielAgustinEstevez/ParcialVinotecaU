using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ParcialVinoteca
{
    class Datos
    {
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader lector;
        string cadenaConexion;

        public Datos()
        {
            this.conexion = new SqlConnection();
            this.comando = new SqlCommand();
            this.lector = null;
            this.cadenaConexion = null;
        }

        public Datos(string cadenaConexion)
        {
            this.conexion = new SqlConnection();
            this.comando = new SqlCommand();
            this.lector = null;
            this.cadenaConexion = cadenaConexion;
        }

        public SqlDataReader pLector { get => lector; set => lector = value; }
        public string pCadenaConexion { get => cadenaConexion; set => cadenaConexion = value; }

        public void conectar()
        {
            conexion.ConnectionString = cadenaConexion;
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
        }

        public void desconectar()
        {
            conexion.Close(); //cierra conexion
            conexion.Dispose();//elimina residuos
        }

        public void leerTabla(string nombreTabla)
        {
            conectar();
            comando.CommandText = "SELECT * FROM " + nombreTabla;

            lector = comando.ExecuteReader();
        }

        public DataTable consultarTabla(string nombreTabla)
        {
            conectar();

            DataTable tabla = new DataTable();
            comando.CommandText = "SELECT * FROM " + nombreTabla;
            tabla.Load(comando.ExecuteReader());
            desconectar();

            return tabla;
        }

        public void actualizarBD(string consultaSQL)
        {
            conectar();
            comando.CommandText = consultaSQL;



            comando.ExecuteNonQuery();
            desconectar();

        }
    }
}