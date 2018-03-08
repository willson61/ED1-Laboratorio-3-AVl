using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using EstructurasDeDatos;
using Newtonsoft.Json;

namespace Laboratorio_3.Clases
{
    public class JsonConverter<T>
    {
        public List<T> datosJson(Stream ruta)
        {
            try
            {
                List<T> info;
                StreamReader lector1 = new StreamReader(ruta);
                string infoJson = lector1.ReadToEnd();
                info = JsonConvert.DeserializeObject<List<T>>(infoJson);
                lector1.Close();
                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}