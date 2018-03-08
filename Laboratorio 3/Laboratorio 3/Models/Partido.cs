using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratorio_3.Models
{
    public class Partido : IComparable
    {
        [Key]
        public int noPartido { get; set; }
        [Display(Name = "Fecha del Partido")]
        public DateTime fechaPartido { get; set; }
        [Display(Name = "Grupo")]
        public string Grupo { get; set; }
        [Display(Name = "Pais No. 1")]
        public string pais1 { get; set; }
        [Display(Name = "Pais No. 2")]
        public string pais2 { get; set; }
        [Display(Name = "Estadio")]
        public string estadio { get; set; }

        public int CompareTo(object obj)
        {

            return noPartido.CompareTo(obj);
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