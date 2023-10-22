using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Modelos
{
    public class PersonasModel
    {
        [Key]
        public int idPersona { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string nrodocumento { get; set; }

        public CiudadModel ciudad { get; set; }
    }
}
