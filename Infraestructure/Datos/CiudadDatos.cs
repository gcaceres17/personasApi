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
   public class CiudadDatos
    {
        private ConexionDB ConexionDB;
        public CiudadDatos(String cadenaConexion)
        {
            ConexionDB = new ConexionDB(cadenaConexion);
        }

        public CiudadModel obtenerCiudadPorId(int id)
        {
            var conn = ConexionDB.GetConexion();
            var ps = new Npgsql.NpgsqlCommand($"SELECT * FROM public.ciudad where \"idCiudad\" = {id};", conn);
            using var reader = ps.ExecuteReader();
            if (reader.Read())
            {
                return new CiudadModel
                {
                    idCiudad = reader.GetInt32("idCiudad"),
                    descripcion = reader.GetString("descripcion"),
                    nombre_corto = reader.GetString("nombre_corto"),
                    estado = reader.GetBoolean("estado")
                };
            }
            return null;
        }

        public List<CiudadModel> obtenerCiudades()
        {
            var conn = ConexionDB.GetConexion();
            var ps = new Npgsql.NpgsqlCommand($"SELECT * FROM public.ciudad", conn);

            List<CiudadModel> ciudades = new List<CiudadModel>();

            using (var reader = ps.ExecuteReader())
            {
                while (reader.Read())
                {
                    ciudades.Add(new CiudadModel
                    {
                        idCiudad = reader.GetInt32("idCiudad"),
                        descripcion = reader.GetString("descripcion"),
                        nombre_corto = reader.GetString("nombre_corto"),
                        estado = reader.GetBoolean("estado")
                    });
                }
            }

            return ciudades;
        }


        public void insertarCiudad(CiudadModel ciudad)
        {
            var conn = ConexionDB.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO ciudad(Descripcion, nombre_corto, estado)" +
                                                "VALUES( @descripcion, @nombre_corto, @estado)", conn);
            comando.Parameters.AddWithValue("descripcion", ciudad.descripcion);
            comando.Parameters.AddWithValue("nombre_corto", ciudad.nombre_corto);
            comando.Parameters.AddWithValue("estado", ciudad.estado);

            comando.ExecuteNonQuery();
        }

        public void modificarCiudad(CiudadModel ciudad)
        {
            var conn = ConexionDB.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE ciudad SET Descripcion = '{ciudad.descripcion}', " +
                                                          $"nombre_corto = '{ciudad.nombre_corto}', " +
                                                          $"estado = '{ciudad.estado}' " +
                                                $" WHERE \"idCiudad\" = {ciudad.idCiudad}", conn);

            comando.ExecuteNonQuery();
        }
    }

    
}
