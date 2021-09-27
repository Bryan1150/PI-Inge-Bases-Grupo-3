using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Planetario.Handlers
{
    public class FuncionariosHandler
    {
        private readonly SqlConnection Conexion;
        private readonly string RutaConexion;

        public FuncionariosHandler()
        {
            RutaConexion = ConfigurationManager.ConnectionStrings["ConexionBaseDatosServidor"].ToString();
            Conexion = new SqlConnection(RutaConexion);
        }

        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();

            Conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            Conexion.Close();
            return consultaFormatoTabla;
        }

        public List<FuncionarioModel> ObtenerTodosLosFuncionarios()
        {
            List<FuncionarioModel> ListaFuncionarios = new List<FuncionarioModel>();
            string Consulta = "SELECT * FROM Funcionarios";
            DataTable tablaResultado = CrearTablaConsulta(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                ListaFuncionarios.Add(
                    new FuncionarioModel
                    {
                        Cedula = Convert.ToInt32(columna["cedula"]),
                        Nombre = Convert.ToString(columna["nombre"]),
                        Apellido1 = Convert.ToString(columna["apellido1"]),
                        Apellido2 = Convert.ToString(columna["apellido2"]),
                        FechaIncorporacion = Convert.ToString(columna["fechaIncorporacion"]),
                        Titulo = Convert.ToString(columna["titulo"]),
                        RolTrabajo = Convert.ToString(columna["rolTrabajo"]),
                        CorreoContacto = Convert.ToString(columna["correoContacto"])
                    }
                );
            }

            return ListaFuncionarios;
        }

    }
}