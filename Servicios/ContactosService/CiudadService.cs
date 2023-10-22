using System;
using Infraestructure.Datos;
using Infraestructure.Modelos;

namespace Servicios.ContactosService
{
    public class CiudadService
    {
        CiudadDatos ciudadDatos;

        public CiudadService(string cadenaConexion)
        {
            ciudadDatos = new CiudadDatos(cadenaConexion);
        }


        public CiudadModel obtenerCiudadById(int id)
        {
            return ciudadDatos.obtenerCiudadPorId(id);
        }

        public List<CiudadModel> obtenerCiudades()
        {
            return ciudadDatos.obtenerCiudades();
        }


        public void insertarCiudad(CiudadModel ciudad)
        {
            validarDatos(ciudad);
            ciudadDatos.insertarCiudad(ciudad);
        }

        public void modificarCiudad(CiudadModel ciudad)
        {
            validarDatos(ciudad);
            ciudadDatos.modificarCiudad(ciudad);
        }

        private void validarDatos(CiudadModel ciudad)
        {
            if (ciudad.descripcion.Trim().Length == 0)
            {
                throw new Exception("La descripción no puede estar vacía");
            }

        }

    }
}

