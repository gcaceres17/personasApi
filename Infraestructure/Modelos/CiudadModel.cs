using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Modelos
{
    public class CiudadModel
    {
        [Key]
        public int idCiudad { get; set; }
        public string descripcion { get; set; }
        public string nombre_corto { get; set; }
        [Required]
        public Boolean estado { get; set; }

    }
}
