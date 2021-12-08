using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Planetario.Models;
using Planetario.Handlers;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data.SqlTypes;

namespace Planetario.Handlers
{
    public class ClientesHandler: BaseDatosHandler
    {
        private List<ClienteModel> ConvertirTablaALista(DataTable tabla)
        {
            List<ClienteModel> clientes = new List<ClienteModel>();
            foreach (DataRow columna in tabla.Rows)
            {
                clientes.Add(
                new ClienteModel
                {
                    correo = Convert.ToString(columna["correoPK"]),
                    nombre = Convert.ToString(columna["nombre"]),
                    apellido1 = Convert.ToString(columna["apellido1"]),
                    apellido2 = Convert.ToString(columna["apellido2"]),
                    pais = Convert.ToString(columna["pais"]),
                    fechaNacimiento = Convert.ToString(columna["fechaNacimiento"]),
                    genero = Convert.ToString(columna["genero"]),
                    nivelEducativo = Convert.ToString(columna["nivelEducativo"])
                });
            }
            return clientes;
        }

        private List<ClienteModel> ObtenerClientes(string consulta)
        {
            DataTable tabla = LeerBaseDeDatos(consulta);
            List<ClienteModel> lista = ConvertirTablaALista(tabla);
            return lista;
        }

        public List<ClienteModel> ObtenerTodosLosClientes()
        {
            string consulta = "SELECT * FROM Cliente C JOIN Persona P ON C.correoClientePK = P.correoPersonaPK";
            return (ObtenerClientes(consulta));
        }

        public ClienteModel ObtenerCliente(string correo)
        {
            string consulta = "SELECT * FROM Cliente C JOIN Persona P ON C.correoClientePK = P.correoPersonaPK WHERE C.correoClientePK  = '" + correo + "';";
            return (ObtenerClientes(consulta)[0]);
        }

        public bool InsertarCliente(ClienteModel cliente)
        {
            string consultaTablaPersona = "INSERT INTO Persona ( correoPersonaPK, nombre, apellido1, apellido2, genero, pais, fechaNacimiento) "
                + "VALUES ( @correo, @nombre, @apellido1, @apellido2, @genero, @pais, @fechaNacimiento,);";
            string consultaTablaCliente = "INSERT INTO Cliente ( correoClientePK, nivelEducativo) "
                + "VALUES ( @correoCliente, @nivelEducativo);";

            Dictionary<string, object> parametrosParaTablaPersona = new Dictionary<string, object> 
            {
                {"@correo", cliente.correo },
                {"@nombre", cliente.nombre },
                {"@apellido1", cliente.apellido1 },
                {"@apellido2", cliente.apellido2 },
                {"@genero", cliente.genero },
                {"@pais", cliente.pais },
                {"@fechaNacimiento", cliente.pais }
            };

            Dictionary<string, object> parametrosParaTablaCliente = new Dictionary<string, object>
            {
                {"@correoCliente", cliente.correo },
                {"@nivelEducativo", cliente.nivelEducativo }
            };

            return (InsertarEnBaseDatos(consultaTablaPersona, parametrosParaTablaPersona) && InsertarEnBaseDatos(consultaTablaCliente, parametrosParaTablaCliente));
        }
    }
}