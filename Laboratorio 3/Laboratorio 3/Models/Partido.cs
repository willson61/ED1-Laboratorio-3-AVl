using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Laboratorio_3.Clases;

namespace Laboratorio_3.Models
{
    public class Partido : IComparable
    {
        [Key]
        public int noPartido { get; set; }
        [Display(Name = "Fecha del Partido")]
        public string fechaPartido { get; set; }
        [Display(Name = "Grupo")]
        public string grupo { get; set; }
        [Display(Name = "Pais No. 1")]
        public string pais1 { get; set; }
        [Display(Name = "Pais No. 2")]
        public string pais2 { get; set; }
        [Display(Name = "Estadio")]
        public string estadio { get; set; }

        public int CompareTo(object obj)
        {
            if(Data.Instance.partidosAVL.dateOrNumber == true)
            {
                Partido p = (Partido)obj;
                return noPartido.CompareTo(p.noPartido);
            }
            else
            {
                DateTime d1 = DateTime.Parse(fechaPartido);
                Partido p = (Partido)obj;
                DateTime d2 = DateTime.Parse(p.fechaPartido);
                if (d1.CompareTo(d2) == 0)
                {
                    return noPartido.CompareTo(p.noPartido);
                }
                return d1.CompareTo(d2);
            }
        }
        public int compareByNoPartido(object obj)
        {
            return noPartido.CompareTo(obj);
        }
        public int compareByFechaPartido(object obj)
        {
            return fechaPartido.CompareTo(obj);
        }
    }
}