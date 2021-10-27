using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Planetario.Models;

namespace Planetario.Handlers
{
    public class MaterialesEducativosHandler
    {
        private readonly BaseDatosHandler BaseDatos;
        private string Consulta;

        public MaterialesEducativosHandler()
        {
            BaseDatos = new BaseDatosHandler();
        }

        public bool AlmacenarMaterialEducativo(MaterialEducativoModel material)
        {
            FileHandler manejadorArchivo = new FileHandler();
            string columnas, valores;

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@titulo", material.Titulo },
                { "@correoResponsable", material.CorreoResponsable },
                { "@publicoDirigido", material.PublicoDirigido },
                { "@tipoArchivo", material.Archivo.ContentType }
            };
            valoresParametros.Add("@archivo", manejadorArchivo.ConvertirArchivoABytes(material.Archivo));
            
            columnas = "( titulo, fechaSubida, correoResponsableFK, publicoDirigido, ";
            valores  = "( @titulo, GETDATE(), @correoResponsable, @publicoDirigido, ";
            if(material.HayVistaPrevia())
            {
                columnas += "imagenVistaPrevia, tipoArchivoVistaPrevia, ";
                valores += "@imagenVistaPrevia, @tipoArchivoVistaPrevia, ";
                valoresParametros.Add("@imagenVistaPrevia", manejadorArchivo.ConvertirArchivoABytes(material.ImagenVistaPrevia));
                valoresParametros.Add("@tipoArchivoVistaPrevia", material.ImagenVistaPrevia.ContentType);
            }
            columnas += "archivo, tipoArchivo ) ";
            valores  += "@archivo, @tipoArchivo );";

            Consulta = "INSERT INTO MaterialEducativo " + columnas + " VALUES " + valores;
            bool exito = BaseDatos.InsertarEnBaseDatos(Consulta, valoresParametros);

            return exito;
        }

        public List<MaterialEducativoModel> obtenerMateriales()
        {
            List<MaterialEducativoModel> material = new List<MaterialEducativoModel>();
            Consulta = "SELECT * FROM MaterialEducativo ";
            DataTable tablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                material.Add(
                new MaterialEducativoModel
                {
                    Titulo = Convert.ToString(columna["titulo"]),
                    Fecha = Convert.ToString(columna["fechaSubida"]),
                    Id = Convert.ToInt32(columna["idMaterialPK"]),
                    CorreoResponsable = Convert.ToString(columna["correoResponsableFK"]),
                    PublicoDirigido = Convert.ToString(columna["publicoDirigido"])
                });
            }
            return material;
        }

        public Tuple<byte[], string> descargarContenido(int id)
        {
            string nombreArchivo = "archivo", tipoArchivo = "tipoArchivo";
            Consulta = "SELECT "+ nombreArchivo +", "+ tipoArchivo + ", titulo FROM MaterialEducativo WHERE idMaterialPK = @materialId";

            Dictionary<string, object> valoresParametros = new Dictionary<string, object>
            {
                { "@materialId",  id }
            };

            return BaseDatos.ObtenerArchivo(Consulta, valoresParametros, nombreArchivo, tipoArchivo);
        }

        public List<MaterialEducativoModel> obtenerMaterialBuscado(string palabra)
        {
            List<MaterialEducativoModel> materialesUnicos = new List<MaterialEducativoModel>();
            Consulta = "SELECT * FROM MaterialEducativo WHERE titulo LIKE '%" + palabra + "%'";

            DataTable TablaResultado = BaseDatos.LeerBaseDeDatos(Consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                materialesUnicos.Add(
                    new MaterialEducativoModel
                    {
                        Titulo = Convert.ToString(columna["titulo"]),
                        Fecha = Convert.ToString(columna["fechaSubida"]),
                        Id = Convert.ToInt32(columna["idMaterialPK"]),
                        CorreoResponsable = Convert.ToString(columna["correoResponsableFK"]),
                        PublicoDirigido = Convert.ToString(columna["publicoDirigido"])
                    });
            }

            return materialesUnicos;
        }
    }
}