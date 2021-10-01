using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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
            string Consulta = " SELECT F.correoFK AS 'correo', F.cedula AS 'cedula'," +
                " CONVERT(VARCHAR(20), fechaIncorporacion, 1) AS fechaIncorporacion," +
                " F.rolTrabajo, F.titulo, U.nombre + ' ' + U.apellido1 + ' ' + U.apellido2 AS 'nombre' " +
                "FROM Funcionario F JOIN Usuario U ON F.correoFK = U.correoPK; ";
            DataTable tablaResultado = CrearTablaConsulta(Consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                ListaFuncionarios.Add(
                    new FuncionarioModel
                    {
                        Cedula = Convert.ToInt32(columna["cedula"]),
                        Nombre = Convert.ToString(columna["nombre"]),
                        FechaIncorporacion = Convert.ToString(columna["fechaIncorporacion"]),
                        Titulo = Convert.ToString(columna["titulo"]),
                        RolTrabajo = Convert.ToString(columna["rolTrabajo"]),
                        CorreoContacto = Convert.ToString(columna["correo"])
                    }
                );
            }

            return ListaFuncionarios;
        }

        public bool crearFuncionario(FuncionarioModel funcionario)
        {
            string consulta = "INSERT INTO Funcionario (correoFK, cedula, fechaIncorporacion, titulo, rolTrabajo, foto, tipoArchivoFoto) " +
            "VALUES (@correo, @cedula, @fecha, @titulo, @trabajo, @foto, @tipoArchivo) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@correo", funcionario.CorreoContacto);
            comandoParaConsulta.Parameters.AddWithValue("@cedula", funcionario.Cedula);
            comandoParaConsulta.Parameters.AddWithValue("@fecha", funcionario.FechaIncorporacion);
            comandoParaConsulta.Parameters.AddWithValue("@titulo", funcionario.Titulo);
            comandoParaConsulta.Parameters.AddWithValue("@trabajo", funcionario.RolTrabajo);
            comandoParaConsulta.Parameters.AddWithValue("@foto", obtenerBytes(funcionario.Foto));
            comandoParaConsulta.Parameters.AddWithValue("@tipoArchivo", funcionario.Foto.ContentType);

            Conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; 
            Conexion.Close();
            return exito;
        }

        private byte[] obtenerBytes(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            BinaryReader lector = new BinaryReader(archivo.InputStream); //
            bytes = lector.ReadBytes(archivo.ContentLength);
            return bytes;
        }


        public Tuple<byte[], string> ObtenerFoto(int Cedula)
        {
            byte[] bytes;
            string contentType;
            string consulta = "SELECT foto, tipoArchivoFoto FROM Funcionario WHERE cedula = @cedula"; 
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, Conexion);
            comandoParaConsulta.Parameters.AddWithValue("@cedula", Cedula);
            Conexion.Open();

            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();

            bytes = (byte[])lectorDeDatos["foto"];
            contentType = lectorDeDatos["tipoArchivoFoto"].ToString();

            Conexion.Close();
            return new Tuple<byte[], string>(bytes, contentType);
        }

    }
}