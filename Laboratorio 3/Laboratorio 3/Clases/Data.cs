using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EstructurasDeDatos;
using Laboratorio_3.Models;

namespace Laboratorio_3.Clases
{
    public class Data
    {       
        private static Data instance = null;

        public static Data Instance
        {
            get
            {
                if (instance == null) instance = new Data();
                return instance;
            }
        }

        public TreeAVL<Partido> partidosAVL = new TreeAVL<Partido>();
        public List<Partido> listaPartidos = new List<Partido>();
    }
}