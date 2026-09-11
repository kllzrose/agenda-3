using System;
using System.Collections.Generic;
using SolucionCapas.Datos;

namespace SolucionCapas.Negocio
{
    
    public class PersonaNegocio
    {
        public CuentaCte ObtenerCuentaPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return null;
            return _datos.BuscarCuentaPorDni(dni);
        }

        public bool AgregarPersonaConCuenta(Persona p, CuentaCte c)
        {
            if (p == null || c == null) return false;
            if (string.IsNullOrWhiteSpace(p.Dni) || string.IsNullOrWhiteSpace(p.Apellido)) return false;
            if (_datos.BuscarPorDni(p.Dni) != null) return false; 

            p.FechaAlta = DateTime.Now;
            c.DniPersona = p.Dni;
            c.FechaApertura = DateTime.Now;

            return _datos.AgregarConCuenta(p, c);
        }

        public bool ModificarPersonaConCuenta(Persona p, CuentaCte c)
        {
            if (p == null || c == null) return false;
            return _datos.ModificarConCuenta(p, c);
        }

        public bool EliminarPersonaConCuenta(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return false;
            return _datos.EliminarConCuenta(dni);
        }
        private PersonaDatos _datos = new PersonaDatos();

        public Persona ObtenerPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return null;
            return _datos.BuscarPorDni(dni);
        }

        public List<Persona> BuscarPorApellido(string apellido) => _datos.BuscarPorTexto("Apellido", apellido);
        public List<Persona> BuscarPorNombres(string nombres) => _datos.BuscarPorTexto("Nombres", nombres);
        public List<Persona> BuscarPorCalle(string calle) => _datos.BuscarPorTexto("Calle", calle);

        public bool AgregarPersona(Persona p)
        {
            if (p == null) return false;
            if (string.IsNullOrWhiteSpace(p.Dni) || string.IsNullOrWhiteSpace(p.Apellido) || string.IsNullOrWhiteSpace(p.Nombres)) return false;

            if (_datos.BuscarPorDni(p.Dni) != null) return false;

            p.FechaAlta = DateTime.Now;
            return _datos.Agregar(p);
        }

        public bool ModificarPersona(Persona p)
        {
            if (p == null) return false;
            if (string.IsNullOrWhiteSpace(p.Dni) || string.IsNullOrWhiteSpace(p.Apellido) || string.IsNullOrWhiteSpace(p.Nombres)) return false;

            return _datos.Modificar(p);
        }

        public bool EliminarPersona(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return false;
            return _datos.Eliminar(dni);
        }
    }
}