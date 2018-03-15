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
        public AVLTreeNode<T> datosJson(Stream ruta)
        {
            try
            {
                AVLTreeNode<T> info;
                StreamReader lector1 = new StreamReader(ruta);
                string infoJson = lector1.ReadToEnd();
                info = JsonConvert.DeserializeObject<AVLTreeNode<T>>(infoJson);
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