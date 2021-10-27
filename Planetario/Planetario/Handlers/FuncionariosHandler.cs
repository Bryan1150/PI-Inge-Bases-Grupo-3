using Planetario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Planetario.Handlers
{
    public class FuncionariosHandler
    {
        private readonly BaseDatosHandler BaseDatos;
        private string Consulta;

        public FuncionariosHandler()
        {
            BaseDatos = new BaseDatosHandler();
        }

        public List<FuncionarioModel> ObtenerTodosLosFuncionarios()
        {
            List<FuncionarioModel> ListaFuncionarios = new List<FuncionarioModel>();
            Consulta = " SELECT F.correoFK AS 'correo', F.cedula AS 'cedula'," +
                " CONVERT(VARCHAR(20), fechaIncorporacion, 1) AS fechaIncorporacion," +
                " F.rolTrabajo, F.titulo, U.nombre + ' ' + U.apellido1 AS 'nombre', F.descripcion AS 'descripcion' " +
                "FROM Funcionario F JOIN Usuario U ON F.correoFK = U.correoPK; ";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

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
                        CorreoContacto = Convert.ToString(columna["correo"]),
                        Descripcion = Convert.ToString(columna["descripcion"])
                    }
                );
            }

            return ListaFuncionarios;
        }

        public bool crearFuncionario(FuncionarioModel funcionario)
        {
            FileHandler manejadorArchivos = new FileHandler();
            bool exito;
            Consulta = "INSERT INTO dbo.Funcionario (correoFK, cedula, fechaIncorporacion, titulo, rolTrabajo, foto, tipoArchivoFoto, descripcion) " +
            "VALUES (@correo, @cedula, @fecha, @titulo, @trabajo, @foto, @tipoArchivo, @descripcion) ";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@correo",      funcionario.CorreoContacto },
                { "@cedula",      funcionario.Cedula },
                { "@fecha",       funcionario.FechaIncorporacion },
                { "@titulo",      funcionario.Titulo },
                { "@trabajo",     funcionario.RolTrabajo },
                { "@tipoArchivo", funcionario.Foto.ContentType },
                { "@descripcion", funcionario.Descripcion }
            };

            valoresParametros.Add("@foto", manejadorArchivos.ConvertirArchivoABytes(funcionario.Foto));

            exito = BaseDatos.InsertarEnBaseDatos(Consulta, valoresParametros); 

            return exito;
        }

        public Tuple<byte[], string> ObtenerFoto(int Cedula)
        {
            string nombreArchivo = "foto", tipoArchivo = "tipoArchivoFoto";
            string consulta = "SELECT " + nombreArchivo + ", "+ tipoArchivo + " FROM Funcionario WHERE cedula = @cedula";
            
            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@cedula", Cedula }
            };

            return BaseDatos.ObtenerArchivo(consulta, valoresParametros, nombreArchivo, tipoArchivo);
        }

    }
}