using SmartAdmin.Web.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Web.Models.Bufete
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [StringLength(100,ErrorMessage =Validaciones.LongitudString)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [StringLength(100, ErrorMessage = Validaciones.LongitudString)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [StringLength(20, ErrorMessage = Validaciones.LongitudString)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [StringLength(50, ErrorMessage = Validaciones.LongitudString)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = Validaciones.Requerido)]
        [StringLength(100, ErrorMessage = Validaciones.LongitudString)]
        public string Correo { get; set; }
    }
}
