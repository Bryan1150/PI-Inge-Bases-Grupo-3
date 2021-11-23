using System.IO;
using System.Web;

namespace Planetario.Handlers
{
    public class FileHandler
    {
        public byte[] ConvertirArchivoABytes(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            BinaryReader lector = new BinaryReader(archivo.InputStream); //
            bytes = lector.ReadBytes(archivo.ContentLength);
            return bytes;
        }

    }
}