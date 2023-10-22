using Infraestructure.Conexiones;
using Infraestructure.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Datos
{
    public class PersonasDatos
    {
        private ConexionDB ConexionDB;
        public PersonasDatos(String cadenaConexion)
        {
            ConexionDB = new ConexionDB(cadenaConexion);
        }



        public PersonasModel obtenerPersonaPorId(string documento)
        {
            var conn = ConexionDB.GetConexion();
            var ps = new Npgsql.NpgsqlCommand($"SELECT p.* , c.* FROM personas p inner join ciudad c on c.\"idCiudad\" = p.\"idCiudad\" where p.nrodocumento = '{documento}';", conn);

            using var reader = ps.ExecuteReader();
            if (reader.Read())
            {
                return new PersonasModel
                {
                    idPersona = reader.GetInt32("idPersona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    nrodocumento = reader.GetString("nrodocumento"),

                    ciudad = new CiudadModel {

                        idCiudad = reader.GetInt32("idCiudad"),
                        descripcion = reader.GetString("descripcion"),
                        nombre_corto = reader.GetString("nombre_corto"),
                        estado = reader.GetBoolean("estado")
                    }
                };
            }
            return null;
        }

        public List<PersonasModel> obtenerPersonas()
        {
            var conn = ConexionDB.GetConexion();
            var ps = new Npgsql.NpgsqlCommand($"SELECT p.* , c.* FROM personas p inner join ciudad c on c.\"idCiudad\" = p.\"idCiudad\"", conn);

            List<PersonasModel> personas = new List<PersonasModel>();

            using (var reader = ps.ExecuteReader())
            {
                while (reader.Read())
                {
                    personas.Add(new PersonasModel
                    {
                        idPersona = reader.GetInt32("idPersona"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        nrodocumento = reader.GetString("nrodocumento"),

                        ciudad = new CiudadModel
                        {
                            idCiudad = reader.GetInt32("idCiudad"),
                            descripcion = reader.GetString("descripcion"),
                            nombre_corto = reader.GetString("nombre_corto"),
                            estado = reader.GetBoolean("estado")

                        }
                      
                    });
                }
            }

            return personas;
        }


        public void insertarPersona(PersonasInsertModel persona)
        {
            var conn = ConexionDB.GetConexion();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // Insertar la persona en la tabla de personas
                    var insertPersonaCommand = new Npgsql.NpgsqlCommand("INSERT INTO personas (nombre, apellido, nrodocumento, \"idCiudad\") VALUES (@nombre, @apellido, @nrodocumento, @idCiudad);", conn);
                    insertPersonaCommand.Parameters.AddWithValue("@nombre", persona.nombre);
                    insertPersonaCommand.Parameters.AddWithValue("@apellido", persona.apellido);
                    insertPersonaCommand.Parameters.AddWithValue("@nrodocumento", persona.nrodocumento);
                    insertPersonaCommand.Parameters.AddWithValue("@idCiudad", persona.IdCiudad);
                    insertPersonaCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de error, puedes manejarlo apropiadamente, como hacer un rollback de la transacción
                    transaction.Rollback();
                    throw ex; // Lanzar la excepción para que sea manejada en el código que llama a este método
                }
            }
        }


        public void actualizarPersona(PersonasInsertModel persona)
        {
            var conn = ConexionDB.GetConexion();
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // Actualizar la persona en la tabla de personas
                    var updatePersonaCommand = new Npgsql.NpgsqlCommand("UPDATE personas SET nombre = @nombre, apellido = @apellido, nrodocumento = @nrodocumento, \"idCiudad\" = @idCiudad WHERE  \"idPersona\" = @idPersona;", conn);
                    updatePersonaCommand.Parameters.AddWithValue("@idPersona", persona.idPersona);
                    updatePersonaCommand.Parameters.AddWithValue("@nombre", persona.nombre);
                    updatePersonaCommand.Parameters.AddWithValue("@apellido", persona.apellido);
                    updatePersonaCommand.Parameters.AddWithValue("@nrodocumento", persona.nrodocumento);
                    updatePersonaCommand.Parameters.AddWithValue("@idCiudad", persona.IdCiudad);
                    updatePersonaCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de error, puedes manejarlo apropiadamente, como hacer un rollback de la transacción
                    transaction.Rollback();
                    throw ex; // Lanzar la excepción para que sea manejada en el código que llama a este método
                }
            }
        }



    }


}
