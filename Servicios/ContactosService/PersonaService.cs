using System;
using Infraestructure.Datos;
using Infraestructure.Modelos;

namespace Servicios.ContactosService
{
    public class PersonaService
    {
        PersonasDatos personaDatos;

        public PersonaService(string cadenaConexion)
        {
            personaDatos = new PersonasDatos(cadenaConexion);
        }


        public PersonasModel obtenerPersonaById(string documento)
        {
            return personaDatos.obtenerPersonaPorId(documento);
        }

        public List<PersonasModel> obtenerPersonas()
        {
            return personaDatos.obtenerPersonas();
        }

        public void insertarPersona(PersonasInsertModel persona)
        {
            validarDatos(persona);
            personaDatos.insertarPersona(persona);
        }

        public void actualizarPersona(PersonasInsertModel persona)
        {
            validarDatos(persona);
            personaDatos.actualizarPersona(persona);
        }

        private void validarDatos(PersonasInsertModel persona)
        {
            if (persona.nombre.Trim().Length == 0)
            {
                throw new Exception("Se debe cargar el nombre");
            }

        }

    }
}

